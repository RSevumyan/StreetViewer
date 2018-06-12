﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Core;
using PathFinder.StreetViewing.Service;
using System.Windows.Forms;
using PathFinder.DataBaseService;
using PathFinder.DatabaseService.Model;
using System.Threading;
using PathFinder.StreetViewing;

namespace PathFinder.Interface
{
    internal class SignDetection : AbstractConcurrentProcess
    {
        private SignDetectionProcessor processor;

        public SignDetection(MainForm mainForm, Controller controller) : base(mainForm, controller) { }

        internal override void Process()
        {
            List<string> streets = new List<string>();
            foreach (DataGridViewRow row in mainForm.StreetsBypassView.Rows)
            {
                bool rowSelected = (bool)row.Cells[2].Value;
                if (rowSelected)
                {
                    streets.Add((string)row.Cells[0].Value);
                }
            }

            if (streets.Count > 0)
            {
                ConcurrencyUtils.SetAvailability(mainForm.DetectSignsInViewsButton, false);

                GeographiData geoData = GeographiData.Instance;
                foreach (string roadName in streets)
                {
                    Road road = geoData.Roads[roadName];
                    foreach (PolylineChunk chunk in road.PolylineChunks)
                    {
                        if (chunk.IsSignDetected)
                        {
                            mainForm.GMapForm.MiniMapRoute.DeHighlightPolylineChunk(chunk.OverpassId);
                        }
                    }

                    List<string> selectedDetectorsNames = mainForm.GetCheckedDetectorsNamesList();
                    if (selectedDetectorsNames.Count > 0)
                    {
                        processor = controller.StartDetection(streets, selectedDetectorsNames);
                        Thread byPassStatusThread = new Thread(UpdateStatus);
                        byPassStatusThread.Start();
                    }
                }
            }
        }

        protected override void UpdateStatus()
        {
            while (processor.Status < 100)
            {
                UpdateMiniMap();
                UpdateRoadsStatus();
                UpdateStreetViewBox();
                Thread.Sleep(1000);
            }
            UpdateMiniMap();
            UpdateRoadsStatus();
            UpdateStreetViewBox();
            ConcurrencyUtils.SetAvailability(mainForm.DetectSignsInViewsButton, true);
        }

        private void UpdateMiniMap()
        {
            List<PolylineChunk> chunks = processor.GetDownloadedChunks();
            foreach (long overpassId in chunks.Select(ch => ch.OverpassId))
            {
                mainForm.GMapForm.MiniMapRoute.DeHighlightPolylineChunk(overpassId);
            }
            mainForm.GMapForm.RefreshGMapMini();
        }

        private void UpdateStreetViewBox()
        {
            List<SignDetectionResult> results = processor.GetBypassResults();

            foreach (SignDetectionResult result in results)
            {
                foreach (var detectorsResult in result.DetectorsResult)
                {
                    foreach (Sign sign in detectorsResult.Value)
                    {
                        ConcurrencyUtils.AddRow(mainForm.SignsGridView, new object[] { sign.ClassName, sign.DetectorName });
                        mainForm.SignRowIndexDictionary.Add(mainForm.SignsGridView.RowCount, sign);
                    }
                }

                ConcurrencyUtils.SetImage(mainForm.StreetViewBox, result.Image);
                Thread.Sleep(1000);
            }
        }

        private void UpdateRoadsStatus()
        {
            List<string> processedRoads = processor.GedProcessedRoadsNames();
            foreach (string roadName in processedRoads)
            {
                if (mainForm.StreetsBypassRowIndexDictionary.ContainsKey(roadName))
                {
                    int rowIndex = mainForm.StreetsBypassRowIndexDictionary[roadName];
                    ConcurrencyUtils.SetCellValue(mainForm.StreetsGridView, rowIndex, 1, "Да");
                    ConcurrencyUtils.SetCellValue(mainForm.StreetsGridView, rowIndex, 2, false);
                }
            }
        }
    }
}
