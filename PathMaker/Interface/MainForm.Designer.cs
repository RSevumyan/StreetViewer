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
            this.SettingsTabPage = new System.Windows.Forms.TabPage();
            this.radiusLabel = new System.Windows.Forms.Label();
            this.radiusUpDown = new System.Windows.Forms.NumericUpDown();
            this.orderInput = new System.Windows.Forms.NumericUpDown();
            this.settingsButton = new System.Windows.Forms.Button();
            this.orderLabel = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.workTabPage.SuspendLayout();
            this.SettingsTabPage.SuspendLayout();
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
            this.gMap.Size = new System.Drawing.Size(947, 464);
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
            this.tabControl.Controls.Add(this.SettingsTabPage);
            this.tabControl.Location = new System.Drawing.Point(-1, 1);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(979, 630);
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
            this.workTabPage.Size = new System.Drawing.Size(971, 601);
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
            // SettingsTabPage
            // 
            this.SettingsTabPage.Controls.Add(this.radiusLabel);
            this.SettingsTabPage.Controls.Add(this.radiusUpDown);
            this.SettingsTabPage.Controls.Add(this.orderInput);
            this.SettingsTabPage.Controls.Add(this.settingsButton);
            this.SettingsTabPage.Controls.Add(this.orderLabel);
            this.SettingsTabPage.Location = new System.Drawing.Point(4, 25);
            this.SettingsTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.SettingsTabPage.Name = "SettingsTabPage";
            this.SettingsTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.SettingsTabPage.Size = new System.Drawing.Size(971, 601);
            this.SettingsTabPage.TabIndex = 1;
            this.SettingsTabPage.Text = "Настройки";
            this.SettingsTabPage.UseVisualStyleBackColor = true;
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
            this.radiusUpDown.Location = new System.Drawing.Point(109, 84);
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
            this.orderInput.Location = new System.Drawing.Point(109, 30);
            this.orderInput.Margin = new System.Windows.Forms.Padding(4);
            this.orderInput.Name = "orderInput";
            this.orderInput.Size = new System.Drawing.Size(160, 22);
            this.orderInput.TabIndex = 3;
            // 
            // settingsButton
            // 
            this.settingsButton.Location = new System.Drawing.Point(399, 510);
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
            this.ClientSize = new System.Drawing.Size(976, 631);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "StreetViewer";
            this.tabControl.ResumeLayout(false);
            this.workTabPage.ResumeLayout(false);
            this.workTabPage.PerformLayout();
            this.SettingsTabPage.ResumeLayout(false);
            this.SettingsTabPage.PerformLayout();
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
        private System.Windows.Forms.TabPage SettingsTabPage;
        private System.Windows.Forms.Label orderLabel;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.NumericUpDown orderInput;
        private System.Windows.Forms.Label radiusLabel;
        private System.Windows.Forms.NumericUpDown radiusUpDown;
        private System.Windows.Forms.Button allDirectionsButton;
        private System.Windows.Forms.Button ClearButton;
    }
}

