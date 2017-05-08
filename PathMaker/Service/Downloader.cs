using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

using StreetViewer.JsonObjects.GoogleApiJson.Common;
using StreetViewer.Service;

namespace StreetViewer.Service
{
    class Downloader
    {
        private int status;
        private string path;
        private List<List<Location>> listOfPoints;
        private int pointsCount;
        private GoogleRestService restService;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public Downloader(string path, List<List<Location>> points, GoogleRestService restService)
        {
            this.path = path;
            listOfPoints = points;
            pointsCount = listOfPoints.Sum(list => list.Count);
            this.restService = restService;
            this.Status = 0;
        }

        public void downloadStreetViews()
        {
            int count = 0;
            for (int i = 0; i < listOfPoints.Count; i++)
            {
                string localPath = path + "\\" + i + 1;
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
                streamWriter.WriteLine(points[i].Lat + ";\t" + points[i].Lng + "\r\n");
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
    }
}
