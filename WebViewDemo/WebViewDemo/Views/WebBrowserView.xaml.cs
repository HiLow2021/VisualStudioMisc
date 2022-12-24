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
using Microsoft.Toolkit.Wpf.UI.Controls;
using WebViewDemo.ViewModels;

namespace WebViewDemo.Views
{
    /// <summary>
    /// WebBrowserView.xaml の相互作用ロジック
    /// </summary>
    public partial class WebBrowserView : UserControl
    {
        public WebBrowserView()
        {
            InitializeComponent();

            var context = DataContext as WebBrowserViewModel;
            var settings = context.Settings;
            var webInformation = settings.WebInformation;
            var configuration = settings.Configuration;

            webView1.NavigationStarting += (sender, e) =>
            {
                webInformation.CurrentUri = e.Uri;
            };

            webView1.NavigationCompleted += (sender, e) =>
            {
                var webView = sender as WebView;

                context.PreviousPageCommand.RaiseCanExecuteChanged();
                context.NextPageCommand.RaiseCanExecuteChanged();
            };

            webView1.NewWindowRequested += (sender, e) =>
            {
                var webView = sender as WebView;

                webView.Navigate(e.Uri);
                e.Handled = true;
            };

            webView1.ScriptNotify += (sender, e) =>
            {
                var temp = e.Value;
            };
        }
    }
}
