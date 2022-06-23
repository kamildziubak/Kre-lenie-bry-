using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BryłyGeometryczne.KlasyBryłGeometrycznych;
using System.Drawing.Drawing2D;
using static BryłyGeometryczne.KlasyBryłGeometrycznych.BryłaAbstrakcyjna;

namespace OOP_Projekt3_Dziubak59363
{
    public partial class kdBryłyZłożone : Form
    {
        //deklaracje pól
        Pen kdPióro;
        Graphics kdRysownica;
        List<BryłaAbstrakcyjna> kdLBG;
        int kdIndeksLBG;
        //deklaracje pomocnicze
        bool kdBlokadaZmianyParametrówBrył; //zapobiega edycji brył w razie przełączania ich przez program, a nie użytkownika
        //egzemplarz formularza startowego, służący do powrotu do ekranu startowego
        kdFormPowitalny kdFormularz;
        //kąt obrotu w jednym cyklu zegara obrotu brył
        int kdKątObrotu = 20;
        public kdBryłyZłożone(kdFormPowitalny kdFormularz)
        {
            this.kdFormularz = kdFormularz;
            InitializeComponent();

            //inicjalizacja pól formularza
            kdPióro = new Pen(Color.Black, 1f);

            panel_59363Dziubak.Image = new Bitmap(panel_59363Dziubak.Width, panel_59363Dziubak.Height);
            kdRysownica = Graphics.FromImage(panel_59363Dziubak.Image);

            kdLBG = new List<BryłaAbstrakcyjna>();

            kdCmbStylLinii.SelectedIndex = 0;
        }

        private void BryłyZłożone_FormClosed(object sender, FormClosedEventArgs e)
        {
            kdFormularz.Visible = true;
        }

        private void kdCmbRodzajBryły_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (kdCmbRodzajBryły.SelectedItem)
            {
                case "Walec pochylony":
                    kdOdblokujKontrolkiDlaTypuBryły(TypyBrył.BG_WalecPochylony);
                    break;
                case "Stożki złączone (wspólna podstawa)":
                    kdOdblokujKontrolkiDlaTypuBryły(TypyBrył.BG_StożkiZłączone);
                    break;
                case "Stożek ścięty":
                    kdOdblokujKontrolkiDlaTypuBryły(TypyBrył.BG_StożekŚcięty);
                    break;
            }
        }

        private void kdTrbPromieńPodstawy_Scroll(object sender, EventArgs e)
        {
            kdTxtPromieńPodstawy.Text = string.Format("{0}", kdTrbPromieńPodstawy.Value);

            if(kdGrpKreatorBrył.Text == "Edytor wyświetlanej bryły")
            {
                kdEdytujBryłę();
            }
        }

        private void kdEdytujBryłę()
        {
            if (kdBlokadaZmianyParametrówBrył)
                return;

            BryłaAbstrakcyjna kdBryła = kdLBG[kdIndeksLBG];

            switch (kdBryła.RodzajBryły)
            {
                case TypyBrył.BG_WalecPochylony:
                    kdWalecPochylony kdWalecPochylony = (kdWalecPochylony)kdBryła;
                    kdWalecPochylony.Wymaż(kdRysownica, panel_59363Dziubak);
                    kdWalecPochylony = new kdWalecPochylony(kdTrbPromieńPodstawy.Value, kdTrbWysokość.Value,
                        (int)kdNudStopieńWielokąta.Value, kdWalecPochylony.XsP, kdWalecPochylony.YsP,
                        180 - kdTrbNachylenie.Value, kdBtnKolorLinii.BackColor, kdPióro.DashStyle,
                        (int)kdNudGrubośćLinii.Value);
                    kdWalecPochylony.Wykreśl(kdRysownica);
                    kdWalecPochylony.KierunekObrotu = kdRbLewo.Checked;
                    kdLBG[kdIndeksLBG] = kdWalecPochylony;
                    break;
                case TypyBrył.BG_StożkiZłączone:
                    kdPodwójnyStożek kdPodwójnyStożek = (kdPodwójnyStożek)kdBryła;
                    kdPodwójnyStożek.Wymaż(kdRysownica, panel_59363Dziubak);
                    kdPodwójnyStożek = new kdPodwójnyStożek(kdTrbPromieńPodstawy.Value, kdTrbWysokość.Value,
                        (int)kdNudStopieńWielokąta.Value, kdPodwójnyStożek.XsP, kdPodwójnyStożek.YsP,
                        90, kdBtnKolorLinii.BackColor, kdPióro.DashStyle,
                        (int)kdNudGrubośćLinii.Value);
                    kdPodwójnyStożek.Wykreśl(kdRysownica);
                    kdPodwójnyStożek.KierunekObrotu = kdRbLewo.Checked;
                    kdLBG[kdIndeksLBG] = kdPodwójnyStożek;
                    break;
                case TypyBrył.BG_StożekŚcięty:
                    kdStożekŚcięty kdStożekŚcięty = (kdStożekŚcięty)kdBryła;
                    kdStożekŚcięty.Wymaż(kdRysownica, panel_59363Dziubak);
                    kdStożekŚcięty = new kdStożekŚcięty(kdTrbPromieńPodstawy.Value, kdTrbWysokość.Value,
                        (int)kdNudStopieńWielokąta.Value, kdStożekŚcięty.XsP, kdStożekŚcięty.YsP,
                        kdBtnKolorLinii.BackColor, kdPióro.DashStyle, (int)kdNudGrubośćLinii.Value);
                    kdStożekŚcięty.Wykreśl(kdRysownica);
                    kdStożekŚcięty.KierunekObrotu = kdRbLewo.Checked;
                    kdLBG[kdIndeksLBG] = kdStożekŚcięty;
                    break;
                default:
                    break;
            }
            panel_59363Dziubak.Refresh();
        }

        private void kdTrbWysokość_Scroll(object sender, EventArgs e)
        {
            kdTxtWysokość.Text = string.Format("{0}", kdTrbWysokość.Value);
            if (kdGrpKreatorBrył.Text == "Edytor wyświetlanej bryły")
            {
                kdEdytujBryłę();
            }
        }

        private void kdTrbNachylenie_Scroll(object sender, EventArgs e)
        {
            kdTxtNachylenie.Text = string.Format("{0}", kdTrbNachylenie.Value);
            if (kdGrpKreatorBrył.Text == "Edytor wyświetlanej bryły")
            {
                kdEdytujBryłę();
            }
        }

        private void kdBtnKolorLinii_Click(object sender, EventArgs e)
        {
            ColorDialog kdKolor = new ColorDialog();
            kdKolor.Color = kdPióro.Color;
            if (kdKolor.ShowDialog() == DialogResult.OK)
            {
                kdPióro.Color = kdKolor.Color;
                kdBtnKolorLinii.BackColor = kdKolor.Color;
            }
            if (kdGrpKreatorBrył.Text == "Edytor wyświetlanej bryły")
            {
                kdEdytujBryłę();
            }
        }

        private void panel_59363Dziubak_Click(object sender, EventArgs e)
        {

        }

        private void panel_59363Dziubak_MouseDown(object sender, MouseEventArgs e)
        {
            if(!kdBtnStop.Enabled)
            {
                //pobranie danych
                int kdR = int.Parse(kdTxtPromieńPodstawy.Text);
                int kdH = int.Parse(kdTxtWysokość.Text);
                int kdStopieńWielokąta = (int)(kdNudStopieńWielokąta.Value);
                int kdNachylenie = 180 - int.Parse(kdTxtNachylenie.Text); //ma to na celu sprawienie, by kierunek pochylenia bryły był zgodny z kierunkiem kontrolki trackbar
                int kdGrubośćLinii = (int)(kdNudGrubośćLinii.Value);
                bool kdWykreślonoBryłę = false; //jeśli w wyniku instrukcji switch-case nie rozpoznano bryły, nie są aktywowane kontrolki, które nie powinny być aktywne, jeśli nie wykreślono bryły

                BryłaAbstrakcyjna kdBryła;
                switch (kdCmbRodzajBryły.SelectedItem)
                {
                    case "Walec pochylony":
                        kdBryła = new kdWalecPochylony(kdR, kdH, kdStopieńWielokąta, e.X, e.Y,
                            kdNachylenie, kdPióro.Color, kdPióro.DashStyle, kdGrubośćLinii);
                        kdBryła.Wykreśl(kdRysownica);
                        kdWykreślonoBryłę = true;
                        if (kdRbLewo.Checked)
                            kdBryła.KierunekObrotu = true;
                        kdLBG.Add(kdBryła);
                        break;
                    case "Stożki złączone":
                        kdBryła = new kdPodwójnyStożek(kdR, kdH, kdStopieńWielokąta, e.X, e.Y, kdNachylenie,
                            kdPióro.Color, kdPióro.DashStyle, kdGrubośćLinii);
                        kdBryła.Wykreśl(kdRysownica);
                        kdWykreślonoBryłę = true;
                        if (kdRbLewo.Checked)
                            kdBryła.KierunekObrotu = true;
                        kdLBG.Add(kdBryła);
                        break;
                    case "Stożek ścięty":
                        kdBryła = new kdStożekŚcięty(kdR, kdH, kdStopieńWielokąta, e.X, e.Y,
                            kdPióro.Color, kdPióro.DashStyle, kdGrubośćLinii);
                        kdBryła.Wykreśl(kdRysownica);
                        kdWykreślonoBryłę = true;
                        if (kdRbLewo.Checked)
                            kdBryła.KierunekObrotu = true;
                        kdLBG.Add(kdBryła);
                        break;
                    default:
                        MessageBox.Show(this, "Nie wybrano żadnej bryły lub wybrana bryła jest niezaimplementowana!",
                            "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                }

                panel_59363Dziubak.Refresh();

                if (kdWykreślonoBryłę)
                {
                    kdTrbPrędkośćObrotuBrył.Enabled = true;
                    kdBtnCofnij.Enabled = true;
                    kdGrpPrzeglądBrył.Enabled = true;
                    kdGrpLosowanieParametrów.Enabled = true;
                }
            }
        }

        private void kdTrbPrędkośćObrotuBrył_Scroll(object sender, EventArgs e)
        {
            kdKątObrotu = kdTrbPrędkośćObrotuBrył.Value;
        }

        private void kdTimer_Tick(object sender, EventArgs e)
        {
            if (kdLBG.Count == 0)
                return;

            if(kdBtnStart.Enabled)
                foreach (BryłaAbstrakcyjna kdBryła in kdLBG)
                {
                    kdBryła.ObróćWykreśl(kdRysownica, panel_59363Dziubak, kdKątObrotu);
                }
            else if (kdBtnStop.Enabled)
            {
                kdLBG[kdIndeksLBG].ObróćWykreśl(kdRysownica, panel_59363Dziubak, kdKątObrotu);
            }
            
             panel_59363Dziubak.Refresh();
        }

        private void kdBtnCofnij_Click(object sender, EventArgs e)
        {
            kdLBG[kdLBG.Count - 1].Wymaż(kdRysownica, panel_59363Dziubak);
            kdLBG.Remove(kdLBG[kdLBG.Count-1]);
            panel_59363Dziubak.Refresh();
            
            if(kdLBG.Count==0) //deaktywacja odpowiednich kontrolek, gdy usunięto wszystkie bryły
            {
                kdBtnCofnij.Enabled = false;
                kdGrpPrzeglądBrył.Enabled = false;
                kdTrbPrędkośćObrotuBrył.Enabled = false;
                kdGrpLosowanieParametrów.Enabled = false;
                
            }
        }

        private void kdCmbStylLinii_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (kdCmbStylLinii.SelectedItem)
            {
                case "Ciągła":
                    kdPióro.DashStyle = DashStyle.Solid;
                    break;
                case "Kropkowana":
                    kdPióro.DashStyle = DashStyle.Dot;
                    break;
                case "Kreskowana":
                    kdPióro.DashStyle = DashStyle.Dash;
                    break;
                case "Kropkowo-kreskowana":
                    kdPióro.DashStyle = DashStyle.DashDot;
                    break;
            }
            if (kdGrpKreatorBrył.Text == "Edytor wyświetlanej bryły")
            {
                kdEdytujBryłę();
            }
        }

        private void kdBtnStart_Click(object sender, EventArgs e)
        {
            //deaktywacja tych kontrolek, które są nieaktywne niezależnie od typu pokazu
            kdBtnStart.Enabled = false;
            kdBtnStop.Enabled = true;
            kdBtnCofnij.Enabled = false;
            kdRbAutomatyczny.Enabled = false;
            kdRbRęczny.Enabled = false;
            kdGrpLosowanieParametrów.Enabled = false;

            //obsługa poszczególnych trybów pokazów
            if (kdRbAutomatyczny.Checked)
            {
                kdBtnPokazWolniej.Enabled = true;
                kdBtnPokazSzybciej.Enabled = true;
                kdGrpKreatorBrył.Enabled = false;
                kdTxtPrędkośćPokazu.Text = "1";
                kdTimerPokazuSlajdów.Enabled = true;
                kdTimerPokazuSlajdów.Interval = 1000;
                if (kdLBG.Count > 1)
                    kdIndeksLBG = 1;
                else
                    kdIndeksLBG = 0;
            }
            if (kdRbRęczny.Checked)
            {
                //ustawienie kontrolek
                kdBtnNastępnaBryła.Enabled = true;
                kdBtnPoprzedniaBryła.Enabled = true;
                kdTxtNrBryły.Text = "1";
                kdIndeksLBG = 0;
                
                //przygotowanie groupboxa kreatora brył do edycji brył
                kdCmbRodzajBryły.Enabled = false;
                kdGrpKreatorBrył.Text = "Edytor wyświetlanej bryły";

                MessageBox.Show(this, "Wybrano pokaz manualny. Aby przeglądać bryły, użyj przycisków " +
                    "znajdujących się pod napisem 'Numer bryły'. Jeśli chcesz dokonać edycji aktualnie " +
                    "wyświetlanej bryły, możesz ustawić jej parametry używając kontrolek po lewej stronie.",
                    "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);

                kdOdblokujKontrolkiDlaTypuBryły(kdLBG[0].RodzajBryły);
                kdUstawParametryBryłyNaKontrolkach(kdLBG[0]);
                kdBtnUsuń.Enabled = true;
            }

            //wymazanie wszystkich brył i wykreślenie pierwszej bryły
            foreach (BryłaAbstrakcyjna kdBryła in kdLBG)
                kdBryła.Wymaż(kdRysownica, panel_59363Dziubak);

            kdLBG[0].Wykreśl(kdRysownica);
            panel_59363Dziubak.Refresh();
        }

        private void kdOdblokujKontrolkiDlaTypuBryły(TypyBrył kdTypyBrył)
        {

            /* dezaktywowanie wszystkich kontrolek; w dalszych instrukcjach aktywowane są tylko te
              które będą potrzebne do wykreślenia danej bryły */
            kdTrbPromieńPodstawy.Enabled = false;
            kdTrbWysokość.Enabled = false;
            kdTrbNachylenie.Enabled = false;
            kdNudGrubośćLinii.Enabled = false;
            kdNudStopieńWielokąta.Enabled = false;
            kdBtnKolorLinii.Enabled = false;
            kdRbLewo.Enabled = false;
            kdRbPrawo.Enabled = false;
            kdCmbStylLinii.Enabled = false;

            //aktywacja odpowiednich kontrolek, potrzebnych do realizacji kreślenia wybranej bryły
            switch (kdTypyBrył)
            {
                case TypyBrył.BG_WalecPochylony:
                    kdTrbPromieńPodstawy.Enabled = true;
                    kdTrbWysokość.Enabled = true;
                    kdTrbNachylenie.Enabled = true;
                    kdNudGrubośćLinii.Enabled = true;
                    kdNudStopieńWielokąta.Enabled = true;
                    kdBtnKolorLinii.Enabled = true;
                    kdRbLewo.Enabled = true;
                    kdRbPrawo.Enabled = true;
                    kdCmbStylLinii.Enabled = true;
                    break;
                case TypyBrył.BG_StożkiZłączone:
                    kdTrbPromieńPodstawy.Enabled = true;
                    kdTrbWysokość.Enabled = true;
                    kdNudGrubośćLinii.Enabled = true;
                    kdNudStopieńWielokąta.Enabled = true;
                    kdBtnKolorLinii.Enabled = true;
                    kdRbLewo.Enabled = true;
                    kdRbPrawo.Enabled = true;
                    kdCmbStylLinii.Enabled = true;
                    break;
                case TypyBrył.BG_StożekŚcięty:
                    kdTrbPromieńPodstawy.Enabled = true;
                    kdTrbWysokość.Enabled = true;
                    kdNudGrubośćLinii.Enabled = true;
                    kdNudStopieńWielokąta.Enabled = true;
                    kdBtnKolorLinii.Enabled = true;
                    kdRbLewo.Enabled = true;
                    kdRbPrawo.Enabled = true;
                    kdCmbStylLinii.Enabled = true;
                    break;
                default:
                    MessageBox.Show(this, "Wybrano niepoprawną (niezaimplementowaną) bryłę!", "Błąd",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

            }
        }

        private void kdBtnStop_Click(object sender, EventArgs e)
        {
            kdRbAutomatyczny.Enabled = true;
            kdRbRęczny.Enabled = true;
            kdGrpKreatorBrył.Enabled = true;
            kdBtnStart.Enabled = true;
            kdBtnStop.Enabled = false;
            kdBtnCofnij.Enabled = true;
            kdTrbPrędkośćObrotuBrył.Enabled = true;
            kdBtnPokazWolniej.Enabled = false;
            kdBtnPokazSzybciej.Enabled = false;
            kdGrpLosowanieParametrów.Enabled = true;
            kdTimerPokazuSlajdów.Interval = 1000/kdTrbPrędkośćObrotuBrył.Value;
            kdTimerPokazuSlajdów.Enabled = false;
            kdIndeksLBG = 0;
            kdGrpKreatorBrył.Text = "Tworzenie brył";
            kdBtnUsuń.Enabled = false;
            kdBtnPoprzedniaBryła.Enabled = false;
            kdBtnNastępnaBryła.Enabled = false;
            kdCmbRodzajBryły.Enabled = true;

            foreach(BryłaAbstrakcyjna kdBryła in kdLBG)
            {
                if (!kdBryła.Widoczny)
                    kdBryła.Wykreśl(kdRysownica);
            }
        }

        private void kdBtnPokazWolniej_Click(object sender, EventArgs e)
        {
            float kdObecnaPrędkosć = float.Parse(kdTxtPrędkośćPokazu.Text);

            //dalsze zmniejszanie wartości doprowadziłoby do przekroczenia dozwolonych wartości zmiennej float
            if (kdObecnaPrędkosć < 0.000004656f)
            {
                MessageBox.Show(this, "Osiągnięto prędkość minimalną!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            float kdNowaPrędkość = kdObecnaPrędkosć / 2;
            kdTimerPokazuSlajdów.Interval = (int)(1000 / kdNowaPrędkość);
            kdTxtPrędkośćPokazu.Text = string.Format("{0}", kdNowaPrędkość);
        }

        private void kdBtnPokazSzybciej_Click(object sender, EventArgs e)
        {
            float kdObecnaPrędkosć = float.Parse(kdTxtPrędkośćPokazu.Text);
            /* W toku testów okazało się, że prędkość powyżej 8x powoduje, że bryły zmieniają się szybciej, niż
             * program jest w stanie je wykreślać (lub bezwładność ludzkiego oka powoduje takie wrażenie), natomiast
             * przy dużo większych wartościach dzielenie 1000/nowa prędkość dawało wynik 0 ze względu na brak miejsc
             * po przecinku, stąd ograniczenie maksymalnej prędkości do 16-krotnej (16 brył na s).
             */
            if(kdObecnaPrędkosć>8)
            {
                MessageBox.Show(this, "Osiągnięto prędkość maksymalną!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            float kdNowaPrędkość = kdObecnaPrędkosć * 2;
            kdTimerPokazuSlajdów.Interval = (int)(1000 / kdNowaPrędkość);
            kdTxtPrędkośćPokazu.Text = string.Format("{0}", kdNowaPrędkość);
        }

        private void kdBtnNastępnaBryła_Click(object sender, EventArgs e)
        {
            kdLBG[kdIndeksLBG].Wymaż(kdRysownica, panel_59363Dziubak);

            if (kdIndeksLBG < kdLBG.Count - 1)
            {
                kdIndeksLBG++;
            }
            else
            {
                kdIndeksLBG = 0;
            }

            kdLBG[kdIndeksLBG].Wykreśl(kdRysownica);

            kdTxtNrBryły.Text = string.Format("{0}", kdIndeksLBG+1);

            kdOdblokujKontrolkiDlaTypuBryły(kdLBG[kdIndeksLBG].RodzajBryły);
            kdUstawParametryBryłyNaKontrolkach(kdLBG[kdIndeksLBG]);
        }

        private void kdBtnPoprzedniaBryła_Click(object sender, EventArgs e)
        {
            kdLBG[kdIndeksLBG].Wymaż(kdRysownica, panel_59363Dziubak);

            if (kdIndeksLBG >0)
            {
                kdIndeksLBG--;
            }
            else
            {
                kdIndeksLBG = kdLBG.Count - 1;
            }

            kdLBG[kdIndeksLBG].Wykreśl(kdRysownica);

            kdTxtNrBryły.Text = string.Format("{0}", kdIndeksLBG+1);

            kdOdblokujKontrolkiDlaTypuBryły(kdLBG[kdIndeksLBG].RodzajBryły);
            kdUstawParametryBryłyNaKontrolkach(kdLBG[kdIndeksLBG]);
        }

        private void kdUstawParametryBryłyNaKontrolkach(BryłaAbstrakcyjna kdBryła)
        {
            kdBlokadaZmianyParametrówBrył = true;
            kdTrbWysokość.Value = kdBryła.WysokośćBryły;
            kdTxtWysokość.Text = string.Format("{0}", kdBryła.WysokośćBryły);
            kdTrbNachylenie.Value = (int)(180 - kdBryła.KątPochylenia);
            kdTxtNachylenie.Text = string.Format("{0}", 180-kdBryła.KątPochylenia);
            kdBtnKolorLinii.BackColor = kdBryła.KolorLinii;
            kdNudGrubośćLinii.Value = kdBryła.GrubośćLinii;
            kdRbLewo.Checked = kdBryła.KierunekObrotu;
            kdRbPrawo.Checked = !kdBryła.KierunekObrotu;

            switch (kdBryła.StylLinii)
            {
                case DashStyle.Solid:
                    kdCmbStylLinii.SelectedItem = "Ciągła";
                    break;
                case DashStyle.Dot:
                    kdCmbStylLinii.SelectedItem = "Kropkowana";
                    break;
                case DashStyle.Dash:
                    kdCmbStylLinii.SelectedItem = "Kreskowana";
                    break;
                case DashStyle.DashDot:
                    kdCmbStylLinii.SelectedItem = "Kropkowo-kreskowana";
                    break;
            }

            switch (kdBryła.RodzajBryły)
            {
                case TypyBrył.BG_WalecPochylony:
                    kdWalecPochylony kdWalecPochylony = (kdWalecPochylony)kdBryła;
                    kdTrbPromieńPodstawy.Value = kdWalecPochylony.Promień;
                    kdTxtPromieńPodstawy.Text = string.Format("{0}", kdWalecPochylony.Promień);
                    kdNudStopieńWielokąta.Value = kdWalecPochylony.StopieńWielokątaPodstawy;
                    break;
                case TypyBrył.BG_StożkiZłączone:
                    kdPodwójnyStożek kdPodwójnyStożek = (kdPodwójnyStożek)kdBryła;
                    kdTrbPromieńPodstawy.Value = kdPodwójnyStożek.Promień;
                    kdTxtPromieńPodstawy.Text = string.Format("{0}", kdPodwójnyStożek.Promień);
                    kdNudStopieńWielokąta.Value = kdPodwójnyStożek.StopieńWielokątaPodstawy;
                    break;
                case TypyBrył.BG_StożekŚcięty:
                    kdStożekŚcięty kdStożekŚcięty = (kdStożekŚcięty)kdBryła;
                    kdTrbPromieńPodstawy.Value = kdStożekŚcięty.Promień;
                    kdTxtPromieńPodstawy.Text = string.Format("{0}", kdStożekŚcięty.Promień);
                    kdNudStopieńWielokąta.Value = kdStożekŚcięty.StopieńWielokątaPodstawy;
                    break;
                default:
                    break;
            }

            kdBlokadaZmianyParametrówBrył = false;
        }

        private void kdNudStopieńWielokąta_ValueChanged(object sender, EventArgs e)
        {
            if (kdGrpKreatorBrył.Text == "Edytor wyświetlanej bryły")
            {
                kdEdytujBryłę();
            }
        }

        private void kdNudGrubośćLinii_ValueChanged(object sender, EventArgs e)
        {
            if (kdGrpKreatorBrył.Text == "Edytor wyświetlanej bryły")
            {
                kdEdytujBryłę();
            }
        }

        private void kdRbLewo_CheckedChanged(object sender, EventArgs e)
        {
            if (kdGrpKreatorBrył.Text == "Edytor wyświetlanej bryły")
            {
                kdEdytujBryłę();
            }
        }

        private void kdRbPrawo_CheckedChanged(object sender, EventArgs e)
        {
            if (kdGrpKreatorBrył.Text == "Edytor wyświetlanej bryły")
            {
                kdEdytujBryłę();
            }
        }

        private void kdBtnUsuń_Click(object sender, EventArgs e)
        {
            kdLBG[kdIndeksLBG].Wymaż(kdRysownica, panel_59363Dziubak);
            kdLBG.Remove(kdLBG[kdIndeksLBG]);
            
            if(kdLBG.Count==0)
            {
                MessageBox.Show(this, "Usunięto wszystkie bryły! Pokaz slajdów zostaje przerwany!", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                kdBtnStop.PerformClick();
                kdTrbPrędkośćObrotuBrył.Enabled = false;
                kdGrpLosowanieParametrów.Enabled = false;
                panel_59363Dziubak.Refresh();
                return;
            }
            else if (kdIndeksLBG != 0)
            {
                kdIndeksLBG--;
            }

            kdLBG[kdIndeksLBG].Wykreśl(kdRysownica);
            kdOdblokujKontrolkiDlaTypuBryły(kdLBG[kdIndeksLBG].RodzajBryły);
            kdUstawParametryBryłyNaKontrolkach(kdLBG[kdIndeksLBG]);
            kdTxtNrBryły.Text = string.Format("{0}", kdIndeksLBG + 1);
            panel_59363Dziubak.Refresh();
        }

        private void kdTimerPokazuSlajdów_Tick(object sender, EventArgs e)
        {
            if (kdRbAutomatyczny.Checked && kdBtnStop.Enabled)
            {
                if (kdIndeksLBG >= kdLBG.Count) //warunek ten zapobiega występowaniu błędów, gdy utworzono tylko jedną bryłę (ale może też zapobiec błędom, gdyby z innych powodów indeksLBG przekroczył wielkość listy)
                {
                    kdIndeksLBG = 0;
                }
                if (kdIndeksLBG == 0)
                    kdLBG[kdLBG.Count - 1].Wymaż(kdRysownica, panel_59363Dziubak);
                else
                    kdLBG[kdIndeksLBG - 1].Wymaż(kdRysownica, panel_59363Dziubak);

                kdLBG[kdIndeksLBG].Wykreśl(kdRysownica);

                if (kdIndeksLBG < kdLBG.Count - 1)
                    kdIndeksLBG++;
                else
                    kdIndeksLBG = 0;
            }
        }

        private void kdBtnLosujKolory_Click(object sender, EventArgs e)
        {
            Random kdRandom = new Random();

            foreach (BryłaAbstrakcyjna kdBryła in kdLBG)
            {
                kdBryła.Wymaż(kdRysownica, panel_59363Dziubak);
                Color kdKolor = Color.FromArgb(kdRandom.Next(255), kdRandom.Next(255), kdRandom.Next(255));
                kdBryła.KolorLinii = kdKolor;
                kdBryła.Wykreśl(kdRysownica);
            }
        }

        private void kdBtnLosujLokalizacje_Click(object sender, EventArgs e)
        {
            Random kdRandom = new Random();

            foreach (BryłaAbstrakcyjna kdBryła in kdLBG)
            {
                kdBryła.PrzesuńDoNowegoXY(kdRysownica, panel_59363Dziubak, kdRandom.Next(panel_59363Dziubak.Width),
                    kdRandom.Next(panel_59363Dziubak.Height));
                kdBryła.Wykreśl(kdRysownica);
            }
        }

        private void kdBtnLosujStylLinii_Click(object sender, EventArgs e)
        {
            Random kdRandom = new Random();

            foreach (BryłaAbstrakcyjna kdBryła in kdLBG)
            {
                kdBryła.Wymaż(kdRysownica, panel_59363Dziubak);
                DashStyle[] kdStyle = { DashStyle.Dot, DashStyle.Solid, DashStyle.DashDot, DashStyle.Dash };
                kdBryła.StylLinii = kdStyle[kdRandom.Next(kdStyle.Length - 1)];
                kdBryła.Wykreśl(kdRysownica);
            }
        }

        private void kdBtnLosujGrubośćLinii_Click(object sender, EventArgs e)
        {
            Random kdRandom = new Random();

            foreach (BryłaAbstrakcyjna kdBryła in kdLBG)
            {
                kdBryła.Wymaż(kdRysownica, panel_59363Dziubak);
                kdBryła.GrubośćLinii = kdRandom.Next(10);
                kdBryła.Wykreśl(kdRysownica);
            }
        }

        private void kdGrpLosowanieParametrów_Enter(object sender, EventArgs e)
        {

        }
    }
}