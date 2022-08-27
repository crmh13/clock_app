namespace clock_app
{
    public partial class Form1 : Form
    {
        private int mouseX;
        private int mouseY;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 500;
            timer1.Enabled = true;
            label1.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;
            label1.Text = String.Format("{0:00}:{1:00}:{2:00}", d.Hour, d.Minute, d.Second);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }

            if (e.KeyCode == Keys.T)
            {
                Form2 f2 = new ();
                f2.Show();
            }
        }


        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - mouseX;
                Top += e.Y - mouseY;
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            TopMost = true;
        }
    }
}