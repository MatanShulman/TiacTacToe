namespace Client
{
    partial class MainProgram
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
            this.New_PLayer_btn = new System.Windows.Forms.Button();
            this.existing_player_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // New_PLayer_btn
            // 
            this.New_PLayer_btn.Location = new System.Drawing.Point(116, 64);
            this.New_PLayer_btn.Name = "New_PLayer_btn";
            this.New_PLayer_btn.Size = new System.Drawing.Size(192, 69);
            this.New_PLayer_btn.TabIndex = 0;
            this.New_PLayer_btn.Text = "New Player";
            this.New_PLayer_btn.UseVisualStyleBackColor = true;
            this.New_PLayer_btn.Click += new System.EventHandler(this.New_PLayer_btn_Click);
            // 
            // existing_player_btn
            // 
            this.existing_player_btn.Location = new System.Drawing.Point(116, 209);
            this.existing_player_btn.Name = "existing_player_btn";
            this.existing_player_btn.Size = new System.Drawing.Size(192, 66);
            this.existing_player_btn.TabIndex = 1;
            this.existing_player_btn.Text = "Existing  Player";
            this.existing_player_btn.UseVisualStyleBackColor = true;
            this.existing_player_btn.Click += new System.EventHandler(this.existing_player_btn_Click);
            // 
            // MainProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 343);
            this.Controls.Add(this.existing_player_btn);
            this.Controls.Add(this.New_PLayer_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainProgram";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainProgram";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button New_PLayer_btn;
        private System.Windows.Forms.Button existing_player_btn;
    }
}

