using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Unity.Attributes;
using WebViewDemo.Models;
using WebViewDemo.Views;

namespace WebViewDemo.ViewModels
{
    class ConfigurationViewModel : BindableBase
    {
        [Dependency]
        public MyAppSettings Settings { get; set; }
        public Configuration Configuration => Settings.Configuration;
        
        public DelegateCommand InitializeSizeCommand { get; }
        public DelegateCommand OKCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public ConfigurationViewModel(IRegionManager regionManager)
        {
            InitializeSizeCommand = new DelegateCommand(() =>
            {
                Application.Current.MainWindow.Width = MyAppSettings.DefaultWindowWidth;
                Application.Current.MainWindow.Height = MyAppSettings.DefaultWindowHeight;
            });

            OKCommand = new DelegateCommand(() => regionManager.RequestNavigate("MainWindowContentRegion", nameof(WebBrowserView)));
            CancelCommand = new DelegateCommand(() => regionManager.RequestNavigate("MainWindowContentRegion", nameof(WebBrowserView)));
        }
    }
}
