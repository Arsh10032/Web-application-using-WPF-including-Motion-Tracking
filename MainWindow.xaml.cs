using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace MotionTrackingWPF
{
    public partial class MainWindow : Window
    {
        private string videoPath = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSelectVideo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Video files|*.mp4;*.avi;*.mov";
            
            if (dialog.ShowDialog() == true)
            {
                videoPath = dialog.FileName;
                txtVideoPath.Text = Path.GetFileName(videoPath);
                btnProcess.IsEnabled = true;
                statusBarText.Text = "Video selected";
            }
        }

        private void BtnProcess_Click(object sender, RoutedEventArgs e)
        {
            btnProcess.Content = "Processing...";
            btnProcess.IsEnabled = false;
            statusBarText.Text = "Processing video...";
            
            System.Threading.Thread.Sleep(1000);
            
            txtStatus.Text = "Done! Points: 100";
            btnExport.IsEnabled = true;
            btnProcess.Content = "Process";
            btnProcess.IsEnabled = true;
            statusBarText.Text = "Processing complete";
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "JSON files|*.json";
            dialog.FileName = "tracking.json";
            
            if (dialog.ShowDialog() == true)
            {
                string data = "{\n  \"video\": \"" + videoPath + "\",\n  \"points\": 100\n}";
                File.WriteAllText(dialog.FileName, data);
                statusBarText.Text = "Export complete";
                MessageBox.Show("Exported successfully!");
            }
        }
    }
}