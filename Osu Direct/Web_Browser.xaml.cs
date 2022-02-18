using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;

namespace Osu_Direct
{

    public partial class Web_Browser : Window
    {
        public Web_Browser()
        {
            InitializeComponent();
        }


        DispatcherTimer timer = new DispatcherTimer();
        private void Web_Browser_KeyDown(object sender, KeyboardEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Escape))
            {
                this.Hide();
                
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if(webView.CanGoBack)
            {
                webView.GoBack();
            }
            
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            if(webView.CanGoForward)
            {
                webView.GoForward();
            }
        }

        private void webView_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {
            addressBar.Text = Convert.ToString(webView.CoreWebView2.Source);
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(@$"C:\Users\{Environment.UserName}\Downloads", "*.osz");//Input your downloading directory
            ProcessStartInfo psStartInfo = new ProcessStartInfo(@$"C:\Users\{Environment.UserName}\AppData\Local\osu!\osu!.exe");//Input directory of Osu! with osu!.exe
            psStartInfo.WorkingDirectory = @$"C:\Users\{Environment.UserName}\AppData\Local\osu!\";//Input directory of Osu!
            foreach (string i in files)
            {
                if (File.Exists(i))
                {
                    Process.Start(psStartInfo.FileName, i);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Start();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            webView.Stop();
            timer.Stop();

        }
    }
}
