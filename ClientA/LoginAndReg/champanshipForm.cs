using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.ServiceReference1;
using System.ServiceModel;

namespace Client
{

    public partial class champanshipForm : Form
    {

        public int playerId { get; set; }

        private ServiceClient server;

        //main constructor
        public champanshipForm(Reg_Form parent, int id, ServiceClient server)
        {
            InitializeComponent();
            playerId = id;
            this.Location = parent.Location;
            this.server = server;
        }


        //on load fill datagrid
        private void champanshipForm_Load(object sender, EventArgs e)
        {

            DataGridViewColumn column = new DataGridViewTextBoxColumn();

            column = new DataGridViewImageColumn();
            //   column.Image
            column.DataPropertyName = "picture";
            column.Width = 80;
            column.Name = "picture";
            column.ReadOnly = false;
            dataGridViewChamp.Columns.Add(column);

            // Initialize and add a text box column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Id";
            column.Name = "Id";
            dataGridViewChamp.Columns.Add(column);

            // Initialize and add a check box column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            dataGridViewChamp.Columns.Add(column);

            // Initialize and add a check box column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Location";
            column.Name = "Location";
            dataGridViewChamp.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Date";
            column.Name = "Date";
            dataGridViewChamp.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Registered";
            column.Name = "Registered";
            column.DefaultCellStyle.NullValue = "No";
            dataGridViewChamp.Columns.Add(column);

            // Initialize and add a check box column.
            DataGridViewCheckBoxColumn columnCheckBox = new DataGridViewCheckBoxColumn();
            columnCheckBox.DataPropertyName = "Action";
            columnCheckBox.Name = "Action";
            columnCheckBox.ReadOnly = false;
            dataGridViewChamp.Columns.Add(columnCheckBox);
            fillDataGrid();
        }

        public void fillDataGrid()
        {

            dataGridViewChamp.DataSource = null;
            dataGridViewChamp.Rows.Clear();

            Champpion[] list = server.getChampList();
            Champpion[] registeredList = server.getChampionshipByPlayerId(playerId);

            // Initialize the DataGridView.
            dataGridViewChamp.AutoGenerateColumns = false;
            dataGridViewChamp.AutoSize = false;


            foreach (Champpion champ in list)
            {
                if (champ.picture != null)
                {

                    Rectangle compressionRectangle = new Rectangle(60, 60,
                     champ.picture.Width / 2, champ.picture.Height / 2);
                    using (Graphics g = Graphics.FromImage(champ.picture))
                        g.DrawImage(champ.picture, compressionRectangle);
                }

            }
            dataGridViewChamp.DataSource = list;
            //set the heigt for picture colum
            foreach (DataGridViewRow row in dataGridViewChamp.Rows)
            {

                row.Height = 75;
            }

            foreach (DataGridViewRow row in this.dataGridViewChamp.Rows)
            {
                foreach (var item in registeredList)
                {
                    if ((int)row.Cells["Id"].Value == item.id)
                    {

                        row.Cells["Registered"].Value = "Yes";

                    }
                }
            }
        }


        //register to championship
        private void register_btn_Click(object sender, EventArgs e)
        {
            List<int> lstId = new List<int>();

            foreach (DataGridViewRow row in this.dataGridViewChamp.Rows)
            {
                var x = row.Cells[5];
                if ((string)row.Cells["Registered"].Value != "Yes" && row.Cells["Action"].Value != null && (bool)row.Cells["Action"].Value == true)
                    lstId.Add((int)row.Cells["Id"].Value);
            }
            string msg = server.regChampionship(lstId.ToArray(), playerId);

            if (msg != "SUCCESS")
            {
                MessageBox.Show(msg);
            }

            fillDataGrid();


        }
        // finish championship section
        private void finsih_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            adviserForm adviserForm = new adviserForm(this, this.server, playerId);
            adviserForm.ShowDialog();
            this.Close();
        }


        //open reg champ form
        private void addChamp_Btn_Click(object sender, EventArgs e)
        {
            regChampionships regForm = new regChampionships(this);
            regForm.ShowDialog();
        }

        //register new champ
        public void addNewChampionship(string name, string location, DateTime date, byte[] picture)
        {
            string msg = server.addNewChampionship(name, location, date, picture, playerId);
            if (msg != "SUCCESS")
            {
                MessageBox.Show(msg);
            }

            fillDataGrid();
        }
    }
}
