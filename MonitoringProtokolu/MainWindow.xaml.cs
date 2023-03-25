using System;
using System.Windows;
using System.Windows.Input;

namespace MonitoringProtokolu {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left) {
                if (e.ClickCount == 2) {
                    if (WindowState == WindowState.Normal) {
                        WindowState = WindowState.Maximized;
                    } else {
                        WindowState = WindowState.Normal;
                    }
                } else {
                    DragMove();
                }
            }
        }

        private void btnTitleBarMinimize_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void btnTitleBarResize_Click(object sender, RoutedEventArgs e) {
            if (WindowState == WindowState.Maximized) {
                WindowState = WindowState.Normal;
            } else {
                WindowState = WindowState.Maximized;
            }
        }
        private void btnTitleBarExit_Click(object sender, RoutedEventArgs e) {
            Environment.Exit(0);
        }
    }
}