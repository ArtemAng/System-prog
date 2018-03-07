using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyFileWithProgressBar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*|Txt files (*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(ofd.FileName))
            {
                tbFrom.Text = ofd.FileName;
            }
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            if (!string.IsNullOrEmpty(tbFrom.Text))
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    tbWhere.Text = sfd.FileName + Path.GetExtension(tbFrom.Text);
                }
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            btnEnabled(false);
            var p = new Progress<double>(percent => tbPercent.Text = percent.ToString());
            Task.Run(() => DoProcessing(p));
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            progressBarWorker.Value = 0;
            tbPercent.Text = string.Empty;
            btnEnabled(true);
        }

        public void DoProcessing(IProgress<double> progress)
        {
            if (tbFrom.Text == null || tbWhere.Text == null) return;
            byte[] buffer = new byte[4096];
            try
            {
                using (var reader = new FileStream(tbFrom.Text, FileMode.Open, FileAccess.Read))
                {
                    double sizeReader = reader.Length;
                    this.Invoke(new Action(() =>
                    {
                        progressBarWorker.Maximum = (int)Math.Round(sizeReader / buffer.Length);
                    }));
                    using (FileStream writer = new FileStream(tbWhere.Text, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        int perc = 0;
                        int percentProgress = 0;
                        this.Invoke(new Action(() =>
                        {
                            perc = progressBarWorker.Maximum / 100;
                        }));
                        int size = 0;
                        int count = 0;
                        do
                        {
                            size = reader.Read(buffer, 0, buffer.Length);
                            writer.Write(buffer, 0, size);
                            Invoke(new Action(() =>
                            {
                                count++;
                                if (increment(ref count,ref perc))
                                {
                                    progress.Report(++percentProgress);
                                    count = 0;
                                }
                                progressBarWorker.Increment(1);
                            }));

                        } while (size > 0);
                    }
                }
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static bool increment(ref int count, ref int percent) 
        {
            return count % percent == 0;
        }

        private void btnEnabled(bool value) 
        {
            this.btnCopy.Enabled = value;
        }

    }
}
