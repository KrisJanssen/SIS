// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerialTerminalSettingsForm.cs" company="">
//   
// </copyright>
// <summary>
//   The serial terminal settings form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.SerialTerminal
{
    using System;
    using System.IO;
    using System.IO.Ports;
    using System.Windows.Forms;

    /// <summary>
    /// The serial terminal settings form.
    /// </summary>
    public partial class SerialTerminalSettingsForm : Form
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialTerminalSettingsForm"/> class.
        /// </summary>
        public SerialTerminalSettingsForm()
        {
            this.InitializeComponent();

            CommPort com = CommPort.Instance;

            int found = 0;
            string[] portList = com.GetAvailablePorts();
            for (int i = 0; i < portList.Length; ++i)
            {
                string name = portList[i];
                this.comboBox1.Items.Add(name);
                if (name == Settings.Port.PortName)
                {
                    found = i;
                }
            }

            if (portList.Length > 0)
            {
                this.comboBox1.SelectedIndex = found;
            }

            int[] baudRates =
                {
                    100, 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400, 56000, 57600, 115200, 
                    128000, 256000, 0
                };
            found = 0;
            for (int i = 0; baudRates[i] != 0; ++i)
            {
                this.comboBox2.Items.Add(baudRates[i].ToString());
                if (baudRates[i] == Settings.Port.BaudRate)
                {
                    found = i;
                }
            }

            this.comboBox2.SelectedIndex = found;

            this.comboBox3.Items.Add("5");
            this.comboBox3.Items.Add("6");
            this.comboBox3.Items.Add("7");
            this.comboBox3.Items.Add("8");
            this.comboBox3.SelectedIndex = Settings.Port.DataBits - 5;

            foreach (string s in Enum.GetNames(typeof(Parity)))
            {
                this.comboBox4.Items.Add(s);
            }

            this.comboBox4.SelectedIndex = (int)Settings.Port.Parity;

            foreach (string s in Enum.GetNames(typeof(StopBits)))
            {
                this.comboBox5.Items.Add(s);
            }

            this.comboBox5.SelectedIndex = (int)Settings.Port.StopBits;

            foreach (string s in Enum.GetNames(typeof(Handshake)))
            {
                this.comboBox6.Items.Add(s);
            }

            this.comboBox6.SelectedIndex = (int)Settings.Port.Handshake;

            switch (Settings.Option.AppendToSend)
            {
                case Settings.Option.AppendType.AppendNothing:
                    this.radioButton1.Checked = true;
                    break;
                case Settings.Option.AppendType.AppendCR:
                    this.radioButton2.Checked = true;
                    break;
                case Settings.Option.AppendType.AppendLF:
                    this.radioButton3.Checked = true;
                    break;
                case Settings.Option.AppendType.AppendCRLF:
                    this.radioButton4.Checked = true;
                    break;
            }

            this.checkBox1.Checked = Settings.Option.HexOutput;
            this.checkBox2.Checked = Settings.Option.MonoFont;
            this.checkBox3.Checked = Settings.Option.LocalEcho;
            this.checkBox4.Checked = Settings.Option.StayOnTop;
            this.checkBox5.Checked = Settings.Option.FilterUseCase;

            this.textBox1.Text = Settings.Option.LogFileName;
        }

        #endregion

        // OK
        #region Methods

        /// <summary>
        /// The button 1_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Port.PortName = this.comboBox1.Text;
            Settings.Port.BaudRate = int.Parse(this.comboBox2.Text);
            Settings.Port.DataBits = this.comboBox3.SelectedIndex + 5;
            Settings.Port.Parity = (Parity)this.comboBox4.SelectedIndex;
            Settings.Port.StopBits = (StopBits)this.comboBox5.SelectedIndex;
            Settings.Port.Handshake = (Handshake)this.comboBox6.SelectedIndex;

            if (this.radioButton2.Checked)
            {
                Settings.Option.AppendToSend = Settings.Option.AppendType.AppendCR;
            }
            else if (this.radioButton3.Checked)
            {
                Settings.Option.AppendToSend = Settings.Option.AppendType.AppendLF;
            }
            else if (this.radioButton4.Checked)
            {
                Settings.Option.AppendToSend = Settings.Option.AppendType.AppendCRLF;
            }
            else
            {
                Settings.Option.AppendToSend = Settings.Option.AppendType.AppendNothing;
            }

            Settings.Option.HexOutput = this.checkBox1.Checked;
            Settings.Option.MonoFont = this.checkBox2.Checked;
            Settings.Option.LocalEcho = this.checkBox3.Checked;
            Settings.Option.StayOnTop = this.checkBox4.Checked;
            Settings.Option.FilterUseCase = this.checkBox5.Checked;

            Settings.Option.LogFileName = this.textBox1.Text;

            CommPort com = CommPort.Instance;
            com.Open();

            Settings.Write();

            this.Close();
        }

        // Cancel
        /// <summary>
        /// The button 2_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// The button 3_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void button3_Click(object sender, EventArgs e)
        {
            Settings.Option.LogFileName = string.Empty;

            SaveFileDialog fileDialog1 = new SaveFileDialog();

            fileDialog1.Title = "Save Log As";
            fileDialog1.Filter = "Log files (*.log)|*.log|All files (*.*)|*.*";
            fileDialog1.FilterIndex = 2;
            fileDialog1.RestoreDirectory = true;
            fileDialog1.FileName = Settings.Option.LogFileName;

            if (fileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = fileDialog1.FileName;
                if (File.Exists(this.textBox1.Text))
                {
                    File.Delete(this.textBox1.Text);
                }
            }
            else
            {
                this.textBox1.Text = string.Empty;
            }
        }

        #endregion
    }
}