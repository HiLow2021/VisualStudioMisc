using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using My.IO;
using My.Security;
using My.Serialization;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using WebViewDemo.Models;
using WebViewDemo.Views;

namespace WebViewDemo
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : PrismApplication
    {
        public MyAppSettings Settings { get; private set; }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void OnInitialized()
        {
            var regionManager = Container.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion("MainWindowContentRegion", typeof(WebBrowserView));
            regionManager.RegisterViewWithRegion("MainWindowContentRegion", typeof(ConfigurationView));

            MainWindow.Loaded += MainWindow_Loaded;
            MainWindow.Closing += MainWindow_Closing;

            base.OnInitialized();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if (FileAdvanced.Exists(MyAppSettings.ConfigPath))
            {
                var data = FileAdvanced.LoadFromBinaryFile<byte[]>(MyAppSettings.ConfigPath);

                Settings = DataSerializer.FromJsonToObject<MyAppSettings>(Cryptography.Decrypt<string>(data));
            }
            else
            {
                Settings = new MyAppSettings();
            }

            containerRegistry.RegisterInstance(Settings);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var configuration = Settings.Configuration;

            MainWindow.Topmost = configuration.TopMost;

            if (configuration.IsSaveWindowPosition)
            {
                MainWindow.Left = configuration.Left;
                MainWindow.Top = configuration.Top;
            }
            if (configuration.IsSaveWindowSize)
            {
                MainWindow.Width = configuration.Width;
                MainWindow.Height = configuration.Height;
            }
        }

        private void MainWindow_Closing(object sender, EventArgs e)
        {
            var configuration = Settings.Configuration;

            if (MainWindow.WindowState == WindowState.Normal)
            {
                configuration.Left = MainWindow.Left;
                configuration.Top = MainWindow.Top;
                configuration.Width = MainWindow.Width;
                configuration.Height = MainWindow.Height;
            }

            var data = Cryptography.Encrypt(DataSerializer.FromObjectToJson(Settings));

            FileAdvanced.SaveToBinaryFile(MyAppSettings.ConfigPath, data);
        }
    }
}
