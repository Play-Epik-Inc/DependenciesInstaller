using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configurator
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            Console.WriteLine($"{DateTime.Now}: Loading config files");
        }

        private void buttonChangeFolder_Click(object sender, EventArgs e)
        {
            OpenFolderBrowser();
        }

        private void buildButton_Click(object sender, EventArgs e)
        {
            StartBuilding();
        }
    }
}
