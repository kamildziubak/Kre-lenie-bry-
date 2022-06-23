using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BryłyGeometryczne
{
    public class KlasyBryłGeometrycznych
    {
        const float KątProsty = 90.0F;
        public abstract class BryłaAbstrakcyjna
        {
            public enum TypyBrył
            {
                BG_Walec, BG_Stożek, BG_Kula, BG_Ostrosłup, BG_Graniastosłup, BG_Sześcian,
                BG_StożekPochylony, BG_WalecPochylony, BG_StożkiZłączone, BG_StożekŚcięty
            };

            //atrybuty geometryczny
            public int XsP, YsP;
            public int WysokośćBryły;
            public float KątPochylenia;

            //arybuty graficzne
            public Color KolorLinii;
            public DashStyle StylLinii;
            public int GrubośćLinii;

            //zmienne dla przyszłych funkcjonalności
            public TypyBrył RodzajBryły;
            public bool KierunekObrotu; //false: w prawo, true: w lewo   
            public float PowierzchniaBryły;
            public float ObjętośćBryły;
            public bool Widoczny;    

            //konstruktor
            public BryłaAbstrakcyjna(Color KolorLinii, DashStyle StylLinii, int GrubośćLinii)
            {
                this.KolorLinii = KolorLinii;
                this.StylLinii = StylLinii;
                this.GrubośćLinii = GrubośćLinii;
            }

            public abstract void Wykreśl(Graphics Rysownica);
            public abstract void Wymaż(Graphics Rysownica, Control Kontrolka);
            public abstract void ObróćWykreśl(Graphics Rysownica, Control Kontrolka, float KątObrotu);
            public abstract void PrzesuńDoNowegoXY(Graphics Rysownica, Control Kontrolka, int X, int Y);
            public void UstalParametryGraficzne(Color KolorLinii, DashStyle StylLinii, int GrubośćLinii)
            {
                this.KolorLinii = KolorLinii;
                this.StylLinii = StylLinii;
                this.GrubośćLinii = GrubośćLinii;
            }
        }
        public class BryłaObrotowa : BryłaAbstrakcyjna
        {
            public int Promień;

            //konstruktor
            public BryłaObrotowa(int R, Color KolorLinii, DashStyle StylLinii, int GrubośćLinii):base(KolorLinii, StylLinii, GrubośćLinii)
            {
                Promień = R;
            }

            //nadpisanie wszystkich metod abstrakcyjnych
            public override void Wykreśl(Graphics Rysownica)
            {
                
            }

            public override void Wymaż(Graphics Rysownica, Control Kontrolka)
            {
                
            }

            public override void ObróćWykreśl(Graphics Rysownica, Control Kontrolka, float KątObrotu)
            {
                
            }

            public override void PrzesuńDoNowegoXY(Graphics Rysownica, Control Kontrolka, int X, int Y)
            {
                
            }
        }
        public class Walec : BryłaObrotowa
        {
            //deklaracje uzupełniające
            protected Point[] WielokątPodłogi; //podstawy walca
            protected Point[] WielokątSufitu; //sufit walca

            protected int XsS, YsS;
            //stopień wielokąta podstawy i sufitu
            public int StopieńWielokątaPodstawy;
            protected float OśDuża, OśMała;

            protected float KątMiędzyWierzchołkami;

            public float KątPołożenia { get;  set; }

            public Walec(int R, int WysokośćWalca, int StopieńWielokątaPodstawy, int XsP, int YsP, 
                Color KolorLinii, DashStyle StylLinii, int GrubośćLinii) : base(R, KolorLinii, StylLinii, GrubośćLinii)
            {
                RodzajBryły = TypyBrył.BG_Walec;
                Widoczny = false;
                KierunekObrotu = false;
                WysokośćBryły = WysokośćWalca;
                this.StopieńWielokątaPodstawy = StopieńWielokątaPodstawy;

                this.XsP = XsP;
                this.YsP = YsP;

                //wyznaczenie osi elipsy wykreślanej w podłodze i suficie walca
                OśDuża = 2 * Promień;
                OśMała = Promień / 2;
                    
                XsS = XsP;
                
                YsS = YsP - WysokośćWalca;

                KątMiędzyWierzchołkami = 360 / StopieńWielokątaPodstawy;
                float KątPołożenia = 0F;

                WielokątPodłogi = new Point[StopieńWielokątaPodstawy];
                WielokątSufitu = new Point[StopieńWielokątaPodstawy];

                for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                {
                    WielokątPodłogi[i] = new Point();
                    WielokątSufitu[i] = new Point();

                    //podłoga
                    WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2 * Math.Cos(Math.PI * (KątMiędzyWierzchołkami*i + KątPołożenia) / 180F));
                    WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2 * Math.Sin(Math.PI * (KątMiędzyWierzchołkami*i + KątPołożenia) / 180F));

                    //sufit
                    WielokątSufitu[i].X = (int)(XsS + OśDuża / 2 * Math.Cos(Math.PI * (KątMiędzyWierzchołkami*i + KątPołożenia) / 180F));
                    WielokątSufitu[i].Y = (int)(YsS + OśMała / 2 * Math.Sin(Math.PI * (KątMiędzyWierzchołkami*i + KątPołożenia) / 180F));
                } //od for

                //obliczanie powierzchni walca

                //obliczanie objętości walca
            } //od konstruktora

            //nadpisanie metod abstrakcyjnych
            public override void Wykreśl(Graphics Rysownica)
            {
                using (Pen Pióro = new Pen(KolorLinii, GrubośćLinii))
                {
                    Pióro.DashStyle = StylLinii;

                    //kreślenie podłogi walca
                    Rysownica.DrawEllipse(Pióro, XsP - OśDuża / 2, YsP - OśMała / 2, OśDuża, OśMała);

                    //kreślenie sufitu walca
                    Rysownica.DrawEllipse(Pióro, XsS - OśDuża / 2, YsS - OśMała / 2, OśDuża, OśMała);

                    //wykreślenie prążków na ścianie bocznej walca
                    using (Pen PióroPrążków = new Pen(KolorLinii, 1f))
                    {
                        PióroPrążków.DashStyle = DashStyle.Dot;

                        for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                            Rysownica.DrawLine(PióroPrążków, WielokątPodłogi[i], WielokątSufitu[i]);
                    }

                    //kreślenie lewej krawędzi
                    Rysownica.DrawLine(Pióro, XsP - OśDuża / 2, YsP, XsS - OśDuża / 2, YsS);

                    //kreślenie prawej krawędzi
                    Rysownica.DrawLine(Pióro, XsP + OśDuża / 2, YsP, XsS + OśDuża / 2, YsS);
                } // od using (zwolnienie pióra)
                Widoczny = true;
            } //od Wykreśl

            public override void Wymaż(Graphics Rysownica, Control Kontrolka)
            {
                if (Widoczny)
                {
                    using (Pen Pióro = new Pen(Kontrolka.BackColor, GrubośćLinii))
                    {
                        Pióro.DashStyle = StylLinii;

                        //kreślenie podłogi walca
                        Rysownica.DrawEllipse(Pióro, XsP - OśDuża / 2, YsP - OśMała / 2, OśDuża, OśMała);

                        //kreślenie sufitu walca
                        Rysownica.DrawEllipse(Pióro, XsS - OśDuża / 2, YsS - OśMała / 2, OśDuża, OśMała);

                        //wykreślenie prążków na ścianie bocznej walca
                        using (Pen PióroPrążków = new Pen(Kontrolka.BackColor, 1F))
                        {
                            PióroPrążków.DashStyle = DashStyle.Dot;

                            for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                                Rysownica.DrawLine(PióroPrążków, WielokątPodłogi[i], WielokątSufitu[i]);
                        }

                        //kreślenie lewej krawędzi
                        Rysownica.DrawLine(Pióro, XsP - OśDuża / 2, YsP, XsS - OśDuża / 2, YsS);

                        //kreślenie prawej krawędzi
                        Rysownica.DrawLine(Pióro, XsP + OśDuża / 2, YsP, XsS + OśDuża / 2, YsS);
                    } // od using (zwolnienie pióra)
                    Widoczny = false;
                }
            }
            public override void ObróćWykreśl(Graphics Rysownica, Control Kontrolka, float KątObrotu)
            {
                if (Widoczny)
                {
                    Wymaż(Rysownica, Kontrolka);
                    //wyznaczenie nowego położenia pierwszego wielokąta wpisanego w podłogę walca
                    if (KierunekObrotu)
                    {
                        KątPołożenia -= KątObrotu;
                    }
                    else
                    {
                        KątPołożenia += KątObrotu;
                    }

                    for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                    {
                        WielokątPodłogi[i] = new Point();
                        WielokątSufitu[i] = new Point();

                        //podłoga
                        WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2 * Math.Cos(Math.PI * (KątMiędzyWierzchołkami*i + KątPołożenia) / 180F));
                        WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2 * Math.Sin(Math.PI * (KątMiędzyWierzchołkami*i + KątPołożenia) / 180F));

                        //sufit
                        WielokątSufitu[i].X = (int)(XsS + OśDuża / 2 * Math.Cos(Math.PI * (KątMiędzyWierzchołkami*i + KątPołożenia) / 180F));
                        WielokątSufitu[i].Y = (int)(YsS + OśMała / 2 * Math.Sin(Math.PI * (KątMiędzyWierzchołkami*i + KątPołożenia) / 180F));
                    } //od for

                    Wykreśl(Rysownica);
                }
            }//od ObrócWykreśl
            public override void PrzesuńDoNowegoXY(Graphics Rysownica, Control Kontrolka, int X, int Y)
            {
                if (Widoczny)
                {
                    int dX, dY;
                    Wymaż(Rysownica, Kontrolka);

                    //wyznaczenie wektora przesunięcia
                    dX = XsP < X ? X - XsP : -(XsP - X);
                    dY = YsP < Y ? Y - YsP : -(YsP - Y);

                    //wyznaczamy nowe położenie dla środka podłogi i sufity
                    XsP = XsP + dX;
                    YsP = YsP + dY;
                    XsS = XsS + dX;
                    YsS = YsS + dY;

                    for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                    {
                        WielokątPodłogi[i] = new Point();
                        WielokątSufitu[i] = new Point();

                        //podłoga
                        WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2 * Math.Cos(Math.PI * (KątMiędzyWierzchołkami + KątPołożenia) / 180F));
                        WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2 * Math.Sin(Math.PI * (KątMiędzyWierzchołkami + KątPołożenia) / 180F));

                        //sufit
                        WielokątSufitu[i].X = (int)(XsS + OśDuża / 2 * Math.Cos(Math.PI * (KątMiędzyWierzchołkami + KątPołożenia) / 180F));
                        WielokątSufitu[i].Y = (int)(YsS + OśMała / 2 * Math.Sin(Math.PI * (KątMiędzyWierzchołkami + KątPołożenia) / 180F));
                    } //od for

                    Wykreśl(Rysownica);
                } //od PrzesuńDoNowegoXY
            } //od klasy Walec
        }
        public class Stożek : BryłaObrotowa
        {
            //opis stożka
            protected int XsS, YsS;
            public int StopieńWielokątaPodstawy;
            protected float OśDuża, OśMała;
            protected float KątMiędzyWierzchołkami;
            protected float KątPołożenia;
            protected Point[] WielokątPodłogi;

            //deklaracja konstruktora
            public Stożek(int R, int WysokośćStożka, int StopieńWielokątaPodstawy, int XsP, int YsP, float KątPochylenia,
                    Color KolorLinii, DashStyle StylLinii, int GrubośćLinii): base (R, KolorLinii, StylLinii, GrubośćLinii)
            {
                RodzajBryły = TypyBrył.BG_Stożek;
                Widoczny = false;
                KierunekObrotu = false;
                WysokośćBryły = WysokośćStożka;
                this.StopieńWielokątaPodstawy = StopieńWielokątaPodstawy;
                OśDuża = 2 * R;
                OśMała = R/2;
                this.XsP = XsP;
                this.YsP = YsP;
                //wyznaczenie współrzędnych wierzchołka
                XsS = XsP;
                YsS = YsP - WysokośćStożka;
                KątPołożenia = 0;
                KątMiędzyWierzchołkami = 360 / StopieńWielokątaPodstawy;
                WielokątPodłogi = new Point[StopieńWielokątaPodstawy];
                    
                for(int i=0; i<StopieńWielokątaPodstawy; i++)
                {
                    WielokątPodłogi[i] = new Point();
                    //wyznaczenie wartości współrzędnych i-tego wierzchołka wielokąta

                    WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2 * Math.Cos(Math.PI*(KątPołożenia + i * KątMiędzyWierzchołkami)/180));
                    WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2 * Math.Sin(Math.PI * (KątPołożenia + i * KątMiędzyWierzchołkami) / 180));
                }
                //obliczenie pola powierzchni stożka
            }//od konstruktora

            public override void Wykreśl(Graphics Rysownica)
            {
                using(Pen Pióro=new Pen(KolorLinii,GrubośćLinii))
                {
                    Pióro.DashStyle = StylLinii;
                    Rysownica.DrawEllipse(Pióro, XsP - OśDuża / 2, YsP - OśMała / 2, OśDuża, OśMała);

                    //wykreślenie prążków
                    using (Pen PióroPrążków = new Pen(Pióro.Color, Pióro.Width/3))
                    {
                        for(int i=0; i<StopieńWielokątaPodstawy; i++)
                        {
                            Rysownica.DrawLine(PióroPrążków, WielokątPodłogi[i], new Point(XsS, YsS));
                        }
                    } //od using PióroPrążków

                    //wykreślenie lewej krawędzi bocznej
                    Rysownica.DrawLine(Pióro, XsP - OśDuża/2, YsP, XsS, YsS);

                    //wykreślenie prawej krawędzi bocznej
                    Rysownica.DrawLine(Pióro, XsP + OśDuża/2, YsP, XsS, YsS);
                }//od using Pióro

                Widoczny = true;
            }

            public override void Wymaż(Graphics Rysownica, Control Kontrolka)
            {
                if (Widoczny)
                {
                    using (Pen Pióro = new Pen(Kontrolka.BackColor, GrubośćLinii))
                    {
                        Pióro.DashStyle = StylLinii;
                        Rysownica.DrawEllipse(Pióro, XsP - OśDuża / 2, YsP - OśMała / 2, OśDuża, OśMała);

                        //wykreślenie prążków
                        using (Pen PióroPrążków = new Pen(Pióro.Color, Pióro.Width / 3))
                        {
                            for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                            {
                                Rysownica.DrawLine(PióroPrążków, WielokątPodłogi[i], new Point(XsS, YsS));
                            }
                        } //od using PióroPrążków

                        //wykreślenie lewej krawędzi bocznej
                        Rysownica.DrawLine(Pióro, XsP - OśDuża, YsP, XsS, YsS);

                        //wykreślenie prawej krawędzi bocznej
                        Rysownica.DrawLine(Pióro, XsP + OśDuża, YsP, XsS, YsS);
                    }//od using Pióro

                    Widoczny = false;
                }
            }

            public override void ObróćWykreśl(Graphics Rysownica, Control Kontrolka, float KątObrotu)
            {
                if (Widoczny)
                {
                    Wymaż(Rysownica, Kontrolka);

                    //wyznaczenie nowego kąta położenia
                    if (KierunekObrotu)
                    {
                        KątPołożenia -= KątObrotu;
                    }
                    else
                    {
                        KątPołożenia += KątObrotu;
                    }

                    //wyznaczenie wielokąta podstawy
                    for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                    {
                        //wyznaczenie wartości współrzędnych i-tego wierzchołka wielokąta

                        WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2 * Math.Cos(Math.PI * (KątPołożenia + i * KątMiędzyWierzchołkami) / 180));
                        WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2 * Math.Sin(Math.PI * (KątPołożenia + i * KątMiędzyWierzchołkami) / 180));
                    }
                    Wykreśl(Rysownica);
                }
        }

            public override void PrzesuńDoNowegoXY(Graphics Rysownica, Control Kontrolka, int X, int Y)
            {
                if (Widoczny)
                {
                    Wymaż(Rysownica, Kontrolka);
                    int dX, dY;
                        
                    //wyznaczenie dX i dY
                    dX = XsP < X ? X - XsP : -(XsP - X);
                    dY = YsP < Y ? Y - YsP : -(YsP - Y);

                    //ustalenie nowych współrzędnych
                    XsP = XsP + dX;
                    YsP = YsP + dY;

                    XsS = XsS + dX;
                    YsS = YsS + dY;

                    //wyznaczenie wielokąta podstawy
                    for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                    {
                        //wyznaczenie wartości współrzędnych i-tego wierzchołka wielokąta

                        WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2 * Math.Cos(Math.PI * (KątPołożenia + i * KątMiędzyWierzchołkami)));
                        WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2 * Math.Sin(Math.PI * (KątPołożenia + i * KątMiędzyWierzchołkami) / 180));
                    }
                    Wykreśl(Rysownica);
                }
            }
            }
        public class StożekPochylony : Stożek
            {
                //opis stożka
                protected int XsS, YsS;
                protected int StopieńWielokątaPodstawy;
                protected float OśDuża, OśMała;
                protected float KątMiędzyWierzchołkami;
                protected float KątPołożenia;
                protected Point[] WielokątPodłogi;

                //deklaracja konstruktora
                public StożekPochylony(int R, int WysokośćStożka, int StopieńWielokątaPodstawy,
                    int XsP, int YsP, float KątPochylenia,
                    Color KolorLinii, DashStyle StylLinii, int GrubośćLinii) : base(R, WysokośćStożka, StopieńWielokątaPodstawy, 
                        XsP, YsP, KątPochylenia, KolorLinii, StylLinii, GrubośćLinii)
                {
                    RodzajBryły = TypyBrył.BG_StożekPochylony;
                    Widoczny = false;
                    KierunekObrotu = false;
                    OśDuża = 2 * R;
                    OśMała = R / 2;
                    this.StopieńWielokątaPodstawy = StopieńWielokątaPodstawy;
                    //wyznaczenie współrzędnych wierzchołka
                    XsS = (int)(XsP + WysokośćStożka / Math.Tan(Math.PI * KątPochylenia / 180F));
                    YsS = YsP - WysokośćStożka;
                    KątPołożenia = 0;
                    KątMiędzyWierzchołkami = 360 / StopieńWielokątaPodstawy;
                    WielokątPodłogi = new Point[StopieńWielokątaPodstawy];

                    for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                    {
                        WielokątPodłogi[i] = new Point();
                        //wyznaczenie wartości współrzędnych i-tego wierzchołka wielokąta

                        WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2 * Math.Cos(Math.PI * (KątPołożenia + i * KątMiędzyWierzchołkami)/180));
                        WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2 * Math.Sin(Math.PI * (KątPołożenia + i * KątMiędzyWierzchołkami) / 180));
                    }
                    //obliczenie pola powierzchni stożka
                }//od konstruktora

                public override void Wykreśl(Graphics Rysownica)
                {
                    using (Pen Pióro = new Pen(KolorLinii, GrubośćLinii))
                    {
                        Pióro.DashStyle = StylLinii;
                        Rysownica.DrawEllipse(Pióro, XsP - OśDuża / 2, YsP - OśMała / 2, OśDuża, OśMała);

                        //wykreślenie prążków
                        using (Pen PióroPrążków = new Pen(Pióro.Color, Pióro.Width / 3))
                        {
                            for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                            {
                                Rysownica.DrawLine(PióroPrążków, WielokątPodłogi[i], new Point(XsS, YsS));
                            }
                        } //od using PióroPrążków

                        //wykreślenie lewej krawędzi bocznej
                        Rysownica.DrawLine(Pióro, XsP - OśDuża/2, YsP, XsS, YsS);

                        //wykreślenie prawej krawędzi bocznej
                        Rysownica.DrawLine(Pióro, XsP + OśDuża/2, YsP, XsS, YsS);
                    }//od using Pióro

                    Widoczny = true;
                }

                public override void Wymaż(Graphics Rysownica, Control Kontrolka)
                {
                    if (Widoczny)
                    {
                        using (Pen Pióro = new Pen(Kontrolka.BackColor, GrubośćLinii))
                        {
                            Pióro.DashStyle = StylLinii;
                            Rysownica.DrawEllipse(Pióro, XsP - OśDuża / 2, YsP - OśMała / 2, OśDuża, OśMała);

                            //wykreślenie prążków
                            using (Pen PióroPrążków = new Pen(Pióro.Color, Pióro.Width / 3))
                            {
                                for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                                {
                                    Rysownica.DrawLine(PióroPrążków, WielokątPodłogi[i], new Point(XsS, YsS));
                                }
                            } //od using PióroPrążków

                            //wykreślenie lewej krawędzi bocznej
                            Rysownica.DrawLine(Pióro, XsP - OśDuża, YsP, XsS, YsS);

                            //wykreślenie prawej krawędzi bocznej
                            Rysownica.DrawLine(Pióro, XsP + OśDuża, YsP, XsS, YsS);
                        }//od using Pióro

                        Widoczny = false;
                    }
                }

                public override void ObróćWykreśl(Graphics Rysownica, Control Kontrolka, float KątObrotu)
                {
                    if (Widoczny)
                    {
                        Wymaż(Rysownica, Kontrolka);
                        //wyznaczenie nowego kąta położenia
                        if (KierunekObrotu)
                        {
                            KątPołożenia -= KątObrotu;
                        }
                        else
                        {
                            KątPołożenia += KątObrotu;
                        }

                        //wyznaczenie wielokąta podstawy
                        for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                        {
                            //wyznaczenie wartości współrzędnych i-tego wierzchołka wielokąta
                            WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2 * Math.Cos(Math.PI * (KątPołożenia + i * KątMiędzyWierzchołkami)/180));
                            WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2 * Math.Sin(Math.PI * (KątPołożenia + i * KątMiędzyWierzchołkami) / 180));
                        }
                        Wykreśl(Rysownica);
                    }
                }

                public override void PrzesuńDoNowegoXY(Graphics Rysownica, Control Kontrolka, int X, int Y)
                {
                    if (Widoczny)
                    {
                        int dX, dY;

                        //wyznaczenie dX i dY
                        dX = XsP < X ? X - XsP : -(XsP - X);
                        dY = YsP < Y ? Y - YsP : -(YsP - Y);

                        //ustalenie nowych współrzędnych
                        XsP = XsP + dX;
                        YsP = YsP + dY;

                        //wyznaczenie wielokąta podstawy
                        for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                        {
                            //wyznaczenie wartości współrzędnych i-tego wierzchołka wielokąta

                            WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2 * Math.Cos(Math.PI * (KątPołożenia + i * KątMiędzyWierzchołkami)));
                            WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2 * Math.Sin(Math.PI * (KątPołożenia + i * KątMiędzyWierzchołkami) / 180));
                        }
                        Wykreśl(Rysownica);
                    }
                }
            }
        public class Wielościan : BryłaAbstrakcyjna
        {
            protected Point[] WielokątPodłogi;
            protected int StopieńWielokątaPodłogi;
            protected int XsS, YsS;
            protected int PromieńBryły;

            //deklaracja konstruktora
            public Wielościan(int R, int StopieńWielokąta, Color KolorLinii, DashStyle StylLinii, 
                int GrubośćLinii) : base(KolorLinii, StylLinii, GrubośćLinii)
            {
                PromieńBryły = R;
                StopieńWielokątaPodłogi = StopieńWielokąta;

            }
            //od konstruktora

            //nadpisanie metod abstrakcyjnych
            public override void Wykreśl(Graphics Rysownica)
            {
                throw new NotImplementedException();
            }
            public override void Wymaż(Graphics Rysownica, Control Kontrolka)
            {
                throw new NotImplementedException();
            }
            public override void ObróćWykreśl(Graphics Rysownica, Control Kontrolka, float KątObrotu)
            {
                throw new NotImplementedException();
            }
            public override void PrzesuńDoNowegoXY(Graphics Rysownica, Control Kontrolka, int X, int Y)
            {
                throw new NotImplementedException();
            }
        }//od wielościanu
        public class Graniastosłup : Wielościan
        {
            //deklaracje uzupełniające
            Point[] WielokątSufitu;
            float KątŚrodkowyMiędzyWierzchołkami;
            float KątPołożeniaPierwszegoWierzchołkaWielokąta;
            float OśDuża;
            float OśMała;

            public Graniastosłup(int R, int WysokośćGraniastosłupa, int StopieńWielokąta, int XsP,
                int YsP, Color KolorLinii, DashStyle StylLinii, int GrubośćLinii)
                :base(R, StopieńWielokąta, KolorLinii, StylLinii, GrubośćLinii)
            {
                RodzajBryły = TypyBrył.BG_Graniastosłup;
                Widoczny = false;
                KierunekObrotu = false;
                WysokośćBryły = WysokośćGraniastosłupa;
                StopieńWielokątaPodłogi = StopieńWielokąta;

                //wyznaczenie pozostałych atrybutów graniastosłupa
                this.XsP = XsP;
                this.YsP = YsP;

                XsS = XsP;
                YsS = YsP - WysokośćGraniastosłupa;

                OśDuża = 2 * R;
                OśMała = R / 2;

                KątŚrodkowyMiędzyWierzchołkami = 360 / StopieńWielokąta;
                KątPołożeniaPierwszegoWierzchołkaWielokąta = 0F;

                //utworzenie tablic wielokąta podłogi i sufitu
                WielokątPodłogi = new Point[StopieńWielokątaPodłogi + 1];
                WielokątSufitu = new Point[StopieńWielokątaPodłogi + 1];

                //wyznaczenie punktów
                for(int i=0; i<=StopieńWielokąta; i++)
                {
                    WielokątPodłogi[i] = new Point();
                    WielokątSufitu[i] = new Point();

                    WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2 
                        * Math.Cos(Math.PI * (KątPołożeniaPierwszegoWierzchołkaWielokąta+i*KątŚrodkowyMiędzyWierzchołkami) / 180));
                    WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2
                        * Math.Sin(Math.PI * (KątPołożeniaPierwszegoWierzchołkaWielokąta + i * KątŚrodkowyMiędzyWierzchołkami) / 180));

                    WielokątSufitu[i].X = WielokątPodłogi[i].X;
                    WielokątSufitu[i].Y = WielokątPodłogi[i].Y - WysokośćGraniastosłupa;
                }//od for

                //obliczenie pola powierzchni i objętości graniastosłupa

            }

            //nadpisanie metod abstrakcyjnych klasy Bryły Abstrakcyjne
            public override void Wykreśl(Graphics Rysownica)
            {
                using(Pen Pióro=new Pen(KolorLinii, GrubośćLinii))
                {
                    Pióro.DashStyle = StylLinii;
                    
                    //wykreślenie podłogi
                    for(int i=0; i<WielokątPodłogi.Length-1; i++)
                    {
                        Rysownica.DrawLine(Pióro, WielokątPodłogi[i], WielokątPodłogi[i + 1]);
                    }

                    //wykreślenie sufity
                    for (int i = 0; i <WielokątSufitu.Length-1; i++)
                    {
                        Rysownica.DrawLine(Pióro, WielokątSufitu[i], WielokątSufitu[i + 1]);
                    }

                    //wykreślenie krawędzi bocznych
                    for (int i=0; i<StopieńWielokątaPodłogi; i++)
                    {
                        Rysownica.DrawLine(Pióro, WielokątPodłogi[i], WielokątSufitu[i]);
                    }
                }

                Widoczny = true;
            }

            public override void Wymaż(Graphics Rysownica, Control Kontrolka)
            {
                if (Widoczny)
                {
                    using (Pen Pióro = new Pen(Kontrolka.BackColor, GrubośćLinii))
                    {
                        Pióro.DashStyle = StylLinii;

                        //wykreślenie podłogi
                        for (int i = 0; i < WielokątPodłogi.Length - 1; i++)
                        {
                            Rysownica.DrawLine(Pióro, WielokątPodłogi[i], WielokątPodłogi[i + 1]);
                        }

                        //wykreślenie sufity
                        for (int i = 0; i < WielokątSufitu.Length - 1; i++)
                        {
                            Rysownica.DrawLine(Pióro, WielokątSufitu[i], WielokątSufitu[i + 1]);
                        }

                        //wykreślenie krawędzi bocznych
                        for (int i = 0; i < StopieńWielokątaPodłogi; i++)
                        {
                            Rysownica.DrawLine(Pióro, WielokątPodłogi[i], WielokątSufitu[i]);
                        }
                    }
                    Widoczny = false;
                }
            }
            public override void ObróćWykreśl(Graphics Rysownica, Control Kontrolka, float KątObrotu)
            {
                if (Widoczny)
                    Wymaż(Rysownica, Kontrolka);

                if (KierunekObrotu)
                {
                    KątPołożeniaPierwszegoWierzchołkaWielokąta -= KątObrotu;
                }
                else
                {
                    KątPołożeniaPierwszegoWierzchołkaWielokąta += KątObrotu;
                }
                //wyznaczenie wielokąta

                for (int i = 0; i <= StopieńWielokątaPodłogi; i++)
                {
                    WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2
                        * Math.Cos(Math.PI * (KątPołożeniaPierwszegoWierzchołkaWielokąta + i * KątŚrodkowyMiędzyWierzchołkami) / 180));
                    WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2
                        * Math.Sin(Math.PI * (KątPołożeniaPierwszegoWierzchołkaWielokąta + i * KątŚrodkowyMiędzyWierzchołkami) / 180));

                    WielokątSufitu[i].X = WielokątPodłogi[i].X;
                    WielokątSufitu[i].Y = WielokątPodłogi[i].Y - WysokośćBryły;
                }//od for
                Wykreśl(Rysownica);
            }
            public override void PrzesuńDoNowegoXY(Graphics Rysownica, Control Kontrolka, int X, int Y)
            {
                if (Widoczny)
                {
                    Wymaż(Rysownica, Kontrolka);

                    //przesunięcie do punktu X Y
                    XsP = X;
                    YsP = Y;
                    XsS = X;
                    YsS = YsP - WysokośćBryły;

                    //wyznaczenie nowych współrzędnych wielokątów
                    for (int i = 0; i <= StopieńWielokątaPodłogi; i++)
                    {
                        WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2
                        * Math.Cos(Math.PI * (KątPołożeniaPierwszegoWierzchołkaWielokąta + i * KątŚrodkowyMiędzyWierzchołkami) / 180));
                        WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2
                            * Math.Sin(Math.PI * (KątPołożeniaPierwszegoWierzchołkaWielokąta + i * KątŚrodkowyMiędzyWierzchołkami) / 180));

                        WielokątSufitu[i].X = WielokątPodłogi[i].X;
                        WielokątSufitu[i].Y = WielokątPodłogi[i].Y - WysokośćBryły;
                    }//od for

                    Wykreśl(Rysownica);
                }

            }
        }
        public class Ostrosłup : Wielościan
        {
            protected int OśDuża, OśMała;
            protected float KątPołożeniaPierwszegoWierzchołka;
            protected float KątŚrodkowyMiędzyWierzchołkami;

            public Ostrosłup(int R, int WysokośćOstrosłupa, int StopieńWielokąta, int XsP, int YsP,
                Color KolorLinii, DashStyle StylLinii, int GrubośćLinii):base(R, StopieńWielokąta, KolorLinii,
                    StylLinii, GrubośćLinii)
            {
                RodzajBryły = TypyBrył.BG_Ostrosłup;
                Widoczny = false;
                KierunekObrotu = false;
                WysokośćBryły = WysokośćOstrosłupa;
                StopieńWielokątaPodłogi = StopieńWielokąta;
                
                this.XsP = XsP;
                this.YsP = YsP;

                XsS = XsP;
                YsS = YsP - WysokośćOstrosłupa;
                OśDuża = 2 * R;
                OśMała = R / 2;
                KątPołożeniaPierwszegoWierzchołka = 0F;
                KątŚrodkowyMiędzyWierzchołkami = 360 / StopieńWielokąta;
                WielokątPodłogi = new Point[StopieńWielokątaPodłogi + 1];

                for(int i=0; i<=StopieńWielokątaPodłogi; i++)
                {
                    WielokątPodłogi[i] = new Point();
                    WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2 * Math.Cos(Math.PI * (KątPołożeniaPierwszegoWierzchołka + 
                        i * KątŚrodkowyMiędzyWierzchołkami) / 180));
                    WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2 * Math.Sin(Math.PI * (KątPołożeniaPierwszegoWierzchołka +
                        i * KątŚrodkowyMiędzyWierzchołkami) / 180));
                }
            }
            public override void Wykreśl(Graphics Rysownica)
            {
                using(Pen Pióro=new Pen(KolorLinii, GrubośćLinii))
                {
                    Pióro.DashStyle = StylLinii;

                    for(int i=0; i<WielokątPodłogi.Length-1; i++)
                    {
                        Rysownica.DrawLine(Pióro, WielokątPodłogi[i], WielokątPodłogi[i + 1]);
                    }

                    for(int i=0; i<=StopieńWielokątaPodłogi; i++)
                    {
                        Rysownica.DrawLine(Pióro, WielokątPodłogi[i], new Point(XsS, YsS));
                    }

                    Widoczny = true;
                }
            }
            public override void Wymaż(Graphics Rysownica, Control Kontrolka)
            {
                if (Widoczny)
                {
                    using(Pen Pióro = new Pen(Kontrolka.BackColor, GrubośćLinii))
                    {
                        Pióro.DashStyle = StylLinii;

                        for (int i = 0; i < WielokątPodłogi.Length - 1; i++)
                        {
                            Rysownica.DrawLine(Pióro, WielokątPodłogi[i], WielokątPodłogi[i + 1]);
                        }

                        for (int i = 0; i <= StopieńWielokątaPodłogi; i++)
                        {
                            Rysownica.DrawLine(Pióro, WielokątPodłogi[i], new Point(XsS, YsS));
                        }

                        Widoczny = false;
                    }
                }
            }
            public override void ObróćWykreśl(Graphics Rysownica, Control Kontrolka, float KątObrotu)
            {
                if (Widoczny)
                {
                    Wymaż(Rysownica, Kontrolka);

                    if (KierunekObrotu)
                        KątPołożeniaPierwszegoWierzchołka -= KątObrotu;
                    else
                        KątPołożeniaPierwszegoWierzchołka += KątObrotu;

                    for(int i=0; i<=StopieńWielokątaPodłogi; i++)
                    {
                        WielokątPodłogi[i].X=(int)(XsP+OśDuża/2*Math.Cos(Math.PI*(KątPołożeniaPierwszegoWierzchołka+
                            i*KątŚrodkowyMiędzyWierzchołkami)/180));

                        WielokątPodłogi[i].Y=(int)(YsP+OśMała/2*Math.Sin(Math.PI*(KątPołożeniaPierwszegoWierzchołka+
                            i*KątŚrodkowyMiędzyWierzchołkami)/180));
                    }

                    Wykreśl(Rysownica);
                }
            }
            public override void PrzesuńDoNowegoXY(Graphics Rysownica, Control Kontrolka, int X, int Y)
            {
                if (Widoczny)
                {
                    Wymaż(Rysownica, Kontrolka);

                    XsP = X;
                    YsP = Y;
                    XsS = XsP;
                    YsS = YsP - WysokośćBryły;

                    for(int i = 0; i<=StopieńWielokątaPodłogi; i++)
                    {
                        WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2 * Math.Cos(Math.PI * (KątPołożeniaPierwszegoWierzchołka +
                            i * KątŚrodkowyMiędzyWierzchołkami) / 180));
                        WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2 * Math.Sin(Math.PI * (KątPołożeniaPierwszegoWierzchołka +
                            i * KątŚrodkowyMiędzyWierzchołkami) / 180));
                    }

                    Wykreśl(Rysownica);
                }
            }
        }
        public class Kula:BryłaObrotowa
        {
            float OśDuża, OśMała;
            int PrzesunięcieObręczy;
            float KątPołożeniaObręczy;

            public Kula(int R, Point ŚrodekPodłogi, Color KolorLinii, DashStyle StylLinii, int GrubośćLinii):
                base(R, KolorLinii, StylLinii, GrubośćLinii)
            {
                RodzajBryły = TypyBrył.BG_Kula;
                Widoczny = false;
                KierunekObrotu = false;
                XsP = ŚrodekPodłogi.X;
                YsP = ŚrodekPodłogi.Y;
                OśDuża = R * 2;
                OśMała = R / 2;
                KątPołożeniaObręczy = 0;
                PrzesunięcieObręczy = 0;

                this.ObjętośćBryły = 4 / 3 * (float)Math.PI * ((OśDuża / 2) * (OśDuża / 2) * (OśDuża / 2));
                this.PowierzchniaBryły = 4 * (float)Math.PI * ((OśDuża / 2) * (OśDuża / 2));
            }
            public override void Wykreśl(Graphics Rysownica)
            {
                using(Pen Pióro = new Pen(KolorLinii, GrubośćLinii))
                {
                    Pióro.DashStyle = StylLinii;

                    Rysownica.DrawEllipse(Pióro, XsP - OśDuża / 2, YsP - OśMała / 2, OśDuża, OśDuża);
                    Rysownica.DrawEllipse(Pióro, XsP - OśDuża / 2, YsP + OśMała, OśDuża, OśMała);

                    using(Pen PióroObręczy = new Pen(Pióro.Color, GrubośćLinii / 3))
                    {
                        Rysownica.DrawEllipse(PióroObręczy, PrzesunięcieObręczy / 2 + XsP - OśDuża / 2,
                            YsP - OśMała / 2, OśDuża - PrzesunięcieObręczy, OśDuża);
                    }
                }

                Widoczny = true;
            }
            public override void Wymaż(Graphics Rysownica, Control Kontrolka)
            {
                if (Widoczny)
                {
                    using (Pen Pióro = new Pen(Kontrolka.BackColor, GrubośćLinii))
                    {
                        Pióro.DashStyle = StylLinii;

                        Rysownica.DrawEllipse(Pióro, XsP - OśDuża / 2, YsP - OśMała / 2, OśDuża, OśMała);
                        Rysownica.DrawEllipse(Pióro, XsP - OśDuża / 2, YsP + OśMała / 2, OśDuża, OśMała);

                        using (Pen PióroObręczy = new Pen(Pióro.Color, GrubośćLinii / 3))
                        {
                            Rysownica.DrawEllipse(PióroObręczy, PrzesunięcieObręczy / 2 + XsP - OśDuża / 2,
                                YsP - OśMała / 2, OśDuża - PrzesunięcieObręczy, OśDuża);
                        }
                    }

                    Widoczny = false;
                } 
            }
            public override void ObróćWykreśl(Graphics Rysownica, Control Kontrolka, float KątObrotu)
            {
                if (Widoczny)
                {
                    KątPołożeniaObręczy = (KątPołożeniaObręczy + KątObrotu) % 360;
                    Wymaż(Rysownica, Kontrolka);
                    PrzesunięcieObręczy = (int)(KątPołożeniaObręczy % (int)(OśDuża)) * 2;
                    Wykreśl(Rysownica);
                }
            }
            public override void PrzesuńDoNowegoXY(Graphics Rysownica, Control Kontrolka, int X, int Y)
            {
                if (Widoczny)
                {
                    Wymaż(Rysownica, Kontrolka);
                    XsP = X;
                    YsP = Y;
                    Wykreśl(Rysownica);
                }
            }
        }
        public class kdWalecPochylony : Walec
        {
            public kdWalecPochylony(int R, int WysokośćWalca, int StopieńWielokątaPodstawy, int XsP, int YsP, 
                float KątPochyleniaWalca, Color KolorLinii, DashStyle StylLinii, int GrubośćLinii)
                :base(R, WysokośćWalca, StopieńWielokątaPodstawy, XsP, YsP,
                   KolorLinii,  StylLinii,  GrubośćLinii)
            {
                RodzajBryły = TypyBrył.BG_WalecPochylony;
                KątPochylenia = KątPochyleniaWalca;
                XsS = (int)(XsP + WysokośćBryły / Math.Tan(Math.PI * KątPochylenia / 180F));
                YsS = YsP - WysokośćBryły;
                KątMiędzyWierzchołkami = 360 / StopieńWielokątaPodstawy;
                WielokątPodłogi = new Point[StopieńWielokątaPodstawy];

                for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                {
                    WielokątPodłogi[i] = new Point();
                    //wyznaczenie wartości współrzędnych i-tego wierzchołka wielokąta

                    WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2 * Math.Cos(Math.PI * (i * KątMiędzyWierzchołkami) / 180));
                    WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2 * Math.Sin(Math.PI * (i * KątMiędzyWierzchołkami) / 180));
                }

                for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                {
                    WielokątSufitu[i] = new Point();
                    //wyznaczenie wartości współrzędnych i-tego wierzchołka wielokąta

                    WielokątSufitu[i].X = (int)(XsS + OśDuża / 2 * Math.Cos(Math.PI * (i * KątMiędzyWierzchołkami) / 180));
                    WielokątSufitu[i].Y = (int)(YsS + OśMała / 2 * Math.Sin(Math.PI * (i * KątMiędzyWierzchołkami) / 180));
                }
            }
        }
        public class kdPodwójnyStożek:Stożek
        {
            public kdPodwójnyStożek(int R, int WysokośćStożka, int StopieńWielokątaPodstawy, int XsP, int YsP, 
                float KątPochylenia, Color KolorLinii, DashStyle StylLinii, int GrubośćLinii)
                :base(R, WysokośćStożka/2, StopieńWielokątaPodstawy, XsP, YsP, KątPochylenia, KolorLinii, StylLinii,
                     GrubośćLinii)
            {
                RodzajBryły = TypyBrył.BG_StożkiZłączone;
                WysokośćBryły = WysokośćStożka;
                this.KątPochylenia = 90;
            }

            public override void Wykreśl(Graphics Rysownica)
            {
                using (Pen kdPióro = new Pen(KolorLinii, GrubośćLinii))
                {
                    kdPióro.DashStyle = StylLinii;
                    Rysownica.DrawEllipse(kdPióro, XsP - OśDuża / 2, YsP - OśMała / 2, OśDuża, OśMała);

                    using (Pen kdPióroPrążków = new Pen(kdPióro.Color, 1f))
                    {
                        kdPióroPrążków.DashStyle = DashStyle.Dot;
                        for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                        {
                            Rysownica.DrawLine(kdPióroPrążków, WielokątPodłogi[i], new Point(XsS, YsP + WysokośćBryły / 2));
                            Rysownica.DrawLine(kdPióroPrążków, WielokątPodłogi[i], new Point(XsS, YsP - WysokośćBryły / 2));
                        }
                    }
                    //wykreślenie krawędzi bocznych

                    Rysownica.DrawLine(kdPióro, XsP - OśDuża / 2, YsP, XsS, YsP + WysokośćBryły / 2);
                    Rysownica.DrawLine(kdPióro, XsP + OśDuża / 2, YsP, XsS, YsP + WysokośćBryły / 2);

                    Rysownica.DrawLine(kdPióro, XsP - OśDuża / 2, YsP, XsS, YsP - WysokośćBryły / 2);
                    Rysownica.DrawLine(kdPióro, XsP + OśDuża / 2, YsP, XsS, YsP - WysokośćBryły / 2);
                }

                Widoczny = true;
            }
            public override void Wymaż(Graphics Rysownica, Control Kontrolka)
            {
                if (Widoczny)
                {
                    using (Pen kdPióro = new Pen(Kontrolka.BackColor, GrubośćLinii))
                    {
                        kdPióro.DashStyle = StylLinii;
                        Rysownica.DrawEllipse(kdPióro, XsP - OśDuża / 2, YsP - OśMała / 2, OśDuża, OśMała);

                        using (Pen kdPióroPrążków = new Pen(kdPióro.Color, 1f))
                        {
                            kdPióroPrążków.DashStyle = DashStyle.Dot;
                            for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                            {
                                Rysownica.DrawLine(kdPióroPrążków, WielokątPodłogi[i], new Point(XsS, YsP + WysokośćBryły / 2));
                                Rysownica.DrawLine(kdPióroPrążków, WielokątPodłogi[i], new Point(XsS, YsP - WysokośćBryły / 2));
                            }
                        }
                        //wykreślenie krawędzi bocznych

                        Rysownica.DrawLine(kdPióro, XsP - OśDuża / 2, YsP, XsS, YsP + WysokośćBryły / 2);
                        Rysownica.DrawLine(kdPióro, XsP + OśDuża / 2, YsP, XsS, YsP + WysokośćBryły / 2);

                        Rysownica.DrawLine(kdPióro, XsP - OśDuża / 2, YsP, XsS, YsP - WysokośćBryły / 2);
                        Rysownica.DrawLine(kdPióro, XsP + OśDuża / 2, YsP, XsS, YsP - WysokośćBryły / 2);

                        Widoczny = false;
                    }
                }
            }
        }
        public class kdStożekŚcięty : Walec
        {
            public kdStożekŚcięty(int R, int WysokośćWalca, int StopieńWielokątaPodstawy, int XsP, int YsP,
                Color KolorLinii, DashStyle StylLinii, int GrubośćLinii):base(R, WysokośćWalca, 
                    StopieńWielokątaPodstawy, XsP, YsP, KolorLinii, StylLinii, GrubośćLinii)
            {
                KątPochylenia = 90;

                RodzajBryły = TypyBrył.BG_StożekŚcięty;

                //przypisanie nowych współrzędnych punktów wielokąta sufitu
                for(int i=0; i<WielokątSufitu.Length; i++)
                {
                    WielokątSufitu[i].X = (int)(XsS + (OśDuża/2) / 2 * Math.Cos(Math.PI * 
                        (KątMiędzyWierzchołkami * i + KątPołożenia) / 180F));
                    WielokątSufitu[i].Y = (int)(YsS + (OśMała/2) / 2 * Math.Sin(Math.PI * 
                        (KątMiędzyWierzchołkami * i + KątPołożenia) / 180F));
                }
            }

            public override void Wykreśl(Graphics Rysownica)
            {
                using (Pen kdPióro = new Pen(KolorLinii, GrubośćLinii))
                {
                    kdPióro.DashStyle = StylLinii;

                    //kreślenie podłogi walca
                    Rysownica.DrawEllipse(kdPióro, XsP - OśDuża/2, YsP - OśMała / 2, OśDuża, OśMała);

                    //kreślenie sufitu walca
                    Rysownica.DrawEllipse(kdPióro, XsS - OśDuża / 4, YsS - OśMała / 4, OśDuża/2, OśMała/2);

                    //wykreślenie prążków na ścianie bocznej walca
                    using (Pen kdPióroPrążków = new Pen(KolorLinii, 1f))
                    {
                        kdPióroPrążków.DashStyle = DashStyle.Dot;

                        for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                            Rysownica.DrawLine(kdPióroPrążków, WielokątPodłogi[i], WielokątSufitu[i]);
                    }

                    //kreślenie lewej krawędzi
                    Rysownica.DrawLine(kdPióro, XsP - OśDuża / 2, YsP, XsS - OśDuża / 4, YsS);

                    //kreślenie prawej krawędzi
                    Rysownica.DrawLine(kdPióro, XsP + OśDuża / 2, YsP, XsS + OśDuża / 4, YsS);
                } // od using (zwolnienie pióra)
                Widoczny = true;
            }

            public override void Wymaż(Graphics Rysownica, Control Kontrolka)
            {
                if (Widoczny)
                {
                    using (Pen Pióro = new Pen(Kontrolka.BackColor, GrubośćLinii))
                    {
                        Pióro.DashStyle = StylLinii;

                        //kreślenie podłogi walca
                        Rysownica.DrawEllipse(Pióro, XsP - OśDuża / 2, YsP - OśMała / 2, OśDuża, OśMała);

                        //kreślenie sufitu walca
                        Rysownica.DrawEllipse(Pióro, XsS - OśDuża / 4, YsS - OśMała / 4, OśDuża/2, OśMała/2);

                        //wykreślenie prążków na ścianie bocznej walca
                        using (Pen PióroPrążków = new Pen(Kontrolka.BackColor, 1F))
                        {
                            PióroPrążków.DashStyle = DashStyle.Dot;

                            for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                                Rysownica.DrawLine(PióroPrążków, WielokątPodłogi[i], WielokątSufitu[i]);
                        }

                        //kreślenie lewej krawędzi
                        Rysownica.DrawLine(Pióro, XsP - OśDuża / 2, YsP, XsS - OśDuża / 4, YsS);

                        //kreślenie prawej krawędzi
                        Rysownica.DrawLine(Pióro, XsP + OśDuża / 2, YsP, XsS + OśDuża / 4, YsS);
                    } // od using (zwolnienie pióra)
                    Widoczny = false;
                }
            }

            public override void ObróćWykreśl(Graphics Rysownica, Control Kontrolka, float KątObrotu)
            {
                if (Widoczny)
                {
                    if (KierunekObrotu)
                    {
                        KątPołożenia -= KątObrotu;
                    }
                    else
                    {
                        KątPołożenia += KątObrotu;
                    }

                    Wymaż(Rysownica, Kontrolka);

                    for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                    {
                        //podłoga
                        WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2 * Math.Cos(Math.PI * (KątMiędzyWierzchołkami * i 
                            + KątPołożenia) / 180F));
                        WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2 * Math.Sin(Math.PI * (KątMiędzyWierzchołkami * i 
                            + KątPołożenia) / 180F));

                        //sufit
                        WielokątSufitu[i].X = (int)(XsS + (OśDuża / 2) / 2 * Math.Cos(Math.PI * (KątMiędzyWierzchołkami * i 
                            + KątPołożenia) / 180F));
                        WielokątSufitu[i].Y = (int)(YsS + (OśMała / 2) / 2 * Math.Sin(Math.PI * (KątMiędzyWierzchołkami * i 
                            + KątPołożenia) / 180F));
                    } //od for

                    Wykreśl(Rysownica);
                }
            }

            public override void PrzesuńDoNowegoXY(Graphics Rysownica, Control Kontrolka, int X, int Y)
            {
                if (Widoczny)
                {
                    int kdDx, kdDy;
                    Wymaż(Rysownica, Kontrolka);

                    //wyznaczenie wektora przesunięcia
                    kdDx = XsP < X ? X - XsP : -(XsP - X);
                    kdDy = YsP < Y ? Y - YsP : -(YsP - Y);

                    //wyznaczamy nowe położenie dla środka podłogi i sufity
                    XsP = XsP + kdDx;
                    YsP = YsP + kdDy;
                    XsS = XsS + kdDx;
                    YsS = YsS + kdDy;

                    for (int i = 0; i < StopieńWielokątaPodstawy; i++)
                    {
                        //podłoga
                        WielokątPodłogi[i].X = (int)(XsP + OśDuża / 2 * Math.Cos(Math.PI * (KątMiędzyWierzchołkami + KątPołożenia) / 180F));
                        WielokątPodłogi[i].Y = (int)(YsP + OśMała / 2 * Math.Sin(Math.PI * (KątMiędzyWierzchołkami + KątPołożenia) / 180F));

                        //sufit
                        WielokątSufitu[i].X = (int)(XsS + (OśDuża / 2) / 2 * Math.Cos(Math.PI * (KątMiędzyWierzchołkami * i + KątPołożenia) / 180F));
                        WielokątSufitu[i].Y = (int)(YsS + (OśMała / 2) / 2 * Math.Sin(Math.PI * (KątMiędzyWierzchołkami * i + KątPołożenia) / 180F));
                    } //od for

                    Wykreśl(Rysownica);
                } //od PrzesuńDoNowegoXY
            }
        }  
    }
}
