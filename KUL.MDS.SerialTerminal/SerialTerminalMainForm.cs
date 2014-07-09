// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerialTerminalMainForm.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The serial terminal main form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.SerialTerminal
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// The serial terminal main form.
    /// </summary>
    public partial class SerialTerminalMainForm : Form
    {
        #region Fields

        /// <summary>
        /// The filter string.
        /// </summary>
        private string filterString = string.Empty;

        /// <summary>
        /// The lines.
        /// </summary>
        private ArrayList lines = new ArrayList();

        /// <summary>
        /// The mono font.
        /// </summary>
        private Font monoFont;

        /// <summary>
        /// The orig font.
        /// </summary>
        private Font origFont;

        /// <summary>
        /// Partial line for AddData().
        /// </summary>
        private Line partialLine = null;

        /// <summary>
        /// context menu for the output window
        /// </summary>
        private ContextMenu popUpMenu;

        /// <summary>
        /// The received color.
        /// </summary>
        private Color receivedColor = Color.Green;

        /// <summary>
        /// The scrolling.
        /// </summary>
        private bool scrolling = true;

        /// <summary>
        /// The sent color.
        /// </summary>
        private Color sentColor = Color.Blue;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialTerminalMainForm"/> class.
        /// </summary>
        public SerialTerminalMainForm()
        {
            this.InitializeComponent();

            this.splitContainer1.FixedPanel = FixedPanel.Panel1;
            this.splitContainer2.FixedPanel = FixedPanel.Panel2;

            this.AcceptButton = this.button5; // Send
            this.CancelButton = this.button4; // Close

            this.outputList_Initialize();

            Settings.Read();
            this.TopMost = Settings.Option.StayOnTop;

            // let form use multiple fonts
            this.origFont = this.Font;
            FontFamily ff = new FontFamily("Courier New");
            this.monoFont = new Font(ff, 8, FontStyle.Regular);
            this.Font = Settings.Option.MonoFont ? this.monoFont : this.origFont;

            CommPort com = CommPort.Instance;
            com.StatusChanged += this.OnStatusChanged;
            com.DataReceived += this.OnDataReceived;
            com.Open();
        }

        #endregion

        // delegate used for Invoke
        #region Delegates

        /// <summary>
        /// The string delegate.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        internal delegate void StringDelegate(string data);

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Handle data received event from serial port.
        /// </summary>
        /// <param name="dataIn">
        /// The data In.
        /// </param>
        public void OnDataReceived(string dataIn)
        {
            // Handle multi-threading
            if (this.InvokeRequired)
            {
                this.Invoke(new StringDelegate(this.OnDataReceived), new object[] { dataIn });
                return;
            }

            // pause scrolling to speed up output of multiple lines
            bool saveScrolling = this.scrolling;
            this.scrolling = false;

            // if we detect a line terminator, add line to output
            int index;
            while (dataIn.Length > 0 && ((index = dataIn.IndexOf("\r")) != -1 || (index = dataIn.IndexOf("\n")) != -1))
            {
                string StringIn = dataIn.Substring(0, index);
                dataIn = dataIn.Remove(0, index + 1);

                this.logFile_writeLine(this.AddData(StringIn).Str);
                this.partialLine = null; // terminate partial line
            }

            // if we have data remaining, add a partial line
            if (dataIn.Length > 0)
            {
                this.partialLine = this.AddData(dataIn);
            }

            // restore scrolling
            this.scrolling = saveScrolling;
            this.outputList_Scroll();
        }

        /// <summary>
        /// Update the connection status
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        public void OnStatusChanged(string status)
        {
            // Handle multi-threading
            if (this.InvokeRequired)
            {
                this.Invoke(new StringDelegate(this.OnStatusChanged), new object[] { status });
                return;
            }

            this.textBox1.Text = status;
        }

        /// <summary>
        /// output string to log file
        /// </summary>
        /// <param name="stringOut">
        /// string to output
        /// </param>
        public void logFile_writeLine(string stringOut)
        {
            if (Settings.Option.LogFileName != string.Empty)
            {
                Stream myStream = File.Open(
                    Settings.Option.LogFileName, 
                    FileMode.Append, 
                    FileAccess.Write, 
                    FileShare.Read);
                if (myStream != null)
                {
                    StreamWriter myWriter = new StreamWriter(myStream, Encoding.UTF8);
                    myWriter.WriteLine(stringOut);
                    myWriter.Close();
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on closed.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnClosed(EventArgs e)
        {
            CommPort com = CommPort.Instance;
            com.Close();

            base.OnClosed(e);
        }

        /// <summary>
        /// Add data to the output.
        /// </summary>
        /// <param name="StringIn">
        /// </param>
        /// <returns>
        /// The <see cref="Line"/>.
        /// </returns>
        private Line AddData(string StringIn)
        {
            string StringOut = this.PrepareData(StringIn);

            // if we have a partial line, add to it.
            if (this.partialLine != null)
            {
                // tack it on
                this.partialLine.Str = this.partialLine.Str + StringOut;
                this.outputList_Update(this.partialLine);
                return this.partialLine;
            }

            return this.outputList_Add(StringOut, this.receivedColor);
        }

        /// <summary>
        /// Prepare a string for output by converting non-printable characters.
        /// </summary>
        /// <param name="StringIn">
        /// input string to prepare.
        /// </param>
        /// <returns>
        /// output string.
        /// </returns>
        private string PrepareData(string StringIn)
        {
            // The names of the first 32 characters
            string[] charNames =
                {
                    "NUL", "SOH", "STX", "ETX", "EOT", "ENQ", "ACK", "BEL", "BS", "TAB", "LF", "VT", "FF", 
                    "CR", "SO", "SI", "DLE", "DC1", "DC2", "DC3", "DC4", "NAK", "SYN", "ETB", "CAN", "EM", 
                    "SUB", "ESC", "FS", "GS", "RS", "US", "Space"
                };

            string StringOut = string.Empty;

            foreach (char c in StringIn)
            {
                if (Settings.Option.HexOutput)
                {
                    StringOut = StringOut + string.Format("{0:X2} ", (int)c);
                }
                else if (c < 32 && c != 9)
                {
                    StringOut = StringOut + "<" + charNames[c] + ">";

                    // Uglier "Termite" style
                    // StringOut = StringOut + String.Format("[{0:X2}]", (int)c);
                }
                else
                {
                    StringOut = StringOut + c;
                }
            }

            return StringOut;
        }

        /// <summary>
        /// Show settings dialog
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.TopMost = false;

            SerialTerminalSettingsForm form2 = new SerialTerminalSettingsForm();
            form2.ShowDialog();

            this.TopMost = Settings.Option.StayOnTop;
            this.Font = Settings.Option.MonoFont ? this.monoFont : this.origFont;
        }

        /// <summary>
        /// Clear the output window
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.outputList_ClearAll();
        }

        /// <summary>
        /// Show about dialog
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.TopMost = false;

            AboutBox about = new AboutBox();
            about.ShowDialog();

            this.TopMost = Settings.Option.StayOnTop;
        }

        /// <summary>
        /// Close the application
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Send command
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void button5_Click(object sender, EventArgs e)
        {
            string command = this.textBox3.Text;
            this.textBox3.Text = string.Empty;
            this.textBox3.Focus();

            if (command.Length > 0)
            {
                CommPort com = CommPort.Instance;
                com.Send(command);

                if (Settings.Option.LocalEcho)
                {
                    this.outputList_Add(command + "\n", this.sentColor);
                }
            }
        }

        /// <summary>
        /// send file to serial port
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.RestoreDirectory = false;
            dialog.Title = "Select a file";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string text = System.IO.File.ReadAllText(dialog.FileName);

                CommPort com = CommPort.Instance;
                com.Send(text);

                if (Settings.Option.LocalEcho)
                {
                    this.outputList_Add(
                        "SendFile " + dialog.FileName + "," + text.Length.ToString() + " byte(s)\n", 
                        this.sentColor);
                }
            }
        }

        /// <summary>
        /// toggle scrolling
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void button7_Click(object sender, EventArgs e)
        {
            this.scrolling = !this.scrolling;
            this.outputList_Scroll();
        }

        /// <summary>
        /// add a new line to output window
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <param name="color">
        /// The color.
        /// </param>
        /// <returns>
        /// The <see cref="Line"/>.
        /// </returns>
        private Line outputList_Add(string str, Color color)
        {
            Line newLine = new Line(str, color);
            this.lines.Add(newLine);

            if (this.outputList_ApplyFilter(newLine.Str))
            {
                this.outputList.Items.Add(newLine);
                this.outputList_Scroll();
            }

            return newLine;
        }

        /// <summary>
        /// check to see if filter matches string
        /// </summary>
        /// <param name="s">
        /// string to check
        /// </param>
        /// <returns>
        /// true if matches filter
        /// </returns>
        private bool outputList_ApplyFilter(string s)
        {
            if (this.filterString == string.Empty)
            {
                return true;
            }
            else if (s == string.Empty)
            {
                return false;
            }
            else if (Settings.Option.FilterUseCase)
            {
                return s.IndexOf(this.filterString) != -1;
            }
            else
            {
                string upperString = s.ToUpper();
                string upperFilter = this.filterString.ToUpper();
                return upperString.IndexOf(upperFilter) != -1;
            }
        }

        /// <summary>
        /// clear the output window
        /// </summary>
        private void outputList_ClearAll()
        {
            this.lines.Clear();
            this.partialLine = null;

            this.outputList.Items.Clear();
        }

        /// <summary>
        /// clear selected in output window
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void outputList_ClearSelected(object sender, EventArgs e)
        {
            this.outputList.ClearSelected();
            this.outputList.SelectedItem = -1;
        }

        /// <summary>
        /// copy selection in output window to clipboard
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void outputList_Copy(object sender, EventArgs e)
        {
            int iCount = this.outputList.SelectedItems.Count;
            if (iCount > 0)
            {
                string[] source = new string[iCount];
                for (int i = 0; i < iCount; ++i)
                {
                    source[i] = ((Line)this.outputList.SelectedItems[i]).Str;
                }

                string dest = string.Join("\r\n", source);
                Clipboard.SetText(dest);
            }
        }

        /// <summary>
        /// copy all lines in output window
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void outputList_CopyAll(object sender, EventArgs e)
        {
            int iCount = this.outputList.Items.Count;
            if (iCount > 0)
            {
                string[] source = new string[iCount];
                for (int i = 0; i < iCount; ++i)
                {
                    source[i] = ((Line)this.outputList.Items[i]).Str;
                }

                string dest = string.Join("\r\n", source);
                Clipboard.SetText(dest);
            }
        }

        /// <summary>
        /// draw item with color in output window
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void outputList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index >= 0 && e.Index < this.outputList.Items.Count)
            {
                Line line = (Line)this.outputList.Items[e.Index];

                // if selected, make the text color readable
                Color color = line.ForeColor;
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    color = Color.Black; // make it readable
                }

                e.Graphics.DrawString(line.Str, e.Font, new SolidBrush(color), e.Bounds, StringFormat.GenericDefault);
            }

            e.DrawFocusRectangle();
        }

        /// <summary>
        /// Initialize the output window
        /// </summary>
        private void outputList_Initialize()
        {
            // owner draw for listbox so we can add color
            this.outputList.DrawMode = DrawMode.OwnerDrawFixed;
            this.outputList.DrawItem += new DrawItemEventHandler(this.outputList_DrawItem);
            this.outputList.ClearSelected();

            // build the outputList context menu
            this.popUpMenu = new ContextMenu();
            this.popUpMenu.MenuItems.Add("&Copy", new EventHandler(this.outputList_Copy));
            this.popUpMenu.MenuItems[0].Visible = true;
            this.popUpMenu.MenuItems[0].Enabled = false;
            this.popUpMenu.MenuItems[0].Shortcut = Shortcut.CtrlC;
            this.popUpMenu.MenuItems[0].ShowShortcut = true;
            this.popUpMenu.MenuItems.Add("Copy All", new EventHandler(this.outputList_CopyAll));
            this.popUpMenu.MenuItems[1].Visible = true;
            this.popUpMenu.MenuItems.Add("Select &All", new EventHandler(this.outputList_SelectAll));
            this.popUpMenu.MenuItems[2].Visible = true;
            this.popUpMenu.MenuItems[2].Shortcut = Shortcut.CtrlA;
            this.popUpMenu.MenuItems[2].ShowShortcut = true;
            this.popUpMenu.MenuItems.Add("Clear Selected", new EventHandler(this.outputList_ClearSelected));
            this.popUpMenu.MenuItems[3].Visible = true;
            this.outputList.ContextMenu = this.popUpMenu;
        }

        /// <summary>
        /// refresh the output window
        /// </summary>
        private void outputList_Refresh()
        {
            this.outputList.BeginUpdate();
            this.outputList.Items.Clear();
            foreach (Line line in this.lines)
            {
                if (this.outputList_ApplyFilter(line.Str))
                {
                    this.outputList.Items.Add(line);
                }
            }

            this.outputList.EndUpdate();
            this.outputList_Scroll();
        }

        /// <summary>
        /// Scroll to bottom of output window
        /// </summary>
        private void outputList_Scroll()
        {
            if (this.scrolling)
            {
                int itemsPerPage = (int)(this.outputList.Height / this.outputList.ItemHeight);
                this.outputList.TopIndex = this.outputList.Items.Count - itemsPerPage;
            }
        }

        /// <summary>
        /// select all lines in output window
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void outputList_SelectAll(object sender, EventArgs e)
        {
            this.outputList.BeginUpdate();
            for (int i = 0; i < this.outputList.Items.Count; ++i)
            {
                this.outputList.SetSelected(i, true);
            }

            this.outputList.EndUpdate();
        }

        /// <summary>
        /// Enable/Disable copy selection in output window
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void outputList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.popUpMenu.MenuItems[0].Enabled = this.outputList.SelectedItems.Count > 0;
        }

        /// <summary>
        /// Update a line in the output window.
        /// </summary>
        /// <param name="line">
        /// line to update
        /// </param>
        private void outputList_Update(Line line)
        {
            // should we add to output?
            if (this.outputList_ApplyFilter(line.Str))
            {
                // is the line already displayed?
                bool found = false;
                for (int i = 0; i < this.outputList.Items.Count; ++i)
                {
                    int index = (this.outputList.Items.Count - 1) - i;
                    if (line == this.outputList.Items[index])
                    {
                        // is item visible?
                        int itemsPerPage = (int)(this.outputList.Height / this.outputList.ItemHeight);
                        if (index >= this.outputList.TopIndex && index < (this.outputList.TopIndex + itemsPerPage))
                        {
                            // is there a way to refresh just one line
                            // without redrawing the entire listbox?
                            // changing the item value has no effect
                            this.outputList.Refresh();
                        }

                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    // not found, so add it
                    this.outputList.Items.Add(line);
                }
            }
        }

        /// <summary>
        /// toggle connection status
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void textBox1_Click(object sender, MouseEventArgs e)
        {
            CommPort com = CommPort.Instance;
            if (com.IsOpen)
            {
                com.Close();
            }
            else
            {
                com.Open();
            }

            this.outputList.Focus();
        }

        /// <summary>
        /// Change filter
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.filterString = this.textBox2.Text;
            this.outputList_Refresh();
        }

        #endregion

        /// <summary>
        /// Class to keep track of string and color for lines in output window.
        /// </summary>
        private class Line
        {
            #region Fields

            /// <summary>
            /// The fore color.
            /// </summary>
            public Color ForeColor;

            /// <summary>
            /// The str.
            /// </summary>
            public string Str;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Line"/> class.
            /// </summary>
            /// <param name="str">
            /// The str.
            /// </param>
            /// <param name="color">
            /// The color.
            /// </param>
            public Line(string str, Color color)
            {
                this.Str = str;
                this.ForeColor = color;
            }

            #endregion
        };
    }
}