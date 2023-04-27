using System;
using SQLite;

namespace MonitoringProtokolu {
    internal class Database {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [PrimaryKey, AutoIncrement]

        // file / directory
        public int id { get; set; } //
        /// <summary>
        /// Gets or sets the log path or path of monitoring.
        /// </summary>
        public String path_logPath { get; set; } // g: logPath, 
        /// <summary>
        /// Gets or sets the recipient of email or sender of email.
        /// </summary>
        public String recipientEmail_senderEmail { get; set; } // g: recipientEmail, s: senderEmail
        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        public String interval { get; set; } // g: interval, 

        /// <summary>
        /// Gets or sets the max size of file.
        /// </summary>
        public String maxSize { get; set; } // g: maxSize, 
        /// <summary>
        /// Gets or sets the max lines of file.
        /// </summary>
        public int maxLines { get; set; } // g: maxLines, 
        /// <summary>
        /// Gets or sets a turn on monitoring or tuning mod activated or SSL is allowed.
        /// </summary>
        public bool turnOn_SSL { get; set; } // s: SSL

        // global
        // public String interval { get; set; }
        // public String maxSize { get; set; }
        // public int maxLines { get; set; }
        // public String recipientEmail { get; set; }

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        public String emailSubject { get; set; } //
        /// <summary>
        /// Gets or sets the email lines.
        /// </summary>
        public int emailLines { get; set; } //

        // public String logPath { get; set; }

        // SMTP
        // public String senderEmail { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public String user { get; set; } //
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public String password { get; set; } //
        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public String host { get; set; } //
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        public int port { get; set; }
        // public Boolean SSL { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// </summary>
        public Database() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// This is for new file or directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="interval">The interval.</param>
        /// <param name="maxSize">The max size.</param>
        /// <param name="maxLines">The max lines.</param>
        /// <param name="turnOn">If true, turn on monitoring.</param>
        public Database(String path, String recipientEmail, String interval, String maxSize, int maxLines, bool turnOn) {
            this.path_logPath = path;
            this.recipientEmail_senderEmail = recipientEmail;
            this.interval = interval;
            this.maxSize = maxSize;
            this.maxLines = maxLines;
            this.turnOn_SSL = turnOn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// This is for edit file or directory.
        /// </summary>
        /// <param name="id">The id in database.</param>
        /// <param name="path">The path.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="interval">The interval.</param>
        /// <param name="maxSize">The max size.</param>
        /// <param name="maxLines">The max lines.</param>
        /// <param name="turnOn">If true, turn on monitoring.</param>
        public Database(int id, String path, String recipientEmail, String interval, String maxSize, int maxLines, bool turnOn) {
            this.id = id;
            this.path_logPath = path;
            this.recipientEmail_senderEmail = recipientEmail;
            this.interval = interval;
            this.maxSize = maxSize;
            this.maxLines = maxLines;
            this.turnOn_SSL = turnOn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// This is for new SMTP server.
        /// </summary>
        /// <param name="senderEmail">The sender email.</param>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <param name="SSL">Using SSL.</param>
        /// <param name="SMTP">useless</param> // úplně k hovnu
        public Database(String senderEmail, String user, String password, String host, int port, bool SSL, bool SMTP) { // SMAZAT, SMTP je k ničemu, ale předělat to výše, má to stejné inputy
            this.recipientEmail_senderEmail = senderEmail;
            this.user = user;
            this.password = password;
            this.host = host;
            this.port = port;
            this.turnOn_SSL = SSL;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// This is for edit SMTP server.
        /// </summary>
        /// <param name="id">The id in database.</param>
        /// <param name="senderEmail">The sender email.</param>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <param name="SSL">Using SSL.</param>
        /// <param name="SMTP">If true, SMTP</param> // úplně k hovnu
        public Database(int id, String senderEmail, String user, String password, String host, int port, bool SSL, bool SMTP) { // SMAZAT, SMTP je k ničemu, ale předělat to výše, má to stejné inputy
            this.id = id;
            this.recipientEmail_senderEmail = senderEmail;
            this.user = user;
            this.password = password;
            this.host = host;
            this.port = port;
            this.turnOn_SSL = SSL;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// This is for new global settings.
        /// </summary>
        /// <param name="interval">The interval.</param>
        /// <param name="maxSize">The max size.</param>
        /// <param name="maxLines">The max lines.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="emailSubject">The email subject.</param>
        /// <param name="emailLines">The email lines.</param>
        /// <param name="logPath">The log path.</param>
        public Database(String interval, String maxSize, int maxLines, String recipientEmail, String emailSubject, int emailLines, String logPath) {
            this.interval = interval;
            this.maxSize = maxSize;
            this.maxLines = maxLines;
            this.recipientEmail_senderEmail = recipientEmail;
            this.emailSubject = emailSubject;
            this.emailLines = emailLines;
            this.path_logPath = logPath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// This is for edit global settings.
        /// </summary>
        /// <param name="id">The id in database.</param>
        /// <param name="interval">The interval.</param>
        /// <param name="maxSize">The max size.</param>
        /// <param name="maxLines">The max lines.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="emailSubject">The email subject.</param>
        /// <param name="emailLines">The email lines.</param>
        /// <param name="logPath">The log path.</param>
        public Database(int id, String interval, String maxSize, int maxLines, String recipientEmail, String emailSubject, int emailLines, String logPath) {
            this.id = id;
            this.interval = interval;
            this.maxSize = maxSize;
            this.maxLines = maxLines;
            this.recipientEmail_senderEmail = recipientEmail;
            this.emailSubject = emailSubject;
            this.emailLines = emailLines;
            this.path_logPath = logPath;
        }
    }
}
