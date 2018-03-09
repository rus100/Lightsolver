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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        int rjad;
        int vrjadu;
        Graphics gr;
        double k1;
        double k2;
        float k;
        private void button2_Click(object sender, EventArgs e)
                {
                     
           
                    double xi = 0;
                    double yi = 0;
                    double[,] karta = new double[100, 100];
                    int[,] kartacveta = new int[100, 100];
                    double minE = 1000000;
                    double maxE = 0;
                    double Esum = 0;
            gr = panel1.CreateGraphics();
            gr.Clear(Color.White);
            rjad = (int)numericUpDown3.Value;
            vrjadu = (int)numericUpDown2.Value;
            
            k1 = (panel1.Width-20) / Data.a;
            k2 = (panel1.Height-20) / Data.b;
            if (k1 >= k2)
            {
                k = (float)k2;
            }
            else { k = (float)k1; }
            gr.DrawRectangle(Pens.Red, 10, 10,k*(float) Data.a,k*(float) Data.b);
            for (int i = 0; i < rjad; i++) {
                for (int j = 0; j < vrjadu; j++) {
                    float y=10+k*(float) Data.b/(2*rjad)+i*k*(float) Data.b/(rjad)-k*(float)Data.dlina/2;
                    float x=10+k*(float) Data.a/(2*vrjadu)+j*k*(float) Data.a/(vrjadu)-k*(float)Data.shirin/2;
                    gr.DrawRectangle(Pens.Green, x, y, k*(float)Data.shirin, k*(float)Data.dlina);
                }
            } 
            for (int i = 0; i < 100; i++) {
                for (int j = 0; j < 100; j++) {
                    Esum=0;
                   for (int i1 = 0; i1 < rjad; i1++) {
                        for (int j1 = 0; j1 < vrjadu; j1++) { 
                     xi = Data.a / 100 * i;
                    yi = Data.b / 100 * j;
                        Esum+=E(i1,j1,xi,yi);
                        
                        }
                    }
                    Esum = Esum / (double)Data.kz;
                   karta[i, j] = Math.Ceiling(Esum);
                }
            }
            for (int i = 0; i < 100; i++) {
                for (int j = 0; j < 100; j++) {
                    if (karta[i, j] >= maxE) {
                        maxE = karta[i, j];
                    }
                    if (karta[i, j] <= minE) {
                        minE = karta[i, j];
                    }
                }
            }
            double dE = (maxE - minE) / 255;
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    double R = Math.Abs(karta[i, j] - minE) / dE;
                    kartacveta[i, j] =(int) Math.Truncate(R);
                }
            }
            StreamWriter wr = new StreamWriter("karta.txt");
            for (int i = 0; i < 100; i++) {
                for (int j = 0; j < 100; j++) {
                    wr.Write(kartacveta[i, j].ToString() + " ");
                }
                wr.WriteLine(" ");
            }
            wr.Close();
            StreamWriter wr1 = new StreamWriter("karta1.txt");
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    wr1.Write(karta[i, j].ToString() + " ");
                }
                wr1.WriteLine(" ");
            }
            wr1.Close();
            SolidBrush myBrush = new SolidBrush(Color.Black);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {float y=10+k*j*(float) Data.b/100;
                    float x=10+k*i*(float) Data.a/100;
                    myBrush.Color = Color.FromArgb(254, kartacveta[i, j], kartacveta[i, j], kartacveta[i, j]);
                    gr.FillRectangle(myBrush, x, y, (float)k2 * (float)Data.b / 100, (float)k1 * (float)Data.a / 100);
                }
            }
            DataTable table = new DataTable();
            for(int i=0;i<100;i++){

                table.Columns.Add(((Data.a / 100) * (double)i).ToString() + " " + "м", typeof(String));
            }
            for (int i = 0; i < 100; i++)
            {

                table.Rows.Add(((Data.b / 100) * (double)i).ToString() + " " + "м", typeof(String));
            }
            for (int i = 0; i < 100; i++) {
                for (int j = 0; j < 100; j++) {
                    table.Rows[i][j] = karta[i, j].ToString();
                }
            }
            BindingSource binding = new BindingSource();
            binding.DataSource = table;
            dataGridView1.DataSource = binding;
            dataGridView1.Update();
            for (int i = 0; i < 100; i++)
            {
                
                dataGridView1.Rows[i].HeaderCell.Value = ((Data.b / 100) * (double)i).ToString()+" "+"м";  
            }
           
       }
        bool prostoe(int x)
        {
            int count = 0;
            for (int i = 1; i < (int)Math.Ceiling(Math.Pow((double)x, 0.5)); i++) {
                if (x % i == 0) {
                    count++;
                }
            }

            if (count > 1)
            {
                return false;
            }
            else { return true; }
        }
        double E(int i, int j, double x0, double y0)
        {
            double y = 0;
            double x = 0;
            double kvadrl = 0;
            double r = 0;
            double l = 0;
            double azimut=0;
            double polyar = 0;
            double polyar1 = 0;
            double polyar2 = 0;
            double azimut1 = 0;
            int i2 = 0, i3 = 0;
            int j2 = 0;
          y= Data.b/(2*rjad)+i* Data.b/(rjad);
         x= Data.a/(2*vrjadu)+j*Data.a/(vrjadu);
         
          kvadrl = Math.Pow((y - y0), 2) + Math.Pow((x - x0), 2);
            r=Math.Pow(kvadrl+Math.Pow(Data.h1-Data.hr-Data.l-Data.visota,2),0.5);
            l=Math.Pow(kvadrl,0.5);
            if ((Math.Abs(y - y0) <= Data.b / 100) && (Math.Abs(x - x0) <= Data.a / 100))
         {
             azimut = 0;
         }
            else { azimut = 180*Math.Acos((y - y0) / l) / Math.PI; }
         polyar = (Math.Asin(l/r)/Math.PI)*180;
         for (int s1 = 0; s1 < Data.polyarangle.Length;s1++) {
             if (s1 + 1 < Data.polyarangle.Length)
             {
                 if ((polyar > Data.polyarangle[s1]) && (polyar < Data.polyarangle[s1 + 1]))
                 {

                     i2 = s1;
                     i3 = s1 + 1;
                     polyar1 = Data.polyarangle[s1];
                     polyar2 = Data.polyarangle[s1+1];
                     break;
                 }
             }
             else {
                 i2 = s1;
             }
         }
         for (int s1 = 0; s1 < Data.azimutangle.Length; s1++)
         {
             if (Data.azimutangle[s1] >= azimut)
             {
                azimut1 = Data.azimutangle[s1];
                j2 = s1;
                 break;
             }
         }
         double I = 0;
            double E1 = 0;
         if (((Math.Abs(y - y0) <= Data.b / 100) && (Math.Abs(x - x0) <= Data.a / 100))) {
             I = Data.kss[j2, i2];
   E1=I/(Math.Pow(Data.h1-Data.hr-Data.l-Data.visota, 2));
            }
         else { I = Data.kss[j2, i2] + ((Data.kss[j2, i3] - Data.kss[j2, i2]) / (polyar2 - polyar1)) * (polyar - polyar1);
             E1 = (I /Math.Pow((l), 2)) * Math.Cos(Math.PI*polyar/180)*Math.Pow(Math.Sin(Math.PI*polyar/180),2);
         }
        return E1;
        }
        int kolvo1;
        double Ezad;
        int rjad1;
        int vrjadu1;
        double Esum1;
        double Esum11;
           
            List<int> rjadl = new List<int>();
            List<int> vrjadul = new List<int>();
            List<double> Esum = new List<double>();

            bool reshil = false;
            List<int> boleeEzad = new List<int>();
        private void button1_Click(object sender, EventArgs e){
            reshil = false;

            dataGridView1.Visible = true;
           
            rjad = 0;
            vrjadu = 0;
            rjad1 = 0;
            vrjadu1 = 0;
            Ezad =(double) numericUpDown1.Value;
            rjadl.Clear();
            vrjadul.Clear();
            boleeEzad.Clear();
            kolvo1 = (int)Math.Ceiling(Ezad * Data.a * Data.b*(double)Data.kz  / (Data.n*Data.F));
            MessageBox.Show("Первоначально" + " " + kolvo1.ToString()); 
            if (kolvo1 > 3)
            {
                for (int i = 2; i <= (int)Math.Ceiling(Math.Pow((double)kolvo1, 0.5)); i++)
                {
                    if (kolvo1 % i == 0)
                    {
                        rjad1 = i; 
                        vrjadu1 = kolvo1 / rjad1;
                        rjadl.Add(rjad1);
                        vrjadul.Add(vrjadu1);
                    }
                }
                         }
            else {
                rjad1 = 1;
                vrjadu1 = kolvo1 / rjad1;
                rjadl.Add(rjad1);
                vrjadul.Add(vrjadu1);
                
            }
            
rjad1 = 0;
vrjadu1 = 0;
double x01 = 0;
double y01 = 0;
double x02 = 0;
double y02 = 0;
int t1 = 0;
do
{
  
    int men=0;
    int  bol=0;
    for (int t = 0; t < vrjadul.Count; t++) {
        Esum1 = 0;
        Esum11 = 0;
    for (int i1 = 0; i1 < rjadl[t]; i1++)
    {
        for (int j1 = 0; j1 < vrjadul[t]; j1++)
        {
             x01 = 0;
             y01 = 0;
             x02 = 0;
             y02 = 0;
             rjad = rjadl[t];
            vrjadu = vrjadul[t];
            if (rjad > 1)
            {
                if (Data.a / vrjadu >= Data.b / rjad)
                {
                    x01 = Data.a / vrjadu;
                    y01 = Data.b / (2 * rjad);
                    x02 = Data.a / vrjadu;
                    y02 = Data.b / rjad;
                }
                else
                {
                    x01 = Data.a / (2 * vrjadu);
                    y01 = Data.b / rjad;
                    x02 = Data.a / vrjadu;
                    y02 = Data.b / rjad;
                }

            }
            else {
                x01 = Data.a / vrjadu;
                y01 = Data.b / (5 * rjad);
                x02 = Data.a / vrjadu;
                y02 = Data.b / (5 * rjad);
            }
            
            Esum1 += E(i1, j1, x01, y01);
            Esum11 += E(i1, j1, x02, y02);
           
        }
    }
        Esum1 = Esum1 / (double)Data.kz;
        Esum11 = Esum11 / (double)Data.kz;
    if (Esum1 >= Esum11)
    {
        
        Esum.Add(Esum11);
    }
    else {

        Esum.Add(Esum1);
    
    }
    }
     for (int t = 0; t < Esum.Count; t++) {
            if (Esum[t] < Ezad)
        {
            men++;
        }
        else {
            bol++;
          boleeEzad.Add(t); }
       
    }

    if (men == Esum.Count)
    {
        vrjadul.Clear();
        rjadl.Clear();
        //MessageBox.Show("Мало");
        kolvo1++;
        if (prostoe(kolvo1))
        {
            kolvo1++;
        }
        if (kolvo1 > 3)
        {
            for (int i = 2; i <= (int)Math.Ceiling(Math.Pow((double)kolvo1, 0.5)); i++)
            {
                if (kolvo1 % i == 0)
                {
                    rjad1 = i;
                    vrjadu1 = kolvo1 / rjad1;
                    rjadl.Add(rjad1);
                    vrjadul.Add(vrjadu1);
                }
            }
        }
        else
        {
            rjad1 = 1;
            vrjadu1 = kolvo1 / rjad1;
            rjadl.Add(rjad1);
            vrjadul.Add(vrjadu1);
        }
    }
    else {
        double min = double.MaxValue;
        for (int i = 0; i < boleeEzad.Count; i++) {
            if (Esum[boleeEzad[i]] <= min) {
                t1 = boleeEzad[i];
                min = Esum[boleeEzad[i]];
            }
        }
        reshil = true;
    }
    Esum.Clear();
} while ((!reshil)); //проверить

rjad = rjadl[t1];
vrjadu = vrjadul[t1];



MessageBox.Show("Расстановка найдена."+" "+ rjad.ToString()+" "+ "рядов по"+" "+vrjadu.ToString()+" "+"светильников" );
numericUpDown2.Value = (decimal)vrjadu;
numericUpDown3.Value = (decimal)rjad;
k1 = (panel1.Width - 20) / Data.a;
k2 = (panel1.Height - 20) / Data.b;
if (k1 >= k2)
{
    k = (float)k2;
}
else { k = (float)k1; }
gr = panel1.CreateGraphics();
gr.Clear(Color.White);
gr.DrawRectangle(Pens.Red, 10, 10, k * (float)Data.a, k * (float)Data.b);
for (int i = 0; i < rjad; i++)
{
    for (int j = 0; j < vrjadu; j++)
    {
        float y = 10 + k * (float)Data.b / (2 * rjad) + i * k * (float)Data.b / (rjad) - k * (float)Data.dlina / 2;
        float x = 10 + k * (float)Data.a / (2 * vrjadu) + j * k * (float)Data.a / (vrjadu) - k * (float)Data.shirin / 2;
        gr.DrawRectangle(Pens.Green, x, y, k * (float)Data.shirin, k * (float)Data.dlina);
    }

} 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Data.a = 0;
            
            Data.azimutangle1 = 0;
            Data.b = 0;
            Data.balast = 0;
            Data.dlina = 0;
            Data.F = 0;
            Data.fotometr = 0;
            Data.h1 = 0;
            Data.hr = 0;
            Data.l = 0;
            Data.mnojit = 0;
            Data.n = 0;
      
            Data.polyarangle1 = 0;
            Data.shirin = 0;
            Data.systemedin = 0;
            Data.versia = 0;
            Data.visota = 0;
            Data.p = 0;
            this.Hide();

        }
    }
}
