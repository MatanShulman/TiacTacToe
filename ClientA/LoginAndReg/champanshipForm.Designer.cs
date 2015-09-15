namespace Client
{
    partial class champanshipForm
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
            this.dataGridViewChamp = new System.Windows.Forms.DataGridView();
            this.submit_btn = new System.Windows.Forms.Button();
            this.ok_btn = new System.Windows.Forms.Button();
            this.addChamp_Btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChamp)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewChamp
            // 
            this.dataGridViewChamp.AllowUserToAddRows = false;
            this.dataGridViewChamp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewChamp.Location = new System.Drawing.Point(12, 42);
            this.dataGridViewChamp.Name = "dataGridViewChamp";
            this.dataGridViewChamp.Size = new System.Drawing.Size(740, 308);
            this.dataGridViewChamp.TabIndex = 0;
            // 
            // submit_btn
            // 
            this.submit_btn.Location = new System.Drawing.Point(397, 12);
            this.submit_btn.Name = "submit_btn";
            this.submit_btn.Size = new System.Drawing.Size(75, 23);
            this.submit_btn.TabIndex = 1;
            this.submit_btn.Text = "Register";
            this.submit_btn.UseVisualStyleBackColor = true;
            this.submit_btn.Click += new System.EventHandler(this.register_btn_Click);
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(313, 356);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(159, 23);
            this.ok_btn.TabIndex = 2;
            this.ok_btn.Text = "Finish";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.finsih_btn_Click);
            // 
            // addChamp_Btn
            // 
            this.addChamp_Btn.Location = new System.Drawing.Point(313, 12);
            this.addChamp_Btn.Name = "addChamp_Btn";
            this.addChamp_Btn.Size = new System.Drawing.Size(75, 23);
            this.addChamp_Btn.TabIndex = 3;
            this.addChamp_Btn.Text = "Add New";
            this.addChamp_Btn.UseVisualStyleBackColor = true;
            this.addChamp_Btn.Click += new System.EventHandler(this.addChamp_Btn_Click);
            // 
            // champanshipForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 391);
            this.Controls.Add(this.addChamp_Btn);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.submit_btn);
            this.Controls.Add(this.dataGridViewChamp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "champanshipForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "champanshipForm";
            this.Load += new System.EventHandler(this.champanshipForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChamp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewChamp;
        private System.Windows.Forms.Button submit_btn;
        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.Button addChamp_Btn;
    }
}