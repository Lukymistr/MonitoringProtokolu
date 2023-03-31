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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Security.Policy;
using System.Windows.Documents;
using System.Windows.Shapes;
using static System.Windows.Forms.LinkLabel;
using System.Text;

namespace MonitoringProtokolu {
    public partial class MainWindow : Window {
        NotifyIcon icon = new NotifyIcon();
        private readonly static String DBPath = @"./data";
        private readonly static String DatabasePath = @$"{DBPath}/Database.db3";
        public MainWindow() {
            InitializeComponent();
            WindowChrome.SetWindowChrome(this, new WindowChrome { CaptionHeight = 0 });

            gridFile.Visibility = Visibility.Visible;

            doSystrayIcon();

            createDBPath();

            createDatabase();

            createGlobalSettings();
            loadGlobalSettings();

            createSmtp();
            loadSmtp();

            loadDataGrids();

        }

        private void createDatabase() {
            if (!File.Exists(DatabasePath)) {
                SQLiteConnection db = new SQLiteConnection(DatabasePath);
                db.CreateTable<Database>();
                db.Close();
            }
        }

        private void createGlobalSettings() {
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            if (db.Table<Database>().Count() == 0) {
                db.Insert(new Database("", "", int.MinValue, "", "", int.MinValue, "", false));
            }
            db.Close();
        }

        private void loadGlobalSettings() {
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            Database DB = db.Table<Database>().ElementAt(0);
            txtBoxGlobalSettingsInterval.Text = DB.interval;
            if (!String.IsNullOrEmpty(DB.maxSize)) {
                txtBoxGlobalSettingsMaxSize.Text = DB.maxSize.Split(" ")[0];
                comBoxGlobalSettingsSizeUnits.Text = DB.maxSize.Split(" ")[1];
            }
            txtBoxGlobalSettingsMaxLines.Text = (DB.maxLines == int.MinValue) ? default : DB.maxLines.ToString();
            txtBoxGlobalSettingsEmailRecipient.Text = DB.emailRecipient_senderEmail;
            txtBoxGlobalSettingsEmailSubject.Text = DB.emailSubject;
            txtBoxGlobalSettingsEmailMaxLines.Text = (DB.emailLines == int.MinValue) ? default : DB.emailLines.ToString();
            txtBoxGlobalSettingslogPath.Text = DB.path_logPath;
            CheckBoxGlobalSettingsTuningMode.IsChecked = DB.turnOn_tuningMode_SSL;
            db.Close();
        }

        private void createSmtp() {
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            if (db.Table<Database>().Count() == 1) {
                db.Insert(new Database("", "", "", "", int.MinValue, false, true));
            }
            db.Close();
        }

        private void loadSmtp() {
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            Database DB = db.Table<Database>().ElementAt(1);
            txtBoxSmtpSenderEmail.Text = DB.emailRecipient_senderEmail;
            txtBoxSmtpUser.Text = DB.user;
            txtBoxSmtpPassword.Text = DB.password;
            txtBoxSmtpHost.Text = DB.host;
            txtBoxSmtpPort.Text = (DB.port == int.MinValue) ? default : DB.port.ToString();
            CheckBoxSmtpSSL.IsChecked = DB.turnOn_tuningMode_SSL;
            db.Close();
        }

        private void createDBPath() {
            if (!Directory.Exists(DBPath)) {
                DirectoryInfo directory = new DirectoryInfo(DBPath);
                directory.Create();
                directory.Attributes |= FileAttributes.Hidden;
            }
        }

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

        private void doSystrayIcon() {
            icon.Icon = new Icon("icon.ico");
            icon.Text = "Monitoring Protokolu";
            icon.Visible = false;
            icon.Click += new System.EventHandler(icon_Click);
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
            if (!checkAllFilled()) { return; }; // nějaký chybový text
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
                Database DBSelectedRow = (Database)dataGridFile.SelectedItem;
                SQLiteConnection db = new SQLiteConnection(DatabasePath);
                db.Delete(DBSelectedRow);
                db.Close();
                loadDataGrids();
            }
        }

        private void btnFileCopy_Click(object sender, RoutedEventArgs e) {
            Database DBSelectedRow = (Database)dataGridFile.SelectedItem;
            txtBoxFilePath.Text = DBSelectedRow.path_logPath;
            txtBoxFileEmail.Text = DBSelectedRow.emailRecipient_senderEmail;
            txtBoxFileInterval.Text = DBSelectedRow.interval.ToString();
            txtBoxFileMaxSize.Text = DBSelectedRow.maxSize.Split(" ")[0];
            comBoxFileSizeUnits.Text = DBSelectedRow.maxSize.Split(" ")[1];
            txtBoxFileMaxLines.Text = DBSelectedRow.maxLines.ToString();
            CheckBoxFileTurnOn.IsChecked = DBSelectedRow.turnOn_tuningMode_SSL;

        }

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

        private void btnFileAdd_Click(object sender, RoutedEventArgs e) {
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            Database DBGlobal = db.Table<Database>().ElementAt(0);
            String path, email, interval, size, linesString;
            int lines;
            Boolean run, formated = true;

            path = @txtBoxFilePath.Text;
            email = @String.IsNullOrEmpty(txtBoxFileEmail.Text) ? DBGlobal.emailRecipient_senderEmail : txtBoxFileEmail.Text;
            interval = @String.IsNullOrEmpty(txtBoxFileInterval.Text) ? DBGlobal.interval : txtBoxFileInterval.Text;
            size = @$"{(String.IsNullOrEmpty(txtBoxFileMaxSize.Text) ? ((!String.IsNullOrEmpty(DBGlobal.maxSize)) ? DBGlobal.maxSize.Split(" ")[0] : "") : txtBoxFileMaxSize.Text)} {(String.IsNullOrEmpty(txtBoxFileMaxSize.Text) ? ((!String.IsNullOrEmpty(DBGlobal.maxSize)) ? (String.IsNullOrEmpty(DBGlobal.maxSize.Split(" ")[0]) ? "" : DBGlobal.maxSize.Split(" ")[1]) : txtBoxFileMaxSize.Text) : comBoxDirectorySizeUnits.Text)}";
            linesString = @String.IsNullOrEmpty(txtBoxFileMaxLines.Text) ? DBGlobal.maxLines.ToString() : txtBoxFileMaxLines.Text;
            run = (Boolean)CheckBoxFileTurnOn.IsChecked;

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

        private void btnDirectoryEdit_Click(object sender, RoutedEventArgs e) {
            btnDirectoryCopy_Click(sender, e);
            btnDirectoryAdd.Content = "uložit";
        }

        private void btnDirectoryRemove_Click(object sender, RoutedEventArgs e) {
            if (System.Windows.MessageBox.Show("Opravdu Smazat?", "Dotaz", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.No) {
                Database DBSelectedRow = (Database)dataGridDirectory.SelectedItem;
                SQLiteConnection db = new SQLiteConnection(DatabasePath);
                db.Delete(DBSelectedRow);
                db.Close();
                loadDataGrids();
            }

        }

        private void btnDirectoryCopy_Click(object sender, RoutedEventArgs e) {
            Database DBSelectedRow = (Database)dataGridDirectory.SelectedItem;
            txtBoxDirectoryPath.Text = DBSelectedRow.path_logPath;
            txtBoxDirectoryEmail.Text = DBSelectedRow.emailRecipient_senderEmail;
            txtBoxDirectoryInterval.Text = DBSelectedRow.interval.ToString();
            txtBoxDirectoryMaxSize.Text = DBSelectedRow.maxSize.Split(" ")[0];
            comBoxDirectorySizeUnits.Text = DBSelectedRow.maxSize.Split(" ")[1];
            txtBoxDirectoryMaxLines.Text = DBSelectedRow.maxLines.ToString();
            CheckBoxDirectoryTurnOn.IsChecked = DBSelectedRow.turnOn_tuningMode_SSL;
        }

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

        private void btnDirectoryAdd_Click(object sender, RoutedEventArgs e) {
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            Database DBGlobal = db.Table<Database>().ElementAt(0);
            String path, email, interval, size, linesString;
            int lines;
            Boolean run, formated = true;

            path = @txtBoxDirectoryPath.Text;
            email = @String.IsNullOrEmpty(txtBoxDirectoryEmail.Text) ? DBGlobal.emailRecipient_senderEmail : txtBoxDirectoryEmail.Text;
            interval = @String.IsNullOrEmpty(txtBoxDirectoryInterval.Text) ? DBGlobal.interval : txtBoxDirectoryInterval.Text;
            size = @$"{(String.IsNullOrEmpty(txtBoxDirectoryMaxSize.Text) ? ((!String.IsNullOrEmpty(DBGlobal.maxSize)) ? DBGlobal.maxSize.Split(" ")[0] : "") : txtBoxDirectoryMaxSize.Text)} {(String.IsNullOrEmpty(txtBoxDirectoryMaxSize.Text) ? ((!String.IsNullOrEmpty(DBGlobal.maxSize)) ? (String.IsNullOrEmpty(DBGlobal.maxSize.Split(" ")[0]) ? "" : DBGlobal.maxSize.Split(" ")[1]) : txtBoxDirectoryMaxSize.Text) : comBoxDirectorySizeUnits.Text)}";
            linesString = @String.IsNullOrEmpty(txtBoxDirectoryMaxLines.Text) ? DBGlobal.maxLines.ToString() : txtBoxDirectoryMaxLines.Text;
            run = (Boolean)CheckBoxDirectoryTurnOn.IsChecked;

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

        private Boolean checkAllFilled() {
            // tady se to bude kontrolovat, když se spustí zapnout

            return true;
        }
        public void CheckBoxFileTurnOn_Checked(object sender, RoutedEventArgs e) {
            Database DBSelectedRow = (Database)dataGridFile.SelectedItem;
            if (DBSelectedRow == null) {
                return;
            }
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            db.Update(new Database(DBSelectedRow.id, DBSelectedRow.path_logPath, DBSelectedRow.emailRecipient_senderEmail, DBSelectedRow.interval, DBSelectedRow.maxSize, DBSelectedRow.maxLines, true));
            db.Close();
        }

        public void CheckBoxFileTurnOn_Unchecked(object sender, RoutedEventArgs e) {
            Database DBSelectedRow = (Database)dataGridFile.SelectedItem;
            if (DBSelectedRow == null) {
                return;
            }
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            db.Update(new Database(DBSelectedRow.id, DBSelectedRow.path_logPath, DBSelectedRow.emailRecipient_senderEmail, DBSelectedRow.interval, DBSelectedRow.maxSize, DBSelectedRow.maxLines, false));
            db.Close();
        }


        public void CheckBoxDirectoryTurnOn_Checked(object sender, RoutedEventArgs e) {
            Database DBSelectedRow = (Database)dataGridDirectory.SelectedItem;
            if (DBSelectedRow == null) {
                return;
            }
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            db.Update(new Database(DBSelectedRow.id, DBSelectedRow.path_logPath, DBSelectedRow.emailRecipient_senderEmail, DBSelectedRow.interval, DBSelectedRow.maxSize, DBSelectedRow.maxLines, true));
            db.Close();
        }

        public void CheckBoxDirectoryTurnOn_Unchecked(object sender, RoutedEventArgs e) {
            Database DBSelectedRow = (Database)dataGridDirectory.SelectedItem;
            if (DBSelectedRow == null) {
                return;
            }
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            db.Update(new Database(DBSelectedRow.id, DBSelectedRow.path_logPath, DBSelectedRow.emailRecipient_senderEmail, DBSelectedRow.interval, DBSelectedRow.maxSize, DBSelectedRow.maxLines, false));
            db.Close();
        }

        private void btnGlobalSettingsSave_Click(object sender, RoutedEventArgs e) {
            String interval, size, emailAddress, emailSubject, emailLinesString, logPath, linesString;
            int lines = int.MinValue, emailLines = int.MinValue;
            Boolean tuningMode, formated = true;

            interval = txtBoxGlobalSettingsInterval.Text;
            size = txtBoxGlobalSettingsMaxSize.Text;
            linesString = txtBoxGlobalSettingsMaxLines.Text;
            emailAddress = txtBoxGlobalSettingsEmailRecipient.Text;
            emailSubject = txtBoxGlobalSettingsEmailSubject.Text;
            emailLinesString = txtBoxGlobalSettingsEmailMaxLines.Text;
            logPath = @txtBoxGlobalSettingslogPath.Text;
            tuningMode = (Boolean)CheckBoxGlobalSettingsTuningMode.IsChecked;

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
            if (!String.IsNullOrEmpty(emailLinesString)) {
                if (!Int32.TryParse(emailLinesString, out _)) {
                    txtBoxGlobalSettingsEmailMaxLines.Text = "Nebylo zadáno číslo!";
                    formated = false;
                } else {
                    emailLines = Int32.Parse(emailLinesString);

                }
            }
            if (!String.IsNullOrEmpty(logPath)) {
                if (!Directory.Exists(logPath)) {
                    txtBoxGlobalSettingslogPath.Text = "Daná cesta nenalezena!";
                    formated = false;
                }
            }

            if (!formated) {
                System.Windows.MessageBox.Show("Vyplňte prosím všechny hodnoty");
                return;
            }
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            db.Update(new Database(1, interval, size, lines, emailAddress, emailSubject, emailLines, logPath, tuningMode));
            db.Close();

            loadGlobalSettings();
        }

        private void btnSmtpSaveAndTry_Click(object sender, RoutedEventArgs e) {
            if (!smtpSave()) {
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
                message.From = new MailAddress(DB.emailRecipient_senderEmail);
                message.To.Add(DBGlobalConfig.emailRecipient_senderEmail);
                message.Subject = "Monitoring Protokolu";
                message.Body = "Teto je test od Monitoringu Protokolu";

                client.Send(message);
                System.Windows.MessageBox.Show("E-mail byl odeslán!");
            } catch (SmtpException) {
                System.Windows.MessageBox.Show("konfigurace byla uložena, ale E-mail nemohl být poslán!");
            }

            db.Close();

        }

        private Boolean smtpSave() {
            String senderEmail, user, password, host, portString;
            int port = int.MinValue;
            Boolean SSL, formated = true;

            senderEmail = txtBoxSmtpSenderEmail.Text;
            user = txtBoxSmtpUser.Text;
            password = txtBoxSmtpPassword.Text;
            host = txtBoxSmtpHost.Text;
            portString = txtBoxSmtpPort.Text;
            SSL = (Boolean)CheckBoxSmtpSSL.IsChecked;


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
                if (port == Int32.MinValue) {
                    txtBoxSmtpPort.Text = "Nebylo zadáno číslo!";
                    formated = false;
                }
            }


            if (!formated) {
                System.Windows.MessageBox.Show("Vyplňte prosím všechny hodnoty");
                return false;
            }
            SQLiteConnection db = new SQLiteConnection(DatabasePath);
            db.Update(new Database(2, senderEmail, user, password, host, port, SSL, true));
            db.Close();

            loadSmtp();
            return true;
        }

        private void btnSmtpSave_Click(object sender, RoutedEventArgs e) {
            smtpSave();
        }

    }
}