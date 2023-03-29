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
}
