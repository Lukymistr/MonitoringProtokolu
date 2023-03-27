using System;
using Microsoft.Win32;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Shell;

namespace MonitoringProtokolu {
    public partial class MainWindow : Window {
        NotifyIcon icon = new NotifyIcon();
        public MainWindow() {
            InitializeComponent();
            WindowChrome.SetWindowChrome(this, new WindowChrome { CaptionHeight = 0 });

            gridFile.Visibility = Visibility.Visible;

            icon.Icon = new Icon("icon.ico");
            icon.Text = "Monitoring Protokolu";
            icon.Visible = false;
            icon.Click += new System.EventHandler(icon_Click);

            DataGridFile John = new DataGridFile();
            John.FileID = 1;
            John.FilePath = "C:\\Users\\Lukáš Patejdl\\Pictures\\Saved Pictures\\thumb-1920-909641.png";
            John.FileEmail = "kamo@kamo.cz";
            John.FileInterval = "00:00:05:00";
            John.FileMaxSize = 1;
            John.FileMaxLines = 1;
            John.FileTurnOn = new CheckBox();
            John.FileEdit = new Button() {};
            John.FileRemove = new Button() {};
            John.FileCopy = new Button() {};
            dataGridFile.Items.Add( John );
            dataGridFile.Items.Add(John);
            dataGridFile.Items.Add(John);

        }

        private void icon_Click(object sender, System.EventArgs e) {
            Show();
            icon.Visible = false;
        }

        private void Window_StateChanged(object sender, EventArgs e) {
            if (WindowState == WindowState.Maximized) {
                WindowState = WindowState.Normal;
                Left = 0;
                Top = 0;
                Width = SystemParameters.PrimaryScreenWidth;
                Height = SystemParameters.PrimaryScreenHeight;
                WindowChrome.SetWindowChrome(this, null);
            }
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left) {
                if (e.ClickCount == 2) {
                    changeSize();
                } else {
                    if (!(Width == SystemParameters.PrimaryScreenWidth && Height == SystemParameters.PrimaryScreenHeight)) {
                        DragMove();
                    }

                }
            }
        }

        private void changeSize() {
            if (Width == SystemParameters.PrimaryScreenWidth && Height == SystemParameters.PrimaryScreenHeight) {
                Width = MinWidth;
                Height = MinHeight;
                Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
                Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
                WindowChrome.SetWindowChrome(this, new WindowChrome { CaptionHeight = 0 });
            } else {
                WindowChrome.SetWindowChrome(this, null);
                WindowState = WindowState.Normal;
                Left = 0;
                Top = 0;
                Width = SystemParameters.PrimaryScreenWidth;
                Height = SystemParameters.PrimaryScreenHeight;
            }
        }

        private void btnTitleBarMinimize_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void btnTitleBarResize_Click(object sender, RoutedEventArgs e) {
            changeSize();
        }
        private void btnTitleBarExit_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void btnFiles_Click(object sender, RoutedEventArgs e) {
            gridFile.Visibility = Visibility.Visible;
            gridDirectory.Visibility = Visibility.Hidden;
            gridGlobalSettings.Visibility = Visibility.Hidden;
            gridSMTP.Visibility = Visibility.Hidden;
        }

        private void btnDirectory_Click(object sender, RoutedEventArgs e) {
            gridFile.Visibility = Visibility.Hidden;
            gridDirectory.Visibility = Visibility.Visible;
            gridGlobalSettings.Visibility = Visibility.Hidden;
            gridSMTP.Visibility = Visibility.Hidden;
        }

        private void btnGlobalSettings_Click(object sender, RoutedEventArgs e) {
            gridFile.Visibility = Visibility.Hidden;
            gridDirectory.Visibility = Visibility.Hidden;
            gridGlobalSettings.Visibility = Visibility.Visible;
            gridSMTP.Visibility = Visibility.Hidden;
        }

        private void btnSmtpSettings_Click(object sender, RoutedEventArgs e) {
            gridFile.Visibility = Visibility.Hidden;
            gridDirectory.Visibility = Visibility.Hidden;
            gridGlobalSettings.Visibility = Visibility.Hidden;
            gridSMTP.Visibility = Visibility.Visible;
        }

        private void btnTurnOnOff_Click(object sender, RoutedEventArgs e) {

        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e) {
            Hide();
            icon.Visible = true;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void btnFileChoosePath_Click(object sender, RoutedEventArgs e) {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog() {
                Title = "Vybrat soubor",
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "Files|*.*"
            };
            if (openFileDialog.ShowDialog() == true) {
                txtBoxFilePath.Text = openFileDialog.FileName;
            }
        }

        private void btnDirectoryChoosePath_Click(object sender, RoutedEventArgs e) {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK) {
                txtBoxDirectoryPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnGlobalSettingslogChoosePath_Click(object sender, RoutedEventArgs e) {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK) {
                txtBoxGlobalSettingslogPath.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
    class DataGridFile {

        public int FileID { get; set; }
        public String FilePath { get; set; }
        public String FileEmail { get; set; }
        public String FileInterval { get; set; }

        public int FileMaxSize { get; set; }
        public int FileMaxLines { get; set; }
        public CheckBox FileTurnOn { get; set; }
        public Button FileEdit { get; set; }
        public Button FileRemove { get; set; }
        public Button FileCopy { get; set; }

    }
}