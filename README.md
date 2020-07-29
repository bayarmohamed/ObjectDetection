# ObjectDetection
'''
 private void ProcessFrame(object sender, EventArgs e)
        {
            try
            {
                var img = capture.QuerySmallFrame().ToImage<Bgr,Byte>();
                var image = img.ToBitmap();
                pictureBox1.Image = image;
                Detect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
           
        }
'''
