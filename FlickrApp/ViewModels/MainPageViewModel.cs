﻿using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using FlickrApp.Services;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Resources;
using FlickrApp.Models;
using FlickrApp.Models.Photo;
using FlickrApp.Helpers;
using System.Collections.ObjectModel;
using FlickrApp.Common;

namespace FlickrApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IPhotosDataServices _photosDataServices;

        private int PageNumber;
        private int PageCount;

        AppSettingsHelper AppSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="photosDataServices"></param>
        public MainPageViewModel(IPhotosDataServices photosDataServices)
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

        #endregion


        #region Event handlers

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            object searchItem = AppSettings.RetrieveSetting(Constants.LastSearchKey);
            if (searchItem != null)
            {
                var searchTerm = searchItem.ToString();
                NavigateToSearchPage(searchTerm);                
            }
            else
                LoadPhotos();

            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        private async void NavigateToSearchPage(string searchTerm)
        {
            if (searchTerm != "")
                await NavigationService.NavigateAsync(typeof(Views.SearchPage),
                       searchTerm);
            else
                ShowError(ResourceLoader.GetForCurrentView().GetString("EnterSearchKeywordError"));
        }

        public async void SearchQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var searchTerm = args.QueryText;
            if (searchTerm != "")
                await NavigationService.NavigateAsync(typeof(Views.SearchPage),
                   searchTerm);
            else
                ShowError(ResourceLoader.GetForCurrentView().GetString("EnterSearchKeywordError"));
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

        #endregion


        #region Methods

        public async void LoadPhotos()
        {
            await GetPhotosList();
        }                       
        
        private async Task GetPhotosList()
        {
            if (PageNumber <= PageCount)
            {
                PageNumber++;
                ProgressVisibility = true;

                var photosresult = await _photosDataServices.GetPhotosAsync(PageNumber);
                if (photosresult.Successful)
                {
                    if (photosresult.Content.Photo != null && photosresult.Content.Photo.Count > 0)
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
