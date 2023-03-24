using System.Windows;
using System.Windows.Input;

namespace MonitoringProtokolu {
    public partial class MainWindow : Window {
        private Point startPoint;
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
                    startPoint = e.GetPosition(this);
                    if (WindowState == WindowState.Maximized) {
                        WindowState = WindowState.Normal;
                    }
                    DragMove();
                }
            }
        }
    }
}