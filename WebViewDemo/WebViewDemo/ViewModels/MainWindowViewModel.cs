using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using Unity.Attributes;
using WebViewDemo.Models;

namespace WebViewDemo.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        [Dependency]
        public MyAppSettings Settings { get; set; }
        public Configuration Configuration => Settings.Configuration;

        public DelegateCommand TopMostCommand { get; }

        public MainWindowViewModel()
        {
            TopMostCommand = new DelegateCommand(() =>
            {
                Configuration.TopMost = !Application.Current.MainWindow.Topmost;
                Application.Current.MainWindow.Topmost = Configuration.TopMost;
            });
        }
    }
}
