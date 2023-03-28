using System;
using SQLite;

namespace MonitoringProtokolu {
    internal class DBFile {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public String path { get; set; }
        public String email { get; set; }
        public int interval { get; set; }

        public int maxSize { get; set; }
        public int maxLines { get; set; }
        public Boolean turnOn { get; set; }

        public DBFile() { }

        public DBFile(String path, String email, int interval, int maxSize, int maxLines, Boolean turnOn) {
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
        public int interval { get; set; }

        public int maxSize { get; set; }
        public int maxLines { get; set; }
        public Boolean turnOn { get; set; }

        public DBDirectory() { }

        public DBDirectory(String path, String email, int interval, int maxSize, int maxLines, Boolean turnOn) {
            this.path = path;
            this.email = email;
            this.interval = interval;
            this.maxSize = maxSize;
            this.maxLines = maxLines;
            this.turnOn = turnOn;
        }
    }
}
