using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Common;
using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Geocoding;
using PathFinder.DataBaseService;

namespace PathFinder.StreetViewing.Service
{
    /// <summary>
    /// Класс загрузки панорам снимков в отдельном потоке.
    /// </summary>
    public class Downloader
    {
        private string path;
        private List<PolylineChunk> listOfChunks;
        private List<PolylineChunk> downloadedChunks;
        private PathFinderContext context;
        private GoogleRestService restService;

        /// <summary>
        /// Процент загруженных панорам от общего количества.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        /// <param name="path">директория, в которой будут создаваться папки с панорамами</param>
        /// <param name="points">Список списков точек, по которым будут загружаться панорамы</param>
        /// <param name="restService">сервис работы с Google Map API</param>
        public Downloader(string path, IList<PolylineChunk> chunks, GoogleRestService restService, PathFinderContext context)
        {
            this.path = path;
            listOfChunks = new List<PolylineChunk>();
            listOfChunks.AddRange(chunks);
            downloadedChunks = new List<PolylineChunk>();
            this.restService = restService;
            this.context = context;
            this.Status = 0;
        }

        /// <summary>
        /// Загрузить панорамы путей.
        /// </summary>
        public void DownloadStreetViews()
        {
            List<PolylineChunk> savedChunks = context.Chunks.AddRange(listOfChunks).ToList();
            context.SaveChanges();
            int pointsCount = listOfChunks.Sum(chunk => chunk.LocationEntities.Count);
            int count = 0;
            foreach (PolylineChunk chunk in savedChunks)
            {
                string localPath = path + "\\" + chunk.Id;
                createDirectory(localPath);
                DownloadChunkStreetViews(chunk, localPath);
                context.SaveChanges();
                AddDownloadedChunkToList(chunk);
                count+= chunk.LocationEntities.Count;
                Status = count * 100 / pointsCount;
            }
        }

        public List<PolylineChunk> GetDownloadedChunks()
        {
            List<PolylineChunk> chunksToReturn = new List<PolylineChunk>();
            lock (downloadedChunks)
            {
                chunksToReturn.AddRange(downloadedChunks);
                downloadedChunks.Clear();
            }
            return chunksToReturn;
        }

        // ==============================================================================================================
        // = Implementation
        // ==============================================================================================================

        private void DownloadChunkStreetViews(PolylineChunk chunk, string path)
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
                    //Stream viewStream = restService.GetStreetViewStream(listOfLocations[j].Lat.ToString(), listOfLocations[j].Lng.ToString(), angle.ToString());
                    string filePath = path + "\\" + j + ".jpeg";
                    using (var fileStream = File.Create(filePath))
                    {
                        //viewStream.CopyTo(fileStream);
                    }
                    listOfLocations[j].PathToStreetView = filePath;
                }
                catch (WebException ex)
                {
                    j--;
                }
            }
        }

        private void AddDownloadedChunkToList(PolylineChunk chunk)
        {
            lock (downloadedChunks)
            {
                downloadedChunks.Add(chunk);
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

        private void logDirectionCoordinates(IList<LocationEntity> points, string path)
        {
            StreamWriter streamWriter = new StreamWriter(path + "\\coordinates.txt");
            for (int i = 0; i < points.Count; i++)
            {
                streamWriter.WriteLine(i + "\t\t" + points[i].Lat + ";\t\t" + points[i].Lng + "\r\n");
            }
            streamWriter.Close();
        }

        private void createDirectory(String path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private string getStreetName(Location location)
        {
            string lat = location.Lat.ToString();
            string lng = location.Lng.ToString();
            try
            {
                GeocodeJsonReply geoCoding = restService.GetGeocoding(lat, lng);
                if ("ZERO_RESULTS".Equals(geoCoding.Status))
                {
                    return lat + ";" + lng;
                }
                else
                {
                    return geoCoding.Results[0].AddressComponents.First(address => address.Types[0].Equals("route")).LongName;
                }
            }
            catch (WebException ex)
            {
                return lat + ";" + lng;
            }
        }
    }
}
