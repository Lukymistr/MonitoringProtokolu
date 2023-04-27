using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Shell;
using System.IO;
using SQLite;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Windows.Controls;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using System.Linq;

namespace MonitoringProtokolu {
    /// <summary>
    /// The main window.
    /// </summary>
    public partial class MainWindow : Window {
        NotifyIcon icon = new NotifyIcon();
        private readonly static String DBPath = @"./data";
        private readonly static String DatabasePath = @$"{DBPath}/Database.db3";

        private List<MonitoringRun> monitoringRuns;
        private bool monitoringRunning = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow() {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            WindowChrome.SetWindowChrome(this, new WindowChrome { CaptionHeight = 0 });

            gridFile.Visibility = Visibility.Visible;

            doSystrayIcon();

            createDatabaseDirectory();

            createDatabase();

            createGlobalSettings();
            loadGlobalSettings();

            createSmtp();
            loadSmtp();

            loadDataGrids();

        }

        /// <summary>
        /// Creates the database.
        /// </summary>
        private void createDatabase() {
            if (!File.Exists(DatabasePath)) {
                SQLiteConnection db = new SQLiteConnection(DatabasePath);
                db.CreateTable<Database>();
                db.Close();
            }
        }

        /// <summary>
        /// Creates the global settings record in database.
        /// </summary>
        private void createGlobalSettings() {
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            if (db.Table<Database>().Count() == 0) {
                db.Insert(new Database("", "", int.MinValue, "", "", int.MinValue, "", false));
            }
            db.Close();
        }

        /// <summary>
        /// Loads the global settings.
        /// </summary>
        private void loadGlobalSettings() {
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            Database DB = db.Table<Database>().ElementAt(0);
            txtBoxGlobalSettingsInterval.Text = DB.interval;
            if (!String.IsNullOrEmpty(DB.maxSize)) {
                txtBoxGlobalSettingsMaxSize.Text = DB.maxSize.Split(" ")[0];
                if (DB.maxSize.Split(" ").Length != 1) {
                    comBoxGlobalSettingsSizeUnits.Text = DB.maxSize.Split(" ")[1];
                }

            }
            txtBoxGlobalSettingsMaxLines.Text = (DB.maxLines == int.MinValue) ? default : DB.maxLines.ToString();
            txtBoxGlobalSettingsEmailRecipient.Text = DB.recipientEmail_senderEmail;
            txtBoxGlobalSettingsEmailSubject.Text = DB.emailSubject;
            txtBoxGlobalSettingsEmailMaxLines.Text = (DB.emailLines == int.MinValue) ? default : DB.emailLines.ToString();
            txtBoxGlobalSettingslogPath.Text = DB.path_logPath;
            CheckBoxGlobalSettingsTuningMode.IsChecked = DB.turnOn_tuningMode_SSL;
            db.Close();
        }

        /// <summary>
        /// Creates the smtp record in database.
        /// </summary>
        private void createSmtp() {
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            if (db.Table<Database>().Count() == 1) {
                db.Insert(new Database("", "", "", "", int.MinValue, false, true));
            }
            db.Close();
        }

        /// <summary>
        /// Loads the smtp.
        /// </summary>
        private void loadSmtp() {
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            Database DB = db.Table<Database>().ElementAt(1);
            txtBoxSmtpSenderEmail.Text = DB.recipientEmail_senderEmail;
            txtBoxSmtpUser.Text = DB.user;
            txtBoxSmtpPassword.Text = DB.password;
            txtBoxSmtpHost.Text = DB.host;
            txtBoxSmtpPort.Text = (DB.port == int.MinValue) ? default : DB.port.ToString();
            CheckBoxSmtpSSL.IsChecked = DB.turnOn_tuningMode_SSL;
            db.Close();
        }


        /// <summary>
        /// Creates the database directory.
        /// </summary>
        private void createDatabaseDirectory() {
            if (!Directory.Exists(DBPath)) {
                DirectoryInfo directory = new DirectoryInfo(DBPath);
                directory.Create();
                directory.Attributes |= FileAttributes.Hidden;
            }
        }

        /// <summary>
        /// Loads the data grids with values from database.
        /// </summary>
        private void loadDataGrids() {
            dataGridFile.Items.Clear();
            dataGridDirectory.Items.Clear();
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            List<Database> DBs = db.Table<Database>().ToList();
            for (int i = 2; i < DBs.Count; i++) {
                Database DB = DBs[i];
                if (File.Exists(DB.path_logPath)) {
                    dataGridFile.Items.Add(DB);
                } else {
                    dataGridDirectory.Items.Add(DB);
                }
            }
            db.Close();
        }

        /// <summary>
        /// Does the systray icon object.
        /// </summary>
        private void doSystrayIcon() {
            icon.Icon = new Icon("icon.ico");
            icon.Text = "Monitoring Protokolu";
            icon.Visible = false;
            icon.Click += new System.EventHandler(Icon_Click);
        }

        /// <summary>
        /// When click on icon in systray.
        /// </summary>
        private void Icon_Click(object sender, System.EventArgs e) {
            Show();
            icon.Visible = false;
        }

        /// <summary>
        /// Window state changed.
        /// </summary>
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

        /// <summary>
        /// Mouse down in title bar.
        /// </summary>
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

        /// <summary>
        /// Changes the size of the window.
        /// </summary>
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

        /// <summary>
        /// Button for window minimize.
        /// </summary>
        private void btnTitleBarMinimize_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Button for maximaze / minimalize the window.
        /// </summary>
        private void btnTitleBarResize_Click(object sender, RoutedEventArgs e) {
            changeSize();
        }
        /// <summary>
        /// Button for ends running of the aplication
        /// </summary>
        private void btnTitleBarExit_Click(object sender, RoutedEventArgs e) {
            monitoringRunning = false;
            Close();
        }


        /// <summary>
        /// Switch visibility of content.
        /// </summary>
        /// <param name="visibleGrid">The visible grid.</param>
        private void VisibilityHiddenOneVisible(Grid visibleGrid) {
            gridFile.Visibility = Visibility.Hidden;
            gridDirectory.Visibility = Visibility.Hidden;
            gridGlobalSettings.Visibility = Visibility.Hidden;
            gridSMTP.Visibility = Visibility.Hidden;
            visibleGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Button that switches visibility to file content.
        /// </summary>
        private void btnFiles_Click(object sender, RoutedEventArgs e) {
            VisibilityHiddenOneVisible(gridFile);

        }

        /// <summary>
        /// Button that switches visibility to directory content.
        /// </summary>
        private void btnDirectory_Click(object sender, RoutedEventArgs e) {
            VisibilityHiddenOneVisible(gridDirectory);
        }

        /// <summary>
        /// Button that switches visibility to global settings content.
        /// </summary>
        private void btnGlobalSettings_Click(object sender, RoutedEventArgs e) {
            loadGlobalSettings();
            VisibilityHiddenOneVisible(gridGlobalSettings);
        }

        /// <summary>
        /// Button that switches visibility to SMTP content.
        /// </summary>
        private void btnSmtpSettings_Click(object sender, RoutedEventArgs e) {
            loadSmtp();
            VisibilityHiddenOneVisible(gridSMTP);
        }

        /// <summary>
        /// Button that minimize the window
        /// </summary>
        private void btnMinimize_Click(object sender, RoutedEventArgs e) {
            Hide();
            icon.Visible = true;
        }

        /// <summary>
        /// Button that ends running of the aplication
        /// </summary>
        private void btnExit_Click(object sender, RoutedEventArgs e) {
            monitoringRunning = false;
            Close();
        }

        /// <summary>
        /// Button that chooses file path.
        /// </summary>
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

        /// <summary>
        /// Button that chooses directory path.
        /// </summary>
        private void btnDirectoryChoosePath_Click(object sender, RoutedEventArgs e) {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK) {
                txtBoxDirectoryPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Button that chooses log file path.
        /// </summary>
        private void btnGlobalSettingslogChoosePath_Click(object sender, RoutedEventArgs e) {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK) {
                txtBoxGlobalSettingslogPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Button that edits the content of the row in database.
        /// </summary>
        private void btnFileEdit_Click(object sender, RoutedEventArgs e) {
            btnFileCopy_Click(sender, e);
            btnFileAdd.Content = "uložit";
        }

        /// <summary>
        /// Button that removes the row in database.
        /// </summary>
        private void btnFileRemove_Click(object sender, RoutedEventArgs e) {
            if (System.Windows.MessageBox.Show("Opravdu Smazat?", "Dotaz", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.No) {
                Database DBSelectedRow = (Database)dataGridFile.SelectedItem;
                SQLiteConnection db = new SQLiteConnection(DatabasePath);
                db.Delete(DBSelectedRow);
                db.Close();
                loadDataGrids();
            }
        }

        /// <summary>
        /// Button that copies the content of the row in database into user input.
        /// </summary>
        private void btnFileCopy_Click(object sender, RoutedEventArgs e) {
            Database DBSelectedRow = (Database)dataGridFile.SelectedItem;
            txtBoxFilePath.Text = DBSelectedRow.path_logPath;
            txtBoxFileEmail.Text = DBSelectedRow.recipientEmail_senderEmail;
            txtBoxFileInterval.Text = DBSelectedRow.interval.ToString();
            txtBoxFileMaxSize.Text = DBSelectedRow.maxSize.Split(" ")[0];
            if (DBSelectedRow.maxSize.Split(" ").Length != 1) {
                comBoxFileSizeUnits.Text = DBSelectedRow.maxSize.Split(" ")[1];
            }
            txtBoxFileMaxLines.Text = DBSelectedRow.maxLines.ToString();
            CheckBoxFileTurnOn.IsChecked = DBSelectedRow.turnOn_tuningMode_SSL;

        }

        /// <summary>
        /// Button that removes all content from user inout.
        /// </summary>
        private void btnFileCancel_Click(object sender, RoutedEventArgs e) {
            txtBoxFilePath.Text = null;
            txtBoxFileEmail.Text = null;
            txtBoxFileInterval.Text = null;
            txtBoxFileMaxSize.Text = null;
            comBoxFileSizeUnits.SelectedIndex = 0;
            txtBoxFileMaxLines.Text = null;
            CheckBoxFileTurnOn.IsChecked = false;
            btnFileAdd.Content = "Přidat";
        }

        /// <summary>
        /// Button that adds the the row into database.
        /// </summary>
        private void btnFileAdd_Click(object sender, RoutedEventArgs e) {
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            Database DBGlobal = db.Table<Database>().ElementAt(0);
            String path, email, interval, size, linesString;
            int lines;
            bool run, formated = true;

            path = @txtBoxFilePath.Text;
            email = @String.IsNullOrEmpty(txtBoxFileEmail.Text) ? DBGlobal.recipientEmail_senderEmail : txtBoxFileEmail.Text;
            interval = @String.IsNullOrEmpty(txtBoxFileInterval.Text) ? DBGlobal.interval : txtBoxFileInterval.Text;
            size = @$"{(String.IsNullOrEmpty(txtBoxFileMaxSize.Text) ? ((!String.IsNullOrEmpty(DBGlobal.maxSize)) ? DBGlobal.maxSize.Split(" ")[0] : "") : txtBoxFileMaxSize.Text)} {(String.IsNullOrEmpty(txtBoxFileMaxSize.Text) ? ((!String.IsNullOrEmpty(DBGlobal.maxSize)) ? (String.IsNullOrEmpty(DBGlobal.maxSize.Split(" ")[0]) ? "" : DBGlobal.maxSize.Split(" ")[1]) : txtBoxFileMaxSize.Text) : comBoxDirectorySizeUnits.Text)}";
            linesString = @String.IsNullOrEmpty(txtBoxFileMaxLines.Text) ? DBGlobal.maxLines.ToString() : txtBoxFileMaxLines.Text;
            run = (bool)CheckBoxFileTurnOn.IsChecked;

            if (!File.Exists(path)) {
                txtBoxFilePath.Text = "Daná cesta nenalezena!";
                formated = false;
            }

            if (interval.Split(":").Length == 4) {
                String[] tmpIntervalArray = interval.Split(":");
                for (int i = 0; i < tmpIntervalArray.Length; i++) {
                    if (!(tmpIntervalArray[i].Length == 2 && Int32.TryParse(tmpIntervalArray[i], out _))) {
                        txtBoxFileInterval.Text = "Nebyl zadán správný formát!";
                        formated = false;
                        break;
                    }
                }
            } else {
                txtBoxFileInterval.Text = "Nebyl zadán správný formát!";
                formated = false;
            }
            try {
                MailAddress mailAddress = new MailAddress(email);
            } catch (FormatException) {
                txtBoxFileEmail.Text = "E-mail není platný!";
                formated = false;
            } catch (System.ArgumentException) {
                txtBoxFileEmail.Text = "E-mail není platný!";
                formated = false;
            }
            if (!(Int32.TryParse(size.Split(" ")[0], out _) && size.Split(" ").Length == 2)) {
                txtBoxFileMaxSize.Text = "Nebylo zadáno číslo!";
                formated = false;
            } else {
                if (Int32.Parse(size.Split(" ")[0]) == int.MinValue) {
                    txtBoxFileMaxSize.Text = "Nebylo zadáno číslo!";
                    formated = false;
                }
            }
            if (!Int32.TryParse(linesString, out lines)) {
                txtBoxFileMaxLines.Text = "Nebylo zadáno číslo!";
                formated = false;
            } else {
                if (lines == int.MinValue) {
                    txtBoxFileMaxLines.Text = "Nebylo zadáno číslo!";
                    formated = false;
                }
            }

            if (!formated) {
                System.Windows.MessageBox.Show("Vyplňte prosím všechny hodnoty");
                return;
            }

            if (btnFileAdd.Content.ToString() == "uložit") {
                Database DBSelectedRow = (Database)dataGridFile.SelectedItem;
                db.Update(new Database(DBSelectedRow.id, path, email, interval, size, lines, run));
                btnFileAdd.Content = "Přidat";


            } else {
                Database DBnewRow = new Database(path, email, interval, size, lines, run);
                db.Insert(DBnewRow);
            }

            db.Close();
            btnFileCancel_Click(sender, e);
            loadDataGrids();

        }

        /// <summary>
        /// Button that edits the content of the row in database.
        /// </summary>
        private void btnDirectoryEdit_Click(object sender, RoutedEventArgs e) {
            btnDirectoryCopy_Click(sender, e);
            btnDirectoryAdd.Content = "uložit";
        }

        /// <summary>
        /// Button that removes the row in database.
        /// </summary>
        private void btnDirectoryRemove_Click(object sender, RoutedEventArgs e) {
            if (System.Windows.MessageBox.Show("Opravdu Smazat?", "Dotaz", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.No) {
                Database DBSelectedRow = (Database)dataGridDirectory.SelectedItem;
                SQLiteConnection db = new SQLiteConnection(DatabasePath);
                db.Delete(DBSelectedRow);
                db.Close();
                loadDataGrids();
            }

        }

        /// <summary>
        /// Button that copies the content of the row in database into user input.
        /// </summary>
        private void btnDirectoryCopy_Click(object sender, RoutedEventArgs e) {
            Database DBSelectedRow = (Database)dataGridDirectory.SelectedItem;
            txtBoxDirectoryPath.Text = DBSelectedRow.path_logPath;
            txtBoxDirectoryEmail.Text = DBSelectedRow.recipientEmail_senderEmail;
            txtBoxDirectoryInterval.Text = DBSelectedRow.interval.ToString();
            txtBoxDirectoryMaxSize.Text = DBSelectedRow.maxSize.Split(" ")[0];
            if (DBSelectedRow.maxSize.Split(" ").Length != 1) {
                comBoxDirectorySizeUnits.Text = DBSelectedRow.maxSize.Split(" ")[1];
            }
            txtBoxDirectoryMaxLines.Text = DBSelectedRow.maxLines.ToString();
            CheckBoxDirectoryTurnOn.IsChecked = DBSelectedRow.turnOn_tuningMode_SSL;
        }

        /// <summary>
        /// Button that removes all content from user inout.
        /// </summary>
        private void btnDirectoryCancel_Click(object sender, RoutedEventArgs e) {
            txtBoxDirectoryPath.Text = null;
            txtBoxDirectoryEmail.Text = null;
            txtBoxDirectoryInterval.Text = null;
            txtBoxDirectoryMaxSize.Text = null;
            comBoxDirectorySizeUnits.SelectedIndex = 0;
            txtBoxDirectoryMaxLines.Text = null;
            CheckBoxDirectoryTurnOn.IsChecked = false;
            btnDirectoryAdd.Content = "Přidat";

        }

        /// <summary>
        /// Button that adds the the row into database.
        /// </summary>
        private void btnDirectoryAdd_Click(object sender, RoutedEventArgs e) {
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            Database DBGlobal = db.Table<Database>().ElementAt(0);
            String path, email, interval, size, linesString;
            int lines;
            bool run, formated = true;

            path = @txtBoxDirectoryPath.Text;
            email = @String.IsNullOrEmpty(txtBoxDirectoryEmail.Text) ? DBGlobal.recipientEmail_senderEmail : txtBoxDirectoryEmail.Text;
            interval = @String.IsNullOrEmpty(txtBoxDirectoryInterval.Text) ? DBGlobal.interval : txtBoxDirectoryInterval.Text;
            size = @$"{(String.IsNullOrEmpty(txtBoxDirectoryMaxSize.Text) ? ((!String.IsNullOrEmpty(DBGlobal.maxSize)) ? DBGlobal.maxSize.Split(" ")[0] : "") : txtBoxDirectoryMaxSize.Text)} {(String.IsNullOrEmpty(txtBoxDirectoryMaxSize.Text) ? ((!String.IsNullOrEmpty(DBGlobal.maxSize)) ? (String.IsNullOrEmpty(DBGlobal.maxSize.Split(" ")[0]) ? "" : DBGlobal.maxSize.Split(" ")[1]) : txtBoxDirectoryMaxSize.Text) : comBoxDirectorySizeUnits.Text)}";
            linesString = @String.IsNullOrEmpty(txtBoxDirectoryMaxLines.Text) ? DBGlobal.maxLines.ToString() : txtBoxDirectoryMaxLines.Text;
            run = (bool)CheckBoxDirectoryTurnOn.IsChecked;

            if (!Directory.Exists(path)) {
                txtBoxDirectoryPath.Text = "Daná cesta nenalezena!";
                formated = false;
            }
            if (interval.Split(":").Length == 4) {
                String[] tmpIntervalArray = interval.Split(":");
                for (int i = 0; i < tmpIntervalArray.Length; i++) {
                    if (!(tmpIntervalArray[i].Length == 2 && Int32.TryParse(tmpIntervalArray[i], out _))) {
                        txtBoxDirectoryInterval.Text = "Nebyl zadán správný formát!";
                        formated = false;
                        break;
                    }
                }
            } else {
                txtBoxDirectoryInterval.Text = "Nebyl zadán správný formát!";
                formated = false;
            }
            try {
                MailAddress mailAddress = new MailAddress(email);
            } catch (FormatException) {
                txtBoxDirectoryEmail.Text = "E-mail není platný!";
                formated = false;
            } catch (System.ArgumentException) {
                txtBoxDirectoryEmail.Text = "E-mail není platný!";
                formated = false;
            }
            if (!(Int32.TryParse(size.Split(" ")[0], out _) && size.Split(" ").Length == 2)) {
                txtBoxDirectoryMaxSize.Text = "Nebylo zadáno číslo!";
                formated = false;
            } else {
                if (Int32.Parse(size.Split(" ")[0]) == int.MinValue) {
                    txtBoxDirectoryMaxSize.Text = "Nebylo zadáno číslo!";
                    formated = false;
                }
            }
            if (!Int32.TryParse(linesString, out lines)) {
                txtBoxDirectoryMaxLines.Text = "Nebylo zadáno číslo!";
                formated = false;
            } else {
                if (lines == int.MinValue) {
                    txtBoxDirectoryMaxLines.Text = "Nebylo zadáno číslo!";
                    formated = false;
                }
            }

            if (!formated) {
                System.Windows.MessageBox.Show("Vyplňte prosím všechny hodnoty");
                return;
            }

            if (btnDirectoryAdd.Content.ToString() == "uložit") {
                Database DBSelectedRow = (Database)dataGridDirectory.SelectedItem;
                db.Update(new Database(DBSelectedRow.id, path, email, interval, size, lines, run));
                btnDirectoryAdd.Content = "Přidat";
            } else {
                Database DBnewRow = new Database(path, email, interval, size, lines, run);
                db.Insert(DBnewRow);
            }
            db.Close();
            btnDirectoryCancel_Click(sender, e);
            loadDataGrids();
        }

        /// <summary>
        /// CheckBox that swaps the state to start monitoring that record.
        /// </summary>
        public void CheckBoxFileTurnOn_Checked(object sender, RoutedEventArgs e) {
            Database DBSelectedRow = (Database)dataGridFile.SelectedItem;
            if (DBSelectedRow == null) {
                return;
            }
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            db.Update(new Database(DBSelectedRow.id, DBSelectedRow.path_logPath, DBSelectedRow.recipientEmail_senderEmail, DBSelectedRow.interval, DBSelectedRow.maxSize, DBSelectedRow.maxLines, true));
            db.Close();
        }

        /// <summary>
        /// CheckBox that swaps the state to do not start monitoring that record.
        /// </summary>
        public void CheckBoxFileTurnOn_Unchecked(object sender, RoutedEventArgs e) {
            Database DBSelectedRow = (Database)dataGridFile.SelectedItem;
            if (DBSelectedRow == null) {
                return;
            }
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            db.Update(new Database(DBSelectedRow.id, DBSelectedRow.path_logPath, DBSelectedRow.recipientEmail_senderEmail, DBSelectedRow.interval, DBSelectedRow.maxSize, DBSelectedRow.maxLines, false));
            db.Close();
        }

        /// <summary>
        /// CheckBox that swaps the state to start monitoring that record.
        /// </summary>
        public void CheckBoxDirectoryTurnOn_Checked(object sender, RoutedEventArgs e) {
            Database DBSelectedRow = (Database)dataGridDirectory.SelectedItem;
            if (DBSelectedRow == null) {
                return;
            }
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            db.Update(new Database(DBSelectedRow.id, DBSelectedRow.path_logPath, DBSelectedRow.recipientEmail_senderEmail, DBSelectedRow.interval, DBSelectedRow.maxSize, DBSelectedRow.maxLines, true));
            db.Close();
        }

        /// <summary>
        /// CheckBox that swaps the state to do not start monitoring that record.
        /// </summary>
        public void CheckBoxDirectoryTurnOn_Unchecked(object sender, RoutedEventArgs e) {
            Database DBSelectedRow = (Database)dataGridDirectory.SelectedItem;
            if (DBSelectedRow == null) {
                return;
            }
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            db.Update(new Database(DBSelectedRow.id, DBSelectedRow.path_logPath, DBSelectedRow.recipientEmail_senderEmail, DBSelectedRow.interval, DBSelectedRow.maxSize, DBSelectedRow.maxLines, false));
            db.Close();
        }

        /// <summary>
        /// Button for saves the user input into first row in database.
        /// </summary>
        private void btnGlobalSettingsSave_Click(object sender, RoutedEventArgs e) {
            GlobalSettingsSave(false);
        }

        /// <summary>
        /// Saves the user input into first row in database.
        /// </summary>
        /// <param name="onlyTry">Only checks values. Do not save them.</param>
        /// <returns>bool if was saved</returns>
        private bool GlobalSettingsSave(bool onlyTry) {
            String interval, size, emailAddress, emailSubject, emailLinesString, logPath, linesString;
            int lines = int.MinValue, emailLines = int.MinValue;
            bool tuningMode, formated = true;

            interval = txtBoxGlobalSettingsInterval.Text;
            size = txtBoxGlobalSettingsMaxSize.Text;
            linesString = txtBoxGlobalSettingsMaxLines.Text;
            emailAddress = txtBoxGlobalSettingsEmailRecipient.Text;
            emailSubject = txtBoxGlobalSettingsEmailSubject.Text;
            emailLinesString = txtBoxGlobalSettingsEmailMaxLines.Text;
            logPath = @txtBoxGlobalSettingslogPath.Text;
            tuningMode = (bool)CheckBoxGlobalSettingsTuningMode.IsChecked;

            if (!String.IsNullOrEmpty(interval)) {
                if (interval.Split(":").Length == 4) {
                    String[] tmpIntervalArray = interval.Split(":");
                    for (int i = 0; i < tmpIntervalArray.Length; i++) {
                        if (!(tmpIntervalArray[i].Length == 2 && Int32.TryParse(tmpIntervalArray[i], out _))) {
                            txtBoxGlobalSettingsInterval.Text = "Nebyl zadán správný formát!";
                            formated = false;
                            break;
                        }
                    }
                } else {
                    txtBoxGlobalSettingsInterval.Text = "Nebyl zadán správný formát!";
                    formated = false;
                }
            }
            if (!String.IsNullOrEmpty(size.Split(" ")[0])) {
                if (!(Int32.TryParse(size.Split(" ")[0], out _))) {
                    txtBoxGlobalSettingsMaxSize.Text = "Nebylo zadáno číslo!";
                    formated = false;
                } else {
                    size = $"{txtBoxGlobalSettingsMaxSize.Text} {comBoxGlobalSettingsSizeUnits.Text}";
                }
            }
            if (!String.IsNullOrEmpty(linesString)) {
                if (!Int32.TryParse(linesString, out lines)) {
                    txtBoxGlobalSettingsMaxLines.Text = "Nebylo zadáno číslo!";
                    formated = false;
                } else {
                    lines = Int32.Parse(linesString);
                }
                if (!String.IsNullOrEmpty(emailAddress)) {
                    try {
                        MailAddress mailAddress = new MailAddress(emailAddress);
                    } catch (FormatException) {
                        txtBoxGlobalSettingsEmailRecipient.Text = "E-mail není platný!";
                        formated = false;
                    } catch (System.ArgumentException) {
                        txtBoxGlobalSettingsEmailRecipient.Text = "E-mail není platný!";
                        formated = false;
                    }
                }
            }
            if (!Int32.TryParse(emailLinesString, out _)) {
                txtBoxGlobalSettingsEmailMaxLines.Text = "Nebylo zadáno číslo!";
                formated = false;
            } else {
                emailLines = Int32.Parse(emailLinesString);

            }
            if (!Directory.Exists(logPath)) {
                txtBoxGlobalSettingslogPath.Text = "Daná cesta nenalezena!";
                formated = false;
            }

            if (!formated) {
                System.Windows.MessageBox.Show("Vyplňte prosím všechny hodnoty");
                if (onlyTry) {
                    VisibilityHiddenOneVisible(gridGlobalSettings);
                }
                return false;
            }

            if (onlyTry) {
                return true;
            }

            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            db.Update(new Database(1, interval, size, lines, emailAddress, emailSubject, emailLines, logPath, tuningMode));
            db.Close();

            loadGlobalSettings();
            return true;
        }


        /// <summary>
        /// Button for saves the user input into first row in database and tries the the SMTP configuration.
        /// </summary>
        private void btnSmtpSaveAndTry_Click(object sender, RoutedEventArgs e) {
            if (!smtpSave(false)) {
                return;
            }
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            Database DB = db.Table<Database>().ElementAt(1);
            Database DBGlobalConfig = new Database();
            try {
                DBGlobalConfig = db.Table<Database>().ElementAt(0);
            } catch (Exception) {
                btnGlobalSettings_Click(sender, e);
                txtBoxGlobalSettingsEmailRecipient.Text = "E-mail není platný!";
                return;
            }
            try {
                SmtpClient client = new SmtpClient(DB.host);
                client.Port = DB.port;
                client.Credentials = new NetworkCredential(DB.user, DB.password);
                client.EnableSsl = DB.turnOn_tuningMode_SSL;

                MailMessage message = new MailMessage();
                message.From = new MailAddress(DB.recipientEmail_senderEmail);
                message.To.Add(DBGlobalConfig.recipientEmail_senderEmail);
                message.Subject = "Monitoring Protokolu";
                message.Body = "Teto je test od Monitoringu Protokolu";

                client.Send(message);
                System.Windows.MessageBox.Show("E-mail byl odeslán!");
            } catch (SmtpException) {
                System.Windows.MessageBox.Show("konfigurace byla uložena, ale E-mail nemohl být poslán!");
            }

            db.Close();

        }

        /// <summary>
        /// Saves the user input into second row in database.
        /// </summary>
        /// <param name="onlyTry">Only checks values. Do not save them.</param>
        /// <returns>Bool if was saved</returns>
        private bool smtpSave(bool onlyTry) {
            String senderEmail, user, password, host, portString;
            int port = int.MinValue;
            bool SSL, formated = true;

            senderEmail = txtBoxSmtpSenderEmail.Text;
            user = txtBoxSmtpUser.Text;
            password = txtBoxSmtpPassword.Text;
            host = txtBoxSmtpHost.Text;
            portString = txtBoxSmtpPort.Text;
            SSL = (bool)CheckBoxSmtpSSL.IsChecked;


            try {
                MailAddress mailAddress = new MailAddress(senderEmail);
            } catch (FormatException) {
                txtBoxSmtpSenderEmail.Text = "E-mail není platný!";
                formated = false;
            } catch (System.ArgumentException) {
                txtBoxSmtpSenderEmail.Text = "E-mail není platný!";
                formated = false;

            }
            if (String.IsNullOrEmpty(user) || user == "Nebylo zadáno!") {
                txtBoxSmtpUser.Text = "Nebylo zadáno!";
                formated = false;
            }
            if (String.IsNullOrEmpty(password) || password == "Nebylo zadáno!") {
                txtBoxSmtpPassword.Text = "Nebylo zadáno!";
                formated = false;
            }
            if (String.IsNullOrEmpty(host) || host == "Nebylo zadáno!") {
                txtBoxSmtpHost.Text = "Nebylo zadáno!";
                formated = false;
            }
            if (!Int32.TryParse(portString, out _)) {
                txtBoxSmtpPort.Text = "Nebylo zadáno číslo!";
                formated = false;
            } else {
                port = Int32.Parse(portString);
                if (port == Int32.MinValue || !(port >= 1 && port <= 65535)) {
                    txtBoxSmtpPort.Text = "Nebylo zadáno správný port!";
                    formated = false;
                }
            }


            if (!formated) {
                System.Windows.MessageBox.Show("Vyplňte prosím všechny hodnoty");
                if (onlyTry) {
                    VisibilityHiddenOneVisible(gridSMTP);
                }
                return false;
            }
            if (onlyTry) {
                return true;
            }
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            db.Update(new Database(2, senderEmail, user, password, host, port, SSL, true));
            db.Close();

            loadSmtp();
            return true;
        }

        /// <summary>
        /// Button that Saves the user input into second row in database.
        /// </summary>
        private void btnSmtpSave_Click(object sender, RoutedEventArgs e) {
            smtpSave(false);
        }

        /// <summary>
        /// Button that starts or ends monitoring.
        /// </summary>
        private void btnTurnOnOff_Click(object sender, RoutedEventArgs e) {
            OnOffMonitoring(monitoringRunning);

        }


        /// <summary>
        /// Checks if every input is in corect format
        /// </summary>
        /// <returns>Bool if all fits</returns>
        private bool checkAllFits() {
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            if (db.Table<Database>().Count() <= 2) {
                System.Windows.MessageBox.Show("V databázi není žádný prvek");
                return false;
            }
            for (int i = 2; i < db.Table<Database>().Count(); i++) {
                Database DB = db.Table<Database>().ElementAt(i);
                if (!checkDatabase(DB, DB.path_logPath, DB.interval, DB.recipientEmail_senderEmail, DB.maxSize, DB.maxLines.ToString())) {
                    return false;
                }

            }
            db.Close();

            loadGlobalSettings();
            if (!GlobalSettingsSave(true)) {
                return false;
            }

            loadSmtp();
            if (!smtpSave(true)) {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if every record in database is correct
        /// </summary
        private bool checkDatabase(Database DB, String path, String interval, String email, String size, String linesString) {
            int lines;
            String error = "našli se tyto chybné imputy: \n";

            StringBuilder errorStringBuilder = new StringBuilder();

            if (!(Directory.Exists(path) || File.Exists(path))) {
                errorStringBuilder.AppendLine($"Daná cesta nenalezena: {DB.path_logPath}");

            }
            if (interval.Split(":").Length == 4) {
                String[] tmpIntervalArray = interval.Split(":");
                for (int i = 0; i < tmpIntervalArray.Length; i++) {
                    if (!(tmpIntervalArray[i].Length == 2 && Int32.TryParse(tmpIntervalArray[i], out _))) {
                        errorStringBuilder.AppendLine($"Nebyl zadán správný formát intervalu: {DB.interval}");
                    }
                }
            } else {
                errorStringBuilder.AppendLine($"Nebyl zadán správný formát intervalu: {DB.interval}");
            }
            try {
                MailAddress mailAddress = new MailAddress(email);
            } catch (FormatException) {
                errorStringBuilder.AppendLine($"Nebyl zadán správný formát e-mailu: {DB.recipientEmail_senderEmail}");
            } catch (System.ArgumentException) {
                errorStringBuilder.AppendLine($"E-mail není platný: {DB.recipientEmail_senderEmail}");
            }
            if (!(Int32.TryParse(size.Split(" ")[0], out _) && size.Split(" ").Length == 2)) {
                errorStringBuilder.AppendLine($"Nebylo zadáno číslo maximální velikosti: {DB.maxSize}");
            } else {
                if (Int32.Parse(size.Split(" ")[0]) == int.MinValue) {
                    errorStringBuilder.AppendLine($"Nebylo zadáno číslo s jednotkami pro velikost: {DB.maxSize}");
                }
            }
            if (!Int32.TryParse(linesString, out lines)) {
                errorStringBuilder.AppendLine($"Nebylo zadáno číslo pro počet řádek: {DB.maxLines}");
            } else {
                if (lines == int.MinValue) {
                    errorStringBuilder.AppendLine($"Nebylo zadáno číslo pro počet řádek: {DB.maxLines}");
                }
            }

            error += errorStringBuilder.ToString();
            if (error != "našli se tyto chybné imputy: \n") {
                System.Windows.MessageBox.Show(error);
                return false;
            }
            return true;
        }


        /// <summary>
        /// Starts or ends monitoring.
        /// </summary>
        /// <param name="running">Is already running.</param>
        private void OnOffMonitoring(bool running) {
            if (!running) {
                // celková kontrola před spuštěním
                if (!checkAllFits()) { return; };
                monitoringRunning = !monitoringRunning;
                // vše zapnout
                Thread run = new Thread(turnOn);
                run.Start();
                btnTurnOnOff.Content = "Vypnout monitoring";
            } else {
                // vše ukončit
                monitoringRunning = !monitoringRunning;
                turnOff();
                btnTurnOnOff.Content = "Zapnout monitoring";
            }

        }

        /// <summary>
        /// turns the monitoring on.
        /// </summary>

        private void turnOn() {
            monitoringRuns = loadData();
            foreach (MonitoringRun monitoringRun in monitoringRuns) {
                int[] numbers = monitoringRun.data.interval.Split(':').Select(int.Parse).ToArray();
                int time = numbers[3] + numbers[2] * 60 + numbers[1] * 60 * 60 + numbers[0] * 60 * 60 * 60;

                System.Timers.Timer timer = new System.Timers.Timer(time * 1000);
                timer.Elapsed += (sender, e) => Tick(monitoringRun);
                timer.AutoReset = true;
                timer.Enabled = true;

                monitoringRun.timer = timer;
            }
        }

        /// <summary>
        /// ticks evokes (předělat)
        /// </summary>
        private void Tick(MonitoringRun monitoringRun) {
            if (monitoringRun.running) {
                return;
            }
            monitoringRun.running = true;
            System.Windows.MessageBox.Show(monitoringRun.data.path_logPath);
            //monitoringRun.running = false;
        }

        /// <summary>
        /// loads the data from database.
        /// </summary>
        /// <returns>A list of enable protocols.</returns>
        private List<MonitoringRun> loadData() {
            List<MonitoringRun> monitoringRuns = new List<MonitoringRun>();
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            for (int i = 2; i < db.Table<Database>().Count(); i++) {
                Database DB = db.Table<Database>().ElementAt(i);
                if (DB.turnOn_tuningMode_SSL) {
                    monitoringRuns.Add(new MonitoringRun(false, DB, new System.Timers.Timer()));
                }
            }
            return monitoringRuns;
        }

        /// <summary>
        /// turns the monitoring off.
        /// </summary>
        private void turnOff() {
            monitoringRuns = null; // zeptat se, jestli funguje
        }
    }
}