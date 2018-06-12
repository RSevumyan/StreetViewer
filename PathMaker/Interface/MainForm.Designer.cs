using System.Windows.Forms;

namespace PathFinder.Interface

{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.gMap = new GMap.NET.WindowsForms.GMapControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.streetViewsRequestButton = new System.Windows.Forms.Button();
            this.streetVewsFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.workTabPage = new System.Windows.Forms.TabPage();
            this.searchLayout = new System.Windows.Forms.TableLayoutPanel();
            this.streetsGridView = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.allDirectionsButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.resultLabel = new System.Windows.Forms.Label();
            this.cvTabPage = new System.Windows.Forms.TabPage();
            this.producingFrameLayout = new System.Windows.Forms.TableLayoutPanel();
            this.producingLayout = new System.Windows.Forms.TableLayoutPanel();
            this.streetViewBox = new System.Windows.Forms.PictureBox();
            this.gMapMini = new GMap.NET.WindowsForms.GMapControl();
            this.detectorsStatisticLabel = new System.Windows.Forms.Label();
            this.detectorsInfoListView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.streetsBypassView = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.detectSignsInViewsButton = new System.Windows.Forms.Button();
            this.signsTabPage = new System.Windows.Forms.TabPage();
            this.signsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.signsMap = new GMap.NET.WindowsForms.GMapControl();
            this.signsPictureBox = new System.Windows.Forms.PictureBox();
            this.signsGridView = new System.Windows.Forms.DataGridView();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.settingsTabPage = new System.Windows.Forms.TabPage();
            this.settingsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.orderLabel = new System.Windows.Forms.Label();
            this.pluginPathTextBox = new System.Windows.Forms.TextBox();
            this.settingsButton = new System.Windows.Forms.Button();
            this.radiusLabel = new System.Windows.Forms.Label();
            this.pluginPathLabel = new System.Windows.Forms.Label();
            this.radiusUpDown = new System.Windows.Forms.NumericUpDown();
            this.orderInput = new System.Windows.Forms.NumericUpDown();
            this.detectorsListBox = new System.Windows.Forms.CheckedListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.mainTabControl.SuspendLayout();
            this.workTabPage.SuspendLayout();
            this.searchLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.streetsGridView)).BeginInit();
            this.cvTabPage.SuspendLayout();
            this.producingFrameLayout.SuspendLayout();
            this.producingLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.streetViewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detectorsInfoListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.streetsBypassView)).BeginInit();
            this.signsTabPage.SuspendLayout();
            this.signsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.signsPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.signsGridView)).BeginInit();
            this.settingsTabPage.SuspendLayout();
            this.settingsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radiusUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderInput)).BeginInit();
            this.SuspendLayout();
            // 
            // gMap
            // 
            this.gMap.Bearing = 0F;
            this.gMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gMap.CanDragMap = true;
            this.searchLayout.SetColumnSpan(this.gMap, 4);
            this.gMap.Cursor = System.Windows.Forms.Cursors.Cross;
            this.gMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMap.GrayScaleMode = false;
            this.gMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMap.LevelsKeepInMemmory = 5;
            this.gMap.Location = new System.Drawing.Point(4, 84);
            this.gMap.Margin = new System.Windows.Forms.Padding(4);
            this.gMap.MarkersEnabled = true;
            this.gMap.MaxZoom = 20;
            this.gMap.MinZoom = 2;
            this.gMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            this.gMap.Name = "gMap";
            this.gMap.NegativeMode = false;
            this.gMap.PolygonsEnabled = true;
            this.gMap.RetryLoadTile = 0;
            this.gMap.RoutesEnabled = true;
            this.gMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMap.ShowTileGridLines = false;
            this.gMap.Size = new System.Drawing.Size(1182, 493);
            this.gMap.TabIndex = 8;
            this.gMap.Zoom = 10D;
            this.gMap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.GMap_OnMarkerClick);
            this.gMap.Load += new System.EventHandler(this.GMap_Load);
            this.gMap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GMap_MouseDoubleClick);
            // 
            // streetViewsRequestButton
            // 
            this.streetViewsRequestButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.streetViewsRequestButton.Location = new System.Drawing.Point(301, 4);
            this.streetViewsRequestButton.Margin = new System.Windows.Forms.Padding(4);
            this.streetViewsRequestButton.Name = "streetViewsRequestButton";
            this.streetViewsRequestButton.Size = new System.Drawing.Size(289, 32);
            this.streetViewsRequestButton.TabIndex = 9;
            this.streetViewsRequestButton.Text = "Скачать";
            this.streetViewsRequestButton.UseVisualStyleBackColor = true;
            this.streetViewsRequestButton.Click += new System.EventHandler(this.StreetViewsRequestButton_Click);
            // 
            // streetVewsFolderDialog
            // 
            this.streetVewsFolderDialog.Description = "Выберите папку для сохранения результатов";
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.workTabPage);
            this.mainTabControl.Controls.Add(this.cvTabPage);
            this.mainTabControl.Controls.Add(this.signsTabPage);
            this.mainTabControl.Controls.Add(this.settingsTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1206, 778);
            this.mainTabControl.TabIndex = 10;
            // 
            // workTabPage
            // 
            this.workTabPage.Controls.Add(this.searchLayout);
            this.workTabPage.Location = new System.Drawing.Point(4, 25);
            this.workTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.workTabPage.Name = "workTabPage";
            this.workTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.workTabPage.Size = new System.Drawing.Size(1198, 749);
            this.workTabPage.TabIndex = 0;
            this.workTabPage.Text = "Поиск пути";
            this.workTabPage.UseVisualStyleBackColor = true;
            // 
            // searchLayout
            // 
            this.searchLayout.ColumnCount = 4;
            this.searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.searchLayout.Controls.Add(this.gMap, 3, 1);
            this.searchLayout.Controls.Add(this.streetsGridView, 0, 3);
            this.searchLayout.Controls.Add(this.allDirectionsButton, 0, 0);
            this.searchLayout.Controls.Add(this.clearButton, 2, 0);
            this.searchLayout.Controls.Add(this.streetViewsRequestButton, 1, 0);
            this.searchLayout.Controls.Add(this.resultLabel, 0, 1);
            this.searchLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchLayout.Location = new System.Drawing.Point(4, 4);
            this.searchLayout.Name = "searchLayout";
            this.searchLayout.RowCount = 4;
            this.searchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.searchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.searchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.searchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.searchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.searchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.searchLayout.Size = new System.Drawing.Size(1190, 741);
            this.searchLayout.TabIndex = 12;
            // 
            // streetsGridView
            // 
            this.streetsGridView.AllowUserToAddRows = false;
            this.streetsGridView.AllowUserToDeleteRows = false;
            this.streetsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.streetsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.streetsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column5,
            this.Column6});
            this.searchLayout.SetColumnSpan(this.streetsGridView, 4);
            this.streetsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.streetsGridView.Location = new System.Drawing.Point(3, 584);
            this.streetsGridView.Name = "streetsGridView";
            this.streetsGridView.RowHeadersVisible = false;
            this.streetsGridView.RowTemplate.Height = 24;
            this.streetsGridView.Size = new System.Drawing.Size(1184, 154);
            this.streetsGridView.TabIndex = 14;
            this.streetsGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.streetsGridView_CellDoubleClick);
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Название улицы";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Улица загружена";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Выбрать";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // allDirectionsButton
            // 
            this.allDirectionsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allDirectionsButton.Location = new System.Drawing.Point(4, 4);
            this.allDirectionsButton.Margin = new System.Windows.Forms.Padding(4);
            this.allDirectionsButton.Name = "allDirectionsButton";
            this.allDirectionsButton.Size = new System.Drawing.Size(289, 32);
            this.allDirectionsButton.TabIndex = 13;
            this.allDirectionsButton.Text = "Найти все пути";
            this.allDirectionsButton.UseVisualStyleBackColor = true;
            this.allDirectionsButton.Click += new System.EventHandler(this.AllDirectionsButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clearButton.Location = new System.Drawing.Point(597, 3);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(291, 34);
            this.clearButton.TabIndex = 11;
            this.clearButton.Text = "Очистить";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.searchLayout.SetColumnSpan(this.resultLabel, 2);
            this.resultLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultLabel.Location = new System.Drawing.Point(3, 40);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(588, 40);
            this.resultLabel.TabIndex = 15;
            // 
            // cvTabPage
            // 
            this.cvTabPage.Controls.Add(this.producingFrameLayout);
            this.cvTabPage.Location = new System.Drawing.Point(4, 25);
            this.cvTabPage.Name = "cvTabPage";
            this.cvTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.cvTabPage.Size = new System.Drawing.Size(1198, 749);
            this.cvTabPage.TabIndex = 2;
            this.cvTabPage.Text = "Обход";
            this.cvTabPage.UseVisualStyleBackColor = true;
            // 
            // producingFrameLayout
            // 
            this.producingFrameLayout.ColumnCount = 3;
            this.producingFrameLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.producingFrameLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.producingFrameLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.producingFrameLayout.Controls.Add(this.producingLayout, 0, 0);
            this.producingFrameLayout.Controls.Add(this.detectSignsInViewsButton, 1, 1);
            this.producingFrameLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.producingFrameLayout.Location = new System.Drawing.Point(3, 3);
            this.producingFrameLayout.Name = "producingFrameLayout";
            this.producingFrameLayout.RowCount = 2;
            this.producingFrameLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.producingFrameLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.producingFrameLayout.Size = new System.Drawing.Size(1192, 743);
            this.producingFrameLayout.TabIndex = 13;
            // 
            // producingLayout
            // 
            this.producingLayout.ColumnCount = 2;
            this.producingFrameLayout.SetColumnSpan(this.producingLayout, 3);
            this.producingLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.producingLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.producingLayout.Controls.Add(this.streetViewBox, 0, 0);
            this.producingLayout.Controls.Add(this.gMapMini, 1, 0);
            this.producingLayout.Controls.Add(this.detectorsStatisticLabel, 0, 1);
            this.producingLayout.Controls.Add(this.detectorsInfoListView, 0, 2);
            this.producingLayout.Controls.Add(this.streetsBypassView, 1, 2);
            this.producingLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.producingLayout.Location = new System.Drawing.Point(3, 3);
            this.producingLayout.Name = "producingLayout";
            this.producingLayout.RowCount = 3;
            this.producingLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.92308F));
            this.producingLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.producingLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.07692F));
            this.producingLayout.Size = new System.Drawing.Size(1186, 697);
            this.producingLayout.TabIndex = 0;
            // 
            // streetViewBox
            // 
            this.streetViewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.streetViewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.streetViewBox.Location = new System.Drawing.Point(3, 3);
            this.streetViewBox.Name = "streetViewBox";
            this.streetViewBox.Size = new System.Drawing.Size(705, 514);
            this.streetViewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.streetViewBox.TabIndex = 0;
            this.streetViewBox.TabStop = false;
            // 
            // gMapMini
            // 
            this.gMapMini.Bearing = 0F;
            this.gMapMini.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gMapMini.CanDragMap = true;
            this.gMapMini.Cursor = System.Windows.Forms.Cursors.Cross;
            this.gMapMini.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMapMini.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapMini.GrayScaleMode = false;
            this.gMapMini.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapMini.LevelsKeepInMemmory = 5;
            this.gMapMini.Location = new System.Drawing.Point(715, 4);
            this.gMapMini.Margin = new System.Windows.Forms.Padding(4);
            this.gMapMini.MarkersEnabled = true;
            this.gMapMini.MaxZoom = 20;
            this.gMapMini.MinZoom = 2;
            this.gMapMini.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            this.gMapMini.Name = "gMapMini";
            this.gMapMini.NegativeMode = false;
            this.gMapMini.PolygonsEnabled = true;
            this.gMapMini.RetryLoadTile = 0;
            this.gMapMini.RoutesEnabled = true;
            this.gMapMini.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapMini.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapMini.ShowTileGridLines = false;
            this.gMapMini.Size = new System.Drawing.Size(467, 512);
            this.gMapMini.TabIndex = 9;
            this.gMapMini.Zoom = 10D;
            this.gMapMini.Load += new System.EventHandler(this.gMapMini_Load);
            // 
            // detectorsStatisticLabel
            // 
            this.detectorsStatisticLabel.AutoSize = true;
            this.detectorsStatisticLabel.Location = new System.Drawing.Point(3, 520);
            this.detectorsStatisticLabel.Name = "detectorsStatisticLabel";
            this.detectorsStatisticLabel.Size = new System.Drawing.Size(84, 17);
            this.detectorsStatisticLabel.TabIndex = 11;
            this.detectorsStatisticLabel.Text = "Статистика";
            // 
            // detectorsInfoListView
            // 
            this.detectorsInfoListView.AllowUserToAddRows = false;
            this.detectorsInfoListView.AllowUserToDeleteRows = false;
            this.detectorsInfoListView.AllowUserToResizeColumns = false;
            this.detectorsInfoListView.AllowUserToResizeRows = false;
            this.detectorsInfoListView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.detectorsInfoListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.detectorsInfoListView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.detectorsInfoListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detectorsInfoListView.Location = new System.Drawing.Point(3, 543);
            this.detectorsInfoListView.Name = "detectorsInfoListView";
            this.detectorsInfoListView.ReadOnly = true;
            this.detectorsInfoListView.RowHeadersVisible = false;
            this.detectorsInfoListView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.detectorsInfoListView.RowTemplate.Height = 24;
            this.detectorsInfoListView.Size = new System.Drawing.Size(705, 151);
            this.detectorsInfoListView.TabIndex = 12;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Детектор";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Всего найдено знаков";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Найденные знаки на изображении";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // streetsBypassView
            // 
            this.streetsBypassView.AllowUserToAddRows = false;
            this.streetsBypassView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.streetsBypassView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.streetsBypassView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column8,
            this.Column9});
            this.streetsBypassView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.streetsBypassView.Location = new System.Drawing.Point(714, 543);
            this.streetsBypassView.Name = "streetsBypassView";
            this.streetsBypassView.RowHeadersVisible = false;
            this.streetsBypassView.RowTemplate.Height = 24;
            this.streetsBypassView.Size = new System.Drawing.Size(469, 151);
            this.streetsBypassView.TabIndex = 13;
            this.streetsBypassView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.streetsBypassView_CellDoubleClick);
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Название улицы";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Улица пройдена";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Выбрать";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // detectSignsInViewsButton
            // 
            this.detectSignsInViewsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.detectSignsInViewsButton.Location = new System.Drawing.Point(549, 717);
            this.detectSignsInViewsButton.Name = "detectSignsInViewsButton";
            this.detectSignsInViewsButton.Size = new System.Drawing.Size(94, 23);
            this.detectSignsInViewsButton.TabIndex = 12;
            this.detectSignsInViewsButton.Text = "Начать обработку";
            this.detectSignsInViewsButton.UseVisualStyleBackColor = true;
            this.detectSignsInViewsButton.Click += new System.EventHandler(this.detectSignsInViewsButton_Click);
            // 
            // signsTabPage
            // 
            this.signsTabPage.Controls.Add(this.signsLayout);
            this.signsTabPage.Location = new System.Drawing.Point(4, 25);
            this.signsTabPage.Name = "signsTabPage";
            this.signsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.signsTabPage.Size = new System.Drawing.Size(1198, 749);
            this.signsTabPage.TabIndex = 3;
            this.signsTabPage.Text = "Знкаи";
            this.signsTabPage.UseVisualStyleBackColor = true;
            // 
            // signsLayout
            // 
            this.signsLayout.ColumnCount = 2;
            this.signsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.signsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.signsLayout.Controls.Add(this.signsMap, 1, 0);
            this.signsLayout.Controls.Add(this.signsPictureBox, 0, 0);
            this.signsLayout.Controls.Add(this.signsGridView, 0, 2);
            this.signsLayout.Controls.Add(this.label1, 0, 1);
            this.signsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.signsLayout.Location = new System.Drawing.Point(3, 3);
            this.signsLayout.Name = "signsLayout";
            this.signsLayout.RowCount = 3;
            this.signsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.signsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.signsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.signsLayout.Size = new System.Drawing.Size(1192, 743);
            this.signsLayout.TabIndex = 0;
            // 
            // signsMap
            // 
            this.signsMap.Bearing = 0F;
            this.signsMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.signsMap.CanDragMap = true;
            this.signsMap.Cursor = System.Windows.Forms.Cursors.Cross;
            this.signsMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.signsMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.signsMap.GrayScaleMode = false;
            this.signsMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.signsMap.LevelsKeepInMemmory = 5;
            this.signsMap.Location = new System.Drawing.Point(600, 4);
            this.signsMap.Margin = new System.Windows.Forms.Padding(4);
            this.signsMap.MarkersEnabled = true;
            this.signsMap.MaxZoom = 20;
            this.signsMap.MinZoom = 2;
            this.signsMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            this.signsMap.Name = "signsMap";
            this.signsMap.NegativeMode = false;
            this.signsMap.PolygonsEnabled = true;
            this.signsMap.RetryLoadTile = 0;
            this.signsMap.RoutesEnabled = true;
            this.signsMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.signsMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.signsMap.ShowTileGridLines = false;
            this.signsMap.Size = new System.Drawing.Size(588, 348);
            this.signsMap.TabIndex = 11;
            this.signsMap.Zoom = 10D;
            // 
            // signsPictureBox
            // 
            this.signsPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.signsPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.signsPictureBox.Location = new System.Drawing.Point(3, 3);
            this.signsPictureBox.Name = "signsPictureBox";
            this.signsPictureBox.Size = new System.Drawing.Size(590, 350);
            this.signsPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.signsPictureBox.TabIndex = 12;
            this.signsPictureBox.TabStop = false;
            // 
            // signsGridView
            // 
            this.signsGridView.AllowUserToAddRows = false;
            this.signsGridView.AllowUserToDeleteRows = false;
            this.signsGridView.AllowUserToResizeColumns = false;
            this.signsGridView.AllowUserToResizeRows = false;
            this.signsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.signsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.signsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column10,
            this.Column11});
            this.signsLayout.SetColumnSpan(this.signsGridView, 2);
            this.signsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.signsGridView.Location = new System.Drawing.Point(3, 389);
            this.signsGridView.Name = "signsGridView";
            this.signsGridView.ReadOnly = true;
            this.signsGridView.RowHeadersVisible = false;
            this.signsGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.signsGridView.RowTemplate.Height = 24;
            this.signsGridView.Size = new System.Drawing.Size(1186, 351);
            this.signsGridView.TabIndex = 13;
            this.signsGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.signsGridView_CellDoubleClick);
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Тип знака";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Детектор";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(3, 369);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(590, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Обнаруженные знаки";
            // 
            // settingsTabPage
            // 
            this.settingsTabPage.Controls.Add(this.settingsLayout);
            this.settingsTabPage.Location = new System.Drawing.Point(4, 25);
            this.settingsTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.settingsTabPage.Name = "settingsTabPage";
            this.settingsTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.settingsTabPage.Size = new System.Drawing.Size(1198, 749);
            this.settingsTabPage.TabIndex = 1;
            this.settingsTabPage.Text = "Настройки";
            this.settingsTabPage.UseVisualStyleBackColor = true;
            // 
            // settingsLayout
            // 
            this.settingsLayout.ColumnCount = 20;
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.Controls.Add(this.orderLabel, 1, 1);
            this.settingsLayout.Controls.Add(this.pluginPathTextBox, 4, 5);
            this.settingsLayout.Controls.Add(this.settingsButton, 9, 11);
            this.settingsLayout.Controls.Add(this.radiusLabel, 1, 3);
            this.settingsLayout.Controls.Add(this.pluginPathLabel, 1, 5);
            this.settingsLayout.Controls.Add(this.radiusUpDown, 4, 3);
            this.settingsLayout.Controls.Add(this.orderInput, 4, 1);
            this.settingsLayout.Controls.Add(this.detectorsListBox, 12, 1);
            this.settingsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsLayout.Location = new System.Drawing.Point(4, 4);
            this.settingsLayout.Name = "settingsLayout";
            this.settingsLayout.RowCount = 20;
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.settingsLayout.Size = new System.Drawing.Size(1190, 741);
            this.settingsLayout.TabIndex = 9;
            // 
            // orderLabel
            // 
            this.orderLabel.AutoSize = true;
            this.settingsLayout.SetColumnSpan(this.orderLabel, 2);
            this.orderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderLabel.Location = new System.Drawing.Point(63, 37);
            this.orderLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.orderLabel.Name = "orderLabel";
            this.orderLabel.Size = new System.Drawing.Size(110, 37);
            this.orderLabel.TabIndex = 1;
            this.orderLabel.Text = "Шаг";
            // 
            // pluginPathTextBox
            // 
            this.settingsLayout.SetColumnSpan(this.pluginPathTextBox, 3);
            this.pluginPathTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginPathTextBox.Location = new System.Drawing.Point(239, 188);
            this.pluginPathTextBox.Name = "pluginPathTextBox";
            this.pluginPathTextBox.Size = new System.Drawing.Size(171, 22);
            this.pluginPathTextBox.TabIndex = 8;
            // 
            // settingsButton
            // 
            this.settingsLayout.SetColumnSpan(this.settingsButton, 2);
            this.settingsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsButton.Location = new System.Drawing.Point(535, 411);
            this.settingsButton.Margin = new System.Windows.Forms.Padding(4);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(110, 29);
            this.settingsButton.TabIndex = 2;
            this.settingsButton.Text = "Загрузить";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // radiusLabel
            // 
            this.radiusLabel.AutoSize = true;
            this.settingsLayout.SetColumnSpan(this.radiusLabel, 2);
            this.radiusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radiusLabel.Location = new System.Drawing.Point(63, 111);
            this.radiusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.radiusLabel.Name = "radiusLabel";
            this.radiusLabel.Size = new System.Drawing.Size(110, 37);
            this.radiusLabel.TabIndex = 5;
            this.radiusLabel.Text = "Радиус";
            // 
            // pluginPathLabel
            // 
            this.pluginPathLabel.AutoSize = true;
            this.settingsLayout.SetColumnSpan(this.pluginPathLabel, 2);
            this.pluginPathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginPathLabel.Location = new System.Drawing.Point(62, 185);
            this.pluginPathLabel.Name = "pluginPathLabel";
            this.pluginPathLabel.Size = new System.Drawing.Size(112, 37);
            this.pluginPathLabel.TabIndex = 7;
            this.pluginPathLabel.Text = "Путь к плагинам";
            // 
            // radiusUpDown
            // 
            this.settingsLayout.SetColumnSpan(this.radiusUpDown, 3);
            this.radiusUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radiusUpDown.Location = new System.Drawing.Point(240, 115);
            this.radiusUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.radiusUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.radiusUpDown.Name = "radiusUpDown";
            this.radiusUpDown.Size = new System.Drawing.Size(169, 22);
            this.radiusUpDown.TabIndex = 4;
            // 
            // orderInput
            // 
            this.settingsLayout.SetColumnSpan(this.orderInput, 3);
            this.orderInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderInput.Location = new System.Drawing.Point(240, 41);
            this.orderInput.Margin = new System.Windows.Forms.Padding(4);
            this.orderInput.Name = "orderInput";
            this.orderInput.Size = new System.Drawing.Size(169, 22);
            this.orderInput.TabIndex = 3;
            // 
            // detectorsListBox
            // 
            this.settingsLayout.SetColumnSpan(this.detectorsListBox, 7);
            this.detectorsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detectorsListBox.FormattingEnabled = true;
            this.detectorsListBox.Location = new System.Drawing.Point(711, 40);
            this.detectorsListBox.Name = "detectorsListBox";
            this.settingsLayout.SetRowSpan(this.detectorsListBox, 7);
            this.detectorsListBox.Size = new System.Drawing.Size(407, 253);
            this.detectorsListBox.TabIndex = 6;
            this.detectorsListBox.SelectedIndexChanged += new System.EventHandler(this.detectorsListBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1206, 778);
            this.Controls.Add(this.mainTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "StreetViewer";
            this.mainTabControl.ResumeLayout(false);
            this.workTabPage.ResumeLayout(false);
            this.searchLayout.ResumeLayout(false);
            this.searchLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.streetsGridView)).EndInit();
            this.cvTabPage.ResumeLayout(false);
            this.producingFrameLayout.ResumeLayout(false);
            this.producingLayout.ResumeLayout(false);
            this.producingLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.streetViewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detectorsInfoListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.streetsBypassView)).EndInit();
            this.signsTabPage.ResumeLayout(false);
            this.signsLayout.ResumeLayout(false);
            this.signsLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.signsPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.signsGridView)).EndInit();
            this.settingsTabPage.ResumeLayout(false);
            this.settingsLayout.ResumeLayout(false);
            this.settingsLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radiusUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private GMap.NET.WindowsForms.GMapControl gMap;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Button streetViewsRequestButton;
        private System.Windows.Forms.FolderBrowserDialog streetVewsFolderDialog;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage workTabPage;
        private System.Windows.Forms.TabPage settingsTabPage;
        private System.Windows.Forms.Label orderLabel;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.NumericUpDown orderInput;
        private System.Windows.Forms.Label radiusLabel;
        private System.Windows.Forms.NumericUpDown radiusUpDown;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.CheckedListBox detectorsListBox;
        private System.Windows.Forms.TextBox pluginPathTextBox;
        private System.Windows.Forms.Label pluginPathLabel;
        private System.Windows.Forms.TabPage cvTabPage;
        private GMap.NET.WindowsForms.GMapControl gMapMini;
        private System.Windows.Forms.PictureBox streetViewBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label detectorsStatisticLabel;
        private Button detectSignsInViewsButton;
        private TableLayoutPanel searchLayout;
        private TableLayoutPanel producingFrameLayout;
        private TableLayoutPanel producingLayout;
        private DataGridView detectorsInfoListView;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private TableLayoutPanel settingsLayout;
        private DataGridView streetsGridView;
        private Label resultLabel;
        private Button allDirectionsButton;
        private DataGridView streetsBypassView;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewCheckBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewCheckBoxColumn Column9;
        private TabPage signsTabPage;
        private TableLayoutPanel signsLayout;
        private GMap.NET.WindowsForms.GMapControl signsMap;
        private PictureBox signsPictureBox;
        private DataGridView signsGridView;
        private Label label1;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
    }
}

