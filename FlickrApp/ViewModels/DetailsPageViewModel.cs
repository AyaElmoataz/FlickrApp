using FlickrApp.Models.PhotoDetails;
using FlickrApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FlickrApp.ViewModels
{
    public class DetailsPageViewModel : ViewModelBase
    {
        private readonly IPhotosDataServices _photosDataServices;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="photosDataServices"></param>
        public DetailsPageViewModel(IPhotosDataServices photosDataServices)
        {
            _photosDataServices = photosDataServices;
            PhotoDetails = new PhotoDetails();
            ProgressVisibility = true;
        }


        #region Properties

        bool _progressVisibility;
        public bool ProgressVisibility
        {
            get { return _progressVisibility; }
            set { Set(ref _progressVisibility, value); }
        }

        private PhotoDetails _photoDetails;
        public PhotoDetails PhotoDetails
        {
            get { return _photoDetails; }
            private set { Set(ref _photoDetails, value); }
        }

        #endregion


        #region Event handlers

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (parameter != null)
            {
                var photoId = parameter.ToString();
                LoadPhotoDetails(photoId);
            }
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        #endregion        


        #region Methods

        public async void LoadPhotoDetails(string photoId)
        {
            await GetPhotoDetails(photoId);
        }

        private async Task GetPhotoDetails(string id)
        {
            var photosresult = await _photosDataServices.GetPhotoDetailsResultAsync(id);
            if (photosresult.Successful)
            {
                PhotoDetails = photosresult.Content;
            }
            else
            {
                ShowError(photosresult.Message);
            }

            ProgressVisibility = false;
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
