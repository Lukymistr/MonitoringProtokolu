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
using System.Net.Mail;

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
            // vše vypnout / zapnout
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
            btnFileCopy_Click(sender, e);
            btnFileAdd.Content = "uložit";
        }

        private void btnFileRemove_Click(object sender, RoutedEventArgs e) {
            if (System.Windows.MessageBox.Show("Opravdu Smazat?", "Dotaz", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.No) {
                var DBSelectedRow = (DBFile)dataGridFile.SelectedItem;
                SQLiteConnection db = new SQLiteConnection(DBFilePath);
                db.Delete(DBSelectedRow);
                db.Close();
                loadDBFile();
            }
        }

        private void btnFileCopy_Click(object sender, RoutedEventArgs e) {
            var DBSelectedRow = (DBFile)dataGridFile.SelectedItem;
            txtBoxFilePath.Text = DBSelectedRow.path;
            txtBoxFileEmail.Text = DBSelectedRow.email;
            txtBoxFileInterval.Text = DBSelectedRow.interval.ToString();
            txtBoxFileMaxSize.Text = DBSelectedRow.maxSize.Split(" ")[0];
            comBoxFileSizeUnits.Text = DBSelectedRow.maxSize.Split(" ")[1];
            txtBoxFileMaxLines.Text = DBSelectedRow.maxLines.ToString();
            CheckBoxFileTurnOn.IsChecked = DBSelectedRow.turnOn;

        }

        private void btnFileCancel_Click(object sender, RoutedEventArgs e) {
            txtBoxFilePath.Text = null;
            txtBoxFileEmail.Text = null;
            txtBoxFileInterval.Text = null;
            txtBoxFileMaxSize.Text = null;
            txtBoxFileMaxLines.Text = null;
            CheckBoxFileTurnOn.IsChecked = false;
            btnFileAdd.Content = "Přidat";
        }

        private void btnFileAdd_Click(object sender, RoutedEventArgs e) {
            String path, email, interval, size, linesString;
            int lines;
            Boolean run, formated = true;

            path = @txtBoxFilePath.Text;
            email = @String.IsNullOrEmpty(txtBoxFileEmail.Text) ? txtBoxGlobalSettingsEmailRecipient.Text : txtBoxFileEmail.Text;
            interval = @String.IsNullOrEmpty(txtBoxFileInterval.Text) ? txtBoxGlobalSettingsInterval.Text : txtBoxFileInterval.Text;
            size = @$"{(String.IsNullOrEmpty(txtBoxFileMaxSize.Text) ? txtBoxGlobalSettingsMaxSize.Text : txtBoxFileMaxSize.Text)} {(String.IsNullOrEmpty(txtBoxFileMaxSize.Text) ? (String.IsNullOrEmpty(txtBoxGlobalSettingsMaxSize.Text) ? "" : comBoxGlobalSettingsSizeUnits.Text) : comBoxFileSizeUnits.Text)}";
            linesString = @String.IsNullOrEmpty(txtBoxFileMaxLines.Text) ? txtBoxGlobalSettingsMaxLines.Text : txtBoxFileMaxLines.Text;
            run = (Boolean)CheckBoxFileTurnOn.IsChecked;

            if (!File.Exists(path)) {
                txtBoxFilePath.Text = "Daná cesta nenalezena!";
                formated = false;
            }

            if (interval.Split(":").Length == 4) {
                String[] tmpIntervalArray = interval.Split(":");
                for (int i = 0; i < tmpIntervalArray.Length; i++) {
                    if (!(tmpIntervalArray[i].Length == 2 && Int32.TryParse(tmpIntervalArray[i], out _))) {
                        txtBoxFileInterval.Text = "Nebyl zadán správný formát";
                        formated = false;
                        break;
                    }
                }
            } else {
                txtBoxFileInterval.Text = "Nebyl zadán správný formát";
                formated = false;
            }
            try {
                MailAddress mailAddress = new MailAddress(email);
            } catch (FormatException) {
                txtBoxFileEmail.Text = "E-mail není platný";
                formated = false;
            } catch (System.ArgumentException) {
                txtBoxFileEmail.Text = "E-mail není platný";
                formated = false;
            }
            if (!Int32.TryParse(size.Split(" ")[0], out _)) {
                txtBoxFileMaxSize.Text = "Nebylo zadáno číslo!";
                formated = false;
            }
            if (!Int32.TryParse(linesString, out lines)) {
                txtBoxFileMaxLines.Text = "Nebylo zadáno číslo!";
                formated = false;
            }

            if (!formated) {
                System.Windows.MessageBox.Show("Vyplňte prosím všechny hodnoty");
                return;
            }

            SQLiteConnection db = new SQLiteConnection(DBFilePath);

            if (btnFileAdd.Content.ToString() == "uložit") {
                var DBSelectedRow = (DBFile)dataGridFile.SelectedItem;
                db.Update(new DBFile(DBSelectedRow.id, path, email, interval, size, lines, run));
                db.Close();
                btnFileAdd.Content = "Přidat";


            } else {
                DBFile tmp = new DBFile(path, email, interval, size, lines, run);
                db.Insert(tmp);
            }

            db.Close();
            btnFileCancel_Click(sender, e);
            loadDBFile();

        }

        private void btnDirectoryEdit_Click(object sender, RoutedEventArgs e) {
            btnDirectoryCopy_Click(sender, e);
            btnDirectoryAdd.Content = "uložit";
        }

        private void btnDirectoryRemove_Click(object sender, RoutedEventArgs e) {
            if (System.Windows.MessageBox.Show("Opravdu Smazat?", "Dotaz", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.No) {
                var DBSelectedRow = (DBDirectory)dataGridDirectory.SelectedItem;
                SQLiteConnection db = new SQLiteConnection(DBDirectoryPath);
                db.Delete(DBSelectedRow);
                db.Close();
                loadDBDirectory();
            }

        }

        private void btnDirectoryCopy_Click(object sender, RoutedEventArgs e) {
            var DBSelectedRow = (DBDirectory)dataGridDirectory.SelectedItem;
            txtBoxDirectoryPath.Text = DBSelectedRow.path;
            txtBoxDirectoryEmail.Text = DBSelectedRow.email;
            txtBoxDirectoryInterval.Text = DBSelectedRow.interval.ToString();
            txtBoxDirectoryMaxSize.Text = DBSelectedRow.maxSize.Split(" ")[0];
            comBoxDirectorySizeUnits.Text = DBSelectedRow.maxSize.Split(" ")[1];
            txtBoxDirectoryMaxLines.Text = DBSelectedRow.maxLines.ToString();
            CheckBoxDirectoryTurnOn.IsChecked = DBSelectedRow.turnOn;
        }

        private void btnDirectoryCancel_Click(object sender, RoutedEventArgs e) {
            txtBoxDirectoryPath.Text = null;
            txtBoxDirectoryEmail.Text = null;
            txtBoxDirectoryInterval.Text = null;
            txtBoxDirectoryMaxSize.Text = null;
            txtBoxDirectoryMaxLines.Text = null;
            CheckBoxDirectoryTurnOn.IsChecked = false;
            btnDirectoryAdd.Content = "Přidat";

        }

        private void btnDirectoryAdd_Click(object sender, RoutedEventArgs e) {
            String path, email, interval, size, linesString;
            int lines;
            Boolean run, formated = true;

            path = @txtBoxDirectoryPath.Text;
            email = @String.IsNullOrEmpty(txtBoxDirectoryEmail.Text) ? txtBoxGlobalSettingsEmailRecipient.Text : txtBoxDirectoryEmail.Text;
            interval = @String.IsNullOrEmpty(txtBoxDirectoryInterval.Text) ? txtBoxGlobalSettingsInterval.Text : txtBoxDirectoryInterval.Text;
            size = @$"{(String.IsNullOrEmpty(txtBoxDirectoryMaxSize.Text) ? txtBoxGlobalSettingsMaxSize.Text : txtBoxDirectoryMaxSize.Text)} {(String.IsNullOrEmpty(txtBoxDirectoryMaxSize.Text) ? (String.IsNullOrEmpty(txtBoxGlobalSettingsMaxSize.Text) ? "" : comBoxGlobalSettingsSizeUnits.Text) : comBoxDirectorySizeUnits.Text)}";
            linesString = @String.IsNullOrEmpty(txtBoxDirectoryMaxLines.Text) ? txtBoxGlobalSettingsMaxLines.Text : txtBoxDirectoryMaxLines.Text;
            run = (Boolean)CheckBoxDirectoryTurnOn.IsChecked;

            if (!Directory.Exists(path)) {
                txtBoxDirectoryPath.Text = "Daná cesta nenalezena!";
                formated = false;
            }
            if (interval.Split(":").Length == 4) {
                String[] tmpIntervalArray = interval.Split(":");
                for (int i = 0; i < tmpIntervalArray.Length; i++) {
                    if (!(tmpIntervalArray[i].Length == 2 && Int32.TryParse(tmpIntervalArray[i], out _))) {
                        txtBoxDirectoryInterval.Text = "Nebyl zadán správný formát";
                        formated = false;
                        break;
                    }
                }
            } else {
                txtBoxDirectoryInterval.Text = "Nebyl zadán správný formát";
                formated = false;
            }
            try {
                MailAddress mailAddress = new MailAddress(email);
            } catch (FormatException) {
                txtBoxDirectoryEmail.Text = "E-mail není platný";
                formated = false;
            } catch (System.ArgumentException) {
                txtBoxDirectoryEmail.Text = "E-mail není platný";
                formated = false;
            }
            if (!Int32.TryParse(size.Split(" ")[0], out _)) {
                txtBoxDirectoryMaxSize.Text = "Nebylo zadáno číslo!";
                formated = false;
            }
            if (!Int32.TryParse(linesString, out lines)) {
                txtBoxDirectoryMaxLines.Text = "Nebylo zadáno číslo!";
                formated = false;
            }

            if (!formated) {
                System.Windows.MessageBox.Show("Vyplňte prosím všechny hodnoty");
                return;
            }

            SQLiteConnection db = new SQLiteConnection(DBDirectoryPath);

            if (btnDirectoryAdd.Content.ToString() == "uložit") {
                var DBSelectedRow = (DBDirectory)dataGridDirectory.SelectedItem;
                db.Update(new DBDirectory(DBSelectedRow.id, path, email, interval, size, lines, run));
                btnDirectoryAdd.Content = "Přidat";
            } else {
                DBDirectory tmp = new DBDirectory(path, email, interval, size, lines, run);
                db.Insert(tmp);
            }
            db.Close();
            btnDirectoryCancel_Click(sender, e);
            loadDBDirectory();
        }

        private Boolean checkAllFilled() {


            return true;
        }
    }
}