using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace Auto_Worker
{

    public partial class Form1 : Form
    {
        int rounds = 0;
        int TgMove, MalX, MalY;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );
        public Form1()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void Feed(int burgerX,  int waterX)
        {

            // get mouse position
            System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;

            // create X,Y point (0,0) explicitly with System.Drawing 
            System.Drawing.Point burger = new System.Drawing.Point(burgerX, 254);


            // create X,Y point (0,0) explicitly with System.Drawing 
            System.Drawing.Point water = new System.Drawing.Point(waterX, 254);


            // create X,Y point (0,0) explicitly with System.Drawing 
            System.Drawing.Point confirm = new System.Drawing.Point(350, 725);

            Cursor.Position = burger;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, Convert.ToUInt32(burgerX), 254, 0, 0);
            Thread.Sleep(2000);
            Cursor.Position = confirm;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 350, 725, 0, 0);
            Thread.Sleep(2000);
            Cursor.Position = water;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, Convert.ToUInt32(waterX), 254, 0, 0);
            Thread.Sleep(2000);
            Cursor.Position = confirm;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 350, 725, 0, 0);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            // Some testing
            // MessageBox.Show(Convert.ToString(Convert.ToInt32(textBox1.Text) * 1000));
            timer1.Enabled = true;
            timer1.Stop();
            timer1.Interval = Convert.ToInt32(textBox1.Text) * 60000;
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (rounds > Convert.ToInt32(textBox2.Text))
            {
                timer1.Enabled = false;
                MessageBox.Show("You are done");
                timer1.Stop();
            }
            else
            {
                rounds++;
                int[] indexFed = new int[] { 350, 462, 592, 612, 750, 852 };
                try
                {
                    Feed(indexFed[Convert.ToInt32(comboBox1.Text) - 1], indexFed[Convert.ToInt32(comboBox2.Text) - 1]);
                }
                catch { MessageBox.Show("Correct your data"); }
          
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
            MessageBox.Show("You have stoped the program.");
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            TgMove = 1;
            MalX = e.X;
            MalY = e.Y;
        }

        //Wenn man nicht auf das Panel klickt wird eine Flagge auf falsch gesetzt
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            TgMove = 0;
        }
        //Form wird verschiben falls Panel angeklickt ist und verschoben wird
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (TgMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MalX, MousePosition.Y - MalY);
            }
        }
    }
}
