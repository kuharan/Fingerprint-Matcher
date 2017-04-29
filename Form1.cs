using System;
using System.Drawing;
using System.Windows.Forms;
using PatternRecognition.FingerprintRecognition.Core;
using PatternRecognition.FingerprintRecognition.FeatureExtractors;
using PatternRecognition.FingerprintRecognition.Matchers;

namespace Fingerprint_Matcher
{
    public partial class Form1 : Form
    {
     
        public Form1()
        {
            InitializeComponent();
        }

        public string score;
        public string qry;
        public string temp;

        private Bitmap Change_Resolution(string file)
        {
            using (Bitmap bitmap = (Bitmap)Image.FromFile(file))
            {
                using (Bitmap newBitmap = new Bitmap(bitmap))
                {
                    newBitmap.SetResolution(500,500);
                    return newBitmap;
                        }
            }
        }

        private void match(string query, string template)
        {
            Change_Resolution(query);
            Change_Resolution(template);
            
            var fingerprintImg1 = ImageLoader.LoadImage(query);
            var fingerprintImg2 = ImageLoader.LoadImage(template);
            
            var featExtractor = new PNFeatureExtractor() { MtiaExtractor = new Ratha1995MinutiaeExtractor() };
            var features1 = featExtractor.ExtractFeatures(fingerprintImg1);
            var features2 = featExtractor.ExtractFeatures(fingerprintImg2);

           
            var matcher = new PN();
            double similarity = matcher.Match(features1, features2);
            score = similarity.ToString("0.000");
            Console.WriteLine("the matched score is {0}", score);
            if (similarity > 70)
            {
               
                MessageBox.Show("Its a Match !!", "Result", MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("Unsuccessfull !!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        

        private void Form1_Load(object sender, EventArgs e)
        { 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            dlg.DefaultExt = "jpg";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                qry = fileName;
                pictureBox1.ImageLocation = qry;
          
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                temp= fileName;
                pictureBox2.ImageLocation = temp;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            match(qry, temp);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
