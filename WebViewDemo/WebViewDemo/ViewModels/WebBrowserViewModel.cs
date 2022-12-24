using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Wpf.UI.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Unity;
using Unity.Attributes;
using WebViewDemo.Models;
using WebViewDemo.Views;

namespace WebViewDemo.ViewModels
{
    class WebBrowserViewModel : BindableBase
    {
        [Dependency]
        public MyAppSettings Settings { get; set; }
        public WebInformation WebInformation => Settings.WebInformation;
        public Configuration Configuration => Settings.Configuration;

        public DelegateCommand<object> PreviousPageCommand { get; }
        public DelegateCommand<object> NextPageCommand { get; }
        public DelegateCommand<object> RefreshCommand { get; }
        public DelegateCommand<object> FilterCommand { get; }
        public DelegateCommand ConfigurationCommand { get; }

        public WebBrowserViewModel(IUnityContainer container, IRegionManager regionManager)
        {
            Settings = container.Resolve<MyAppSettings>();

            PreviousPageCommand = new DelegateCommand<object>(o => (o as WebView).GoBack(), o => (o as WebView)?.CanGoBack ?? false);
            NextPageCommand = new DelegateCommand<object>(o => (o as WebView).GoForward(), o => (o as WebView)?.CanGoForward ?? false);
            RefreshCommand = new DelegateCommand<object>(o => (o as WebView).Refresh());

            FilterCommand = new DelegateCommand<object>(async o =>
            {
                using (var filter = new RikunabiFilter())
                {
                    var webView = o as WebView;
                    var script = "var items = document.querySelector('.rnn-jobOfferList__item');";

                    script += "window.external.notify(items.toString());";

                    var htmlText = await webView.InvokeScriptAsync("eval", script);

                    //webView.NavigateToString(await filter.FilterAsync(htmlText));
                }
            });

            ConfigurationCommand = new DelegateCommand(() => regionManager.RequestNavigate("MainWindowContentRegion", nameof(ConfigurationView)));
        }
    }
}
