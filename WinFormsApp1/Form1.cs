namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
    }
}
