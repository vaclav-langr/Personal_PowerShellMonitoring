namespace PowershellMonitor.UserControls
{
    partial class PowerShellStation
    {
        /// <summary> 
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód vygenerovaný pomocí Návrháře komponent

        /// <summary> 
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PowerShellStation));
            this.labelSpeedDown = new System.Windows.Forms.Label();
            this.labelSpeedUp = new System.Windows.Forms.Label();
            this.labelStationName = new System.Windows.Forms.Label();
            this.pictureBoxStatus = new System.Windows.Forms.PictureBox();
            this.pictureBoxSpeedDown = new System.Windows.Forms.PictureBox();
            this.pictureBoxSpeedUp = new System.Windows.Forms.PictureBox();
            this.pictureBoxServiceStatus = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpeedDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpeedUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxServiceStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSpeedDown
            // 
            this.labelSpeedDown.AutoSize = true;
            this.labelSpeedDown.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSpeedDown.ForeColor = System.Drawing.Color.White;
            this.labelSpeedDown.Location = new System.Drawing.Point(291, 12);
            this.labelSpeedDown.Name = "labelSpeedDown";
            this.labelSpeedDown.Size = new System.Drawing.Size(70, 22);
            this.labelSpeedDown.TabIndex = 2;
            this.labelSpeedDown.Text = "50 MB/s";
            // 
            // labelSpeedUp
            // 
            this.labelSpeedUp.AutoSize = true;
            this.labelSpeedUp.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSpeedUp.ForeColor = System.Drawing.Color.White;
            this.labelSpeedUp.Location = new System.Drawing.Point(436, 12);
            this.labelSpeedUp.Name = "labelSpeedUp";
            this.labelSpeedUp.Size = new System.Drawing.Size(70, 22);
            this.labelSpeedUp.TabIndex = 3;
            this.labelSpeedUp.Text = "12 MB/s";
            // 
            // labelStationName
            // 
            this.labelStationName.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelStationName.ForeColor = System.Drawing.Color.White;
            this.labelStationName.Location = new System.Drawing.Point(82, 12);
            this.labelStationName.Name = "labelStationName";
            this.labelStationName.Size = new System.Drawing.Size(120, 22);
            this.labelStationName.TabIndex = 4;
            this.labelStationName.Text = "Notebook";
            // 
            // pictureBoxStatus
            // 
            this.pictureBoxStatus.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxStatus.Image")));
            this.pictureBoxStatus.Location = new System.Drawing.Point(0, 5);
            this.pictureBoxStatus.Name = "pictureBoxStatus";
            this.pictureBoxStatus.Size = new System.Drawing.Size(35, 35);
            this.pictureBoxStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxStatus.TabIndex = 5;
            this.pictureBoxStatus.TabStop = false;
            // 
            // pictureBoxSpeedDown
            // 
            this.pictureBoxSpeedDown.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSpeedDown.Image")));
            this.pictureBoxSpeedDown.Location = new System.Drawing.Point(240, 0);
            this.pictureBoxSpeedDown.Name = "pictureBoxSpeedDown";
            this.pictureBoxSpeedDown.Size = new System.Drawing.Size(45, 45);
            this.pictureBoxSpeedDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxSpeedDown.TabIndex = 1;
            this.pictureBoxSpeedDown.TabStop = false;
            // 
            // pictureBoxSpeedUp
            // 
            this.pictureBoxSpeedUp.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSpeedUp.Image")));
            this.pictureBoxSpeedUp.Location = new System.Drawing.Point(385, 0);
            this.pictureBoxSpeedUp.Name = "pictureBoxSpeedUp";
            this.pictureBoxSpeedUp.Size = new System.Drawing.Size(45, 45);
            this.pictureBoxSpeedUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxSpeedUp.TabIndex = 0;
            this.pictureBoxSpeedUp.TabStop = false;
            // 
            // pictureBoxServiceStatus
            // 
            this.pictureBoxServiceStatus.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.pictureBoxServiceStatus.Image = global::PowershellMonitor.Properties.Resources.ic_running_white_48dp_2x;
            this.pictureBoxServiceStatus.Location = new System.Drawing.Point(41, 5);
            this.pictureBoxServiceStatus.Name = "pictureBoxServiceStatus";
            this.pictureBoxServiceStatus.Size = new System.Drawing.Size(35, 35);
            this.pictureBoxServiceStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxServiceStatus.TabIndex = 6;
            this.pictureBoxServiceStatus.TabStop = false;
            // 
            // PowerShellStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(88)))), ((int)(((byte)(90)))));
            this.Controls.Add(this.pictureBoxServiceStatus);
            this.Controls.Add(this.pictureBoxStatus);
            this.Controls.Add(this.labelStationName);
            this.Controls.Add(this.labelSpeedUp);
            this.Controls.Add(this.labelSpeedDown);
            this.Controls.Add(this.pictureBoxSpeedDown);
            this.Controls.Add(this.pictureBoxSpeedUp);
            this.Name = "PowerShellStation";
            this.Size = new System.Drawing.Size(538, 45);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpeedDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpeedUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxServiceStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxSpeedUp;
        private System.Windows.Forms.PictureBox pictureBoxSpeedDown;
        private System.Windows.Forms.Label labelSpeedDown;
        private System.Windows.Forms.Label labelSpeedUp;
        private System.Windows.Forms.Label labelStationName;
        private System.Windows.Forms.PictureBox pictureBoxStatus;
        private System.Windows.Forms.PictureBox pictureBoxServiceStatus;
    }
}
