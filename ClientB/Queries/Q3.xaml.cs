using Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for Q3.xaml
    /// </summary>
    public partial class Q3 : System.Windows.Controls.UserControl
    {
        public int id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public DateTime date { get; set; }
            
        public Champpion[] list { get; set; }
        public byte[] picByte;

        public List<Champpion> editList { get; set; }


        private int formMode { get; set; }
        private ServiceClient server { get; set; }  
        private int playerId;
        private bool picFlag = false;

        public Q3(ServiceClient server, int mode)
        {
            InitializeComponent();
            this.server = server;
            dgv.CanUserAddRows = false;
            dgv.IsReadOnly = true;
            this.formMode = mode;
        }

        public Q3(ServiceClient server, int mode, int playerId)
        {
            InitializeComponent();
            this.server = server;
            dgv.CanUserAddRows = false;
            dgv.IsReadOnly = true;
            this.formMode = mode;
            this.playerId = playerId;
            this.editList = new List<Champpion>();
        }

        private  async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (formMode == 0)
            {
                list = await Task<Champpion[]>.Factory.StartNew(getChamps);
                foreach (var item in list)
                {
                    if (item.CreatedBy == playerId)
                        editList.Add(item);

                }


                dgv.ItemsSource = list;
            }

            dgv.AutoGenerateColumns = false;
            

         
        }

        public async void updateDataGrid(int playerId)
        {
            dgv.ItemsSource = null;
            list = await Task<Champpion[]>.Factory.StartNew(() => getChampByPlayer(playerId));

      
            dgv.AutoGenerateColumns = false;
            dgv.ItemsSource = list;

        }


        public Champpion[] getChamps()
        {
            answerQuerieForm.manualResetEvent.Reset();
            if(chooseQueriesForm.slowServer)
                Thread.Sleep(3000);
            Champpion[] lst = server.getChampList();
            answerQuerieForm.manualResetEvent.Set();
            return lst;

        }

        public Champpion[] getChampByPlayer(int playerId)
        {
            answerQuerieForm.manualResetEvent.Reset();
            if (chooseQueriesForm.slowServer)
                Thread.Sleep(3000);
            Champpion[] lst = server.wpfGetChampListByPlayer(playerId);
            answerQuerieForm.manualResetEvent.Set();
            return lst;

        }

        public void setDataGrid(bool flag)
        {
            if (flag)
            {
                dgv.ItemsSource = editList;
                dgv.IsReadOnly = false;
                dgv.Columns[0].IsReadOnly = true;
             
            }
            else
            {
                list = server.getChampList();
                dgv.ItemsSource = list;
                dgv.IsReadOnly = true;


            }
            picFlag = flag;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (picFlag)
            {
                Bitmap bm;
                OpenFileDialog fileChooser = new OpenFileDialog();
                if (fileChooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var str = fileChooser.FileName;
                    try
                    {
                        FileStream fs = new FileStream(str, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        MemoryStream ms = new MemoryStream(br.ReadBytes((int)fs.Length));
                        System.Drawing.Image imageIn = System.Drawing.Image.FromStream(ms);
                        bm = new Bitmap(imageIn);
                        DataGridRow rowContainingElement =
                        DataGridRow.GetRowContainingElement(e.OriginalSource as FrameworkElement);
                        int x = rowContainingElement.GetIndex();
                        editList[x].picture = bm;
                    }
                    catch { }

                   
                }
            }
        }

        internal void updateData()
        {
            server.updateChamp(editList.ToArray(),playerId);
            dgv.ItemsSource = null;
            dgv.ItemsSource = editList;

        }
        //Set tis value and property to delete from DB
        internal void deleteData()
        {
            var value="";
            var columName = dgv.SelectedCells[0].Column.Header.ToString();
            if (columName != null && columName != "Picture" && columName != "Date")
            {
                var index = (Champpion)dgv.SelectedCells[0].Item;
                var property = columName;  //propety
                switch (property)
                {
                    case "Name":
                        value = index.name;
                        break;
                    case "Location":
                        value = index.location;
                        break;
                    case "ID":
                        value = index.id.ToString();;
                        break;
                }
                bool ans = server.deletChampByValue(value, property,playerId);
                editList.Clear();
                list = server.getChampList();
                foreach (var item in list)
                {
                    if (item.CreatedBy == playerId)
                        editList.Add(item);
                }
                dgv.ItemsSource = null;
                dgv.ItemsSource = editList;
            }
            else
                System.Windows.Forms.MessageBox.Show("Cannot delete date or picture ");
        }
    }
}
