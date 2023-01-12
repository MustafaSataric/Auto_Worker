using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace Auto_Worker
{

    public partial class Form1 : Form
    {
        int rounds = 0;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public Form1()
        {
            InitializeComponent();

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
            System.Drawing.Point confirm = new System.Drawing.Point(483, 614);

            Cursor.Position = burger;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, Convert.ToUInt32(burgerX), 254, 0, 0);
            Thread.Sleep(2000);
            Cursor.Position = confirm;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 483, 614, 0, 0);
            Thread.Sleep(2000);
            Cursor.Position = water;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, Convert.ToUInt32(waterX), 254, 0, 0);
            Thread.Sleep(2000);
            Cursor.Position = confirm;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 483, 614, 0, 0);
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
                int[] indexFed = new int[] { 350, 462, 592, 612, 719, 852 };
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
    }
}
