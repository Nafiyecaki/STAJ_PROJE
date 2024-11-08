using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STAJ_PROJE
{
    public partial class AnaSayfa : Form
    {
        public AnaSayfa()
        {
            InitializeComponent();
        }

        private void fATURALARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Faturalar fr = new Faturalar(); // Faturalar formunu oluşturuyoruz
            fr.Show();
        }

        private void cARİHESAPLARToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
