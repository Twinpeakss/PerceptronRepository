using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using OxyPlot.WindowsForms;
using OxyPlot;
using OxyPlot.Series;

namespace Perceptron_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            

        }

       
        // w1(t+1)=w1(t)-alpha*x1*(y-e)
        // w2(t+1)=w2(t)-alpha*x2*(y-e)
        // T(t+1)=T(t)+alpha *(y-e)
        // else  
        // S=w1*x1+w2*x2-T

      


        int[,] inputSamples = new int[,]{
                                        {1,1 },
                                        {-1,1 },
                                        {-1,-1 },
                                        {1,-1 }};

        int[] designed_otput = new int[] { 1, 1, 0, 0 };

        double alpha = 0.1;
        int y = 0;
        Random rn = new Random();

        double[] Results;
        int global_epoch = 1;
        double w1;
        double w2;
        double Threshold;

        double[,] tab = new double[4, 2];
        double x1, x2, k;
        private void button1_Click(object sender, EventArgs e)
        {
            Results = new double[4];
            double Threshold = rn.NextDouble() * 2 - 1.0;
            double w1 = rn.NextDouble() * 2 - 1.0;
            double w2 = rn.NextDouble() * 2 - 1.0;
            int condition_counter = 0;
            int equals_counter = 0;

            while (equals_counter != 4 && condition_counter != 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    double S = w1 * inputSamples[i, 0] + w2 * inputSamples[i, 1] - Threshold;
                    if (S > 0)
                    {
                        y = 1;
                    }
                    else
                    {
                        y = 0;
                    }

                    Results[i] = y;
                    if (y != designed_otput[i])
                    {
                        w1 = w1 - alpha * inputSamples[i, 0] * (y - designed_otput[i]);
                        w2 = w2 - alpha * inputSamples[i, 1] * (y - designed_otput[i]);
                        Threshold = Threshold + alpha * (y - designed_otput[i]);
                    }
                    else
                    {
                        condition_counter++;
                    }

                    equals_counter++;

                    if (equals_counter == 4 && condition_counter != 4)
                    {
                        equals_counter = 0;
                        condition_counter = 0;
                    }
                    global_epoch++;
                }

                //Console.WriteLine("w1=" + w1);
                //Console.WriteLine("w2=" + w2);
                //Console.WriteLine("Threshold=" + Threshold);
            }
            bool flag = true;
            for (int i = 0; i < 4; i++)
            {
                if (Results[i] != designed_otput[i])
                {
                    flag = false;
                    break;
                }
            }

            textBox1.Text = "w1=" + w1 + Environment.NewLine;
            textBox1.Text += "w2=" + w2 + Environment.NewLine;
            textBox1.Text += "Threshold=" + Threshold + Environment.NewLine;
            textBox1.Text += "flag:" + flag + Environment.NewLine;
            textBox1.Text += "Epoch:" + global_epoch + Environment.NewLine;
            //////////////////////////////////////////////////////////////////

            textBox1.Text += "x1:" + x1 + Environment.NewLine;
            textBox1.Text += "x2=" + x2 + Environment.NewLine;



            //x2 = Threshold / w2; //dla 0

            //x1 = Threshold / w1;


            int j = -100;    
            tab[0, 0] = j;
            tab[0, 1] = (Threshold / w2) - ((j * w1 / w2));//x1=j

            j = 100;

            tab[1, 0] = j;
            tab[1, 1] = (Threshold / w2) - ((j * w1 / w2));//x1=j








            

        }
        private void button2_Click(object sender, EventArgs e)
        {          
            PlotView pv = new PlotView();
            pv.Location = new Point(0,0);
            pv.Size = new Size(550, 500);
                             
            this.Controls.Add(pv);

            pv.Model = new PlotModel { Title = "Perceptron's separation line" };
            FunctionSeries fs = new FunctionSeries();
           
            
            for(int i = 0; i < 2; i++) 
            {              
                fs.Points.Add(new OxyPlot.DataPoint(tab[i,0], tab[i, 1]));              
            }

           
            pv.Model.Series.Add(fs);

            ScatterSeries sc1 = new ScatterSeries { Title="1", MarkerStroke=OxyColor.FromRgb(255, 1, 0), MarkerType = MarkerType.Plus};
            ScatterSeries sc2 = new ScatterSeries { Title = "1", MarkerStroke = OxyColor.FromRgb(255, 1, 0), MarkerType = MarkerType.Plus };
            ScatterSeries sc3 = new ScatterSeries { Title = "0", MarkerStroke = OxyColor.FromRgb(0, 1, 0), MarkerType = MarkerType.Plus };
            ScatterSeries sc4 = new ScatterSeries { Title = "0", MarkerStroke = OxyColor.FromRgb(0, 1, 0), MarkerType = MarkerType.Plus };
            sc1.Points.Add(new ScatterPoint(inputSamples[0, 0], inputSamples[0, 1]));
            sc2.Points.Add(new ScatterPoint(inputSamples[1, 0], inputSamples[1, 1]));
            sc3.Points.Add(new ScatterPoint(inputSamples[2, 0], inputSamples[2, 1]));
            sc4.Points.Add(new ScatterPoint(inputSamples[3, 0], inputSamples[3, 1]));
           
            pv.Model.Series.Add(sc1);
            pv.Model.Series.Add(sc2);
            pv.Model.Series.Add(sc3);
            pv.Model.Series.Add(sc4);

        }

       

    }
}
