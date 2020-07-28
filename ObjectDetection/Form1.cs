using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alturos.Yolo;
using Alturos.Yolo.Model;
using Emgu.CV;
using Emgu.CV.Structure;
using ZedGraph;

namespace ObjectDetection
{
    public partial class Form1 : Form
    {
        VideoCapture capture;
        public Form1()
        {
            InitializeComponent();
            Run();
        }

        private void Run()
        {
            try
            {
                capture = new VideoCapture();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            Application.Idle += ProcessFrame;
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            
            var img = capture.QuerySmallFrame();
            
            pictureBox1.Image = img.ToBitmap();
            Detect();
        }

        private void Detect()
        {
            var configurationDetector = new ConfigurationDetector();
            var config = configurationDetector.Detect();
            var yolo = new YoloWrapper(config);
            using (var memorystream = new MemoryStream())
            {
                pictureBox1.Image.Save(memorystream, ImageFormat.Png);
                var _items = yolo.Detect(memorystream.ToArray()).ToList();
                AddDetailsToPictureBox(pictureBox1, _items);
            }
        }

        private void AddDetailsToPictureBox(PictureBox pictureBox1, List<YoloItem> items)
        {
            var img = pictureBox1.Image;
            var font = new Font("Arial", 18, FontStyle.Bold);
            var brush = new SolidBrush(Color.Red);
            var graphics = Graphics.FromImage(img);
            foreach (var item in items)
            {
                var x = item.X;
                var y = item.Y;
                var width = item.Width;
                var height = item.Height;
                var tung = item.Type;
                var rect = new Rectangle(x, y, width, height);
                var pen = new Pen(Color.LightGreen, 6);
                var point = new Point(x, y);
                graphics.DrawRectangle(pen, rect);
                graphics.DrawString(tung, font, brush, point);
                pictureBox1.Image = img;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
