using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Serial_Port
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // GET ports and display it on the combobox
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
            if (ports.Length > 0)
            {
                comboBox1.SelectedIndex = 0;
            }

            serialPort = new SerialPort
            {
                BaudRate = 9600, // Example baud rate, adjust as necessary
                DataBits = 8,    // Number of data bits per byte
                Parity = Parity.None,
                StopBits = StopBits.One
            };

            serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(ports);
            if (ports.Length > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem != null)
                {
                    string selectedPort = comboBox1.SelectedItem.ToString();
                    if (!serialPort.IsOpen)
                    {
                        serialPort.PortName = selectedPort;
                        serialPort.Open();
                        MessageBox.Show("Serial port opened successfully.");
                        textBox1.Text = "";
                        label2.Text = serialPort.PortName;
                    }
                }
                else
                {
                    MessageBox.Show("Please select a COM port from the list.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening serial port: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    MessageBox.Show("Serial port closed successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error closing serial port: {ex.Message}");
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort.ReadLine();
                this.Invoke(new MethodInvoker(() => textBox1.AppendText(data + Environment.NewLine)));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error receiving serial port data: {ex.Message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            base.OnFormClosing(e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
