using CommonDetectorApi;
using PathFinder.DataBaseService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;

namespace PathFinder.StreetViewing.Service
{
    public class BypassService //ToDo merge with Downloader
    {
        private HashSet<long> passedChunksId;

        private GoogleRestService restService;
        private PathFinderContext context;
        private List<IDetector> detectors;
        private string path;
        private PolylineChunk start;

        private List<BypassResult> bypassResults;
        private List<PolylineChunk> downloadedChunks;

        public BypassService(GoogleRestService restService, PathFinderContext context, List<IDetector> detectors, string path, PolylineChunk start)
        {
            this.restService = restService;
            this.context = context;
            this.detectors = detectors;
            this.path = path;
            this.start = start;
            bypassResults = new List<BypassResult>();
            downloadedChunks = new List<PolylineChunk>();
            passedChunksId = new HashSet<long>();
            Status = -1;
        }

        public void Start()
        {
            Status = 1;
            int depth = 0;
            BypassRecursive(start, path, depth);
            Status = -1;
        }

        public int Status { get; set; }

        public List<BypassResult> GetBypassResults()
        {
            List<BypassResult> bypassReturn = new List<BypassResult>();
            lock (bypassResults)
            {
                bypassReturn.AddRange(bypassResults);
                // bypassResults.Clear();
            }
            return bypassReturn;
        }

        public List<PolylineChunk> GetDownloadedChunks()
        {
            List<PolylineChunk> chunksToReturn = new List<PolylineChunk>();
            lock (downloadedChunks)
            {
                chunksToReturn.AddRange(downloadedChunks);
                //downloadedChunks.Clear();
            }
            return chunksToReturn;
        }

        private void BypassRecursive(PolylineChunk chunk, String path, int depth)
        {
            if (!passedChunksId.Contains(chunk.OverpassId) && depth <= 10)
            {
                string localPath = path + "\\" + chunk.OverpassId;
                createDirectory(localPath);
                BypassChunkStreetViews(chunk, localPath);
                lock (downloadedChunks)
                {
                    downloadedChunks.Add(chunk);
                }
                passedChunksId.Add(chunk.OverpassId);
                context.SaveChanges();
                if (chunk.NextChunks != null)
                {
                    foreach (PolylineChunk nextChunk in chunk.NextChunks)
                    {
                        BypassRecursive(nextChunk, path, ++depth);
                    }
                }
            }
        }


        private void BypassChunkStreetViews(PolylineChunk chunk, string path)
        {
            IList<LocationEntity> listOfLocations = chunk.LocationEntities;
            for (int j = 0; j < listOfLocations.Count; j++)
            {
                double angle = 0;

                if (j < listOfLocations.Count - 1)
                {
                    angle = calculateAngle(listOfLocations[j].Lat - listOfLocations[j + 1].Lat, listOfLocations[j].Lng - listOfLocations[j + 1].Lng);
                }
                else
                {
                    angle = calculateAngle(listOfLocations[j - 1].Lat - listOfLocations[j].Lat, listOfLocations[j - 1].Lng - listOfLocations[j].Lng);
                }
                try
                {
                    Stream viewStream = restService.GetStreetViewStream(listOfLocations[j].Lat.ToString(), listOfLocations[j].Lng.ToString(), angle.ToString());
                    string filePath = path + "\\" + j + ".bmp";

                    Bitmap image = new Bitmap(viewStream);
                    image.Save(filePath);
                    listOfLocations[j].PathToStreetView = filePath;
                    BypassResult bypassResult = new BypassResult();

                    Color[] colorArray = new Color[] { Color.Green, Color.Blue, Color.Red, Color.Yellow, Color.Cyan, Color.Brown};
                    using (Graphics newGraphics = Graphics.FromImage(image))
                    {
                        for(int i = 0; i < detectors.Count; i++)
                        {
                            List<Sign> signs = detectors[i].Detect(image);
                            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(50, colorArray[i])))
                            {
                                foreach (Sign sign in signs)
                                {
                                    newGraphics.FillRectangle(shadowBrush, new Rectangle(sign.X, sign.Y, sign.Width, sign.Height));
                                }
                            }
                            bypassResult.DetectorsResult.Add(detectors[i].Name, signs);
                        }
                        bypassResult.Image = new Bitmap(600, 600, newGraphics);
                    }
                   
                    lock (bypassResults)
                    {
                        bypassResults.Add(bypassResult);
                    }
                }
                catch (WebException ex)
                {
                    j--;
                }
            }
        }


        private double calculateAngle(double height, double width)
        {
            height += height == 0 ? 0.00001 : 0;
            double angle = Math.Atan(width / height) * (180 / Math.PI);

            if (angle < 0)
            {
                angle += 90;
            }

            if (height >= 0 && width < 0)
            {
                angle += 90;
            }
            else if (height > 0 && width >= 0)
            {
                angle += 180;
            }
            else if (height <= 0 && width > 0)
            {
                angle += 270;
            }
            return angle;
        }


        private void createDirectory(String path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
