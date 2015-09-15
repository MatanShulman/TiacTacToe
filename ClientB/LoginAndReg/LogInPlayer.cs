using Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class LogInPlayer : Form
    {
        public int counter { get; set; }
        public int playerId { get; set; }

        private ServiceClient server;


        //main constructor
        public LogInPlayer(Form parent, ServiceClient server)
        {
            InitializeComponent();
            this.Location = parent.Location;
            this.server = server;
            counter = 1;

        }


        //on load fill datagrid
        private void LogInPlayer_Load(object sender, EventArgs e)
        {
            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            // Initialize and add a check box column.

            column = new DataGridViewImageColumn();
            column.DataPropertyName = "pictureArrByte";
            column.Width = 80;

            column.Name = "picture";
            column.ReadOnly = false;
            dataGridView.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "playerId";
            column.Name = "Id";
            dataGridView.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "firstName";
            column.Name = "First Name";
            dataGridView.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "lastName";
            column.Name = "Last Name";
            dataGridView.Columns.Add(column);

            // Initialize and add a check box column.
            DataGridViewCheckBoxColumn columnCheckBox = new DataGridViewCheckBoxColumn();

            columnCheckBox.DataPropertyName = "Action";
            columnCheckBox.Name = "Action";
            columnCheckBox.ReadOnly = false;
            dataGridView.Columns.Add(columnCheckBox);
            Players[] list;
            try
            {

                //Fill data to form
                list = server.getAllPlayers();
                dataGridView.AutoGenerateColumns = false;
                dataGridView.AutoSize = false;
                foreach (Players player in list)
                {
                    if (player.pictureArrByte != null)
                    {

                        Rectangle compressionRectangle = new Rectangle(60, 60,
                         player.pictureArrByte.Width / 2, player.pictureArrByte.Height / 2);
                        using (Graphics g = Graphics.FromImage(player.pictureArrByte))
                            g.DrawImage(player.pictureArrByte, compressionRectangle);
                    }

                }
            }
            catch (Exception ex)
            {               
                throw;
            }
            dataGridView.DataSource = list;
            //set the heigt for picture colum
            foreach (DataGridViewRow row in dataGridView.Rows)
            {

                row.Height = 75;
            }

        }

        //check for the choosen player
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;

                if ((bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == false && counter == 0)
                {

                    dataGridView.EndEdit();
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                    MessageBox.Show("Max of 1 player is allowed");
                }
                else
                    if ((bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == false && counter > 0)
                    {
                        playerId = (int)dataGridView.Rows[e.RowIndex].Cells[1].Value;
                        counter--;

                    }
                    else
                        if ((bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == true)
                        {

                            if (counter < 1)
                            {

                                counter++;
                            }
                        }
                dataGridView.EndEdit();
            }
        }

        //do the login
        private void submit_btn_Click(object sender, EventArgs e)
        {
            if (counter == 0)
            {                
                this.Hide();
                MainMenu mainManuForm = new MainMenu(this,server, playerId);
                mainManuForm.ShowDialog();
                this.Close();
            }
            else
                MessageBox.Show("Please select a player first");


           
        }


    }
}
