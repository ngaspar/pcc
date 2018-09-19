namespace PCC
{
    partial class PCC
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxZoom = new System.Windows.Forms.PictureBox();
            this.panelColor = new System.Windows.Forms.Panel();
            this.buttonCapture = new System.Windows.Forms.Button();
            this.labelRGB = new System.Windows.Forms.Label();
            this.boxHex = new System.Windows.Forms.TextBox();
            this.labelHex = new System.Windows.Forms.Label();
            this.boxRGB = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxZoom
            // 
            this.pictureBoxZoom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxZoom.Location = new System.Drawing.Point(6, 5);
            this.pictureBoxZoom.Name = "pictureBoxZoom";
            this.pictureBoxZoom.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxZoom.TabIndex = 0;
            this.pictureBoxZoom.TabStop = false;
            this.pictureBoxZoom.Click += new System.EventHandler(this.PictureBoxZoom_Click);
            // 
            // panelColor
            // 
            this.panelColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelColor.CausesValidation = false;
            this.panelColor.Location = new System.Drawing.Point(112, 5);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(40, 40);
            this.panelColor.TabIndex = 2;
            this.panelColor.Click += new System.EventHandler(this.PanelColor_Click);
            // 
            // buttonCapture
            // 
            this.buttonCapture.CausesValidation = false;
            this.buttonCapture.Location = new System.Drawing.Point(156, 5);
            this.buttonCapture.Name = "buttonCapture";
            this.buttonCapture.Size = new System.Drawing.Size(64, 40);
            this.buttonCapture.TabIndex = 3;
            this.buttonCapture.Text = "Capture!";
            this.buttonCapture.UseVisualStyleBackColor = true;
            this.buttonCapture.Click += new System.EventHandler(this.ButtonCapture_Click);
            // 
            // labelRGB
            // 
            this.labelRGB.AutoSize = true;
            this.labelRGB.CausesValidation = false;
            this.labelRGB.Location = new System.Drawing.Point(109, 59);
            this.labelRGB.Name = "labelRGB";
            this.labelRGB.Size = new System.Drawing.Size(33, 13);
            this.labelRGB.TabIndex = 4;
            this.labelRGB.Text = "RGB:";
            this.labelRGB.Click += new System.EventHandler(this.LabelRGB_Click);
            // 
            // boxHex
            // 
            this.boxHex.Location = new System.Drawing.Point(142, 84);
            this.boxHex.Name = "boxHex";
            this.boxHex.Size = new System.Drawing.Size(78, 20);
            this.boxHex.TabIndex = 5;
            this.boxHex.Click += new System.EventHandler(this.BoxRGB_Click);
            this.boxHex.CursorChanged += new System.EventHandler(this.BoxHex_TextChanged);
            this.boxHex.TextChanged += new System.EventHandler(this.BoxHex_TextChanged);
            // 
            // labelHex
            // 
            this.labelHex.BackColor = System.Drawing.SystemColors.Control;
            this.labelHex.CausesValidation = false;
            this.labelHex.Location = new System.Drawing.Point(109, 87);
            this.labelHex.Name = "labelHex";
            this.labelHex.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelHex.Size = new System.Drawing.Size(32, 13);
            this.labelHex.TabIndex = 6;
            this.labelHex.Text = "Hex:";
            // 
            // boxRGB
            // 
            this.boxRGB.Location = new System.Drawing.Point(142, 56);
            this.boxRGB.Name = "boxRGB";
            this.boxRGB.Size = new System.Drawing.Size(78, 20);
            this.boxRGB.TabIndex = 7;
            this.boxRGB.Click += new System.EventHandler(this.BoxRGB_Click);
            this.boxRGB.CursorChanged += new System.EventHandler(this.BoxRGB_TextChanged);
            this.boxRGB.TextChanged += new System.EventHandler(this.BoxRGB_TextChanged);
            // 
            // PCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(225, 110);
            this.Controls.Add(this.boxRGB);
            this.Controls.Add(this.labelHex);
            this.Controls.Add(this.boxHex);
            this.Controls.Add(this.labelRGB);
            this.Controls.Add(this.buttonCapture);
            this.Controls.Add(this.panelColor);
            this.Controls.Add(this.pictureBoxZoom);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PCC";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pixel Color Capture";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.PCC_Deactivate);
            this.Click += new System.EventHandler(this.PCC_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.PictureBox pictureBoxZoom;
        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.Button buttonCapture;
        private System.Windows.Forms.Label labelRGB;
        private System.Windows.Forms.TextBox boxHex;
        private System.Windows.Forms.Label labelHex;
        private System.Windows.Forms.TextBox boxRGB;
    }
}

