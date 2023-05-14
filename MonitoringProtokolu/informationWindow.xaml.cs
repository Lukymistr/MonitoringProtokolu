using System;
using System.Windows;
using System.Windows.Input;

namespace MonitoringProtokolu {
    /// <summary>
    /// Interakční logika pro InformationWindow.xaml
    /// </summary>
    public partial class InformationWindow : Window {
        public InformationWindow(String message, int fontSize) {
            InitializeComponent();
            lblMessage.Content = message;
            lblMessage.FontSize = fontSize;
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

        /// <summary>
        /// Button for ends running of the aplication
        /// </summary>
        private void btnClose_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
