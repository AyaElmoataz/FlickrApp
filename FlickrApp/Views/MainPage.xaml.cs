using FlickrApp.ViewModels;
using System;
using Windows.UI.Xaml.Controls;

namespace FlickrApp.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            this.DataContext = ViewModel = new MainPageViewModel(new Services.PhotosDataServices());
        }

        public MainPageViewModel ViewModel { get; set; }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
                ViewModel.LoadPhotos();
        }
    }
}