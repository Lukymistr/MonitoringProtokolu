using System;
using SQLite;

namespace MonitoringProtokolu {
    internal class DBFile {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public String path { get; set; }
        public String email { get; set; }
        public String interval { get; set; }

        public String maxSize { get; set; }
        public int maxLines { get; set; }
        public Boolean turnOn { get; set; }

        public DBFile() { }

        public DBFile(String path, String email, String interval, String maxSize, int maxLines, Boolean turnOn) {
            this.path = path;
            this.email = email;
            this.interval = interval;
            this.maxSize = maxSize;
            this.maxLines = maxLines;
            this.turnOn = turnOn;
        }

        public DBFile(int id, String path, String email, String interval, String maxSize, int maxLines, Boolean turnOn) {
            this.id = id;
            this.path = path;
            this.email = email;
            this.interval = interval;
            this.maxSize = maxSize;
            this.maxLines = maxLines;
            this.turnOn = turnOn;
        }
    }

    internal class DBDirectory {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public String path { get; set; }
        public String email { get; set; }
        public String interval { get; set; }

        public String maxSize { get; set; }
        public int maxLines { get; set; }
        public Boolean turnOn { get; set; }

        public DBDirectory() { }

        public DBDirectory(String path, String email, String interval, String maxSize, int maxLines, Boolean turnOn) {
            this.path = path;
            this.email = email;
            this.interval = interval;
            this.maxSize = maxSize;
            this.maxLines = maxLines;
            this.turnOn = turnOn;
        }

        public DBDirectory(int id, String path, String email, String interval, String maxSize, int maxLines, Boolean turnOn) {
            this.id = id;
            this.path = path;
            this.email = email;
            this.interval = interval;
            this.maxSize = maxSize;
            this.maxLines = maxLines;
            this.turnOn = turnOn;
        }
    }
    internal class DBGlobalSettings {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public String interval { get; set; }
        public String maxSize { get; set; }
        public int maxLines { get; set; }
        public String email { get; set; }
        public String emailSubject { get; set; }
        public int emailLines { get; set; }
        public String logPath { get; set; }
        public Boolean tuningMode { get; set; }
        public DBGlobalSettings() { }

        public DBGlobalSettings(String interval, String maxSize, int maxLines, String email, String emailSubject, int emailLines, String logPath, Boolean tuningMode) {
            this.interval = interval;
            this.maxSize = maxSize;
            this.maxLines = maxLines;
            this.email = email;
            this.emailSubject = emailSubject;
            this.emailLines = emailLines;
            this.logPath = logPath;
            this.tuningMode = tuningMode;
        }

        public DBGlobalSettings(int id, String interval, String maxSize, int maxLines, String email, String emailSubject, int emailLines, String logPath, Boolean tuningMode) {
            this.id = id;
            this.interval = interval;
            this.maxSize = maxSize;
            this.maxLines = maxLines;
            this.email = email;
            this.emailSubject = emailSubject;
            this.emailLines = emailLines;
            this.logPath = logPath;
            this.tuningMode = tuningMode;
        }
    }

    internal class DBSmtp {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public String senderEmail { get; set; }
        public String user { get; set; }
        public String password { get; set; }
        public String host { get; set; }
        public int port { get; set; }
        public Boolean SSL { get; set; }

        public DBSmtp() { }

        public DBSmtp(String senderEmail, String user, String password, String host, int port, Boolean SSL) {
            this.senderEmail = senderEmail;
            this.user = user;
            this.password = password;
            this.host = host;
            this.port = port;
            this.SSL = SSL;
        }

        public DBSmtp(int id, String senderEmail, String user, String password, String host, int port, Boolean SSL) {
            this.id = id;
            this.senderEmail = senderEmail;
            this.user = user;
            this.password = password;
            this.host = host;
            this.port = port;
            this.SSL = SSL;
        }
    }
}
