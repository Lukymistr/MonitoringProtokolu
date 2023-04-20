using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MonitoringProtokolu {
    internal class MonitoringRun {
        public bool running { get; set; }
        public Database data { get; set; }
        public DispatcherTimer timer { get; set; }

        public MonitoringRun(bool running, Database data, DispatcherTimer timer) {
            this.running = running;
            this.data = data;
            this.timer = timer;
        }
    }
}
