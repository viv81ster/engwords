using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordsToMemory
{
    
    public partial class FMain : Form
    {
        private FWords fWords;

        public FMain()
        {
            InitializeComponent();
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fWords = new FWords();
            fWords.Show();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
