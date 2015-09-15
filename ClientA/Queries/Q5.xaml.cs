using Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for Q5.xaml
    /// </summary>
    public partial class Q5 : UserControl
    {
        private ServiceClient server { get; set; }
        public List<Players> list { get; set; }
        public Q5(ServiceClient Server)
        {
            InitializeComponent();
            this.server = Server;
        }


        public void GetAmountOfGames()
        {

            answerQuerieForm.manualResetEvent.Reset();
            if (chooseQueriesForm.slowServer)
                Thread.Sleep(3000);
            cities[] lst = server.wpfGetAmountOfCities();
            answerQuerieForm.manualResetEvent.Set();
            var lastOpenedForm = (answerQuerieForm)System.Windows.Forms.Application.OpenForms[System.Windows.Forms.Application.OpenForms.Count - 1];
            lastOpenedForm.BeginInvoke((Action)(() =>
            {
                dgv.ItemsSource = lst;

            }));


        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
        
            dgv.ItemsSource = null;
           ThreadPool.QueueUserWorkItem(new WaitCallback((_) => GetAmountOfGames()));
            dgv.AutoGenerateColumns = false;
        }
    }
}
