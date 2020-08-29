using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PhotoViewer
{
    public partial class Form1 : Form
    {
        private string _filePath = Path.Combine(Environment.CurrentDirectory, "image.jpg");
        public bool _firstStart = true;

        public Form1()
        {
            InitializeComponent();
            StartingProgram();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();

            opf.Title = "Wybierz obraz: ";
            opf.Filter = "Obraz  (*.jpg; *.jpeg;) | *.jpg; *.jpeg;";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(_filePath))
                    File.Delete(_filePath);

                Bitmap image = new Bitmap(opf.FileName);
                picBox.Image = image;
                picBox.SizeMode = PictureBoxSizeMode.StretchImage;

                string newImagePath = opf.FileName;

                File.Copy(newImagePath, _filePath);
                
                btnDelete.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Czy na pewno chcesz usunąć to zdjęcie?", "Usuwanie zdjęcia",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                File.Delete(_filePath);
                picBox.Image = null;
                btnDelete.Enabled = false;
            }
        }

        private void StartingProgram()
        {
            if (File.Exists(_filePath) && _firstStart)
            {
                btnDelete.Enabled = true;

                Bitmap image = new Bitmap(_filePath);
                picBox.Image = image;
                picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            _firstStart = false;
        }
    }
}
