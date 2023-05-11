using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Shell;

namespace MonitoringProtokolu {
    /// <summary>
    /// Interakční logika pro informationWindow.xaml
    /// </summary>
    public partial class informationWindow : Window {
        public informationWindow(String message) {
            InitializeComponent();
            lblMessage.Content = message;
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
