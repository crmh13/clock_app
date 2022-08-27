using WMPLib;

namespace clock_app
{
    public partial class Form2 : Form
    {
        private int mouseX;
        private int mouseY;
        private TimeSpan countTime;
        private bool timerStart = false;
        private readonly WindowsMediaPlayer mediaPlayer = new ();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!timerStart)
            {
                int Hour = (int)numericUpDownHour.Value;
                int Minute = (int)numericUpDownMinute.Value;
                int Second = (int)numericUpDownSecond.Value;

                if (Hour == 0 && Minute == 0 && Second == 0)
                {
                    return;
                }

                numericUpDownHour.ReadOnly = true;
                numericUpDownMinute.ReadOnly = true;
                numericUpDownSecond.ReadOnly = true;
                button1.Text = "STOP";

                countTime = new TimeSpan(Hour, Minute, Second);
                timerStart = true;
                timer1.Start();
            } else
            {
                TopMost = false;
                numericUpDownHour.ReadOnly = false;
                numericUpDownMinute.ReadOnly = false;
                numericUpDownSecond.ReadOnly = false;
                button1.Text = "START";
                timerStart = false;
                timer1.Stop();
                mediaPlayer.controls.stop();
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan time_0 = new ();

            countTime -= new TimeSpan(0, 0, 1);
            numericUpDownHour.Value = countTime.Hours;
            numericUpDownMinute.Value = countTime.Minutes;
            numericUpDownSecond.Value = countTime.Seconds;

            if (countTime <= time_0)
            {
                TopMost = true;
                timer1.Stop();
                mediaPlayer.URL = "timer.mp3";
                mediaPlayer.controls.play();
                
            }
        }

        private void numericUpDownHour_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownEscape(sender, e);
        }

        private void numericUpDownMinute_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownEscape(sender, e);
        }

        private void numericUpDownSecond_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownEscape(sender, e);
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownEscape(sender, e);
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - mouseX;
                Top += e.Y - mouseY;
            }
        }

        private void KeyDownEscape(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                mediaPlayer.controls.stop();
                Close();
            }
        }
    }
}
