using System;
using SQLite;

namespace MonitoringProtokolu {
    internal class Database {
        [PrimaryKey, AutoIncrement]

        // file / directory
        public int id { get; set; } //
        public String path_logPath { get; set; } // g: logPath, 
        public String emailRecipient_senderEmail { get; set; } // g: emailRecipient, s: senderEmail
        public String interval { get; set; } // g: interval, 

        public String maxSize { get; set; } // g: maxSize, 
        public int maxLines { get; set; } // g: maxLines, 
        public bool turnOn_tuningMode_SSL { get; set; } // g: tuningMode, s: SSL

        // global
        // public String interval { get; set; }
        // public String maxSize { get; set; }
        // public int maxLines { get; set; }
        // public String emailRecipient { get; set; }
        public String emailSubject { get; set; } //
        public int emailLines { get; set; } //
        // public String logPath { get; set; }
        // public Boolean tuningMode { get; set; }

        // SMTP
        // public String senderEmail { get; set; }
        public String user { get; set; } //
        public String password { get; set; } //
        public String host { get; set; } //
        public int port { get; set; }
        // public Boolean SSL { get; set; }

        public Database() { }

        public Database(String path, String email, String interval, String maxSize, int maxLines, bool turnOn) {
            this.path_logPath = path;
            this.emailRecipient_senderEmail = email;
            this.interval = interval;
            this.maxSize = maxSize;
            this.maxLines = maxLines;
            this.turnOn_tuningMode_SSL = turnOn;
        }

        public Database(String senderEmail, String user, String password, String host, int port, bool SSL, bool SMTP) {
            this.emailRecipient_senderEmail = senderEmail;
            this.user = user;
            this.password = password;
            this.host = host;
            this.port = port;
            this.turnOn_tuningMode_SSL = SSL;
        }

        public Database(int id, String path, String email, String interval, String maxSize, int maxLines, bool turnOn) {
            this.id = id;
            this.path_logPath = path;
            this.emailRecipient_senderEmail = email;
            this.interval = interval;
            this.maxSize = maxSize;
            this.maxLines = maxLines;
            this.turnOn_tuningMode_SSL = turnOn;
        }

        public Database(int id, String senderEmail, String user, String password, String host, int port, bool SSL, bool SMTP) {
            this.id = id;
            this.emailRecipient_senderEmail = senderEmail;
            this.user = user;
            this.password = password;
            this.host = host;
            this.port = port;
            this.turnOn_tuningMode_SSL = SSL;
        }

        public Database(String interval, String maxSize, int maxLines, String email, String emailSubject, int emailLines, String logPath, bool tuningMode) {
            this.interval = interval;
            this.maxSize = maxSize;
            this.maxLines = maxLines;
            this.emailRecipient_senderEmail = email;
            this.emailSubject = emailSubject;
            this.emailLines = emailLines;
            this.path_logPath = logPath;
            this.turnOn_tuningMode_SSL = tuningMode;
        }

        public Database(int id, String interval, String maxSize, int maxLines, String email, String emailSubject, int emailLines, String logPath, bool tuningMode) {
            this.id = id;
            this.interval = interval;
            this.maxSize = maxSize;
            this.maxLines = maxLines;
            this.emailRecipient_senderEmail = email;
            this.emailSubject = emailSubject;
            this.emailLines = emailLines;
            this.path_logPath = logPath;
            this.turnOn_tuningMode_SSL = tuningMode;
        }
    }
}
