using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

using StreetViewer.JsonObjects.Common;
using StreetViewer.Service;

namespace StreetViewer.Core
{
    class Downloader
    {
        private int status;
        private string path;
        private IList<Location> points;
        private RestService restService;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public Downloader(string path, IList<Location> points, RestService restService)
        {
            this.path = path;
            this.points = points;
            this.restService = restService;
            this.Status = 0;
        }

        public void downloadStreetViews()
        {
            createDirectory(path);
            for (int i = 0; i < points.Count; i++)
            {
                double angle = 0;

                if (i < points.Count - 1)
                {
                    angle = calculateAngle(points[i].Lat - points[i + 1].Lat, points[i].Lng - points[i + 1].Lng);
                }
                else
                {
                    angle = calculateAngle(points[i - 1].Lat - points[i].Lat, points[i - 1].Lng - points[i].Lng);
                }
                try
                {
                    Stream viewStream = restService.getStreetViewStream(points[i].Lat.ToString(), points[i].Lng.ToString(), angle.ToString());
                    FileStream fileStream = File.OpenWrite(path + "\\" + i + ".jpeg");
                    viewStream.CopyTo(fileStream);
                    fileStream.Close();
                    Status = (i + 1) * 100 / points.Count;
                }
                catch (WebException ex)
                {
                    i--;
                }
            }
            logDirectionCoordinates(points, path);
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
