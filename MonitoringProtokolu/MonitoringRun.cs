using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MonitoringProtokolu {
    internal class MonitoringRun {
        /// <summary>
        /// Gets or sets a value indicating íf is running.
        /// </summary>
        public bool running { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public Database data { get; set; }

        /// <summary>
        /// Gets or sets the timer.
        /// </summary>
        public Timer timer { get; set; }

        /// <summary>
        /// Gets or sets the max size.
        /// </summary>
        public long maxSize { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitoringRun"/> class.
        /// </summary>
        /// <param name="running">If true, running.</param>
        /// <param name="data">The data.</param>
        /// <param name="timer">The timer.</param>
        public MonitoringRun(bool running, Database data, long maxSize, Timer timer) {
            this.running = running;
            this.data = data;
            this.maxSize = maxSize;
            this.timer = timer;
        }
    }
}   