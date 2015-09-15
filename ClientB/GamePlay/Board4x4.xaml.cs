using Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for _4x4Board.xaml
    /// </summary>
    public partial class Board4x4 : UserControl
    {
        public static int shape;

        public const int BOARD_SIZE = 4;
        public static List<StackPanel> refernceArr = new List<StackPanel>();
        private bool Turn;
        private ServiceClient server;
        public int playerId { get; set; }
        public int rivalId { get; set; }
        public bool win { get; set; }
        public bool draw { get; set; }
        public gameBoardForm gameBoard { get; set; }
        public int moveCounter { get; set; }

        public Board4x4(bool turn, ServiceClient Server, int playerid, int rivalid, gameBoardForm gbf)
        {

            InitializeComponent();
            moveCounter = 0;
            this.gameBoard = gbf;
            this.Turn = turn;
            this.playerId = playerid;
            this.rivalId = rivalid;
            this.win = false;
            this.draw = false;

            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int z = 0; z < BOARD_SIZE; z++)
                {
                    gameBoardForm.boardGame4x4[i, z] = 2;
                }
            }
            //Determine who is cross or circle
            if (Turn)
            {
                shape = 1;
                gameBoard.turn_label.Text = "Please make a move";
            }
            else
            {
                gameBoard.turn_label.Text = "Please wait to your turn";
                shape = 0;
            }

            this.server = Server;

            refernceArr.Clear();
            IEnumerable<StackPanel> collection = grid.Children.OfType<StackPanel>();
            foreach (var item in collection)
            {
                refernceArr.Add(item);

            }

        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            gameBoard.turn_label.Text = "Please wait to your turn";
            Storyboard sb1 = new Storyboard();
            Storyboard sb2 = new Storyboard();
            Canvas cv = new Canvas();
            ((StackPanel)sender).Children.Add(cv);


            //the cords of the panel that was pressed
            string[] arr = ((StackPanel)sender).Name.Split('_');
            int x = Convert.ToInt32(arr[1]);
            int y = Convert.ToInt32(arr[2]);
            if (gameBoardForm.boardGame4x4[x, y] == 2 && Turn)
            {


                if (shape == 0)
                {
                    gameBoardForm.boardGame4x4[x, y] = 0;
                    DrawCircle(sb1, cv);

                }
                else
                {
                    gameBoardForm.boardGame4x4[x, y] = 1;
                    DrawX(sb1, sb2, cv);

                }
                checkForWin();

                Turn = !Turn;
                server.myMove(playerId, (x * BOARD_SIZE) + y, rivalId, "Medium", win, shape, moveCounter, gameBoardForm.gameId);
                moveCounter++;

            }
            else if (!Turn)
                gameBoard.showMsg("It is Not your turn please wait");
            else
                gameBoard.showMsg("you cant press here");

            if (win)
            {
                gameBoard.turn_label.Text = "You Won :)";
                gameBoard.showMsg("You Won !!!");
                gameBoardForm.closeByX = false;
                gameBoard.Close();
            }
            else
                if (moveCounter == BOARD_SIZE * BOARD_SIZE)
                {
                    gameBoard.turn_label.Text = "It Was A Draw :\\";
                    gameBoard.showMsg("Its A Draw");
                    gameBoardForm.closeByX = false;
                    gameBoard.Close();
                }
        }

        private void checkForWin()
        {

            win = false;
            if (gameBoardForm.boardGame4x4[0, 0] == shape && gameBoardForm.boardGame4x4[1, 1] == shape && gameBoardForm.boardGame4x4[2, 2] == shape && gameBoardForm.boardGame4x4[3, 3] == shape)
                win = true;
            if (gameBoardForm.boardGame4x4[0, 3] == shape && gameBoardForm.boardGame4x4[1, 2] == shape && gameBoardForm.boardGame4x4[2, 1] == shape && gameBoardForm.boardGame4x4[3, 0] == shape)
                win = true;
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                if (gameBoardForm.boardGame4x4[i, 0] == shape && gameBoardForm.boardGame4x4[i, 1] == shape && gameBoardForm.boardGame4x4[i, 2] == shape && gameBoardForm.boardGame4x4[i, 3] == shape)
                    win = true;
                if (gameBoardForm.boardGame4x4[0, i] == shape && gameBoardForm.boardGame4x4[1, i] == shape && gameBoardForm.boardGame4x4[2, i] == shape && gameBoardForm.boardGame4x4[3, i] == shape)
                    win = true;

            }

        }

        public void rivalDraw(StackPanel sp, bool oppentWin)
        {
            gameBoard.turn_label.Text = "Please make a move";
            Storyboard sb1 = new Storyboard();
            Storyboard sb2 = new Storyboard();
            Canvas cv = new Canvas();
            sp.Children.Add(cv);
            string[] arr = sp.Name.Split('_');
            int x = Convert.ToInt32(arr[1]);
            int y = Convert.ToInt32(arr[2]);



            if (shape == 1)
            {
                //  gameBoardForm.boardGame3x3[x, y] = 1;
                DrawCircle(sb1, cv);
                gameBoardForm.boardGame4x4[x, y] = 0;

            }
            else
            {
                // gameBoardForm.boardGame3x3[x, y] = 0;
                DrawX(sb1, sb2, cv);
                gameBoardForm.boardGame4x4[x, y] = 1;

            }
            Turn = !Turn;
            moveCounter++;
            if (oppentWin)
            {
                gameBoard.turn_label.Text = "You Lost :(";
                gameBoard.showMsg("Rival Has Won");
                gameBoardForm.closeByX = false;
                gameBoard.Close();
            }
            else
                if (moveCounter == BOARD_SIZE * BOARD_SIZE)
                {
                    gameBoard.turn_label.Text = "It Was A Draw :\\";
                    gameBoard.showMsg("Its A Draw");
                    gameBoardForm.closeByX = false;
                    gameBoard.Close();
                }



        }
        //draw a circle in stackPanel
        private void DrawCircle(Storyboard sb, Canvas cv)
        {
            Ellipse el = new Ellipse();
            el.Margin = new Thickness(13, 8, 0, 0);
            el.Width = 0;
            el.Height = 0;
            el.Stroke = Brushes.Blue;
            el.StrokeThickness = 10;


            cv.Children.Add(el);

            DoubleAnimation widthAnimation = new DoubleAnimation(el.Width, 90, new Duration(new TimeSpan(0, 0, 1)));
            DoubleAnimation heightAnimation = new DoubleAnimation(el.Height, 80, new Duration(new TimeSpan(0, 0, 1)));

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Ellipse.WidthProperty));
            Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(Ellipse.HeightProperty));

            sb.Children.Add(widthAnimation);
            sb.Children.Add(heightAnimation);

            el.BeginStoryboard(sb);
        }
        //draw a cross in stackPanel
        public void DrawX(Storyboard sb1, Storyboard sb2, Canvas cv)
        {
            Line line1 = new Line();
            cv.Children.Add(line1);
            line1.Stroke = Brushes.Red;
            line1.StrokeThickness = 8;
            line1.X1 = 15;
            line1.Y1 = 15;
            line1.Y2 = 15;
            line1.X2 = 15;
            DoubleAnimation da = new DoubleAnimation(line1.Y2, 80, new Duration(new TimeSpan(0, 0, 1)));
            DoubleAnimation da1 = new DoubleAnimation(line1.X2, 95, new Duration(new TimeSpan(0, 0, 1)));
            Storyboard.SetTargetProperty(da, new PropertyPath("(Line.Y2)"));
            Storyboard.SetTargetProperty(da1, new PropertyPath("(Line.X2)"));
            sb1.Children.Add(da);
            sb1.Children.Add(da1);



            Line line2 = new Line();
            cv.Children.Add(line2);
            line2.Stroke = Brushes.Red;
            line2.StrokeThickness = 8;
            line2.X1 = 95;
            line2.Y1 = 15;
            line2.Y2 = 15;
            line2.X2 = 95;
            DoubleAnimation db = new DoubleAnimation(line2.Y2, 80, new Duration(new TimeSpan(0, 0, 1)));
            DoubleAnimation db1 = new DoubleAnimation(line2.X2, 15, new Duration(new TimeSpan(0, 0, 1)));
            Storyboard.SetTargetProperty(db, new PropertyPath("(Line.Y2)"));
            Storyboard.SetTargetProperty(db1, new PropertyPath("(Line.X2)"));
            sb2.Children.Add(db);
            sb2.Children.Add(db1);

            line1.BeginStoryboard(sb1);
            line2.BeginStoryboard(sb2);
        }

        //use for next button
        public void histroyDraw(bool shape, int location)
        {
            Storyboard sb1 = new Storyboard();
            Storyboard sb2 = new Storyboard();
            Canvas cv = new Canvas();
            StackPanel sp = refernceArr[location];
            sp.Children.Add(cv);

            if (shape)
                DrawX(sb1, sb2, cv);
            else
                DrawCircle(sb1, cv);


        }
        //use for back cutton
        public void HistoryDelete(int location)
        {
            StackPanel sp = refernceArr[location];
            sp.Children.RemoveAt(0);
        }




    }


}
