using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

using StreetViewer.JsonObjects.GoogleApiJson.Common;
using StreetViewer.JsonObjects.GoogleApiJson.Geocoding;
using StreetViewer.Service;

namespace StreetViewer.Service
{
    /// <summary>
    /// Класс загрузки панорам снимков в отдельном потоке.
    /// </summary>
    public class Downloader
    {
        private int status;
        private string path;
        private List<List<Location>> listOfPoints;
        private int pointsCount;
        private GoogleRestService restService;

        /// <summary>
        /// Процент загруженных панорам от общего количества.
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        /// <param name="path">директория, в которой будут создаваться папки с панорамами</param>
        /// <param name="points">Список списков точек, по которым будут загружаться панорамы</param>
        /// <param name="restService">сервис работы с Google Map API</param>
        public Downloader(string path, List<List<Location>> points, GoogleRestService restService)
        {
            this.path = path;
            listOfPoints = points;
            pointsCount = listOfPoints.Sum(list => list.Count);
            this.restService = restService;
            this.Status = 0;
        }

        /// <summary>
        /// Загрузить панорамы путей.
        /// </summary>
        public void downloadStreetViews()
        {
            int count = 0;
            for (int i = 0; i < listOfPoints.Count; i++)
            {
                string localPath = path + "\\" + getStreetName(listOfPoints[i][0]) + ";" + getStreetName(listOfPoints[i][listOfPoints[i].Count - 1]);
                createDirectory(localPath);
                for (int j = 0; j < listOfPoints[i].Count; j++)
                {
                    double angle = 0;

                    if (j < listOfPoints[i].Count - 1)
                    {
                        angle = calculateAngle(listOfPoints[i][j].Lat - listOfPoints[i][j + 1].Lat, listOfPoints[i][j].Lng - listOfPoints[i][j + 1].Lng);
                    }
                    else
                    {
                        angle = calculateAngle(listOfPoints[i][j - 1].Lat - listOfPoints[i][j].Lat, listOfPoints[i][j - 1].Lng - listOfPoints[i][j].Lng);
                    }
                    try
                    {
                        Stream viewStream = restService.getStreetViewStream(listOfPoints[i][j].Lat.ToString(), listOfPoints[i][j].Lng.ToString(), angle.ToString());
                        FileStream fileStream = File.OpenWrite(localPath + "\\" + j + ".jpeg");
                        viewStream.CopyTo(fileStream);
                        fileStream.Close();
                        count++;
                        Status = count * 100 / pointsCount;
                    }
                    catch (WebException ex)
                    {
                        j--;
                    }
                }
                logDirectionCoordinates(listOfPoints[i], localPath);
            } 
        }

        // ==============================================================================================================
        // = Implementation
        // ==============================================================================================================

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

        private void logDirectionCoordinates(IList<Location> points, string path)
        {
            StreamWriter streamWriter = new StreamWriter(path + "\\coordinates.txt");
            for (int i = 0; i < points.Count; i++)
            {
                streamWriter.WriteLine(i+"\t\t"+points[i].Lat + ";\t\t" + points[i].Lng + "\r\n");
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
                GeocodeJsonReply geoCoding = restService.getGeocoding(lat, lng);
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
