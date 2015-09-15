namespace Client
{
    partial class MainMenu
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
            this.label1 = new System.Windows.Forms.Label();
            this.online_btn = new System.Windows.Forms.Button();
            this.history_btn = new System.Windows.Forms.Button();
            this.queries_btn = new System.Windows.Forms.Button();
            this.LogOut = new System.Windows.Forms.Button();
            this.AboutUsBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome";
            // 
            // online_btn
            // 
            this.online_btn.Location = new System.Drawing.Point(74, 36);
            this.online_btn.Name = "online_btn";
            this.online_btn.Size = new System.Drawing.Size(75, 23);
            this.online_btn.TabIndex = 1;
            this.online_btn.Text = "On Line";
            this.online_btn.UseVisualStyleBackColor = true;
            this.online_btn.Click += new System.EventHandler(this.online_btn_Click);
            // 
            // history_btn
            // 
            this.history_btn.Location = new System.Drawing.Point(74, 90);
            this.history_btn.Name = "history_btn";
            this.history_btn.Size = new System.Drawing.Size(75, 23);
            this.history_btn.TabIndex = 2;
            this.history_btn.Text = "History";
            this.history_btn.UseVisualStyleBackColor = true;
            this.history_btn.Click += new System.EventHandler(this.history_btn_Click);
            // 
            // queries_btn
            // 
            this.queries_btn.Location = new System.Drawing.Point(74, 147);
            this.queries_btn.Name = "queries_btn";
            this.queries_btn.Size = new System.Drawing.Size(75, 23);
            this.queries_btn.TabIndex = 3;
            this.queries_btn.Text = "Queries ";
            this.queries_btn.UseVisualStyleBackColor = true;
            this.queries_btn.Click += new System.EventHandler(this.queries_btn_Click);
            // 
            // LogOut
            // 
            this.LogOut.Location = new System.Drawing.Point(74, 261);
            this.LogOut.Name = "LogOut";
            this.LogOut.Size = new System.Drawing.Size(75, 23);
            this.LogOut.TabIndex = 4;
            this.LogOut.Text = "LogOut";
            this.LogOut.UseVisualStyleBackColor = true;
            this.LogOut.Click += new System.EventHandler(this.LogOut_Click);
            // 
            // AboutUsBtn
            // 
            this.AboutUsBtn.Location = new System.Drawing.Point(74, 206);
            this.AboutUsBtn.Name = "AboutUsBtn";
            this.AboutUsBtn.Size = new System.Drawing.Size(75, 23);
            this.AboutUsBtn.TabIndex = 5;
            this.AboutUsBtn.Text = "AboutUs";
            this.AboutUsBtn.UseVisualStyleBackColor = true;
            this.AboutUsBtn.Click += new System.EventHandler(this.AboutUsBtn_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 313);
            this.Controls.Add(this.AboutUsBtn);
            this.Controls.Add(this.LogOut);
            this.Controls.Add(this.queries_btn);
            this.Controls.Add(this.history_btn);
            this.Controls.Add(this.online_btn);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MainMenu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMenu_FormClosing);
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.VisibleChanged += new System.EventHandler(this.MainMenu_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button online_btn;
        private System.Windows.Forms.Button history_btn;
        private System.Windows.Forms.Button queries_btn;
        private System.Windows.Forms.Button LogOut;
        private System.Windows.Forms.Button AboutUsBtn;
    }
}