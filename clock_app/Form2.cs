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
        private string? soundFileName;
        
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists("sound.conf"))
            {
                soundFileName = File.ReadAllText("sound.conf");
                if (!File.Exists(soundFileName))
                {
                    MessageBox.Show("タイマー音を選択してください。",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return;
                }
            } else
            {
                MessageBox.Show("タイマー音を選択してください。",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
 
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
                mediaPlayer.settings.setMode("loop", true);
                mediaPlayer.URL = soundFileName;
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

        private void サウンド選択SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "sound files (*.mp3;*.wma;*.wmv;*.wm;*.wav;*.flac)|*.mp3;*.wma;*.wmv;*.wm;*.wav;*.flac";
            openFileDialog1.FileName = "";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText("sound.conf", openFileDialog1.FileName);
            }
        }
    }
}
