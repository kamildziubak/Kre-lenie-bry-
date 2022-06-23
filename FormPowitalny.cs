using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Projekt3_Dziubak59363
{
    public partial class kdFormPowitalny : Form
    {
        public kdFormPowitalny()
        {
            InitializeComponent();
        }

        private void kdFormPowitalny_Load(object sender, EventArgs e)
        {

        }

        private void kdBtnLaboratorium_Click(object sender, EventArgs e)
        {
            BryłyRegularne kdLaboratorium = new BryłyRegularne(this);
            kdLaboratorium.Visible = true;
            Visible = false;
        }

        public void kdPowróćDoEkranuGłównego()
        {
            Visible = true;
        }

        private void kdBtnProjekt_Click(object sender, EventArgs e)
        {
            kdBryłyZłożone kdProjekt = new kdBryłyZłożone(this);
            kdProjekt.Visible = true;
            Visible = false;
            MessageBox.Show(this, "Witaj w formularzu! Aby utworzyć bryły, wybierz ich właściwości korzystając z " +
            "kontrolek po lewo, po czym kliknij na powierzchnię rysownicy, by wskazać lokalizację. Bryła " +
            "zostanie automatycznie wykreślona!", "Witaj!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
