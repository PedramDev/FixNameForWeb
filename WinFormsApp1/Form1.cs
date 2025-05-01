using System.Text;
using System.Text.RegularExpressions;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string folderPath = textBox1.Text; // Change this to your folder path

            if (Directory.Exists(folderPath))
            {
                var files = Directory.GetFiles(folderPath);

                foreach (var file in files)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string fileExtension = Path.GetExtension(file);

                    // Replace spaces with hyphens and convert to lowercase
                    string newFileName = fileName.Replace(" ", "-").ToLowerInvariant() + fileExtension;
                    string newFilePath = Path.Combine(folderPath, newFileName);

                    // Rename the file
                    File.Move(file, newFilePath);
                    Console.WriteLine($"Renamed: {file} to {newFilePath}");
                }
            }
            else
            {
                Console.WriteLine("Directory does not exist.");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://svgsprit.es/");
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
                var source_path = textBox2.Text;

                var idList = new List<string>();

                var source_file = File.ReadAllText(source_path);
                var matches = Regex.Matches(source_file, @"(?<=id="")[a-z-0-9]+(?="")", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                foreach (Match match in matches)
                {
                    idList.Add(match.Value);
                }

                var duplicates = idList.GroupBy(s => s)
                                .Where(g => g.Count() > 1)
                                .Select(g => g.Key);

                var sb = new StringBuilder();
                foreach (var duplicate in duplicates)
                {
                    sb.AppendLine(duplicate);
                }

                richTextBox1.Text = sb.ToString();
            }
        }
    }
}
