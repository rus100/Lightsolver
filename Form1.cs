using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LightSolver
{
    public partial class Form1 : Form
    {
        Form2 f = new Form2();
        public Form1()
        {
            InitializeComponent();
        }
        OpenFileDialog fd;
        double n = 0;//число ламп
        double F = 0;//световой поток
        double mnojit = 0;//множитель
        double polyarangle1 = 0;//количество полярных углов
        double azimutangle1 = 0;//количество азимутальных углов
        double fotometr = 0;//тип фотометрии
        double systemedin = 0;//система единиц
        double shirin = 0;//ширина светильника
        double dlina = 0;//длина светильника
        double visota = 0;//высота светильника
        double balast = 0;//коэффициент баласта
        double versia = 0;//версия
        double p = 0;//активная мощность светильника
        double[] polyarangle;//список полярных углов
        double[] azimutangle;//список азимутальных углов
        double[,] kss;//КСС
        double a;//длинна помещения
        double b;//ширина помещения
        double h1;//высота помещения
        double l;//длинна подвеса
        double hr;//высота рабочей поверхности
        int number;
     
        List<string> fl= new List<string>();
        string cifr;
        private void button1_Click(object sender, EventArgs e)
        {
            fd = new OpenFileDialog();
            cifr = "";
            fl.Clear();
            fd.Filter = "Файлы фотометрии|*.ies|Все файлы|*.*";
            if (fd.ShowDialog() != DialogResult.OK) {return; }
                
              
           
            StreamReader read = new StreamReader(fd.FileName);
            while (true) {
                String s = read.ReadLine();
                if (s == null) {
                    break;
                }
               
                fl.Add(s);
            }
            read.Close();
           
            for (int i = 0; i < fl.Count; i++) {
                if (fl[i].IndexOf("TILT")==0) {
                   
                    number = i;
                 }
               
                   
                   
                
                }
            
            for (int i = number+1; i < fl.Count; i++)
            {
               
                    cifr += fl[i] + " ";
            }
            
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string[] nabor = cifr.Split(' ');
                string[] raschifr;
                List<string> rashifr1 = new List<string>();
                List<int> number = new List<int>();
                n = 0;//число ламп
                F = 0;//световой поток
                mnojit = 0;//множитель
                polyarangle1 = 0;//количество полярных углов
                azimutangle1 = 0;//количество азимутальных углов
                fotometr = 0;//тип фотометрии
                systemedin = 0;//система единиц
                shirin = 0;//ширина светильника
                dlina = 0;//длина светильника
                visota = 0;//высота светильника
                balast = 0;//коэффициент баласта
                versia = 0;//версия
                p = 0;
                for (int i = 0; i < nabor.Length; i++)
                {
                    rashifr1.Add(nabor[i]);
                }
                for (int i = 0; i < rashifr1.Count; i++)
                {
                    if (rashifr1[i] == "")
                    {
                        number.Add(i);

                    }
                }
                for (int i = 0; i < number.Count; i++)
                {
                    rashifr1.Remove("");
                }
                raschifr = new string[rashifr1.Count];

                for (int i = 0; i < rashifr1.Count; i++)
                {
                    raschifr[i] = rashifr1[i];
                }
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                n = Convert.ToDouble(raschifr[0]);
                F = Convert.ToDouble(raschifr[1]);
                mnojit = Convert.ToDouble(raschifr[2]);
                polyarangle1 = Convert.ToDouble(raschifr[3]);
                azimutangle1 = Convert.ToDouble(raschifr[4]);
                fotometr = Convert.ToDouble(raschifr[5]);
                systemedin = Convert.ToDouble(raschifr[6]);
                shirin = Convert.ToDouble(raschifr[7]);
                dlina = Convert.ToDouble(raschifr[8]);
                visota = Convert.ToDouble(raschifr[9]);
                balast = Convert.ToDouble(raschifr[10]);
                versia = Convert.ToDouble(raschifr[11]);
                p = Convert.ToDouble(raschifr[12]);
                polyarangle = new double[(int)polyarangle1];
                azimutangle = new double[(int)azimutangle1];
                kss = new double[(int)azimutangle1, (int)polyarangle1];
                for (int i = 13; i < 13 + (int)polyarangle1; i++)
                {
                    polyarangle[i - 13] = Convert.ToDouble(raschifr[i]);
                }
                for (int i = 13 + (int)polyarangle1; i < 13 + (int)polyarangle1 + (int)azimutangle1; i++)
                {
                    azimutangle[i - 13 - (int)polyarangle1] = Convert.ToDouble(raschifr[i]);
                }
                for (int i = 0; i < (int)azimutangle1; i++)
                {
                    for (int j = 0; j < (int)polyarangle1; j++)
                    {
                        int x = 13 + (int)polyarangle1 + (int)azimutangle1 + i * (int)polyarangle1 + j;
                        kss[i, j] = Convert.ToDouble(raschifr[x]);
                    }
                }

                a = Convert.ToDouble(textBox1.Text);
                b = Convert.ToDouble(textBox2.Text);
                h1 = Convert.ToDouble(textBox3.Text);
                l = Convert.ToDouble(textBox4.Text);
                hr = Convert.ToDouble(textBox5.Text);
                if (F != -1)
                {
                    for (int i = 0; i < azimutangle1; i++)
                    {
                        for (int j = 0; j < polyarangle1; j++)
                        {
                            kss[i, j] = mnojit * kss[i, j];
                        }
                    }
                }
                decimal kz = numericUpDown1.Value;
                Data.a = a;
                Data.azimutangle = azimutangle;
                Data.azimutangle1 = azimutangle1;
                Data.b = b;
                Data.balast = balast;
                Data.dlina = dlina;
                Data.F = F;
                Data.fotometr = fotometr;
                Data.h1 = h1;
                Data.hr = hr;
                Data.l = l;
                Data.mnojit = mnojit;
                Data.n = n;
                Data.polyarangle = polyarangle;
                Data.polyarangle1 = polyarangle1;
                Data.shirin = shirin;
                Data.systemedin = systemedin;
                Data.versia = versia;
                Data.visota = visota;
                Data.p = p;
                Data.kss = kss;
                Data.kz = kz;
                f.ShowDialog();
            }
            catch {
                MessageBox.Show("Введите все данные или правильные по формату");
            
            
            } 
           
           
            
        }

        private void авторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }

              private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
    }
}
