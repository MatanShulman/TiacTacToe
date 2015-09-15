namespace Client
{
    partial class regChampionships
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
            this.Submit_Btn = new System.Windows.Forms.Button();
            this.Name_Tb = new System.Windows.Forms.TextBox();
            this.location_TB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.pic_btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Submit_Btn
            // 
            this.Submit_Btn.Location = new System.Drawing.Point(92, 224);
            this.Submit_Btn.Name = "Submit_Btn";
            this.Submit_Btn.Size = new System.Drawing.Size(75, 23);
            this.Submit_Btn.TabIndex = 0;
            this.Submit_Btn.Text = "Submit";
            this.Submit_Btn.UseVisualStyleBackColor = true;
            this.Submit_Btn.Click += new System.EventHandler(this.Submit_Btn_Click);
            // 
            // Name_Tb
            // 
            this.Name_Tb.Location = new System.Drawing.Point(93, 38);
            this.Name_Tb.Name = "Name_Tb";
            this.Name_Tb.Size = new System.Drawing.Size(100, 20);
            this.Name_Tb.TabIndex = 1;
            // 
            // location_TB
            // 
            this.location_TB.Location = new System.Drawing.Point(92, 82);
            this.location_TB.Name = "location_TB";
            this.location_TB.Size = new System.Drawing.Size(100, 20);
            this.location_TB.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "location";
            // 
            // datePicker
            // 
            this.datePicker.Font = new System.Drawing.Font("Microsoft NeoGothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.datePicker.Location = new System.Drawing.Point(25, 132);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(200, 22);
            this.datePicker.TabIndex = 6;
            // 
            // pic_btn
            // 
            this.pic_btn.Location = new System.Drawing.Point(92, 175);
            this.pic_btn.Name = "pic_btn";
            this.pic_btn.Size = new System.Drawing.Size(75, 23);
            this.pic_btn.TabIndex = 7;
            this.pic_btn.Text = "Add picture";
            this.pic_btn.UseVisualStyleBackColor = true;
            this.pic_btn.Click += new System.EventHandler(this.pic_btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Client.Properties.Resources.emptyImage;
            this.pictureBox1.Location = new System.Drawing.Point(245, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // regChampionships
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 288);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pic_btn);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.location_TB);
            this.Controls.Add(this.Name_Tb);
            this.Controls.Add(this.Submit_Btn);
            this.Name = "regChampionships";
            this.Text = "regChampionshipcs";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Submit_Btn;
        private System.Windows.Forms.TextBox Name_Tb;
        private System.Windows.Forms.TextBox location_TB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.Button pic_btn;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}