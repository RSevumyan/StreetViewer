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
            this.resultLabel = new System.Windows.Forms.Label();
            this.startStreet = new System.Windows.Forms.TextBox();
            this.endStreet = new System.Windows.Forms.TextBox();
            this.directionRequestButton = new System.Windows.Forms.Button();
            this.DirecrionLabel = new System.Windows.Forms.Label();
            this.gMap = new GMap.NET.WindowsForms.GMapControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.streetViewsRequestButton = new System.Windows.Forms.Button();
            this.streetVewsFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.workTabPage = new System.Windows.Forms.TabPage();
            this.ClearButton = new System.Windows.Forms.Button();
            this.allDirectionsButton = new System.Windows.Forms.Button();
            this.cvTabPage = new System.Windows.Forms.TabPage();
            this.detectSignsInViewsButton = new System.Windows.Forms.Button();
            this.detectorsStatisticLabel = new System.Windows.Forms.Label();
            this.detecorsInfoListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gMapMini = new GMap.NET.WindowsForms.GMapControl();
            this.streetViewBox = new System.Windows.Forms.PictureBox();
            this.settingsTabPage = new System.Windows.Forms.TabPage();
            this.pluginPathTextBox = new System.Windows.Forms.TextBox();
            this.pluginPathLabel = new System.Windows.Forms.Label();
            this.detectorsListBox = new System.Windows.Forms.CheckedListBox();
            this.radiusLabel = new System.Windows.Forms.Label();
            this.radiusUpDown = new System.Windows.Forms.NumericUpDown();
            this.orderInput = new System.Windows.Forms.NumericUpDown();
            this.settingsButton = new System.Windows.Forms.Button();
            this.orderLabel = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabControl.SuspendLayout();
            this.workTabPage.SuspendLayout();
            this.cvTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.streetViewBox)).BeginInit();
            this.settingsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radiusUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderInput)).BeginInit();
            this.SuspendLayout();
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(17, 66);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(0, 17);
            this.resultLabel.TabIndex = 2;
            // 
            // startStreet
            // 
            this.startStreet.Location = new System.Drawing.Point(111, 16);
            this.startStreet.Margin = new System.Windows.Forms.Padding(4);
            this.startStreet.Name = "startStreet";
            this.startStreet.Size = new System.Drawing.Size(303, 22);
            this.startStreet.TabIndex = 3;
            this.toolTip1.SetToolTip(this.startStreet, "Введите конечную улицу");
            this.startStreet.KeyUp += new System.Windows.Forms.KeyEventHandler(this.StartStreet_KeyUp);
            this.startStreet.MouseLeave += new System.EventHandler(this.StartStreet_MouseLeave);
            this.startStreet.MouseHover += new System.EventHandler(this.StartStreet_MouseHover);
            // 
            // endStreet
            // 
            this.endStreet.Location = new System.Drawing.Point(468, 16);
            this.endStreet.Margin = new System.Windows.Forms.Padding(4);
            this.endStreet.Name = "endStreet";
            this.endStreet.Size = new System.Drawing.Size(303, 22);
            this.endStreet.TabIndex = 4;
            this.endStreet.KeyUp += new System.Windows.Forms.KeyEventHandler(this.EndStreet_KeyUp);
            this.endStreet.MouseLeave += new System.EventHandler(this.EndStreet_MouseLeave);
            this.endStreet.MouseHover += new System.EventHandler(this.EndStreet_MouseHover);
            // 
            // directionRequestButton
            // 
            this.directionRequestButton.Enabled = false;
            this.directionRequestButton.Location = new System.Drawing.Point(793, 16);
            this.directionRequestButton.Margin = new System.Windows.Forms.Padding(4);
            this.directionRequestButton.Name = "directionRequestButton";
            this.directionRequestButton.Size = new System.Drawing.Size(148, 28);
            this.directionRequestButton.TabIndex = 5;
            this.directionRequestButton.Text = "Найти путь";
            this.directionRequestButton.UseVisualStyleBackColor = true;
            this.directionRequestButton.Click += new System.EventHandler(this.DirectionRequestButton_Click);
            // 
            // DirecrionLabel
            // 
            this.DirecrionLabel.AutoSize = true;
            this.DirecrionLabel.Location = new System.Drawing.Point(17, 16);
            this.DirecrionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DirecrionLabel.Name = "DirecrionLabel";
            this.DirecrionLabel.Size = new System.Drawing.Size(82, 17);
            this.DirecrionLabel.TabIndex = 7;
            this.DirecrionLabel.Text = "Поиск пути";
            // 
            // gMap
            // 
            this.gMap.Bearing = 0F;
            this.gMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gMap.CanDragMap = true;
            this.gMap.Cursor = System.Windows.Forms.Cursors.Cross;
            this.gMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMap.GrayScaleMode = false;
            this.gMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMap.LevelsKeepInMemmory = 5;
            this.gMap.Location = new System.Drawing.Point(8, 124);
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
            this.gMap.Size = new System.Drawing.Size(1182, 615);
            this.gMap.TabIndex = 8;
            this.gMap.Zoom = 10D;
            this.gMap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.GMap_OnMarkerClick);
            this.gMap.Load += new System.EventHandler(this.GMap_Load);
            this.gMap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GMap_MouseDoubleClick);
            // 
            // streetViewsRequestButton
            // 
            this.streetViewsRequestButton.Enabled = false;
            this.streetViewsRequestButton.Location = new System.Drawing.Point(459, 66);
            this.streetViewsRequestButton.Margin = new System.Windows.Forms.Padding(4);
            this.streetViewsRequestButton.Name = "streetViewsRequestButton";
            this.streetViewsRequestButton.Size = new System.Drawing.Size(133, 28);
            this.streetViewsRequestButton.TabIndex = 9;
            this.streetViewsRequestButton.Text = "Скачать";
            this.streetViewsRequestButton.UseVisualStyleBackColor = true;
            this.streetViewsRequestButton.Click += new System.EventHandler(this.StreetViewsRequestButton_Click);
            // 
            // streetVewsFolderDialog
            // 
            this.streetVewsFolderDialog.Description = "Выберите папку для сохранения результатов";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.workTabPage);
            this.tabControl.Controls.Add(this.cvTabPage);
            this.tabControl.Controls.Add(this.settingsTabPage);
            this.tabControl.Location = new System.Drawing.Point(-1, 1);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1209, 779);
            this.tabControl.TabIndex = 10;
            // 
            // workTabPage
            // 
            this.workTabPage.Controls.Add(this.ClearButton);
            this.workTabPage.Controls.Add(this.allDirectionsButton);
            this.workTabPage.Controls.Add(this.gMap);
            this.workTabPage.Controls.Add(this.resultLabel);
            this.workTabPage.Controls.Add(this.streetViewsRequestButton);
            this.workTabPage.Controls.Add(this.DirecrionLabel);
            this.workTabPage.Controls.Add(this.directionRequestButton);
            this.workTabPage.Controls.Add(this.startStreet);
            this.workTabPage.Controls.Add(this.endStreet);
            this.workTabPage.Location = new System.Drawing.Point(4, 25);
            this.workTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.workTabPage.Name = "workTabPage";
            this.workTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.workTabPage.Size = new System.Drawing.Size(1201, 750);
            this.workTabPage.TabIndex = 0;
            this.workTabPage.Text = "Поиск пути";
            this.workTabPage.UseVisualStyleBackColor = true;
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(278, 66);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(136, 28);
            this.ClearButton.TabIndex = 11;
            this.ClearButton.Text = "Очистить";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // allDirectionsButton
            // 
            this.allDirectionsButton.Location = new System.Drawing.Point(793, 60);
            this.allDirectionsButton.Margin = new System.Windows.Forms.Padding(4);
            this.allDirectionsButton.Name = "allDirectionsButton";
            this.allDirectionsButton.Size = new System.Drawing.Size(148, 28);
            this.allDirectionsButton.TabIndex = 10;
            this.allDirectionsButton.Text = "Найти все пути";
            this.allDirectionsButton.UseVisualStyleBackColor = true;
            this.allDirectionsButton.Click += new System.EventHandler(this.AllDirectionsButton_Click);
            // 
            // cvTabPage
            // 
            this.cvTabPage.Controls.Add(this.detectSignsInViewsButton);
            this.cvTabPage.Controls.Add(this.detectorsStatisticLabel);
            this.cvTabPage.Controls.Add(this.detecorsInfoListView);
            this.cvTabPage.Controls.Add(this.gMapMini);
            this.cvTabPage.Controls.Add(this.streetViewBox);
            this.cvTabPage.Location = new System.Drawing.Point(4, 25);
            this.cvTabPage.Name = "cvTabPage";
            this.cvTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.cvTabPage.Size = new System.Drawing.Size(1201, 750);
            this.cvTabPage.TabIndex = 2;
            this.cvTabPage.Text = "Обработка";
            this.cvTabPage.UseVisualStyleBackColor = true;
            // 
            // detectSignsInViewsButton
            // 
            this.detectSignsInViewsButton.Location = new System.Drawing.Point(474, 693);
            this.detectSignsInViewsButton.Name = "detectSignsInViewsButton";
            this.detectSignsInViewsButton.Size = new System.Drawing.Size(151, 23);
            this.detectSignsInViewsButton.TabIndex = 12;
            this.detectSignsInViewsButton.Text = "Начать обработку";
            this.detectSignsInViewsButton.UseVisualStyleBackColor = true;
            this.detectSignsInViewsButton.Click += new System.EventHandler(this.detectSignsInViewsButton_Click);
            // 
            // detectorsStatisticLabel
            // 
            this.detectorsStatisticLabel.AutoSize = true;
            this.detectorsStatisticLabel.Location = new System.Drawing.Point(26, 443);
            this.detectorsStatisticLabel.Name = "detectorsStatisticLabel";
            this.detectorsStatisticLabel.Size = new System.Drawing.Size(84, 17);
            this.detectorsStatisticLabel.TabIndex = 11;
            this.detectorsStatisticLabel.Text = "Статистика";
            // 
            // detecorsInfoListView
            // 
            this.detecorsInfoListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.detecorsInfoListView.Location = new System.Drawing.Point(29, 463);
            this.detecorsInfoListView.Name = "detecorsInfoListView";
            this.detecorsInfoListView.Size = new System.Drawing.Size(1137, 192);
            this.detecorsInfoListView.TabIndex = 10;
            this.detecorsInfoListView.UseCompatibleStateImageBehavior = false;
            this.detecorsInfoListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Детектор";
            this.columnHeader1.Width = 89;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Всего обнаружено знаков";
            this.columnHeader2.Width = 209;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Знаки, обнаруженные на данном изображении";
            this.columnHeader3.Width = 357;
            // 
            // gMapMini
            // 
            this.gMapMini.Bearing = 0F;
            this.gMapMini.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gMapMini.CanDragMap = true;
            this.gMapMini.Cursor = System.Windows.Forms.Cursors.Cross;
            this.gMapMini.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapMini.GrayScaleMode = false;
            this.gMapMini.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapMini.LevelsKeepInMemmory = 5;
            this.gMapMini.Location = new System.Drawing.Point(706, 21);
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
            this.gMapMini.Size = new System.Drawing.Size(460, 399);
            this.gMapMini.TabIndex = 9;
            this.gMapMini.Zoom = 10D;
            this.gMapMini.Load += new System.EventHandler(this.gMapMini_Load);
            this.gMapMini.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gMapMini_MouseDoubleClick);
            // 
            // streetViewBox
            // 
            this.streetViewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.streetViewBox.Location = new System.Drawing.Point(29, 21);
            this.streetViewBox.Name = "streetViewBox";
            this.streetViewBox.Size = new System.Drawing.Size(642, 399);
            this.streetViewBox.TabIndex = 0;
            this.streetViewBox.TabStop = false;
            // 
            // settingsTabPage
            // 
            this.settingsTabPage.Controls.Add(this.pluginPathTextBox);
            this.settingsTabPage.Controls.Add(this.pluginPathLabel);
            this.settingsTabPage.Controls.Add(this.detectorsListBox);
            this.settingsTabPage.Controls.Add(this.radiusLabel);
            this.settingsTabPage.Controls.Add(this.radiusUpDown);
            this.settingsTabPage.Controls.Add(this.orderInput);
            this.settingsTabPage.Controls.Add(this.settingsButton);
            this.settingsTabPage.Controls.Add(this.orderLabel);
            this.settingsTabPage.Location = new System.Drawing.Point(4, 25);
            this.settingsTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.settingsTabPage.Name = "settingsTabPage";
            this.settingsTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.settingsTabPage.Size = new System.Drawing.Size(1201, 750);
            this.settingsTabPage.TabIndex = 1;
            this.settingsTabPage.Text = "Настройки";
            this.settingsTabPage.UseVisualStyleBackColor = true;
            // 
            // pluginPathTextBox
            // 
            this.pluginPathTextBox.Location = new System.Drawing.Point(149, 145);
            this.pluginPathTextBox.Name = "pluginPathTextBox";
            this.pluginPathTextBox.Size = new System.Drawing.Size(160, 22);
            this.pluginPathTextBox.TabIndex = 8;
            // 
            // pluginPathLabel
            // 
            this.pluginPathLabel.AutoSize = true;
            this.pluginPathLabel.Location = new System.Drawing.Point(17, 150);
            this.pluginPathLabel.Name = "pluginPathLabel";
            this.pluginPathLabel.Size = new System.Drawing.Size(116, 17);
            this.pluginPathLabel.TabIndex = 7;
            this.pluginPathLabel.Text = "Путь к плагинам";
            // 
            // detectorsListBox
            // 
            this.detectorsListBox.FormattingEnabled = true;
            this.detectorsListBox.Location = new System.Drawing.Point(735, 32);
            this.detectorsListBox.Name = "detectorsListBox";
            this.detectorsListBox.Size = new System.Drawing.Size(359, 208);
            this.detectorsListBox.TabIndex = 6;
            this.detectorsListBox.SelectedIndexChanged += new System.EventHandler(this.detectorsListBox_SelectedIndexChanged);
            // 
            // radiusLabel
            // 
            this.radiusLabel.AutoSize = true;
            this.radiusLabel.Location = new System.Drawing.Point(17, 92);
            this.radiusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.radiusLabel.Name = "radiusLabel";
            this.radiusLabel.Size = new System.Drawing.Size(55, 17);
            this.radiusLabel.TabIndex = 5;
            this.radiusLabel.Text = "Радиус";
            // 
            // radiusUpDown
            // 
            this.radiusUpDown.Location = new System.Drawing.Point(149, 87);
            this.radiusUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.radiusUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.radiusUpDown.Name = "radiusUpDown";
            this.radiusUpDown.Size = new System.Drawing.Size(160, 22);
            this.radiusUpDown.TabIndex = 4;
            // 
            // orderInput
            // 
            this.orderInput.Location = new System.Drawing.Point(149, 27);
            this.orderInput.Margin = new System.Windows.Forms.Padding(4);
            this.orderInput.Name = "orderInput";
            this.orderInput.Size = new System.Drawing.Size(160, 22);
            this.orderInput.TabIndex = 3;
            // 
            // settingsButton
            // 
            this.settingsButton.Location = new System.Drawing.Point(483, 343);
            this.settingsButton.Margin = new System.Windows.Forms.Padding(4);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(100, 28);
            this.settingsButton.TabIndex = 2;
            this.settingsButton.Text = "Загрузить";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // orderLabel
            // 
            this.orderLabel.AutoSize = true;
            this.orderLabel.Location = new System.Drawing.Point(17, 32);
            this.orderLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.orderLabel.Name = "orderLabel";
            this.orderLabel.Size = new System.Drawing.Size(32, 17);
            this.orderLabel.TabIndex = 1;
            this.orderLabel.Text = "Шаг";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1206, 778);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "StreetViewer";
            this.tabControl.ResumeLayout(false);
            this.workTabPage.ResumeLayout(false);
            this.workTabPage.PerformLayout();
            this.cvTabPage.ResumeLayout(false);
            this.cvTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.streetViewBox)).EndInit();
            this.settingsTabPage.ResumeLayout(false);
            this.settingsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radiusUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.TextBox startStreet;
        private System.Windows.Forms.TextBox endStreet;
        private System.Windows.Forms.Button directionRequestButton;
        private System.Windows.Forms.Label DirecrionLabel;
        private GMap.NET.WindowsForms.GMapControl gMap;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Button streetViewsRequestButton;
        private System.Windows.Forms.FolderBrowserDialog streetVewsFolderDialog;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage workTabPage;
        private System.Windows.Forms.TabPage settingsTabPage;
        private System.Windows.Forms.Label orderLabel;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.NumericUpDown orderInput;
        private System.Windows.Forms.Label radiusLabel;
        private System.Windows.Forms.NumericUpDown radiusUpDown;
        private System.Windows.Forms.Button allDirectionsButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.CheckedListBox detectorsListBox;
        private System.Windows.Forms.TextBox pluginPathTextBox;
        private System.Windows.Forms.Label pluginPathLabel;
        private System.Windows.Forms.TabPage cvTabPage;
        private GMap.NET.WindowsForms.GMapControl gMapMini;
        private System.Windows.Forms.PictureBox streetViewBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ListView detecorsInfoListView;
        private System.Windows.Forms.Label detectorsStatisticLabel;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private Button detectSignsInViewsButton;
    }
}

