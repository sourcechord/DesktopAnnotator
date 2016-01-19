using DesktopAnnotator.Settings;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopAnnotator
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get { return this.DataContext as MainWindowViewModel; } }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var win = new AboutWindow();
            win.Owner = this;
            win.ShowDialog();
        }

        private void Properties_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 設定画面を開く
            var win = new SettingWindow();
            win.Owner = this;
            var result = win.ShowDialog();
            if (result == true)
            {
                this.ViewModel.CurrentMode = this.ViewModel.CurrentMode;
            }
        }
    }
}
