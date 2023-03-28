using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Shell;
using System.IO;
using System.Windows.Controls;
using SQLite;
using System.Collections.Generic;

namespace MonitoringProtokolu {
    public partial class MainWindow : Window {
        NotifyIcon icon = new NotifyIcon();
        private static String DBPath = @"./data";
        private static String DBFilePath = @$"{DBPath}/DBFile.db3";
        private static String DBDirectoryPath = @$"{DBPath}/DBDirectory.db3";
        public MainWindow() {
            InitializeComponent();
            WindowChrome.SetWindowChrome(this, new WindowChrome { CaptionHeight = 0 });

            gridFile.Visibility = Visibility.Visible;

            doSystrayIcon();

            createDBPath();

            createDBFile();
            createDBDirectory();

            loadDBFile();
            loadDBDirectory();
            
        }

        private void createDBPath() {
            if (!Directory.Exists(DBPath)) {
                Directory.CreateDirectory(DBPath);
            }
        }

        private void loadDBFile() {
            dataGridFile.Items.Clear();
            SQLiteConnection db = new SQLiteConnection(DBFilePath);
            List<DBFile> DBs = db.Table<DBFile>().ToList();
            if (DBs.Count > 0) {
                foreach (DBFile DB in DBs) {
                    dataGridFile.Items.Add(DB);
                }
            }
            db.Close();
        }

        private void loadDBDirectory() {
            dataGridDirectory.Items.Clear();
            SQLiteConnection db = new SQLiteConnection(DBDirectoryPath);
            List<DBDirectory> DBs = db.Table<DBDirectory>().ToList();
            if (DBs.Count > 0) {
                foreach (DBDirectory DB in DBs) {
                    dataGridDirectory.Items.Add(DB);
                }
            }
            db.Close();
        }

        private void doSystrayIcon() { 
            icon.Icon = new Icon("icon.ico");
            icon.Text = "Monitoring Protokolu";
            icon.Visible = false;
            icon.Click += new System.EventHandler(icon_Click);
        }

        private void createDBFile() {
            if (!File.Exists(DBFilePath)) {
                SQLiteConnection db = new SQLiteConnection(DBFilePath);

                db.CreateTable<DBFile>();

                db.Close();
            }
        }

        private void createDBDirectory() {
            if (!File.Exists(DBDirectoryPath)) {
                SQLiteConnection db = new SQLiteConnection(DBDirectoryPath);

                db.CreateTable<DBDirectory>();

                db.Close();
            }
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

        private void btnFileEdit_Click(object sender, RoutedEventArgs e) {

        }

        private void btnFileRemove_Click(object sender, RoutedEventArgs e) {
            //DataGrid SelectedItem = (DataGrid)dataGridFile.SelectedItem;
            //System.Windows.MessageBox.Show($"ID of selected row: {dataGridFile}");
        }

        private void btnFileCopy_Click(object sender, RoutedEventArgs e) {
        }

        private void btnFileCancel_Click(object sender, RoutedEventArgs e) {

        }

        private void btnFileAdd_Click(object sender, RoutedEventArgs e) {
            DBFile tmp = new DBFile(txtBoxFilePath.Text, txtBoxFileEmail.Text, Int32.Parse(txtBoxFileInterval.Text), Int32.Parse(txtBoxFileMaxSize.Text), Int32.Parse(txtBoxFileMaxLines.Text), (Boolean)CheckBoxFileTurnOn.IsChecked);

            SQLiteConnection db = new SQLiteConnection(DBFilePath);

            db.Insert(tmp);

            db.Close();
            loadDBFile();
        }

        private void btnDirectoryEdit_Click(object sender, RoutedEventArgs e) {

        }

        private void btnDirectoryRemove_Click(object sender, RoutedEventArgs e) {
            
        }

        private void btnDirectoryCopy_Click(object sender, RoutedEventArgs e) {
        }

        private void btnDirectoryCancel_Click(object sender, RoutedEventArgs e) {

        }

        private void btnDirectoryAdd_Click(object sender, RoutedEventArgs e) {
            DBDirectory tmp = new DBDirectory(txtBoxDirectoryPath.Text, txtBoxDirectoryEmail.Text, Int32.Parse(txtBoxDirectoryInterval.Text), Int32.Parse(txtBoxDirectoryMaxSize.Text), Int32.Parse(txtBoxDirectoryMaxLines.Text), (Boolean)CheckBoxDirectoryTurnOn.IsChecked);
            
            SQLiteConnection db = new SQLiteConnection(DBDirectoryPath);

            db.Insert(tmp);

            db.Close();
            loadDBDirectory();
        }
    }
}