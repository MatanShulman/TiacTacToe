namespace Client
{
    partial class Reg_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reg_Form));
            this.Text_box_first = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Text_box_Last = new System.Windows.Forms.TextBox();
            this.Submit_button = new System.Windows.Forms.Button();
            this.Reset_btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.adviser_cb = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pic_btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Text_box_first
            // 
            this.Text_box_first.Location = new System.Drawing.Point(113, 35);
            this.Text_box_first.Name = "Text_box_first";
            this.Text_box_first.Size = new System.Drawing.Size(163, 20);
            this.Text_box_first.TabIndex = 0;
            this.Text_box_first.Validating += new System.ComponentModel.CancelEventHandler(this.Text_box_first_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "First Name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Last Name :";
            // 
            // Text_box_Last
            // 
            this.Text_box_Last.Location = new System.Drawing.Point(113, 69);
            this.Text_box_Last.Name = "Text_box_Last";
            this.Text_box_Last.Size = new System.Drawing.Size(163, 20);
            this.Text_box_Last.TabIndex = 3;
            // 
            // Submit_button
            // 
            this.Submit_button.Location = new System.Drawing.Point(22, 220);
            this.Submit_button.Name = "Submit_button";
            this.Submit_button.Size = new System.Drawing.Size(75, 23);
            this.Submit_button.TabIndex = 5;
            this.Submit_button.Text = "Submit";
            this.Submit_button.UseVisualStyleBackColor = true;
            this.Submit_button.Click += new System.EventHandler(this.Submit_button_Click);
            // 
            // Reset_btn
            // 
            this.Reset_btn.Location = new System.Drawing.Point(201, 220);
            this.Reset_btn.Name = "Reset_btn";
            this.Reset_btn.Size = new System.Drawing.Size(75, 23);
            this.Reset_btn.TabIndex = 6;
            this.Reset_btn.Text = "Reset";
            this.Reset_btn.UseVisualStyleBackColor = true;
            this.Reset_btn.Click += new System.EventHandler(this.Reset_btn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Are you adivser ? ";
            // 
            // adviser_cb
            // 
            this.adviser_cb.AutoSize = true;
            this.adviser_cb.Location = new System.Drawing.Point(178, 129);
            this.adviser_cb.Name = "adviser_cb";
            this.adviser_cb.Size = new System.Drawing.Size(61, 17);
            this.adviser_cb.TabIndex = 10;
            this.adviser_cb.Text = "Adviser";
            this.adviser_cb.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Have You Participated in Championship?";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "yes",
            "no"});
            this.comboBox1.Location = new System.Drawing.Point(88, 184);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 12;
            // 
            // pic_btn
            // 
            this.pic_btn.Location = new System.Drawing.Point(22, 97);
            this.pic_btn.Name = "pic_btn";
            this.pic_btn.Size = new System.Drawing.Size(101, 23);
            this.pic_btn.TabIndex = 13;
            this.pic_btn.Text = "Add a picture";
            this.pic_btn.UseVisualStyleBackColor = true;
            this.pic_btn.Click += new System.EventHandler(this.pic_btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(293, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // Reg_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 277);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pic_btn);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.adviser_cb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Reset_btn);
            this.Controls.Add(this.Submit_button);
            this.Controls.Add(this.Text_box_Last);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Text_box_first);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Reg_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "First Name :";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Text_box_first;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Text_box_Last;
        private System.Windows.Forms.Button Submit_button;
        private System.Windows.Forms.Button Reset_btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox adviser_cb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button pic_btn;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}