using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MAS
{
    public partial class DESMainForm : Form
    {

        public DESMainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           /// <summary>
            /// 2D area  (a cellular automata space)
            /// </summary>
            Inhabitant[,] ca, ca1;
            /// <summary>
            /// List of viruses
            /// </summary>
            List<Virus>[] viruses;
            /// <summary>
            /// Communication agent (responsible for sharing enviroment information)
            /// </summary>
            Expert expert;


            Statistic stats;
    
            int time = (int)numericUpDownTime.Value;
            int h = (int)numericUpDownH.Value;
            int w = (int)numericUpDownW.Value;
            int lifetime = (int)numericUpDownLifetime.Value;
            float inhRate = (float)numericUpDownInhRate.Value;
            float virRate = (float)numericUpDownVirRate.Value;

            float infectionRate = -1;
            float recoveryRate = -1;
            if (!checkBoxRndInfection.Checked)
                infectionRate = (float)numericUpDownInfectionRate.Value;
            if (!checkBoxRndRecovery.Checked)
                recoveryRate = (float)numericUpDownRecoveryRate.Value;
            int maxNumbExp = 10; //10000
            pb_info.Maximum = maxNumbExp;
            pb_info.Value = 0;

           for (int exp = 0; exp < maxNumbExp; exp++)
            {

                viruses = new List<Virus>[h];
                ca = new Inhabitant[h, w];
                ca1 = new Inhabitant[h, w];
                expert = new Expert(ref ca, ref ca1, ref viruses, virRate, inhRate, time, checkBoxCognitive.Checked, infectionRate, recoveryRate, lifetime + 1, cb_const_viruses_nmb.Checked);

                if (checkBoxInterOutput.Checked)
                    stats = StartGame(expert, ca, ca1, viruses, h, w, virRate, inhRate, time);
                else
                    stats = StartGameWithoutOutput(expert, ca, ca1, viruses, h, w, virRate, inhRate, time);
                if (exp == 0)
                {
                    stats.WriteResults(true, expert, inhRate, virRate);
                }else
                {
                    stats.WriteResults(false, expert, inhRate, virRate);
                }
                //stats.WriteResults(expert, inhRate, virRate);
                pb_info.Value +=1;
            }
        }

        void Print(Inhabitant [,] ca, List<Virus>[] viruses, int time, int[] virusesChanges)
        {
            AppendText(richTextBoxCA, Color.Black, "--------------------------------\n");
            AppendText(richTextBoxCA, Color.Black, "t = " + time.ToString() + "\n");
            AppendText(richTextBoxCA, Color.Black, "# of new viruses " + virusesChanges[0].ToString() + ", # of deleted viruses " + virusesChanges[1].ToString() + "\n");
            AppendText(richTextBoxCA, Color.Black, "--------------------------------\n");
            string [] row = new string[ca.GetLength(1) + 1];
            for (int i = 0; i < ca.GetLength(0); i++)
            {
                int rtbLength = richTextBoxCA.Text.Length;
                for (int j = 0; j < row.GetLength(0); j++)
                    row[j] = " X ";
                if (viruses[i] != null)
                    for (int j = 0; j < viruses[i].Count; j++)
                        row[viruses[i][j].Y] = "V" + viruses[i][j].Id.ToString();
                        
                for (int j = 0; j < ca.GetLength(1); j++)
                    if (ca[i,j] != null)
                        row[j] = ca[i, j].Infected ? "D" + ca[i, j].Id.ToString() : "S" + ca[i, j].Id.ToString();
                row[ca.GetLength(1)] = "\n";

                Dictionary<char, Color> dictionary = new Dictionary<char, Color>();

                dictionary.Add('D', Color.Orange);
                dictionary.Add('S', Color.Green);
                dictionary.Add(' ', Color.LightGray);
                dictionary.Add('V', Color.Red);
                dictionary.Add('\n', Color.Black);

                foreach (string c in row)
                    AppendText(richTextBoxCA, dictionary[c[0]], (c).ToString());
            }
        }

        void PrintInfo(Inhabitant [,] ca, List<Virus>[] viruses)
        {    
            AppendText(richTextBoxCA, Color.Blue, "INITIAL PARAMETERS\n");
            AppendText(richTextBoxCA, Color.Blue, "--------------------------------\n");
            for (int i = 0; i < ca.GetLength(0); i++)
                if (viruses[i] != null)
                    for (int j = 0; j < viruses[i].Count; j++)
                         AppendText(richTextBoxCA, Color.Blue, "V" + viruses[i][j].Id.ToString()  + "[" 
                            + viruses[i][j].X.ToString() + "," + viruses[i][j].Y.ToString() + "] = " 
                            + viruses[i][j].Contagiousness + ", lifetime = " + viruses[i][j].Lifetime+"\n");
            AppendText(richTextBoxCA, Color.Blue, "\n");
        }

        Statistic StartGame(Expert expert, Inhabitant [,] ca, Inhabitant[,] ca1, List<Virus>[] viruses, int h, int w, float virRate, float inhRate, int time)
        {
            int [] virusesChanges = new int[2];
            PrintInfo(ca, viruses);
            Print(ca,viruses, 0, new int[2]{(int)(virRate * w* h), 0});
            if (checkBoxCognitive.Checked)
            {
                for (int t = 0; t < time / 2; t++)
                {
                    virusesChanges = expert.Next01Cog();
                    Print(ca1, viruses, 2 * t + 1, virusesChanges);
                    virusesChanges = expert.Next10Cog();
                    Print(ca, viruses, 2 * t + 2, virusesChanges);
                }
                if (time % 2 == 1)
                {
                    virusesChanges = expert.Next01Cog();
                    Print(ca1, viruses, time, virusesChanges);
                }
            }
            else
            {
                for (int t = 0; t < time / 2; t++)
                {
                    virusesChanges = expert.Next01();
                    Print(ca1, viruses, 2 * t + 1, virusesChanges);
                    virusesChanges = expert.Next10();
                    Print(ca, viruses, 2 * t + 2, virusesChanges);
                }
                if (time % 2 == 1)
                {
                    virusesChanges = expert.Next01();
                    Print(ca1, viruses, time, virusesChanges);
                }
            }
            return expert.stats;
        }

        Statistic  StartGameWithoutOutput(Expert expert, Inhabitant[,] ca, Inhabitant[,] ca1, List<Virus>[] viruses, int h, int w, float virRate, float inhRate, int time)
        {
            Stopwatch sw = Stopwatch.StartNew();

            int[] virusesChanges = new int[2];

            PrintInfo(ca, viruses);
            Print(ca, viruses, 0, new int[2] { (int)(virRate * w * h), 0 });
            if (checkBoxCognitive.Checked)
            {
                sw.Start();
                for (int t = 0; t < time / 2; t++)
                {
                    virusesChanges = expert.Next01Cog();
                    virusesChanges = expert.Next10Cog();

                }
                if (time % 2 == 1)
                {
                    virusesChanges = expert.Next01Cog();
                    sw.Stop();
                    Print(ca1, viruses, time, virusesChanges);
                }
                else
                {
                    sw.Stop();
                    Print(ca, viruses, time, virusesChanges);
                }
            }
            else
            {
                sw.Start();
                for (int t = 0; t < time / 2; t++)
                {
                    virusesChanges = expert.Next01();
                    virusesChanges = expert.Next10();
                }
                if (time % 2 == 1)
                {
                    virusesChanges = expert.Next01();
                    sw.Stop();
                    Print(ca1, viruses, time, virusesChanges);
                }
                else
                {
                    sw.Stop();
                    Print(ca, viruses, time, virusesChanges);
                }
            }
            expert.stats.ElapsedTime = (long)sw.Elapsed.TotalMilliseconds;
            return expert.stats;
        }

            void AppendText(RichTextBox box, Color color, string text)
        {
            text = text.PadLeft(5, ' ');
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;

            box.Select(start, end - start);
                box.SelectionColor = color;
            box.SelectionLength = 0;
        }

        private void numericUpDown_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.richTextBoxCA.Clear();
        }

        private void checkBoxRndRecovery_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRndRecovery.Checked)
                numericUpDownRecoveryRate.Enabled = false;
            else
                numericUpDownRecoveryRate.Enabled = true;
        }

        private void checkBoxRndInfection_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRndInfection.Checked)
                numericUpDownInfectionRate.Enabled = false;
            else
                numericUpDownInfectionRate.Enabled = true;
        }

    }
}
