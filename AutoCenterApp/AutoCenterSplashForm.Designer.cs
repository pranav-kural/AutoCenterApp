namespace AutoCenterApp
{
    partial class AutoCenterSplashForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoCenterSplashForm));
            this.SharpAutoFormLabel = new System.Windows.Forms.Label();
            this.WelcomeMessageLabel = new System.Windows.Forms.Label();
            this.SplashFormTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // SharpAutoFormLabel
            // 
            this.SharpAutoFormLabel.AutoSize = true;
            this.SharpAutoFormLabel.BackColor = System.Drawing.Color.Transparent;
            this.SharpAutoFormLabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SharpAutoFormLabel.ForeColor = System.Drawing.Color.Teal;
            this.SharpAutoFormLabel.Location = new System.Drawing.Point(174, 29);
            this.SharpAutoFormLabel.Name = "SharpAutoFormLabel";
            this.SharpAutoFormLabel.Size = new System.Drawing.Size(418, 65);
            this.SharpAutoFormLabel.TabIndex = 0;
            this.SharpAutoFormLabel.Text = "Sharp Auto Center";
            // 
            // WelcomeMessageLabel
            // 
            this.WelcomeMessageLabel.AutoSize = true;
            this.WelcomeMessageLabel.BackColor = System.Drawing.Color.Transparent;
            this.WelcomeMessageLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WelcomeMessageLabel.ForeColor = System.Drawing.Color.White;
            this.WelcomeMessageLabel.Location = new System.Drawing.Point(32, 288);
            this.WelcomeMessageLabel.Name = "WelcomeMessageLabel";
            this.WelcomeMessageLabel.Size = new System.Drawing.Size(701, 37);
            this.WelcomeMessageLabel.TabIndex = 1;
            this.WelcomeMessageLabel.Text = "Welcome! Calculate the cost of purchasing your new car";
            // 
            // SplashFormTimer
            // 
            this.SplashFormTimer.Enabled = true;
            this.SplashFormTimer.Interval = 3000;
            this.SplashFormTimer.Tick += new System.EventHandler(this.SplashFormTimer_Tick);
            // 
            // AutoCenterSplashForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(750, 350);
            this.ControlBox = false;
            this.Controls.Add(this.WelcomeMessageLabel);
            this.Controls.Add(this.SharpAutoFormLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "AutoCenterSplashForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoCenterSplashForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SharpAutoFormLabel;
        private System.Windows.Forms.Label WelcomeMessageLabel;
        private System.Windows.Forms.Timer SplashFormTimer;
    }
}