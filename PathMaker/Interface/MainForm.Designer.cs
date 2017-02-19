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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.streetTextBox = new System.Windows.Forms.TextBox();
            this.requestButton = new System.Windows.Forms.Button();
            this.resultLabel = new System.Windows.Forms.Label();
            this.startStreet = new System.Windows.Forms.TextBox();
            this.endStreet = new System.Windows.Forms.TextBox();
            this.requestButton2 = new System.Windows.Forms.Button();
            this.GeocodingLabel = new System.Windows.Forms.Label();
            this.DirecrionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // streetTextBox
            // 
            this.streetTextBox.Location = new System.Drawing.Point(28, 51);
            this.streetTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.streetTextBox.Name = "streetTextBox";
            this.streetTextBox.Size = new System.Drawing.Size(100, 20);
            this.streetTextBox.TabIndex = 0;
            // 
            // requestButton
            // 
            this.requestButton.Location = new System.Drawing.Point(28, 97);
            this.requestButton.Margin = new System.Windows.Forms.Padding(2);
            this.requestButton.Name = "requestButton";
            this.requestButton.Size = new System.Drawing.Size(100, 19);
            this.requestButton.TabIndex = 1;
            this.requestButton.Text = "Найти";
            this.requestButton.UseVisualStyleBackColor = true;
            this.requestButton.Click += new System.EventHandler(this.requestButton_Click);
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(154, 51);
            this.resultLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(0, 13);
            this.resultLabel.TabIndex = 2;
            // 
            // startStreet
            // 
            this.startStreet.Location = new System.Drawing.Point(28, 159);
            this.startStreet.Name = "startStreet";
            this.startStreet.Size = new System.Drawing.Size(100, 20);
            this.startStreet.TabIndex = 3;
            // 
            // endStreet
            // 
            this.endStreet.Location = new System.Drawing.Point(157, 159);
            this.endStreet.Name = "endStreet";
            this.endStreet.Size = new System.Drawing.Size(100, 20);
            this.endStreet.TabIndex = 4;
            // 
            // requestButton2
            // 
            this.requestButton2.Location = new System.Drawing.Point(28, 198);
            this.requestButton2.Name = "requestButton2";
            this.requestButton2.Size = new System.Drawing.Size(100, 23);
            this.requestButton2.TabIndex = 5;
            this.requestButton2.Text = "Найти";
            this.requestButton2.UseVisualStyleBackColor = true;
            this.requestButton2.Click += new System.EventHandler(this.requestButton2_Click);
            // 
            // GeocodingLabel
            // 
            this.GeocodingLabel.AutoSize = true;
            this.GeocodingLabel.Location = new System.Drawing.Point(28, 33);
            this.GeocodingLabel.Name = "GeocodingLabel";
            this.GeocodingLabel.Size = new System.Drawing.Size(59, 13);
            this.GeocodingLabel.TabIndex = 6;
            this.GeocodingLabel.Text = "Geocoding";
            // 
            // DirecrionLabel
            // 
            this.DirecrionLabel.AutoSize = true;
            this.DirecrionLabel.Location = new System.Drawing.Point(28, 140);
            this.DirecrionLabel.Name = "DirecrionLabel";
            this.DirecrionLabel.Size = new System.Drawing.Size(49, 13);
            this.DirecrionLabel.TabIndex = 7;
            this.DirecrionLabel.Text = "Direction";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 241);
            this.Controls.Add(this.DirecrionLabel);
            this.Controls.Add(this.GeocodingLabel);
            this.Controls.Add(this.requestButton2);
            this.Controls.Add(this.endStreet);
            this.Controls.Add(this.startStreet);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.requestButton);
            this.Controls.Add(this.streetTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox streetTextBox;
        private System.Windows.Forms.Button requestButton;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.TextBox startStreet;
        private System.Windows.Forms.TextBox endStreet;
        private System.Windows.Forms.Button requestButton2;
        private System.Windows.Forms.Label GeocodingLabel;
        private System.Windows.Forms.Label DirecrionLabel;
    }
}

