using Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class adviserForm : Form
    {
        public int playerId { get; set; }
        public List<int> selectedAdvisors { get; set; }

        private int counter = 3;
        private ServiceClient server;

        //main constructor
        public adviserForm(Form parent, ServiceClient Server, int id)
        {
            InitializeComponent();
            this.Location = parent.Location;
            playerId = id;
            server = Server;
            selectedAdvisors = new List<int>();
        }

        //submit selection
        private void Submit_btn_Click(object sender, EventArgs e)
        {
            server.regAdvisors(selectedAdvisors.ToArray(), playerId);
            this.Hide();
            MainMenu mainManuForm = new MainMenu(this, server, playerId);
            mainManuForm.ShowDialog();
            this.Close();
        }

        //onload fill datagrid
        private void adviserForm_Load(object sender, EventArgs e)
        {
            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            // Initialize and add a check box column.

            column = new DataGridViewImageColumn();
            //   column.Image
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

            //Fill data to form
            Players[] list = server.getAdvisors(playerId);
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
            dataGridView.DataSource = list;
            //set the heigt for picture colum
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.Height = 75;
            }

        }



        //check for selections
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
                    MessageBox.Show("Max of 3 advisors is allowed");
                }
                else
                    if ((bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == false && counter > 0)
                    {
                        selectedAdvisors.Add((int)dataGridView.Rows[e.RowIndex].Cells[1].Value);
                        counter--;

                    }
                    else
                        if ((bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == true)
                        {
                            selectedAdvisors.Remove((int)dataGridView.Rows[e.RowIndex].Cells[1].Value);

                            if (counter < 3)
                            {

                                counter++;
                            }
                        }
                dataGridView.EndEdit();
            }
        }

    }
}
