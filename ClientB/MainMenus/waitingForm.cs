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
    public partial class waitingForm : Form
    {
        public static bool isRunning;
        public static bool ansFromRival;
        public int i { get; set; }

        private Timer timer = new Timer();

        //main constructor
        public waitingForm(Form parent)
        {
            i = 0;
            InitializeComponent();
            this.Location = parent.Location;
            ansFromRival = false;
            isRunning = true;
            label.Text = "wating ";
            label1.Text = "...";
            timer.Interval = 1000;
            timer.Tick += new EventHandler(TimerEventProcessor);
            timer.Start();

        }

        private void TimerEventProcessor(object sender, EventArgs e)
        {

            if (i % 3 == 0)
                label1.Text = ".";
            if (i % 3 == 1)
                label1.Text = "..";
            if (i % 3 == 2)
                label1.Text = "...";
            i++;

            if (!isRunning)
            {
                timer.Stop();
                this.Close();
            }

        }

        public static void receivedAnswer(bool ans)
        {
            isRunning = false;
            ansFromRival = ans;

        }

    }
}
