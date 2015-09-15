using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class regChampionships : Form
    {
        public string  name { get; set; }
        public string  location { get; set; }
        public DateTime date { get; set; }
        public byte[] pictureStremByte { get; set; }

        private champanshipForm cForm;

        //main constructor
        public regChampionships(champanshipForm cForm)
        {
            InitializeComponent();
            pictureStremByte = null;
            this.cForm = cForm;

        }

        //submit form data
        private void Submit_Btn_Click(object sender, EventArgs e)
        {
            name = Name_Tb.Text;
            location = location_TB.Text;
            date = datePicker.Value;

            cForm.addNewChampionship(name, location, date, pictureStremByte);
            this.Hide();
            this.Close();

        }

        //add champ pic
        private void pic_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileChooser = new OpenFileDialog();
            if (fileChooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var str = fileChooser.FileName;
                try
                {
                    FileStream fs = new FileStream(str, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    MemoryStream ms = new MemoryStream(br.ReadBytes((int)fs.Length));
                    Image imageIn = Image.FromStream(ms);
                    Bitmap bm = new Bitmap(imageIn);
                    if (bm.Size.Width == 256 && bm.Size.Height == 256)
                    {
                        pictureBox1.Image = imageIn;
                        pictureStremByte = ms.ToArray();
                    }
                    else
                    {
                        MessageBox.Show("Please insert a 256X256 pixel image");
                    }
                }
                catch
                {

                }
            }
        }

        
    }
}
