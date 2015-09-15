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
using System.IO;
using System.Drawing.Imaging;

namespace Client
{
    public partial class Reg_Form : Form
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public bool IsAdvisor { get; set; }
        public int IdPlayer { get; set; }
        public   byte[] pictureStremByte { get; set; }
       
        public ServiceClient server;
        private ErrorProvider ep = new ErrorProvider();
        //for picture reset
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reg_Form));

        //main constructor
        public Reg_Form(MainProgram parent, ServiceClient Server)
        {
            InitializeComponent();
            this.Location = parent.Location;
            pictureStremByte = null;
            comboBox1.SelectedIndex = 1;
            server = Server;
        }

        //reset form input
        private void Reset_btn_Click(object sender, EventArgs e)
        {
            Text_box_first.Clear();
            Text_box_Last.Clear();
            comboBox1.SelectedIndex = 1;
            adviser_cb.Checked = false;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
           
        }
        //Submit all data and check validtion for data 
        private void Submit_button_Click(object sender, EventArgs e)
        {
            this.ValidateChildren();
            FirstName = Text_box_first.Text;
            LastName = Text_box_Last.Text;
            IsAdvisor = adviser_cb.Checked;
            this.Hide();
            this.Close();
           
            RegPlayer(FirstName, LastName, IsAdvisor);



            if (comboBox1.SelectedIndex == 0)
            {
                champanshipForm champForm = new champanshipForm(this,IdPlayer,server);
                champForm.ShowDialog();
            }
            else
            {
                adviserForm adviserForm = new adviserForm(this,server, IdPlayer);
                adviserForm.ShowDialog();
            }


            this.Close();
            
        }


        //Validting form 
        private void Text_box_first_Validating(object sender, CancelEventArgs e)
        {
            String str1 = Text_box_first.Text;
           
            if (str1.Length == 0 || str1.Any(char.IsDigit))
            {
                ep.SetError(Text_box_first, "Please Provide First Name");
                e.Cancel = true;

            }
         
        }
        //register player on the server
        private void RegPlayer(string FirstName, string LastName, bool tick)
        {
         
            IdPlayer = server.regPlayer(FirstName, LastName, tick,pictureStremByte); 
            

        }

        //choose pic to upload as avatar
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


