using System.Net.Mail;
using System.Net;
using C_Sharp.ArrayList;

namespace MonitoringProtokolu {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            radioBtnB.Checked = true;
            radioBtnSeconds.Checked = true;
        }

        // needed variables
        static Boolean running = false;
        static String path = "";
        static long size = 0;
        static int lines = 0, frequency = 0;
        // 0 = original path, 1 = path with prefix
        private static MyArrayList<String[]>? foundFiles;

        // for ini file work
        static IniFile MyIni = new IniFile(@"../../../MonitoringProtokolu.ini");

        // end lokking for
        private void btnEnd_Click(object sender, EventArgs e) {
            end = true;
        }

        private void btnStart_Click(object sender, EventArgs e){

            // if unChecked take values from input
            // if Checked takes values form ini file
            if (!checkBoxFile.Checked) {
                if (!userInput()) { return; }
            } else {
                if (!fileInput()) { return; }
            }

            // test some basics
            if (!test()) { return; }

            // have still running
            if (!running) {
                foundFiles = new MyArrayList<String[]>();
                // new thread for finding
                Thread t2 = new Thread(find);
                t2.Start();
            }
        }

        // exit 
        private void btnExit_Click(object sender, EventArgs e) {
            end = true;
            Application.Exit();
        }

        // looking for the file
        private static void find() {
            running = true;
            end = false;
            while (tree(path, size, lines)) {
            }
            renameBack();
            running = false;

        }

        // take values from user
        private Boolean userInput() {

            // write name
            @path = textBoxName.Text;
            MyIni.Write("name", path);

            // choose between file size or lines count
            if (numUpDownLines.Value.ToString().Equals("0")) {

                // will calculate size to bytes
                size = Convert.ToInt64(numUpDownSize.Value);
                if (radioBtnGB.Checked) {
                    size *= 1073741824;
                }
                if (radioBtnMB.Checked) {
                    size *= 1048576;
                }
                if (radioBtnKB.Checked) {
                    size *= 1024;
                }

                // write size
                MyIni.Write("size", size.ToString());

                // write lines
                MyIni.Write("lines", "-1");
                lines = -1;
            } else {

                // write size 
                MyIni.Write("size", "-1");
                size = -1;

                // save lines into variable and write it
                lines = Convert.ToInt32(numUpDownLines.Value);
                MyIni.Write("lines", lines.ToString());

            }

            // calculate frequency
            frequency = Convert.ToInt32(numUpDownFrequency.Value);
            if (radioBtnMins.Checked) {
                frequency *= 60;
            }

            // write frequency
            MyIni.Write("frequency", frequency.ToString());
            return true;
        }

        // take values from file
        private Boolean fileInput() {
            // takes values from ini file
            path = MyIni.Read("name");
            size = Int64.Parse(MyIni.Read("size"));
            lines = int.Parse(MyIni.Read("lines"));
            frequency = int.Parse(MyIni.Read("frequency"));

            if (frequency < 1) {
                MessageBox.Show("Interval mezi hledáním nemùže být menší než 1");
                return false;
            }
            return true;
        }

        // test some basic things, like if file exists etc.
        private Boolean test() {
            Boolean passed = true;

            // if file exists
            if (!pathDoNotExists(path)) {
                MessageBox.Show("Cesta nebyla nalezena");
                return false;
                
            }

            // if both values are not filled
            if (sizeAndLinesAreNotFilled(size, lines)) {
                MessageBox.Show("Nebyly zadány hodnoty pro velikost ani pro øádky");
                return false;
            }

            // if only one value is filled
            if (sizeAndLinesAreFilled(size, lines)) {
                MessageBox.Show("Byly zadány hodnoty pro velikost i pro øádky, zadejte jen jednu hodnotu");
                return false;
            }
            return passed;
        }

        

        // if size and lines are not filled, do not know what to do
        private static Boolean sizeAndLinesAreNotFilled(long size, int lines) {
            if (size <= 0 && lines <= 0) {
                return true;
            }
            return false;
        }

        // if size and lines are filled, do not know what to do
        private static Boolean sizeAndLinesAreFilled(long size, int lines) {
            if (size > 0 && lines > 0) {
                return true;
            }
            return false;
        }

        // check if path is correct
        private static Boolean pathDoNotExists(String path) {
            return File.Exists(path) || Directory.Exists(path);
        }

        // check if file have that size or lines
        // add to MyArrayList {path, pathWithPrefix}
        private static Boolean haveSizeOrLines(String path, long size, int lines) {
            FileInfo fi = new FileInfo(path);
            if ((fi.Length == size || File.ReadLines(path).Count() == lines) && !isInList(path)) {
                return true;
            }
            return false;
        }

        // rename path to the path with prefix
        // add path and path with prefix to the list (to prevent duplicate files)
        private static void renameAndAdd(String path) {
            string fileName = Path.GetFileName(path);
            string modifiedFileName = "$$" + fileName;
            string modifiedFilePath = Path.Combine(Path.GetDirectoryName(path), modifiedFileName);
            foundFiles.add(new string[] {path, modifiedFilePath});
            File.Move(path, modifiedFilePath);

        }

        // check if file was already found
        private static Boolean isInList(String path) {
            for (int i = 0; i < foundFiles.size(); i++) {
                if (path == foundFiles.get(i)[1]) {
                    return true;
                }
            }
            return false;
        }

        // varialble for end loop if found
        static private Boolean end = false;

        

        // will go through every file, if it is folder do it in that folder 
        static private Boolean tree(String path, long size, int lines) {
            string[] files = Directory.GetFiles(path).Concat(Directory.GetDirectories(path)).ToArray();
            foreach (String @tmp in files) {
                if (end) {
                    break;
                }
                if (File.Exists(tmp)) {
                    if (haveSizeOrLines(tmp, size, lines)) {                                             
                        renameAndAdd(tmp);
                        sendMail(tmp);
                    } else {
                        Thread.Sleep(frequency * 1000);
                    }
                } else {
                    tree(tmp, size, lines);
                }
            }
            return !end;
        }

        // send mail via SMTP server to reciver
        public static Boolean sendMail(String path) {
            try {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("gogodalu@brand-app.biz");
                msg.To.Add("lukymistr3@seznam.cz");
                msg.Subject = path;
                msg.Body = null;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.elasticemail.com";
                System.Net.NetworkCredential ntcd = new NetworkCredential();
                ntcd.UserName = "gogodalu@brand-app.biz";
                ntcd.Password = "66514BBBDF03982C11B92FF223CFFEC824AC";
                smtp.Credentials = ntcd;
                smtp.EnableSsl = true;
                smtp.Port = 2525;
                smtp.Send(msg);
                return true;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        // remove prefix from files 
        private static void renameBack() {
            for (int i = 0; i < foundFiles.size(); i++) {
                File.Move(foundFiles.get(i)[1], foundFiles.get(i)[0]);
            }
        }


    }
}