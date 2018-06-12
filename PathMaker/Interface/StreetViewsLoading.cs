using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Core;
using System.Threading;
using PathFinder.StreetViewing.Service;
using PathFinder.DatabaseService.Model;
using System.Windows.Forms;
using PathFinder.DataBaseService;

namespace PathFinder.Interface
{
    internal class StreetViewsLoading : AbstractConcurrentProcess
    {
        private Downloader downloader;

        public StreetViewsLoading(MainForm mainForm, Controller controller) : base(mainForm, controller) { }

        internal override void Process()
        {
            List<string> streets = new List<string>();
            foreach (DataGridViewRow row in mainForm.StreetsGridView.Rows)
            {
                bool rowSelected = (bool)row.Cells[2].Value;
                if (rowSelected)
                {
                    streets.Add((string)row.Cells[0].Value);
                }
            }

            if (streets.Count > 0)
            {
                ConcurrencyUtils.SetAvailability(mainForm.AllDirectionButton, false);
                ConcurrencyUtils.SetAvailability(mainForm.ClearButton, false);
                ConcurrencyUtils.SetAvailability(mainForm.StreetViewsRequestButton, false);

                GeographiData geoData = GeographiData.Instance;
                foreach (string roadName in streets)
                {
                    Road road = geoData.Roads[roadName];
                    foreach (PolylineChunk chunk in road.PolylineChunks)
                    {
                        if (chunk.IsStreetViewsDownloaded)
                        {
                            mainForm.GMapForm.MainMapRoute.DeHighlightPolylineChunk(chunk.OverpassId);
                        }
                    }
                }
                downloader = controller.GetStreetViews(streets, AppDomain.CurrentDomain.BaseDirectory + "Chunks");

                Thread roadLoadStatusThread = new Thread(UpdateStatus);
                roadLoadStatusThread.Start();
            }
            else
            {
                ConcurrencyUtils.SetText(mainForm.ResultLabel, "Не выбрано ни одной улицы. Для загрузки панорам выберете хотябы одну улицу.");
            }
        }

        protected override void UpdateStatus()
        {
            while (downloader.Status < 100)
            {
                ConcurrencyUtils.SetText(mainForm.ResultLabel, "Идет загрузка панорамных изображений. Загружено " + downloader.Status + "%");
                Thread.Sleep(1000);
                List<PolylineChunk> chunks = downloader.GetDownloadedChunks();
                foreach (long overpassId in chunks.Select(ch => ch.OverpassId))
                {
                    mainForm.GMapForm.MainMapRoute.DeHighlightPolylineChunk(overpassId);
                }
                UpdateRoadsStatus();
            }
            UpdateRoadsStatus();
            ConcurrencyUtils.SetAvailability(mainForm.AllDirectionButton, true);
            ConcurrencyUtils.SetAvailability(mainForm.ClearButton, true);
            ConcurrencyUtils.SetAvailability(mainForm.StreetViewsRequestButton, true);
            ConcurrencyUtils.SetText(mainForm.ResultLabel, "Загрузка панорамных изображений завершена");
        }

        private void UpdateRoadsStatus()
        {
            List<Road> downloadedRoads = downloader.GetDownloadedRoads();
            foreach (Road road in downloadedRoads)
            {
                if (mainForm.StreetViewsRowIndexDictionary.ContainsKey(road.Name))
                {
                    int rowIndex = mainForm.StreetViewsRowIndexDictionary[road.Name];
                    ConcurrencyUtils.SetCellValue(mainForm.StreetsGridView, rowIndex, 1, "Да");
                    ConcurrencyUtils.SetCellValue(mainForm.StreetsGridView, rowIndex, 2, false);
                    if (mainForm.StreetsBypassRowIndexDictionary.ContainsKey(road.Name))
                    {
                        rowIndex = mainForm.StreetsBypassRowIndexDictionary[road.Name];
                        ConcurrencyUtils.SetCellValue(mainForm.StreetsBypassView, rowIndex, 1, "Нет");
                    }
                    else
                    {
                        ConcurrencyUtils.AddRow(mainForm.StreetsBypassView, new object[] { road.Name, road.IsSignDetected ? "Да" : "Нет", false });
                        mainForm.StreetsBypassRowIndexDictionary.Add(road.Name, mainForm.StreetsBypassView.RowCount - 1);
                    }
                    mainForm.GMapForm.MiniMapRoute.AddRoad(road);
                }
            }
            mainForm.GMapForm.RefreshGMapMini();
        }
    }
}
