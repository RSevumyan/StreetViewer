using CommonDetectorApi;
using PathFinder.DatabaseService.Model;
using PathFinder.DataBaseService;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PathFinder.StreetViewing.Service
{
    /// <summary>
    /// Класс детектирования объектов на изображениях пути
    /// </summary>
    public class SignDetectionProcessor //ToDo merge with Downloader
    {
        private PathFinderContext dbContext;
        private List<IDetector> detectors;
        private List<PolylineChunk> processedChunks;
        private List<SignDetectionResult> signDetectionResults;
        private List<string> roadsToProcess;
        private List<string> processedRoadsNames;

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="roads">Список названий улиц</param>
        /// <param name="detectors">Список детекторов</param>
        /// <param name="dbContext">контекст базы данных</param>
        public SignDetectionProcessor(List<string> roads, List<IDetector> detectors, PathFinderContext dbContext)
        {
            this.detectors = detectors;
            this.dbContext = dbContext;
            processedChunks = new List<PolylineChunk>();
            signDetectionResults = new List<SignDetectionResult>();
            roadsToProcess = roads;
            processedRoadsNames = new List<string>();
        }

        /// <summary>
        /// Начать обход графа пути
        /// </summary>
        public void Start()
        {
            ProcessSignViewDetection(roadsToProcess);
            Status = 0;
        }

        /// <summary>
        /// Статус обхода графа
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Получить результаты обхода
        /// </summary>
        /// <returns>Объект результатов обхода</returns>
        public List<SignDetectionResult> GetBypassResults()
        {
            List<SignDetectionResult> bypassReturn = new List<SignDetectionResult>();
            lock (signDetectionResults)
            {
                bypassReturn.AddRange(signDetectionResults);
                signDetectionResults.Clear();
            }
            return bypassReturn;
        }

        /// <summary>
        /// Получить список пройденных участков путей
        /// </summary>
        /// <returns>Список пройженных участков путей</returns>
        public List<PolylineChunk> GetDownloadedChunks()
        {
            List<PolylineChunk> chunksToReturn = new List<PolylineChunk>();
            lock (processedChunks)
            {
                chunksToReturn.AddRange(processedChunks);
                processedChunks.Clear();
            }
            return chunksToReturn;
        }

        public List<string> GetProcessedRoadsNames()
        {
            List<string> roadsNamesToReturn = new List<string>();
            lock (processedRoadsNames)
            {
                roadsNamesToReturn.AddRange(processedRoadsNames);
                processedRoadsNames.Clear();
            }
            return roadsNamesToReturn;
        }

        // ==============================================================================================================
        // = Implementation
        // ==============================================================================================================

        private void ProcessSignViewDetection(List<string> roadNames)
        {
            int count = 0;
            GeographiData geoData = GeographiData.Instance;
            foreach (string roadName in roadNames)
            {
                Road road = geoData.Roads[roadName];
                if (!road.IsSignDetected)
                {
                    List<PolylineChunk> orderedPolylineChunk = road.PolylineChunks.OrderBy(x => x.Order).ToList();
                    for (int i = 0; i < orderedPolylineChunk.Count; i++)
                    {
                        if (!orderedPolylineChunk[i].IsSignDetected)
                        {
                            DetectSignForChunk(orderedPolylineChunk[i]);
                        }
                    }
                    for (int i = orderedPolylineChunk.Count -1 ; i >=0; i--)
                    {
                        if (!orderedPolylineChunk[i].IsSignDetected)
                        {
                            DetectSignForChunk(orderedPolylineChunk[i]);
                        }
                        orderedPolylineChunk[i].IsSignDetected = true;
                        dbContext.SaveChanges();
                    }
                    road.IsSignDetected = true;
                    dbContext.SaveChanges();
                }
                AddProcessedRoadName(roadName);
                count++;
                Status = count / roadNames.Count;
            }
        }

        private void DetectSignForChunk(PolylineChunk chunk)
        {
            for (int i = 0; i < chunk.OrderedLocationEntities.Count - 1; i++)
            {
                LocationEntity start = chunk.OrderedLocationEntities.Find(x => x.Order == i).LocationEntity;
                LocationEntity end = chunk.OrderedLocationEntities.Find(x => x.Order == i + 1).LocationEntity;
                ImagePack packToProcess = GetImagePack(start, end);

                if (packToProcess != null)
                {
                    DetectSignForImagePack(packToProcess);
                }
            }
            AddChunkToProcessedList(chunk);
        }

        private void DetectSignForChunkInversed(PolylineChunk chunk)
        {
            for (int i = 0; i < chunk.OrderedLocationEntities.Count - 1; i++)
            {
                LocationEntity start = chunk.OrderedLocationEntities.Find(x => x.Order == i).LocationEntity;
                LocationEntity end = chunk.OrderedLocationEntities.Find(x => x.Order == i + 1).LocationEntity;
                ImagePack packToProcess = GetImagePack(end, start);

                if (packToProcess != null)
                {
                    DetectSignForImagePack(packToProcess);
                }
            }
            AddChunkToProcessedList(chunk);
        }

        private ImagePack GetImagePack(LocationEntity start, LocationEntity end)
        {
            ImagePack returnPack = null;
            foreach (ImagePack pack in start.ImagePacks)
            {
                if (pack.EndLocation == end.OverpassId)
                {
                    returnPack = pack;
                }
            }
            return returnPack;
        }

        private void DetectSignForImagePack(ImagePack pack)
        {
            foreach (StreetView view in pack.ImageList)
            {
                DetectSignsForStreetView(view);
            }
        }

        private void DetectSignsForStreetView(StreetView streetView)
        {
            Bitmap image = new Bitmap(streetView.Path);

            SignDetectionResult detectionResult = new SignDetectionResult();
            detectionResult.Image = image;
            Color[] colorArray = new Color[] { Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Cyan, Color.Brown };
            using (Graphics newGraphics = Graphics.FromImage(image))
            {
                for (int j = 0; j < detectors.Count; j++)
                {
                    detectionResult.DetectorsResult.Add(detectors[j].Name, new List<DatabaseService.Model.Sign>());
                    List<CommonDetectorApi.Sign> signs = detectors[j].Detect(image);
                    using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(50, colorArray[j])))
                    {
                        foreach (CommonDetectorApi.Sign sign in signs)
                        {
                            newGraphics.FillRectangle(shadowBrush, new Rectangle(sign.X, sign.Y, sign.Width, sign.Height));
                            DatabaseService.Model.Sign signModel = new DatabaseService.Model.Sign(streetView, detectors[j].Name, sign);
                            detectionResult.DetectorsResult[detectors[j].Name].Add(signModel);
                            dbContext.Signs.Add(signModel);
                            dbContext.SaveChanges();
                        }
                    }
                }
            }
            AddSignDetectedResult(detectionResult);
        }

        private void AddChunkToProcessedList(PolylineChunk chunk)
        {
            lock (processedChunks)
            {
                processedChunks.Add(chunk);
            }
        }

        private void AddSignDetectedResult(SignDetectionResult signDetectionResult)
        {
            lock (signDetectionResult)
            {
                signDetectionResults.Add(signDetectionResult);
            }
        }

        private void AddProcessedRoadName(string roadName)
        {
            lock (processedRoadsNames)
            {
                processedRoadsNames.Add(roadName);
            }
        }
    }
}
