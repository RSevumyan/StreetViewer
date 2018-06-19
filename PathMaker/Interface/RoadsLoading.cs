using PathFinder.Core;
using PathFinder.DatabaseService.Model;

using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace PathFinder.Interface
{
    internal class RoadsLoading : AbstractConcurrentProcess
    {
        private bool isLoadComplete;

        internal RoadsLoading(MainForm mainForm, Controller controller) : base(mainForm, controller)
        {
            isLoadComplete = false;
        }

        internal Dictionary<string, Road> LoadedRoades;

        internal override void Process()
        {
            Thread roadLoadStatusThread = new Thread(UpdateStatus);
            roadLoadStatusThread.Start();
            ConcurrencyUtils.SetText(mainForm.ResultLabel, "Идет пстроение улиц");
            ConcurrencyUtils.SetAvailability(mainForm.AllDirectionButton, false);
            ConcurrencyUtils.SetAvailability(mainForm.ClearButton, false);
            ConcurrencyUtils.SetAvailability(mainForm.StreetViewsRequestButton, false);

            if (mainForm.GMapForm.Markers.Count > 1)
            {
                LoadedRoades = controller.GetAllRoadsOfArea(
                    mainForm.GMapForm.Markers[0].Position.Lat, mainForm.GMapForm.Markers[0].Position.Lng,
                    mainForm.GMapForm.Markers[1].Position.Lat, mainForm.GMapForm.Markers[1].Position.Lng);
            }
            else
            {
                LoadedRoades = controller.GetAllRoadsOfArea(mainForm.GMapForm.Markers[0].Position.Lat, mainForm.GMapForm.Markers[0].Position.Lng);
            }
            foreach (Road road in LoadedRoades.Values)
            {
                mainForm.GMapForm.MainMapRoute.AddRoad(road);
                int rowIndex = mainForm.FindRowByStreetName(mainForm.StreetsGridView, road.Name);
                if (rowIndex != -1)
                {
                    ConcurrencyUtils.SetCellValue(mainForm.StreetsGridView, rowIndex, 1, "Нет");
                }
                else
                {
                    ConcurrencyUtils.AddRow(mainForm.StreetsGridView, new object[] { road.Name, road.IsStreetViewsDownloaded ? "Да" : "Нет", false });
                }
            }
            isLoadComplete = true;
        }

        protected override void UpdateStatus()
        {
            while (!isLoadComplete)
            {
                Thread.Sleep(1000);
            }
            ConcurrencyUtils.SetAvailability(mainForm.AllDirectionButton, true);
            ConcurrencyUtils.SetAvailability(mainForm.ClearButton, true);
            ConcurrencyUtils.SetAvailability(mainForm.StreetViewsRequestButton, true);
            ConcurrencyUtils.SetText(mainForm.ResultLabel, "Построение улиц завершено");
        }
    }
}
