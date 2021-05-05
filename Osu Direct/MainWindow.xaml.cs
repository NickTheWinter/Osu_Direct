using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Osu_Direct
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            
        }

        //DispatcherTimer timer = new DispatcherTimer();

        //private void Application_Startup(object sender, KeyEventArgs e)
        //{
        //foreach (Process clsProcess in Process.GetProcesses())
        //{
        //    if (clsProcess.ProcessName.Contains("Osu"))
        //    {
        //if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.D)
        //{



        //}


        //private void OnTimedEvent(object sender, EventArgs e)
        //{
        //    if (Convert.ToString(Process.GetCurrentProcess()).Contains("osu!"))
        //    {
        //        if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D))
        //        {
        //            Web_Browser web_browser = new Web_Browser();
        //            web_browser.Show();

        //        }
        //    }
        //}

        WindowState prevState;
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
            }
            else
                prevState = WindowState;
        }
        private void TaskbarIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            Show();
            WindowState = prevState;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Hide();
            //TextBox.Text = Convert.ToString(Application.Current.Windows.Count);

            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //foreach (Process clsProcess in Process.GetProcesses())
            //{
                //if (clsProcess.ProcessName.Contains("osu"))
                //{
                //if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D))
                //{
                //    Web_Browser web_browser = new Web_Browser();
                //    web_browser.Show();

                //}
                //}

                //if(Convert.ToString(Process.GetCurrentProcess()) == "osu!")
                //{
                //    if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D))
                //    {
                //        Web_Browser web_browser = new Web_Browser();
                //        web_browser.Show();

                //    }
                //}


            //}
        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        //    timer.Tick += new EventHandler(OnTimedEvent);
        //}

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 9000;

        //Modifiers:
        private const uint MOD_NONE = 0x0000; //(none)
        private const uint MOD_ALT = 0x0001; //ALT
        private const uint MOD_CONTROL = 0x0002; //CTRL
        private const uint MOD_SHIFT = 0x0004; //SHIFT
        private const uint MOD_WIN = 0x0008; //WINDOWS
        private const uint VK_D = 0x0044;
        //CAPS LOCK:
        private const uint VK_CAPITAL = 0x14;


        private IntPtr _windowHandle;
        private HwndSource _source;
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            _windowHandle = new WindowInteropHelper(this).Handle;
            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);

            RegisterHotKey(_windowHandle, HOTKEY_ID, MOD_CONTROL, VK_D); //CTRL + D
        }
        Web_Browser web_browser = new Web_Browser();
        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            int vkey = (((int)lParam >> 16) & 0xFFFF);
                            Process[] proclist = Process.GetProcessesByName("osu!");
                            foreach(Process proc in proclist)
                            {
                                
                                if (vkey == VK_D && web_browser.IsLoaded == false && Convert.ToString(proc).Contains("osu!"))
                                {
                                    
                                    web_browser.Show();
                                }
                                if (vkey == VK_D && web_browser.IsLoaded == true && Convert.ToString(proc).Contains("osu!"))
                                {

                                    web_browser.Activate();
                                }
                                if(vkey == VK_D && web_browser.IsVisible == false && Convert.ToString(proc).Contains("osu!"))
                                {
                                    web_browser.Visibility = Visibility.Visible;
                                }
                                handled = true;
                                
                            }
                            break;

                    }
                    break;
            }
            return IntPtr.Zero;
        }

        protected override void OnClosed(EventArgs e)
        {
            _source.RemoveHook(HwndHook);
            UnregisterHotKey(_windowHandle, HOTKEY_ID);
            base.OnClosed(e);
        }

        private void AutoStart_Checked(object sender, RoutedEventArgs e)
        {
            if (Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", @"Osu Direct 
                                                 C:\Users\Admin\source\repos\Osu Direct\Osu Direct\bin\Debug\net5.0-windows\Osu Direct.exe", null) == null)
            {
                Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", true);
                Key.SetValue("Osu Direct", @"C:\Users\Admin\source\repos\Osu Direct\Osu Direct\bin\Debug\net5.0-windows\Osu Direct.exe");
                Key.Close();
            }
        }

        private void AutoStart_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", @"Osu Direct 
                                                 C:\Users\Admin\source\repos\Osu Direct\Osu Direct\bin\Debug\net5.0-windows\Osu Direct.exe", null) == null)
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.DeleteValue("Osu Direct", false);
                key.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", @"Osu Direct 
                                                 C:\Users\Admin\source\repos\Osu Direct\Osu Direct\bin\Debug\net5.0-windows\Osu Direct.exe", null) == null)
            {
                AutoStart.IsChecked = true;
            }
            else
                AutoStart.IsChecked = false;
        }
    }
}

