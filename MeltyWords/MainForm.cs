using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MeltyWords
{
    public partial class MainForm : Form
    {
        static RemovePatternForm RemovePatternForm { get; } = new RemovePatternForm();
        FrequencyAnalysis FrequencyAnalysis { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string text;
                    using (var sr = new StreamReader(OpenFileDialog.FileName))
                    {
                        text = sr.ReadToEnd();
                    }
                    FrequencyAnalysis = new FrequencyAnalysis(text);
                    FrequencyAnalysis.IsIgnoreCase = true;
                    DataGridView.Rows.Clear();
                    DataGridView.ColumnCount = 2;
                    DataGridView.Columns[0].HeaderText = "項目";
                    DataGridView.Columns[1].HeaderText = "頻度";
                    var list = FrequencyAnalysis.CharacterFrequencyAnalysis.OrderByDescending(i => i.Value);
                    foreach (var item in list)
                    {
                        DataGridView.Rows.Add(item.Key, item.Value);
                    }
                    Chart.Series.Clear();
                    var ser1 = new Series(OpenFileDialog.FileName, 10);
                    Chart.Series.Add(ser1);
                    var dic = list.ToDictionary(i => i.Key, j => j.Value);
                    Chart.Series[OpenFileDialog.FileName].Points.DataBindXY(dic.Keys, dic.Values);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void CharacterFrequencyAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView.Rows.Clear();
            DataGridView.ColumnCount = 2;
            DataGridView.Columns[0].HeaderText = "項目";
            DataGridView.Columns[1].HeaderText = "頻度";
            FrequencyAnalysis.RemovePattern = RemovePatternForm.textBox1.Text;
            var list = FrequencyAnalysis.CharacterFrequencyAnalysis.OrderByDescending(i => i.Value);
            foreach (var item in list)
            {
                DataGridView.Rows.Add(item.Key, item.Value);
            }
            Chart.Series.Clear();
            var ser = new Series(OpenFileDialog.FileName, 10);
            Chart.Series.Add(ser);
            var dic = list.ToDictionary(i => i.Key, j => j.Value);
            Chart.Series[OpenFileDialog.FileName].Points.DataBindXY(dic.Keys, dic.Values);
            ToolStripStatusLabel.Text = FrequencyAnalysis.Text.Length + "文字";
        }

        private void WordFrequencyAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView.Rows.Clear();
            DataGridView.ColumnCount = 2;
            DataGridView.Columns[0].HeaderText = "項目";
            DataGridView.Columns[1].HeaderText = "頻度";
            FrequencyAnalysis.RemovePattern = RemovePatternForm.textBox2.Text;
            var list = FrequencyAnalysis.WordFrequencyAnalysis.OrderByDescending(i => i.Value);
            foreach (var item in list)
            {
                DataGridView.Rows.Add(item.Key, item.Value);
            }
            Chart.Series.Clear();
            var ser = new Series(OpenFileDialog.FileName, 10);
            Chart.Series.Add(ser);
            var dic = list.ToDictionary(i => i.Key, j => j.Value);
            Chart.Series[OpenFileDialog.FileName].Points.DataBindXY(dic.Keys, dic.Values);
            ToolStripStatusLabel.Text = FrequencyAnalysis.Text.Length + "文字";
        }

        private void JapaneseFrequencyAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView.Rows.Clear();
            DataGridView.ColumnCount = 2;
            DataGridView.Columns[0].HeaderText = "項目";
            DataGridView.Columns[1].HeaderText = "頻度";
            FrequencyAnalysis.RemovePattern = string.Empty;
            var list = FrequencyAnalysis.JapaneseFrequencyAnalysis.OrderByDescending(i => i.Value);
            foreach (var item in list)
            {
                DataGridView.Rows.Add(item.Key, item.Value);
            }
            Chart.Series.Clear();
            var ser = new Series(OpenFileDialog.FileName, 10);
            Chart.Series.Add(ser);
            var dic = list.ToDictionary(i => i.Key, j => j.Value);
            Chart.Series[OpenFileDialog.FileName].Points.DataBindXY(dic.Keys, dic.Values);
            ToolStripStatusLabel.Text = FrequencyAnalysis.Text.Length + "文字";
        }
    }
}
