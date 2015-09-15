namespace Client
{
    partial class onlinePlayersForm
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
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.easy_btn = new System.Windows.Forms.RadioButton();
            this.mediun_btn = new System.Windows.Forms.RadioButton();
            this.hard_btn = new System.Windows.Forms.RadioButton();
            this.label = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.replayTxt_btn = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox
            // 
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(78, 46);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(121, 21);
            this.comboBox.TabIndex = 0;
            // 
            // easy_btn
            // 
            this.easy_btn.AutoSize = true;
            this.easy_btn.Location = new System.Drawing.Point(46, 24);
            this.easy_btn.Name = "easy_btn";
            this.easy_btn.Size = new System.Drawing.Size(48, 17);
            this.easy_btn.TabIndex = 2;
            this.easy_btn.TabStop = true;
            this.easy_btn.Text = "Easy";
            this.easy_btn.UseVisualStyleBackColor = true;
            // 
            // mediun_btn
            // 
            this.mediun_btn.AutoSize = true;
            this.mediun_btn.Location = new System.Drawing.Point(46, 52);
            this.mediun_btn.Name = "mediun_btn";
            this.mediun_btn.Size = new System.Drawing.Size(62, 17);
            this.mediun_btn.TabIndex = 3;
            this.mediun_btn.TabStop = true;
            this.mediun_btn.Text = "Medium";
            this.mediun_btn.UseVisualStyleBackColor = true;
            // 
            // hard_btn
            // 
            this.hard_btn.AutoSize = true;
            this.hard_btn.Cursor = System.Windows.Forms.Cursors.Default;
            this.hard_btn.Location = new System.Drawing.Point(46, 77);
            this.hard_btn.Name = "hard_btn";
            this.hard_btn.Size = new System.Drawing.Size(48, 17);
            this.hard_btn.TabIndex = 4;
            this.hard_btn.TabStop = true;
            this.hard_btn.Text = "Hard";
            this.hard_btn.UseVisualStyleBackColor = true;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(94, 30);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(82, 13);
            this.label.TabIndex = 5;
            this.label.Text = "choose a player";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(97, 205);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Submit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.easy_btn);
            this.groupBox1.Controls.Add(this.mediun_btn);
            this.groupBox1.Controls.Add(this.hard_btn);
            this.groupBox1.Location = new System.Drawing.Point(64, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 108);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose Difficulty";
            // 
            // replayTxt_btn
            // 
            this.replayTxt_btn.AutoSize = true;
            this.replayTxt_btn.Location = new System.Drawing.Point(5, 229);
            this.replayTxt_btn.Name = "replayTxt_btn";
            this.replayTxt_btn.Size = new System.Drawing.Size(0, 13);
            this.replayTxt_btn.TabIndex = 8;
            // 
            // onlinePlayersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 288);
            this.Controls.Add(this.replayTxt_btn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label);
            this.Controls.Add(this.comboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "onlinePlayersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "onlinePlayersForm";
            this.Load += new System.EventHandler(this.onlinePlayersForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.RadioButton easy_btn;
        private System.Windows.Forms.RadioButton mediun_btn;
        private System.Windows.Forms.RadioButton hard_btn;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label replayTxt_btn;
    }
}