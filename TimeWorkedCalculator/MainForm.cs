/*
 * Program Name: Time Calculator 
 * Programmer: Oleksii Butakov
 * Purpose: This program was made by me in the purpose to make my life easier and to automate the process of calculating time I spent working. 
 * How it's works: To calculate the time, you need to copy history logs from the employer's website (see example in the project's root folder) and paste it into the left textbox. 
 *                 Then press "Compute", and the program will return detailed information, such as: average time per task spent, amount of tasks submitted, total time spent. 
 * Last Modified: 6/28/2017
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace TimeWorkedCalculator
{
    public partial class MainForm : Form
    {
        String line;
        String prevLine;

        DateTime time;
        DateTime prevTime;
        
        TimeSpan span;

        double totalMinutes;

        int numIterations = 0;

        const short JUNK_CHARACTERS = 20;
        const short MAX_TIME_PER_TASK = 14;

        public MainForm()
        {
            InitializeComponent();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtBinary.BackColor = Color.Black;
            txtBinary.ForeColor = Color.ForestGreen;
            
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            bool isFirstIteration = false;
            short counter = 0;
            totalMinutes = 0;

            if (txtInput.Lines.Length != 0)
            {
                for (int i = 0; i < txtInput.Lines.Length; i++)
                {
                    line = txtInput.Lines[i];
                    if (prevLine == null)
                    {
                        prevLine = line.Remove(line.Length - JUNK_CHARACTERS);
                        prevTime = DateTime.Parse(prevLine);
                        isFirstIteration = true;
                    }
                    else
                    {
                        if (line.Length > JUNK_CHARACTERS)
                        {
                            line = line.Remove(line.Length - JUNK_CHARACTERS);

                            time = DateTime.Parse(line);

                            span = prevTime.Subtract(time);

                            txtLogs.AppendText("#" + (++counter).ToString() + Environment.NewLine);
                            txtLogs.AppendText(prevTime.ToShortTimeString() + Environment.NewLine);
                            txtLogs.AppendText(time.ToShortTimeString() + Environment.NewLine);

                            prevTime = time;
                            isFirstIteration = false;
                        }
                        else
                            continue;
                    }


                    numIterations++;

                    if (!isFirstIteration)
                    {
                        if (span.Hours == 0 && span.Minutes < MAX_TIME_PER_TASK)
                        {
                            if (span.Minutes != 0)
                                totalMinutes += span.Minutes;
                            else
                                totalMinutes++;

                            txtLogs.AppendText("Time Spent: " + span.Hours + "h " + span.Minutes + "m " + "---- ADDED");
                            txtLogs.AppendText(Environment.NewLine + Environment.NewLine);
                        }
                        else
                        {
                            txtLogs.AppendText("Time Spent: " + span.Hours + "h " + span.Minutes + "m" + "---- DECLINED");
                            txtLogs.AppendText(Environment.NewLine + Environment.NewLine);
                        }
                    }
                }
                // casting total minutes (int) into (TimeSpan data type 
                span = TimeSpan.FromMinutes(totalMinutes);

                // Writing down logs 
                txtLogs.AppendText("==========================");
                txtLogs.AppendText(Environment.NewLine);
                txtLogs.AppendText("Total Time: " + span.ToString(@"hh\:mm") + " hrs");
                txtLogs.AppendText(Environment.NewLine);
                txtLogs.AppendText("Tasks submitted: " + counter.ToString());
                txtLogs.AppendText(Environment.NewLine);
                txtLogs.AppendText("Task/Time Ratio: " + (TimeSpan.FromMinutes(span.TotalMinutes / counter)).ToString(@"mm\.ss") + " m.");
                txtLogs.AppendText(Environment.NewLine);
                txtLogs.AppendText("Iterations: " + numIterations.ToString());
                txtLogs.AppendText(Environment.NewLine);

                // Displaying output
                txtTasksSubmitted.Text = counter.ToString();
                txtAvgTime.Text = (TimeSpan.FromMinutes(span.TotalMinutes / counter)).ToString(@"mm\.ss") + " m";
                txtHours.Text = span.ToString(@"hh\:mm") + " hrs";
            }
            else
                txtLogs.AppendText("ERROR: Input Textbox is empty");
        }
    }
}

