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
    /// Interaction logic for Q2.xaml
    /// </summary>
    public partial class Q2 : UserControl
    {
        public int gameId { get; set; }
        public string playerX { get; set; }
        public string playerCircle { get; set; }
        public string gameMode { get; set; }
        public string winner { get; set; }
        public int formMode { get; set; }
        private ServiceClient server { get; set; }     
        public MyGames[] list { get; set; }

        public Q2(ServiceClient server, int mode)
        {
            InitializeComponent();
            this.server = server;
            dgv.CanUserAddRows = false;
            dgv.IsReadOnly = true;
            formMode = mode;
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if(formMode == 0)
                list = await Task<MyGames[]>.Factory.StartNew(getGames);


           
          
            dgv.AutoGenerateColumns = false;
            if (formMode == 0)
                dgv.ItemsSource = list;
        }

        public async void updateDataGrid(int playerId)
        {
            dgv.ItemsSource = null;
            list = await Task<MyGames[]>.Factory.StartNew(() => getGamesByPlayer(playerId));

            dgv.AutoGenerateColumns = false;
            dgv.ItemsSource = list;
        }

        private MyGames[] getGames()
        {
            answerQuerieForm.manualResetEvent.Reset();
            if (chooseQueriesForm.slowServer)
                Thread.Sleep(3000);
            MyGames[] lst = server.wpfGetAllGames();
            answerQuerieForm.manualResetEvent.Set();
            return lst;
        }

        private MyGames[] getGamesByPlayer(int playerId)
        {
            answerQuerieForm.manualResetEvent.Reset();
            if (chooseQueriesForm.slowServer)
                Thread.Sleep(3000);
            MyGames[] lst = server.wpfGetGamesByPlayer(playerId);
            answerQuerieForm.manualResetEvent.Set();
            return lst;
        }
    }
}
