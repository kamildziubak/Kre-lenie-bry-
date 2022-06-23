using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using static BryłyGeometryczne.KlasyBryłGeometrycznych;
using static BryłyGeometryczne.KlasyBryłGeometrycznych.Walec;

namespace OOP_Projekt3_Dziubak59363
{
    public partial class BryłyRegularne : Form
    {
        Graphics Rysownica, PowierzchniaGraficznaWziernikaLinii;
        Pen Pióro;
        List<BryłaAbstrakcyjna> LBG = new List<BryłaAbstrakcyjna>();
        //zmienna pomocnicza przechowująca wybrany punkt na rysownicy
        Point PunktLokalizacjiBryły = new Point(-1,-1);
        kdFormPowitalny kdFormularz;

        public BryłyRegularne(kdFormPowitalny kdFormularz)
        {
            this.kdFormularz = kdFormularz;

            InitializeComponent();

            //utworzenie egzemplarza rysownicy
            pbRysownica.Image = new Bitmap(pbRysownica.Width, pbRysownica.Height);
            Rysownica = Graphics.FromImage(pbRysownica.Image);

            //ustalenie pióra
            Pióro = new Pen(Color.Black, 1f);
            Pióro.DashStyle = DashStyle.Solid;

            //stworzenie wzierników
            pbWziernikKoloruWypełnienia.BorderStyle = BorderStyle.Fixed3D;
            pbWziernikKoloruWypełnienia.BackColor = pbRysownica.BackColor;

            pbWziernikLinii.Image = new Bitmap(pbWziernikLinii.Width, pbWziernikLinii.Height);
            PowierzchniaGraficznaWziernikaLinii = Graphics.FromImage(pbWziernikLinii.Image);

            //wykreślenie domyślnego wzorca linii
            WykreślenieWziernikaLinii();
        }

        private void kolorLiniiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog PaletaKolorów = new ColorDialog();
            PaletaKolorów.Color = Pióro.Color;

            if (PaletaKolorów.ShowDialog() == DialogResult.OK)
            {
                Pióro.Color = PaletaKolorów.Color;
            }

            //uaktualnienie wziernika koloru
            WykreślenieWziernikaLinii();
            PaletaKolorów.Dispose();
        }

        private void kropkowanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pióro.DashStyle = DashStyle.Dot;
            WykreślenieWziernikaLinii();
        }

        private void btnDodajNowąBryłę_Click(object sender, EventArgs e)
        {
            //pobranie atrybutów ustawionych dla wybranej bryły
            int WysokośćBryły = trbWysokośćBryły.Value;
            int PromieńBryły = trbPromieńBryły.Value;
            int StopieńWielokąta = (int)nudStopieńWielokąta.Value;
            int XsP = PunktLokalizacjiBryły.X;
            int YsP = PunktLokalizacjiBryły.Y;
            float KątPochyleniaBryły = trbKątPochyleniaBryły.Value;

            using(SolidBrush Pędzel = new SolidBrush(pbRysownica.BackColor))
            {
                Rysownica.FillEllipse(Pędzel, PunktLokalizacjiBryły.X - 3, PunktLokalizacjiBryły.Y - 3, 6, 6);
            }

            //rozpoznanie wybranej bryły
            switch (cmbListaBrył.SelectedItem)
            {
                case "Walec":
                    Walec walec = new Walec(PromieńBryły, WysokośćBryły, StopieńWielokąta, XsP, YsP,
                        Pióro.Color, Pióro.DashStyle, (int)Pióro.Width);

                    walec.Wykreśl(Rysownica);

                    //dodanie nowego egzemplarza
                    LBG.Add(walec);
                    break;
                case "Stożek":
                    Stożek EgzemplarzStożka = new Stożek(PromieńBryły, WysokośćBryły, StopieńWielokąta, XsP, YsP, KątPochyleniaBryły,
                        Pióro.Color, Pióro.DashStyle, (int)Pióro.Width);
                    EgzemplarzStożka.Wykreśl(Rysownica);
                    LBG.Add(EgzemplarzStożka);
                    break;
                case "Stożek pochylony":
                    StożekPochylony EgzemplarzStożkaPochylonego = new StożekPochylony(PromieńBryły, WysokośćBryły, StopieńWielokąta, XsP, YsP, KątPochyleniaBryły,
                        Pióro.Color, Pióro.DashStyle, (int)Pióro.Width);
                    EgzemplarzStożkaPochylonego.Wykreśl(Rysownica);
                    LBG.Add(EgzemplarzStożkaPochylonego);
                    break;
                case "Graniastosłup":
                    Graniastosłup EgzemplarzGraniastosłupa = new Graniastosłup(PromieńBryły, WysokośćBryły, StopieńWielokąta, XsP, YsP, Pióro.Color, Pióro.DashStyle, (int)Pióro.Width);
                    EgzemplarzGraniastosłupa.Wykreśl(Rysownica);
                    LBG.Add(EgzemplarzGraniastosłupa);
                    break;
                case "Ostrosłup":
                    Ostrosłup kdOstrosłup = new Ostrosłup(PromieńBryły, WysokośćBryły, StopieńWielokąta, XsP,
                        YsP, Pióro.Color, Pióro.DashStyle, (int)Pióro.Width);
                    kdOstrosłup.Wykreśl(Rysownica);
                    LBG.Add(kdOstrosłup);
                    break;
                case "Kula":
                    Kula kdKula = new Kula(PromieńBryły, new Point(XsP, YsP), Pióro.Color, Pióro.DashStyle, (int)Pióro.Width);
                    kdKula.Wykreśl(Rysownica);
                    LBG.Add(kdKula);
                    break;
                default:
                    MessageBox.Show("Nad tą bryłą jeszcze pracuję! Wybierz inną!");
                    break;
            }

            pbRysownica.Refresh();
        }

        private void ZegarObrotu_Tick(object sender, EventArgs e)
        {
            const float KątObrotu = 5F;
            
            //obracamy wszystkie bryły o podany kąt
            for (int i=0; i<LBG.Count; i++)
            {
                LBG[i].ObróćWykreśl(Rysownica, pbRysownica, KątObrotu);
            }
            pbRysownica.Refresh();
        }

        private void cmbListaBrył_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Wybierz bryłę geometryczną, atrybuty geometryczne i graficzne, następnie wskaż lokalizację, klikajac lewym" +
                "przyciskiem myszy na rysownicę. Potwierdź klikając przycisk 'Dodaj nową bryłę'");

            trbWysokośćBryły.Enabled = false;
            trbPromieńBryły.Enabled = false;
            nudStopieńWielokąta.Enabled = false;
            trbKątPochyleniaBryły.Enabled = false;

            if ((cmbListaBrył.SelectedItem == "Walec") || (cmbListaBrył.SelectedItem == "Stożek")
                ||(cmbListaBrył.SelectedItem=="Stożek pochylony")||(cmbListaBrył.SelectedItem=="Graniastosłup")
                ||(cmbListaBrył.SelectedItem=="Ostrosłup"))
            {
                trbWysokośćBryły.Enabled = true;
                trbPromieńBryły.Enabled = true;
                nudStopieńWielokąta.Enabled = true;
                if (cmbListaBrył.SelectedItem == "Stożek pochylony")
                {
                    trbKątPochyleniaBryły.Enabled = true;
                }
            }
            else
            {
                if (cmbListaBrył.SelectedItem == "Kula")
                {
                    trbPromieńBryły.Enabled = true;
                }
                else; 
            }
        }

        private void pbRysownica_MouseClick(object sender, MouseEventArgs e)
        {
            //zaznaczony punkt wykreślamy o możliwie małych rozmiarach
            using (SolidBrush Pędzel = new SolidBrush(Color.Red))
            {
                if (PunktLokalizacjiBryły.X != -1)
                {
                    //wymazanie punktu
                    Pędzel.Color = pbRysownica.BackColor;
                    Rysownica.FillEllipse(Pędzel, PunktLokalizacjiBryły.X - 3, PunktLokalizacjiBryły.Y - 3, 6, 6);
                    Pędzel.Color = Color.Red;
                }

                PunktLokalizacjiBryły = e.Location;
                Rysownica.FillEllipse(Pędzel, PunktLokalizacjiBryły.X - 3, PunktLokalizacjiBryły.Y - 3, 6, 6);
            }


            btnDodajNowąBryłę.Enabled = true;
            pbRysownica.Refresh();
        }

        private void kolorWypełnieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void kreskowanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pióro.DashStyle = DashStyle.Dash;
            WykreślenieWziernikaLinii();
        }

        private void BryłyRegularne_FormClosed(object sender, FormClosedEventArgs e)
        {
            kdFormularz.Visible = true;
        }

        private void ciagłaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pióro.DashStyle = DashStyle.Solid;
            WykreślenieWziernikaLinii();
        }

        private void kropkowanokreskowanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pióro.DashStyle = DashStyle.DashDot;
            WykreślenieWziernikaLinii();
        }

        private void pbRysownica_Click(object sender, EventArgs e)
        {

        }

        private void kdTsmi1_Click(object sender, EventArgs e)
        {
            Pióro.Width = 1;
            WykreślenieWziernikaLinii();
        }

        private void kdTsmi2_Click(object sender, EventArgs e)
        {
            Pióro.Width = 2;
            WykreślenieWziernikaLinii();
        }

        private void kdTsmi3_Click(object sender, EventArgs e)
        {
            Pióro.Width = 3;
            WykreślenieWziernikaLinii();
        }

        private void kdTsmi4_Click(object sender, EventArgs e)
        {
            Pióro.Width = 4;
            WykreślenieWziernikaLinii();
        }

        private void kdTsmi5_Click(object sender, EventArgs e)
        {
            Pióro.Width = 5;
            WykreślenieWziernikaLinii();
        }

        private void kdTsmi6_Click(object sender, EventArgs e)
        {
            Pióro.Width = 6;
            WykreślenieWziernikaLinii();
        }

        private void kdTsmi7_Click(object sender, EventArgs e)
        {
            Pióro.Width = 7;
            WykreślenieWziernikaLinii();
        }

        private void kdTsmi8_Click(object sender, EventArgs e)
        {
            Pióro.Width = 8;
            WykreślenieWziernikaLinii();
        }

        private void kdTsmi9_Click(object sender, EventArgs e)
        {
            Pióro.Width = 9;
            WykreślenieWziernikaLinii();
        }

        private void kdTsmi10_Click(object sender, EventArgs e)
        {
            Pióro.Width = 10;
            WykreślenieWziernikaLinii();
        }

        private void nudStopieńWielokąta_ValueChanged(object sender, EventArgs e)
        {
            if(nudStopieńWielokąta.Value==0)
            {
                MessageBox.Show(this, "Liczba stopni wielokąta nie może być mniejsza od 0", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nudStopieńWielokąta.Value = 1;
            }
        }

        private void WykreślenieWziernikaLinii()
        {
            const int Odstęp = 5;

            //wyczyszczenie powierzchni graficznej wziernika linii
            PowierzchniaGraficznaWziernikaLinii.Clear(pbWziernikLinii.BackColor);

            //wykreślenie linii wzorcowej
            PowierzchniaGraficznaWziernikaLinii.DrawLine(Pióro, Odstęp, pbWziernikLinii.Height / 2, 
                pbWziernikLinii.Width-2*Odstęp, pbWziernikLinii.Height/2);

            pbWziernikLinii.Refresh();
        }
    }
}
