using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    partial class AboutUs : Form
    {
        public AboutUs()
        {
            InitializeComponent();
        }


        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
