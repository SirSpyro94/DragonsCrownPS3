using JokerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DragonsCrownPS3
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private FileIO IO;

        private void btnLoad_Click(object sender, EventArgs e)
        {
            IO = FileIO.OpenIO("Open File", "|SAVE0.DAT", true);
            if (IO != null)
            {
                txtCurrent.Text = IO.SeekNReadInt32(0).ToString("X4");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IO == null)
                MessageBox.Show("No file open!");
            else
            {
                IO.Offset = 4;
                byte[] buffer = IO.ReadBytes(0x20EB14);
                uint crc = Crc32.Compute(buffer);
                IO.Offset = 0;
                txtComputed.Text = crc.ToString("X4");
                IO.Close();
                MessageBox.Show("Saved!");
            }
        }
    }
}
