namespace StreetViewer.Interface

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
            this.SuspendLayout();
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(21, 47);
            this.resultLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(0, 13);
            this.resultLabel.TabIndex = 2;
            // 
            // startStreet
            // 
            this.startStreet.Location = new System.Drawing.Point(106, 17);
            this.startStreet.Name = "startStreet";
            this.startStreet.Size = new System.Drawing.Size(228, 20);
            this.startStreet.TabIndex = 3;
            this.toolTip1.SetToolTip(this.startStreet, "Введите конечную улицу");
            this.startStreet.KeyUp += new System.Windows.Forms.KeyEventHandler(this.startStreet_KeyUp);
            this.startStreet.MouseLeave += new System.EventHandler(this.startStreet_MouseLeave);
            this.startStreet.MouseHover += new System.EventHandler(this.startStreet_MouseHover);
            // 
            // endStreet
            // 
            this.endStreet.Location = new System.Drawing.Point(364, 17);
            this.endStreet.Name = "endStreet";
            this.endStreet.Size = new System.Drawing.Size(228, 20);
            this.endStreet.TabIndex = 4;
            this.endStreet.KeyUp += new System.Windows.Forms.KeyEventHandler(this.endStreet_KeyUp);
            this.endStreet.MouseLeave += new System.EventHandler(this.endStreet_MouseLeave);
            this.endStreet.MouseHover += new System.EventHandler(this.endStreet_MouseHover);
            // 
            // directionRequestButton
            // 
            this.directionRequestButton.Location = new System.Drawing.Point(620, 17);
            this.directionRequestButton.Name = "directionRequestButton";
            this.directionRequestButton.Size = new System.Drawing.Size(100, 23);
            this.directionRequestButton.TabIndex = 5;
            this.directionRequestButton.Text = "Найти";
            this.directionRequestButton.UseVisualStyleBackColor = true;
            this.directionRequestButton.Click += new System.EventHandler(this.directionRequestButton_Click);
            // 
            // DirecrionLabel
            // 
            this.DirecrionLabel.AutoSize = true;
            this.DirecrionLabel.Location = new System.Drawing.Point(9, 17);
            this.DirecrionLabel.Name = "DirecrionLabel";
            this.DirecrionLabel.Size = new System.Drawing.Size(64, 13);
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
            this.gMap.Location = new System.Drawing.Point(12, 78);
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
            this.gMap.Size = new System.Drawing.Size(708, 408);
            this.gMap.TabIndex = 8;
            this.gMap.Zoom = 10D;
            this.gMap.Load += new System.EventHandler(this.gMap_Load);
            // 
            // streetViewsRequestButton
            // 
            this.streetViewsRequestButton.Enabled = false;
            this.streetViewsRequestButton.Location = new System.Drawing.Point(620, 49);
            this.streetViewsRequestButton.Name = "streetViewsRequestButton";
            this.streetViewsRequestButton.Size = new System.Drawing.Size(100, 23);
            this.streetViewsRequestButton.TabIndex = 9;
            this.streetViewsRequestButton.Text = "Скачать";
            this.streetViewsRequestButton.UseVisualStyleBackColor = true;
            this.streetViewsRequestButton.Click += new System.EventHandler(this.streetViewsRequestButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 513);
            this.Controls.Add(this.streetViewsRequestButton);
            this.Controls.Add(this.gMap);
            this.Controls.Add(this.DirecrionLabel);
            this.Controls.Add(this.directionRequestButton);
            this.Controls.Add(this.endStreet);
            this.Controls.Add(this.startStreet);
            this.Controls.Add(this.resultLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}

