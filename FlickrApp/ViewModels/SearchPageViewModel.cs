using FlickrApp.Common;
using FlickrApp.Helpers;
using FlickrApp.Models.Photo;
using FlickrApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FlickrApp.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        private readonly IPhotosDataServices _photosDataServices;

        private int PageNumber;
        private int PageCount;

        AppSettingsHelper AppSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="photosDataServices"></param>
        public SearchPageViewModel(IPhotosDataServices photosDataServices)
        {
            _photosDataServices = photosDataServices;
            PhotosList = new ObservableCollection<Photo>();
            AppSettings = new AppSettingsHelper();
        }


        #region Properties

        bool _progressVisibility;
        public bool ProgressVisibility
        {
            get { return _progressVisibility; }
            set { Set(ref _progressVisibility, value); }
        }
        
        private ObservableCollection<Photo> _PhotosList;
        public ObservableCollection<Photo> PhotosList
        {
            get { return _PhotosList; }
            private set { Set(ref _PhotosList, value); }
        }

        string _searchTerm;
        public string SearchTerm
        {
            get { return _searchTerm; }
            set { Set(ref _searchTerm, value); }
        }

        #endregion


        #region Event handlers

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (parameter != null)
            {
                SearchTerm = parameter.ToString();
                LoadSearchResultList();
            }
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void SearchQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            SearchTerm = sender.Text;
            PhotosList.Clear();
            LoadSearchResultList();
        }

        public async void SearchTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var matchingContacts = await GetSearchSuggestionsList(sender.Text);
                if (matchingContacts.Count == 0)
                    matchingContacts.Add(ResourceLoader.GetForCurrentView().GetString("NoResults"));
                sender.ItemsSource = matchingContacts;
            }
        }

        public void SearchClick(object sender, RoutedEventArgs e)
        {
            Button _myButton = (Button)sender;
            SearchTerm = _myButton.CommandParameter.ToString();
            PhotosList.Clear();
            LoadSearchResultList();
        }

        public async void GridViewItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedItem = e.ClickedItem as Photo;
            if (selectedItem != null)
                await NavigationService.NavigateAsync(typeof(Views.DetailsPage),
                    selectedItem.Id);
        }

        #endregion


        #region Methods

        public async void LoadSearchResultList()
        {            
            if (SearchTerm != "")
                await GetSearchResultList(SearchTerm);
            else
                ShowError(ResourceLoader.GetForCurrentView().GetString("EmptySearchMessage"));            
        }

        private async Task GetSearchResultList(string text)
        {
            if (PageNumber <= PageCount)
            {
                PageNumber++;
                ProgressVisibility = true;

                var photosresult = await _photosDataServices.GetPhotosSearchResultAsync(PageNumber, text);
                if (photosresult.Successful)
                {                    
                    if (photosresult.Content != null && photosresult.Content.Photo.Count > 0)
                    {
                        PageCount = int.Parse(photosresult.Content.Pages);
                        foreach (var item in photosresult.Content.Photo)
                        {
                            PhotosList.Add(item);
                        }
                    }
                }
                else
                {
                    ShowError(photosresult.Message);
                }

                ProgressVisibility = false;

                AppSettings.AddSetting(Constants.LastSearchKey, text);
            }
        }

        private async Task<List<string>> GetSearchSuggestionsList(string text)
        {
            var suggestions = new List<string>();
            var photosresult = await _photosDataServices.GetPhotosSearchResultAsync(1, text);
            if (photosresult.Successful)
            {
                if (photosresult.Content != null)
                {
                    foreach (var item in photosresult.Content.Photo)
                    {
                        suggestions.Add(item.Title);
                    }
                }
            }
            else
            {
                ShowError(photosresult.Message);
            }

            ProgressVisibility = false;            

            return suggestions;
        }                

        private async void ShowError(string message)
        {
            ContentDialog messageDialog = new ContentDialog()
            {
                Content = message,
                PrimaryButtonText = ResourceLoader.GetForCurrentView().GetString("Close")
            };

            await messageDialog.ShowAsync();
        }

        #endregion

    }
}
