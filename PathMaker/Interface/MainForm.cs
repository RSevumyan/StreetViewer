using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Net;

using PathFinder.StreetViewing.Service;
using PathFinder.Core;

using GMap.NET.WindowsForms;
using PathFinder.StreetViewing;
using PathFinder.DataBaseService;
using PathFinder.DatabaseService.Model;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;
using System.Drawing;

namespace PathFinder.Interface
{
    public partial class MainForm : Form
    {
        private const string ERROR_MESSAGE = "Введены некорректные данные";
        private const string WEB_ERROR_MESSAGE = "Ошибка соединения";
        private const string STREET1_TOOLTIP_MESSAGE = "Введите начальную улицу";
        private const string STREET2_TOOLTIP_MESSAGE = "Введите конечную улицу";
        private const string RESULTLABEL_STREETVIEWS_DOWNLOADING = "Идет загрузка панорам";
        private const string RESULTLABEL_STREETVIEWS_SUCCESS = "Панорамы успешно загружены";

        private Controller controller;
        private GMapForm gMapForm;
        private Parameters parameters;

        private Dictionary<int, Sign> signsRowIndexDictionary;

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            controller = new Controller();
            parameters = Parameters.Instance;
            SetToolTipProperties(toolTip1);
            detectorsListBox.Items.AddRange(controller.GetDetectorNamesList().ToArray());
            SetToolTipProperties(toolTip2);
            orderInput.Value = parameters.Order;
            radiusUpDown.Value = parameters.Radius;
            pluginPathTextBox.Text = parameters.PluginsPath;
            signsRowIndexDictionary = new Dictionary<int, Sign>();
            LoadMapData();
        }

        internal Button AllDirectionButton { get { return allDirectionsButton; } }

        internal Button StreetViewsRequestButton { get { return streetViewsRequestButton; } }

        internal Button DetectSignsInViewsButton { get { return detectSignsInViewsButton; } }

        internal Button ClearButton { get { return clearButton; } }

        internal GMapForm GMapForm { get { return gMapForm; } }

        internal Label ResultLabel { get { return resultLabel; } }

        internal DataGridView StreetsGridView { get { return streetsGridView; } }

        internal DataGridView StreetsBypassView { get { return streetsBypassView; } }

        internal DataGridView SignsGridView { get { return signsGridView; } }

        internal Dictionary<int, Sign> SignsRowIndexDictionary { get { return signsRowIndexDictionary; } }

        internal PictureBox StreetViewBox { get { return streetViewBox; } }

        internal int FindRowByStreetName(DataGridView dataGrid, string roadName)
        {
            int rowIndex = -1;
            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                if (roadName.Equals(row.Cells[0].Value))
                {
                    rowIndex = row.Index;
                    break;
                }
            }
            return rowIndex;
        }

        // ==============================================================================================================
        // = Implementation
        // ==============================================================================================================

        // 
        // allDirectionsButtonEvents
        //
        private void AllDirectionsButton_Click(object sender, EventArgs e)
        {
            if (gMapForm.Markers.Count < 1)
            {
                resultLabel.Text = "Не установлено ни одного маркера. Для загрузки путей установите маркер на карту.";
            }
            else if (gMapForm.Markers[0].IsVisible)
            {
                try
                {
                    RoadsLoading roadsLoading = new RoadsLoading(this, controller);
                    Thread thread = new Thread(roadsLoading.Process);
                    thread.Start();
                }
                catch (WebException we)
                {
                    resultLabel.Text = WEB_ERROR_MESSAGE;
                }
            }
        }

        // 
        // streetViewsRequestButton events
        //
        private void StreetViewsRequestButton_Click(object sender, EventArgs e)
        {
            streetVewsFolderDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                StreetViewsLoading streetViewsLoading = new StreetViewsLoading(this, controller);
                Thread thread = new Thread(streetViewsLoading.Process);
                thread.Start();
            }
            catch (WebException ex)
            {
                resultLabel.Text = WEB_ERROR_MESSAGE;
            }
        }
        private void detectSignsInViewsButton_Click(object sender, EventArgs e)
        {
            try
            {
                SignDetection detection = new SignDetection(this, controller);
                //bypassService = controller.StartDetection(gMapForm.GetDetectorsStartChunk(), GetCheckedDetectorsNamesList());
                Thread thread = new Thread(detection.Process);
                thread.Start();
            }
            catch (WebException ex)
            {
                resultLabel.Text = WEB_ERROR_MESSAGE;
            }
        }


        // 
        // settingsButton events
        //
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            if (orderInput.Value != 0)
            {
                parameters.Order = (int)orderInput.Value;
            }

            if (radiusUpDown.Value != 0)
            {
                parameters.Radius = (int)radiusUpDown.Value;
            }

            if (!string.IsNullOrEmpty(pluginPathTextBox.Text))
            {
                parameters.PluginsPath = pluginPathTextBox.Text;
                controller.ReloadDetectorManager();
                detectorsListBox.Items.Clear();
                detectorsListBox.Items.AddRange(controller.GetDetectorNamesList().ToArray());
            }
        }

        private void LoadMapData()
        {
            LoadMainAndMiniMapData();
            LoadSignMapData();
        }

        private void LoadMainAndMiniMapData()
        {
            gMapForm = new GMapForm(gMap, gMapMini);
            controller.LoadGeoData();
            GeographiData geoData = GeographiData.Instance;
            foreach (Road road in geoData.Roads.Values)
            {
                LoadMainMapRoad(road);
                LoadMiniMapRoad(road);
            }
        }

        private void LoadMainMapRoad(Road road)
        {
            gMapForm.MainMapRoute.AddRoad(road);
            streetsGridView.Rows.Add(road.Name, road.IsStreetViewsDownloaded ? "Да" : "Нет", false);
        }

        private void LoadMiniMapRoad(Road road)
        {
            if (road.IsStreetViewsDownloaded)
            {
                gMapForm.MiniMapRoute.AddRoad(road);
                streetsBypassView.Rows.Add(road.Name, road.IsSignDetected ? "Да" : "Нет", false);

            }
            else
            {
                List<PolylineChunk> chunksToAdd = new List<PolylineChunk>();
                foreach (PolylineChunk chunk in road.PolylineChunks)
                {
                    if (chunk.IsStreetViewsDownloaded)
                    {
                        chunksToAdd.Add(chunk);
                    }
                }
                if (chunksToAdd.Count > 0)
                {
                    streetsBypassView.Rows.Add(road.Name, road.IsSignDetected ? "Да" : "Нет", false);
                    gMapForm.MiniMapRoute.AddPolylineChunks(road.Name, chunksToAdd);
                }
            }
        }

        private void LoadSignMapData()
        {
            signsMap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            signsMap.SetPositionByKeywords("Moscow, Russia");
            signsMap.ShowCenter = false;
            signsMap.DragButton = MouseButtons.Left;
            GMapOverlay currentOverlay = new GMapOverlay("SignsMap Overlay");
            signsMap.Overlays.Add(currentOverlay);
            LoadSignsGridViewData();
        }

        private void GMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            gMapForm.MouseDoubleClick(e);
        }

        private void GMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            gMapForm.OnMarkerClick(item);
        }

        // 
        // common
        //
        private void SetToolTipProperties(System.Windows.Forms.ToolTip toolTip)
        {
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            //gMapForm.MainMapRoute.Clear();
        }

        private void detectorsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            detectorsInfoListView.Rows.Clear();
            foreach (string detectorName in GetCheckedDetectorsNamesList())
            {
                detectorsInfoListView.Rows.Add(detectorName, "0", "Не найдено ни одного знака на данном изображении");

            }
            detectorsInfoListView.Refresh();
        }

        internal List<string> GetCheckedDetectorsNamesList()
        {
            List<string> checkedDetectorsList = new List<string>();
            for (int i = 0; i < detectorsListBox.Items.Count; i++)
            {
                if (detectorsListBox.GetItemChecked(i))
                {
                    checkedDetectorsList.Add(detectorsListBox.Items[i].ToString());
                }
            }
            return checkedDetectorsList;
        }

        private void streetsGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                bool rowSelectValue = !(bool)streetsGridView.Rows[e.RowIndex].Cells[2].FormattedValue;
                streetsGridView.Rows[e.RowIndex].Cells[2].Value = rowSelectValue;
                bool completed = "Да".Equals(streetsGridView.Rows[e.RowIndex].Cells[1].Value);
                if (rowSelectValue)
                {
                    gMapForm.MainMapRoute.HighlightRoadByViewsDownloadStatus(streetsGridView.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
                }
                else
                {
                    gMapForm.MainMapRoute.DeHighlightRoad(streetsGridView.Rows[e.RowIndex].Cells[0].FormattedValue.ToString(), completed);
                }

                gMapForm.RefreshGMapMain();
            }
        }

        private void streetsBypassView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                bool rowSelectValue = !(bool)streetsBypassView.Rows[e.RowIndex].Cells[2].FormattedValue;
                streetsBypassView.Rows[e.RowIndex].Cells[2].Value = rowSelectValue;
                bool completed = "Да".Equals(streetsBypassView.Rows[e.RowIndex].Cells[1].Value);
                if (rowSelectValue)
                {
                    gMapForm.MiniMapRoute.HighlightRoadBySignDetectedStatus(streetsBypassView.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
                }
                else
                {
                    gMapForm.MiniMapRoute.DeHighlightRoad(streetsBypassView.Rows[e.RowIndex].Cells[0].FormattedValue.ToString(), completed);
                }

                gMapForm.RefreshGMapMini();
            }
        }

        private void LoadSignsGridViewData()
        {
            List<Sign> signs = controller.LoadSigns();
            foreach (Sign sign in signs)
            {
                signsGridView.Rows.Add(sign.ClassName, sign.DetectorName);
                signsRowIndexDictionary.Add(signsGridView.RowCount, sign);
            }
        }

        private void signsGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex + 1;
            if (index != -1 && signsRowIndexDictionary.ContainsKey(index))
            {
                Sign sign = signsRowIndexDictionary[index];
                if (signsMap.Overlays[0].Markers.Count < 1)
                {
                    GMapMarker marker = new GMarkerGoogle(new PointLatLng(sign.Image.Lat, sign.Image.Lng), GMarkerGoogleType.blue);
                    marker.ToolTipText = sign.ClassName;
                    signsMap.Overlays[0].Markers.Add(marker);

                }
                else
                {
                    PointLatLng position = new PointLatLng(sign.Image.Lat, sign.Image.Lng);
                    signsMap.Overlays[0].Markers[0].Position = position;
                    signsMap.Overlays[0].Markers[0].ToolTipText = sign.ClassName;
                }
                Bitmap img = new Bitmap(sign.Image.Path);

                using (Graphics newGraphics = Graphics.FromImage(img))
                {
                    newGraphics.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.Red)), new Rectangle(sign.X, sign.Y, sign.Width, sign.Height));
                }
                signsPictureBox.Image = img;
            }
        }
    }
}