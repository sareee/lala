using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace server
{
    public partial class Form1 : Form
    {
        Socket socketserver = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket socketclient = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void getmess()
        {
            try
            {
                while (true)
                {
                    byte[] arr = new Byte[1024];
                    int c = socketclient.Receive(arr);
                    if (c > 0)
                    {
                        listBox1.Items.Add("client : " + Encoding.Unicode.GetString(arr, 0, c));

                    }

                }


            }
            catch { }


        }
        private void button3_Click(object sender, EventArgs e)
        {
            IPEndPoint ipendportserver = new IPEndPoint(IPAddress.Parse(textBox2.Text), int.Parse(textBox1.Text));
            socketserver.Bind(ipendportserver);
            socketserver.Listen(1);
            socketclient = socketserver.Accept();
            Thread t = new Thread(new ThreadStart(getmess));
            t.Start();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Byte [] arr= new Byte [1024];
           arr = Encoding.Unicode.GetBytes(textBox3.Text);
            socketclient.Send(arr);
            listBox1.Items.Add("me :" + textBox3.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {
                if (socketclient != null)
                {
                    socketclient.Shutdown(SocketShutdown.Both);
                    
                Environment.Exit(Environment.ExitCode);
                Application.Exit();
                }
                if (socketserver != null)
                {
                    socketserver.Shutdown(SocketShutdown.Both);

                    Environment.Exit(Environment.ExitCode);
                    Application.Exit();
                }
               

            }
            catch
            {
                MessageBox.Show("adame etesal");

                Environment.Exit(Environment.ExitCode);
                Application.Exit();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (socketclient != null)
                {
                    socketclient.Shutdown(SocketShutdown.Both);
                }
                if (socketserver != null)
                {
                    socketserver.Shutdown(SocketShutdown.Both);
                }
                Environment.Exit(Environment.ExitCode);
                Application.Exit();
            }
            catch
            {
                MessageBox.Show("adame etesal");
                Environment.Exit(Environment.ExitCode);
                Application.Exit();

            }
        }
    }
}
