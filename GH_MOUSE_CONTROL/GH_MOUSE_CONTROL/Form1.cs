using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Configuration;

namespace GH_MOUSE_CONTROL
{
    public partial class Form1 : Form
    {
        Thread t;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox3.Checked = true;
            t = new Thread(this.loop);
            t.Start();
            timer1.Start();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();


        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label2.Text = "Y AXIS: " + trackBar1.Value.ToString();
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = "SMOOTHING Y: " + trackBar2.Value.ToString();
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            label4.Text = "X AXIS: " + trackBar3.Value.ToString();
        }

        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {
            label5.Text = "SMOOTHING X: " + trackBar4.Value.ToString();
        }

        private void Enable()
        {

            int sleeptime = (int)this.trackBar2.Value;
            int strength = (int)this.trackBar1.Value;
            int sleeptime2 = (int)this.trackBar4.Value;
            int strength2 = (int)this.trackBar3.Value;
            Form1.sleeptime = sleeptime;
            Form1.strength = strength;
            Form1.sleeptime2 = sleeptime2;
            Form1.strength2 = strength2;
        }

        static uint x = 0x2D;
        static uint xx = 0x01;
        public static bool Enabled = true;
        public static int sleeptime = 1;
        public static int strength = 1;
        public static int sleeptime2 = 1;
        public static int strength2 = 0;

        public void loop()
        {
            while (true)
            {
                CheckForIllegalCrossThreadCalls = false;

                if ((Win32.GetKeyState(x) & 0x8000) > 0)
                {
                    Enabled = !Enabled;
                    if (Enabled)
                    {
                        Win32.Beep(900, 500);
                    }
                    else
                    {
                        Win32.Beep(200, 500);
                    }
                }

                if (!Enabled) continue;


                    if (checkBox3.Checked)
                    {
                        xx = 0x02;
                    }
                    else
                    {
                        xx = 0x01;
                    }


                    if ((Win32.GetKeyState(xx) & 0x8000) > 0)
                    {
                        if ((Win32.GetKeyState(0x01) & 0x8000) > 0)
                        {                           
                                Win32.Move(0, strength);
                                Thread.Sleep(sleeptime);
                                Win32.Move(strength2, 0);
                                Thread.Sleep(sleeptime2);
          
                        }
                    }
                
        
        Thread.Sleep(1);
            }

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Enabled)
            {
                label7.Text = "ENABLED";
                label7.ForeColor = Color.LimeGreen;
                Enable();
            }
            else
            {
                label7.Text = "DISABLED";
                label7.ForeColor = Color.Red;

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            t.Abort();
            System.Windows.Forms.Application.ExitThread();

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
