using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;

namespace MonitoringProtokolu {
    /// <summary>
    /// Interakční logika pro InformationWindow.xaml
    /// </summary>
    public partial class YesNoWindow : Window {
        private bool _delete;

        public bool delete {
            get { 
                return _delete;
            }
        }
        public YesNoWindow() {
            InitializeComponent();
        }

        /// <summary>
        /// Mouse down in title bar.
        /// </summary>
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left) {
                if (!(Width == SystemParameters.PrimaryScreenWidth && Height == SystemParameters.PrimaryScreenHeight)) {
                    DragMove();
                }
            }
        }

        /// <summary>
        /// Button for window minimize.
        /// </summary>
        private void btnTitleBarMinimize_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Button for ends running of the aplication
        /// </summary>
        private void btnTitleBarExit_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e) {
            _delete = true;
            Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e) {
            _delete = false;
            Close();
        }
    }
}
