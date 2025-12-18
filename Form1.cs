using System.Diagnostics;

using NReco.Csv;

namespace blendovideoknife
{
    public partial class Form1 : Form
    {
        DateTime startTime;



        public Form1()
        {
            InitializeComponent();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;

            if (!File.Exists("ffmpeg.exe"))
            {
                AddLog("ERROR: You need ffmpeg.exe in the same folder as this program.");
                AddLog("1. Download it from https://ffmpeg.org");
                AddLog("2. Copy ffmpeg.exe into the same folder as Blendo Video Knife.");
                AddLog("3. Restart Blendo Video Knife.");
                listBox1.BackColor = System.Drawing.Color.Pink;
                return;
            }

            AddLog("Fill time/filename information, then drag video file into this window.");
        }

        //auto populate the next line's timecode
        private void DataGridView1_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (!checkBox1.Checked)
                return;

            if (e.ColumnIndex != 1)
                return;

            int columnIdx = 0;
            int rowIdx = e.RowIndex + 1;
            string newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            dataGridView1.Rows[rowIdx].Cells[columnIdx].Value = newValue;
        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }


        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            //File was dragged in

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length <= 0)
                return;

            if (files.Length > 1)
            {
                AddLog("ERROR: only one video file can be dragged in.");
                return;
            }

            string fileExtension = Path.GetExtension(files[0]);

            if (fileExtension.Contains("txt", StringComparison.InvariantCultureIgnoreCase)
                || fileExtension.Contains("tsv", StringComparison.InvariantCultureIgnoreCase)
                || fileExtension.Contains("csv", StringComparison.InvariantCultureIgnoreCase))
            {
                DragInTextfile(files[0]);
                return;
            }


            StartSplice(files[0]);
        }

        private void DragInTextfile(string file)
        {

            AddLog(string.Empty);
            AddLog("- - - - - - - - - - - - - - -");
            AddLog("Dragged in file:");
            AddLog(file);
            AddLog(string.Empty);

            ClearDatagrid();


            string delineator = string.Empty;
            string rawText = GetFileContents(file);
            int tabIndex = rawText.IndexOf('\t');
            if (tabIndex > 0)
            {
                AddLog("Delineator character = tab");
                delineator = "\t";
            }
            else
            {
                AddLog("Delineator character = comma");
                delineator = ",";
            }


            AddLog(string.Empty);
            AddLog("Reading text file...");

            int lineNumber = 0;
            using (var streamRdr = new StreamReader(file))
            {
                var csvReader = new CsvReader(streamRdr, delineator);
                while (csvReader.Read())
                {
                    try
                    {
                        dataGridView1.Rows.Add(GetCSVValue(csvReader[0]), GetCSVValue(csvReader[1]), GetCSVValue(csvReader[2]));
                        lineNumber++;
                    }
                    catch (Exception ex)
                    {
                        AddLog_Invoked("CSV: failed to parse line #{0}. Error: {1}", (lineNumber + 1).ToString(), ex.Message);
                    }
                }
            }


            AddLog("Done. Imported text file.");
        }

        string GetCSVValue(object value)
        {
            if (value == null)
                return string.Empty;

            return value.ToString();
        }





        private void StartSplice(string filename)
        {
            //Process the file....

            startTime = DateTime.Now;

            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;


            AddLog(string.Empty);
            AddLog("- - - - - - - - - - - - - - - {0} - - - - - - - - - - - - - - -", startTime.ToShortTimeString());
            AddLog("Dragged in file:");
            AddLog(filename);
            AddLog(string.Empty);

            if (HasErrors())
                return;

            //check if there is padding.
            int padding = 0;
            if (!string.IsNullOrWhiteSpace(textBox_padding.Text))
            {
                if (!int.TryParse(textBox_padding.Text, out padding))
                {
                    AddLog("ERROR: padding value '{0}' is invalid. Must be integer value.", textBox_padding.Text);
                    return;
                }
                else
                {
                    if (padding < 0)
                    {
                        AddLog("ERROR: padding value has to be larger than 0.");
                        return;
                    }
                }
            }


            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value == null
                    || dataGridView1.Rows[i].Cells[1].Value == null
                    || dataGridView1.Rows[i].Cells[2].Value == null)
                    continue;

                if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells[0].Value.ToString())
                    || string.IsNullOrEmpty(dataGridView1.Rows[i].Cells[1].Value.ToString())
                    || string.IsNullOrEmpty(dataGridView1.Rows[i].Cells[2].Value.ToString()))
                    continue;

                string startStr = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string endStr = dataGridView1.Rows[i].Cells[1].Value.ToString();
                string newfilename = dataGridView1.Rows[i].Cells[2].Value.ToString();

                string dir = Path.GetDirectoryName(filename);

                if (padding > 0)
                {
                    //has padding.

                    string[] timeformats = { @"hh\:mm\:ss", @"h\:mm\:ss", @"mm\:ss", @"m\:ss", "ss", "s" };

                    TimeSpan startSpan = TimeSpan.ParseExact(startStr, timeformats, System.Globalization.CultureInfo.InvariantCulture);
                    startSpan = startSpan.Subtract(new TimeSpan(0, 0, padding));

                    if (startSpan < TimeSpan.Zero)
                    {
                        //is negative time
                        startSpan = TimeSpan.Zero;
                    }

                    TimeSpan endSpan = TimeSpan.ParseExact(endStr, timeformats, System.Globalization.CultureInfo.InvariantCulture);
                    endSpan = endSpan.Add(new TimeSpan(0, 0, padding));

                    startStr = startSpan.ToString(@"hh\:mm\:ss");
                    endStr = endSpan.ToString(@"hh\:mm\:ss");

                }


                string newExtension = Path.GetExtension(newfilename);
                if (string.IsNullOrWhiteSpace(newExtension))
                {
                    //the newfilename does NOT have an extension
                    string oldExtension = Path.GetExtension(filename);
                    newfilename += oldExtension;
                    newfilename = Path.Combine(dir, newfilename);
                }
                else
                {
                    //the new filename HAS an extension already
                    newfilename = Path.Combine(dir, newfilename);
                }




                string args = string.Format("-ss {0} -to {1} -y -i \"{2}\" -c:v copy -c:a copy \"{3}\"",
                    startStr, endStr, filename, newfilename);
                AddLog_Invoked(string.Empty);
                AddLog_Invoked("********************* #{0} *********************", (i + 1).ToString());
                AddLog_Invoked(string.Empty);
                AddLog_Invoked("Args: {0}", args);

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "ffmpeg.exe";
                startInfo.Arguments = args;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.RedirectStandardInput = true;
                startInfo.CreateNoWindow = true;
                Process proc = new Process();

                try
                {
                    proc.StartInfo = startInfo;
                    proc.Start();

                    while (!proc.StandardError.EndOfStream)
                    {
                        string line = proc.StandardError.ReadLine();
                        AddLog_Invoked("    " + line);
                    }
                }
                catch (Exception err)
                {
                    AddLog_Invoked("------------------------------");
                    AddLog_Invoked(string.Format("ERROR: {0}", err));
                    AddLog_Invoked("------------------------------");
                }
            }



            TimeSpan delta = DateTime.Now.Subtract(startTime);
            AddLog(" ");
            AddLog("Done. (Total time: {0} seconds)", Math.Round(delta.TotalSeconds, 1).ToString());
        }

        private bool HasErrors()
        {
            bool hasError = false;

            //If there's a start time but every other field is blank, just erase the start time
            //this is to simplify/QOL the final line being auto-populated but half-empty
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (!FieldIsNullOrEmpty(dataGridView1.Rows[i].Cells[0].Value)
                    && FieldIsNullOrEmpty(dataGridView1.Rows[i].Cells[1].Value)
                    && FieldIsNullOrEmpty(dataGridView1.Rows[i].Cells[2].Value))
                {
                    //empty it out.
                    dataGridView1.Rows[i].Cells[0].Value = string.Empty;
                    continue;
                }


            }


            int totalFields = 0;

            //check for blank fields.
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                int blanks = 0;

                if (dataGridView1.Rows[i].Cells[0].Value == null)
                    blanks++;
                else if (string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells[0].Value.ToString()))
                    blanks++;

                if (dataGridView1.Rows[i].Cells[1].Value == null)
                    blanks++;
                else if (string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells[1].Value.ToString()))
                    blanks++;

                if (dataGridView1.Rows[i].Cells[2].Value == null)
                    blanks++;
                else if (string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells[2].Value.ToString()))
                    blanks++;



                if (blanks > 0 && blanks < 3)
                {
                    hasError = true;
                    AddLog("ERROR: line #{0} has a blank field.", (i + 1).ToString());
                }

                if (blanks <= 0)
                {
                    totalFields++;
                }
            }

            if (totalFields <= 0)
            {
                hasError = true;
                AddLog("ERROR: you need at least 1 start/end time & filename.");
            }



            //check timestamps
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                TimeSpan span;

                if (dataGridView1.Rows[i].Cells[0].Value == null)
                    continue;
                else if (string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells[0].Value.ToString()))
                    continue;
                else if (!TimeSpan.TryParse(dataGridView1.Rows[i].Cells[0].Value.ToString(), out span))
                    continue;

                if (dataGridView1.Rows[i].Cells[1].Value == null)
                    continue;
                if (string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells[1].Value.ToString()))
                    continue;
                else if (!TimeSpan.TryParse(dataGridView1.Rows[i].Cells[1].Value.ToString(), out span))
                    continue;

                string startStr = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string endStr = dataGridView1.Rows[i].Cells[1].Value.ToString();

                startStr = startStr.Trim();
                endStr = endStr.Trim();

                int colonIdx1 = startStr.IndexOf(':');
                int colonIdx2 = endStr.IndexOf(':');

                if (colonIdx1 < 0 || colonIdx2 < 0)
                {
                    hasError = true;
                    AddLog("ERROR: line {0} has a bad time field.", i.ToString());
                }
            }

            return hasError;
        }

        bool FieldIsNullOrEmpty(object value)
        {
            if (value == null)
                return true;

            if (string.IsNullOrWhiteSpace(value.ToString()))
                return true;

            return false;
        }

        private void AddLog_Invoked(string text, params string[] args)
        {
            MethodInvoker mi = delegate () { AddLog(text, args); };
            this.Invoke(mi);
        }

        private void AddLog(string text, params string[] args)
        {
            string displaytext = string.Format(text, args);

            listBox1.Items.Add(displaytext);

            //scroll list down
            int nItems = (int)(listBox1.Height / listBox1.ItemHeight);
            listBox1.TopIndex = listBox1.Items.Count - nItems;

            this.Update();
            this.Refresh();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("BLENDO VIDEO KNIFE\nTool for splicing a video into multiple segments.\nDec 2025\n1. Enter time+filename info into the grid.\n2. Drag video file into window.\n\n'Start time/End time' format:\nH:MM:SS or M:SS\n\n'Padding time' = will extend the start/end time by this amount of seconds.\n\n'Auto-populate next start time' = will automatically use the 'end time' to fill in the next line's 'start time'.\n\nNote: additionally, a CSV (Comma-Separated values) or TSV (Tab-Separated Values) text file can be dragged in to populate the information fields.");
        }

        private void contextMenuStrip_listbox_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ClearDatagrid();
        }

        private void ClearDatagrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.DataSource = null;

            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;

            dataGridView1.Refresh();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            listBox1.BackColor = System.Drawing.Color.White;

            string output = string.Empty;
            foreach (object item in listBox1.Items)
            {
                output += item.ToString() + Environment.NewLine;
            }

            if (string.IsNullOrWhiteSpace(output))
            {
                return;
            }

            Clipboard.SetText(output);
        }

        public string GetFileContents(string filepath)
        {
            string output = string.Empty;

            try
            {
                using (FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        output = reader.ReadToEnd(); //dump file contents into a string.
                    }
                }
            }
            catch (Exception e)
            {
                AddLog_Invoked("Error reading text file. ({0})", e.Message);
                return string.Empty;
            }

            return output;
        }

        private void copyAllEntriesToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.SelectAll();

                DataObject dataObj = dataGridView1.GetClipboardContent();
                if (dataObj != null)
                {
                    Clipboard.SetDataObject(dataObj, true);
                }

                dataGridView1.ClearSelection();

                AddLog(string.Empty);
                AddLog("Copied to clipboard.");
            }
            catch (Exception err)
            {
                AddLog("ERROR: {0}", err.Message);
            }
        }

        // -- end --
    }


}
