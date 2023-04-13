using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitoringProtokolu {
    internal class needed {
        /*

        // needed variables
        static Boolean running = false;
        static String path = "";
        static long size = 0, period = 0, globalPeriod = 0;
        static int lines = 0, bytesToRead = 0;

        // timer for periods
        static DateTime timer = DateTime.UtcNow;

        // 0 = original path, 1 = path with prefix
        private static MyArrayList<String[]> foundFiles = new MyArrayList<String[]>(); // importovat myArrayList, nebo najít něco lepšího

        // create path for files (ini and log
        static String filesFolder = @"cfg";

        // path for log email sent
        static String logFile = @"cfg/protocols.csv";

        // path for ini file
        static String iniFilePath = @"cfg/MonitoringProtokolu.ini";

        private void btnStart_Click(object sender, EventArgs e) {
            // have still running
            if (!running) {

                // new thread for finding
                Thread t2 = new Thread(find);
                t2.Start();
            }
        }


        private static void find() {
            running = true;
            end = false;
            while (tree(path, size, lines)) {
                MessageBox.Show("opakovani");
                //globální perioda
                timer = DateTime.UtcNow;
                while (DateTime.UtcNow - timer < TimeSpan.FromMilliseconds(globalPeriod)) { // TODO: je to v milisekundách, ale v GUI jsou sekudy, změnit GUI
                    if (end) {
                        break;
                    }
                }
            }
            renameBack();
            foundFiles.clear();
            running = false;

        }

        private static void renameAndAdd(String path) {
            string fileName = Path.GetFileName(path);
            string modifiedFileName = $"$${fileName}";
            string modifiedFilePath = Path.Combine(Path.GetDirectoryName(path), modifiedFileName);
            foundFiles.add(new string[] { path, modifiedFilePath });
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
                    }
                    //souborová perioda
                    timer = DateTime.UtcNow;
                    while (DateTime.UtcNow - timer < TimeSpan.FromMilliseconds(period)) { // TODO: je to v milisekundách, ale v GUI jsou sekudy, změnit GUI
                        if (end) {
                            break;
                        }
                    }
                } else {
                    tree(tmp, size, lines);
                }
            }
            return !end;
        }

        private static Boolean haveSizeOrLines(String path, long size, int lines) {
            FileInfo fi = new FileInfo(path);
            if ((fi.Length >= size || File.ReadLines(path).Count() >= lines) && !isInList(path)) {
                return true;
            }
            return false;
        }

        // send email via SMTP server to reciver
        private static Boolean sendMail(String path) {
            try {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("gogodalu@brand-app.biz");
                msg.To.Add("lukymistr3@seznam.cz");
                msg.Subject = path;
                msg.Body = bodyOfEmail(path);

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.elasticemail.com";
                System.Net.NetworkCredential ntcd = new NetworkCredential();
                ntcd.UserName = "gogodalu@brand-app.biz";
                ntcd.Password = "66514BBBDF03982C11B92FF223CFFEC824AC";
                smtp.Credentials = ntcd;
                smtp.EnableSsl = true;
                smtp.Port = 2525;
                smtp.Send(msg);
                addLog(path, msg.From.ToString(), msg.To.ToString());
                return true;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        // will separate return specific amount of bytes from file
        private static String bodyOfEmail(String path) {
            string fileContent = File.ReadAllText(Path.Combine(Path.GetDirectoryName(path), $"$${Path.GetFileName(path)}"));
            return (fileContent.Substring(0, Math.Min(fileContent.Length, bytesToRead)));
        }

        // write email log to the file
        private static void addLog(String path, String from, String to) {
            if (!File.Exists(logFile)) {
                File.Create(logFile).Close();

            }
            StreamWriter protocols = new StreamWriter(logFile, true);
            if (new FileInfo(logFile).Length == 0) {
                protocols.WriteLine($"date;time;name;path;to;from");
            }
            protocols.WriteLine($"{DateTime.Now.ToString("dd.MM.yyyy;HH:mm:ss")};{Path.GetFileName(path)};{Path.GetDirectoryName(path)};{to};{from}");
            protocols.Close();
        }

        // remove prefix from files 
        private static void renameBack() {
            for (int i = 0; i < foundFiles.size(); i++) {
                File.Move(foundFiles.get(i)[1], foundFiles.get(i)[0]);
            }
        }
        */
    }
}
