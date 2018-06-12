using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;

using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Common;
using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Geocoding;
using PathFinder.DataBaseService;
using PathFinder.DatabaseService.Model;

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
        private List<Road> roads;
        private List<Road> downloadedRoads;
        private PathFinderContext context;
        private GoogleRestService restService;
        private GeographiService geoService;

        /// <summary>
        /// Процент загруженных панорам от общего количества.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Конструктор для загрузки изображений пути по участкам
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
            downloadedRoads = new List<Road>();
            this.restService = restService;
            this.context = context;
            Status = 0;
        }

        /// <summary>
        /// Конструктор для загрузки изображений пути по дорогам
        /// </summary>
        /// <param name="path">Путь к директории для скачивания</param>
        /// <param name="roads">Словарь дорог</param>
        /// <param name="restService">сервис работы с Google Map API</param>
        /// <param name="context">контекст работы базы данных</param>
        /// <param name="geoService">сервис работы с географическими данными</param>
        public Downloader(string path, List<Road> roads, GoogleRestService restService, PathFinderContext context, GeographiService geoService)
        {
            this.path = path;
            this.roads = roads;
            this.restService = restService;
            this.context = context;
            this.geoService = geoService;
            downloadedChunks = new List<PolylineChunk>();
            downloadedRoads = new List<Road>();
            Status = 0;
        }

        /// <summary>
        /// Загрузить панорамы по дорогам
        /// </summary>
        public void DownloadRoadsStreetViews()
        {
            int pointsCount = roads.Where(road => !road.IsStreetViewsDownloaded).Sum(road => road.PolylineChunks.Where(chunk => !chunk.IsStreetViewsDownloaded)
                .Sum(chunk => chunk.OrderedLocationEntities.Count));
            int count = 0;

            foreach (Road road in roads)
            {
                if (!road.IsStreetViewsDownloaded)
                {
                    foreach (PolylineChunk chunk in road.PolylineChunks)
                    {
                        if (!chunk.IsStreetViewsDownloaded)
                        {
                            DownloadChunkStreetViews(chunk);
                            count += chunk.OrderedLocationEntities.Count;
                            Status = count * 100 / pointsCount;
                        }
                        AddDownloadedChunkToList(chunk);
                    }
                    road.IsStreetViewsDownloaded = true;
                    context.SaveChanges();
                }
                AddDownloadedRoadToList(road);
            }
            Status = 100;
        }

        /// <summary>
        /// Загрузить панорамы путей.
        /// </summary>
        public void DownloadStreetViews()
        {
            int pointsCount = listOfChunks.Sum(chunk => chunk.OrderedLocationEntities.Count);
            int count = 0;
            foreach (PolylineChunk chunk in listOfChunks)
            {
                string localPath = path + "\\" + chunk.OverpassId;
                createDirectory(localPath);
                DownloadChunkStreetViews(chunk, localPath);
                context.Chunks.Add(chunk);
                context.SaveChanges();
                AddDownloadedChunkToList(chunk);
                count += chunk.OrderedLocationEntities.Count;
                Status = count * 100 / pointsCount;
            }
        }

        /// <summary>
        /// Получить список участво путей, по которым были загружены изображения
        /// </summary>
        /// <returns>Список участков путей</returns>
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

        public List<Road> GetDownloadedRoads()
        {
            List<Road> roadsToReturn = new List<Road>();
            lock (downloadedRoads)
            {
                roadsToReturn.AddRange(downloadedRoads);
                downloadedRoads.Clear();
            }
            return roadsToReturn;
        }

        // ==============================================================================================================
        // = Implementation
        // ==============================================================================================================

        private void DownloadChunkStreetViews(PolylineChunk chunk, string path)
        {
            List<LocationEntity> listOfLocations = new List<LocationEntity>();
            foreach (OrderedLocationEntity ordered in chunk.OrderedLocationEntities)
            {
                listOfLocations.Add(ordered.LocationEntity);
            }
            for (int j = 0; j < listOfLocations.Count; j++)
            {
                double angle = 0;

                if (j < listOfLocations.Count - 1)
                {
                    angle = geoService.CalculateAngle(listOfLocations[j].Lat - listOfLocations[j + 1].Lat, listOfLocations[j].Lng - listOfLocations[j + 1].Lng);
                }
                else
                {
                    angle = geoService.CalculateAngle(listOfLocations[j - 1].Lat - listOfLocations[j].Lat, listOfLocations[j - 1].Lng - listOfLocations[j].Lng);
                }
                try
                {
                    Stream viewStream = restService.GetStreetViewStream(listOfLocations[j].Lat.ToString(), listOfLocations[j].Lng.ToString(), angle.ToString());
                    string filePath = path + "\\" + j + ".jpeg";
                    using (var fileStream = File.Create(filePath))
                    {
                        viewStream.CopyTo(fileStream);
                    }
                    //listOfLocations[j].PathToStreetView = filePath;
                }
                catch (WebException ex)
                {
                    j--;
                }
            }
        }

        private void DownloadChunkStreetViews(PolylineChunk chunk)
        {
            List<LocationEntity> locations = new List<LocationEntity>();
            foreach (OrderedLocationEntity ordered in chunk.OrderedLocationEntities)
            {
                locations.Add(ordered.LocationEntity);
            }
            for (int j = 0; j < locations.Count - 1; j++)
            {
                DownloadImagePack(locations[j], locations[j + 1]);
                DownloadImagePack(locations[j + 1], locations[j]);
            }
            chunk.IsStreetViewsDownloaded = true;
            context.SaveChanges();
        }

        private void DownloadImagePack(LocationEntity start, LocationEntity end)
        {
            List<LocationEntity> locations = new List<LocationEntity>();
            locations.Add(start);
            locations.AddRange(geoService.GetIntermediateLocations(start, end));
            locations.Add(end);
            ImagePack pack = new ImagePack(start.OverpassId, end.OverpassId);

            for (int j = 0; j < locations.Count - 1; j++)
            {
                double angle = geoService.CalculateAngle(locations[j].Lat - locations[j + 1].Lat, locations[j].Lng - locations[j + 1].Lng);
                string directory = path + "\\" + pack.StartLocation + "-" + pack.EndLocation;
                createDirectory(directory);
                try
                {
                    Stream viewStream = restService.GetStreetViewStream(locations[j].Lat.ToString(), locations[j].Lng.ToString(), angle.ToString());
                    string filePath = directory + "\\" + j + ".jpeg";
                    using (var fileStream = File.Create(filePath))
                    {
                        viewStream.CopyTo(fileStream);
                    }
                    pack.ImageList.Add(new StreetView(j, filePath, locations[j].Lat, locations[j].Lng));
                }
                catch (WebException ex)
                {
                    j--;
                }
            }
            start.ImagePacks.Add(pack);
            context.ImagePacks.Add(pack);
            context.SaveChanges();
        }

        private void AddDownloadedChunkToList(PolylineChunk chunk)
        {
            lock (downloadedChunks)
            {
                downloadedChunks.Add(chunk);
            }
        }

        private void AddDownloadedRoadToList(Road road)
        {
            lock (downloadedRoads)
            {
                downloadedRoads.Add(road);
            }
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

        private void createDirectory(string path)
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
