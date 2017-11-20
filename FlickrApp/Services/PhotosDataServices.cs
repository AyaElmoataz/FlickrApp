using FlickrApp.Common;
using FlickrApp.Helpers;
using FlickrApp.Models;
using FlickrApp.Models.Photo;
using FlickrApp.Models.PhotoDetails;
using FlickrApp.Services.ServicesHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.ApplicationModel.Resources;
using Windows.Web.Http;

namespace FlickrApp.Services
{
    /// <summary>
    /// Thid class implements IPhotosDataServices which contains 
    /// all methods related to Photos retrieval from the Api
    /// </summary>
    public class PhotosDataServices : IPhotosDataServices
    {
        ICacheService _CacheService = new MemoryCacheService();

        /// <summary>
        /// Photos list method
        /// Used in MainPage to show data when opening app for the first time instead of showing blank page.
        /// </summary>
        private const string photosCacheKey = "PhotosDataService_CacheKey";

        public Task<ServiceResult<Photos>> GetPhotosAsync(int pageNumber)
        {
            var Photos = _CacheService.GetData<Photos>(photosCacheKey + pageNumber);
            if (Photos != null)
                return Task.FromResult(new ServiceResult<Photos> { Successful = true, Content = Photos });
            else
                return SyncPhotosAsync(pageNumber);
        }

        public async Task<ServiceResult<Photos>> SyncPhotosAsync(int pageNumber)
        {
            ServiceResult<Photos> result = new ServiceResult<Photos>();
            try
            {
                if (NetworkConnection.CheckNetwork())
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetAsync(new Uri
                            (Constants.ServiceURL + "method=flickr.photos.getRecent&api_key=" + Constants.ApiKey + "&page=" + pageNumber));

                        if (response.StatusCode == HttpStatusCode.Ok)
                        {
                            string photosXml = await response.Content.ReadAsStringAsync();


                            XmlSerializer serializer = new XmlSerializer(typeof(Models.Photo.Rsp));
                            StringReader reader = new StringReader(photosXml);
                            Models.Photo.Rsp resultingMessage = (Models.Photo.Rsp)serializer.Deserialize(reader);

                            _CacheService.SaveData(photosCacheKey + pageNumber, resultingMessage.Photos);
                            result.Successful = true;
                            result.Content = resultingMessage.Photos;
                                                        
                        }
                        else
                        {
                            result.Content = null;
                            result.Successful = false;
                            result.Message = ResourceLoader.GetForCurrentView().GetString("NetworkError");
                        }
                    }
                }
                else
                {
                    result.Message = ResourceLoader.GetForCurrentView().GetString("NoNetworkConnection");
                    result.Successful = false;
                }
            }
            catch (COMException)
            {
                result.Content = null;
                result.Successful = false;
                result.Message = ResourceLoader.GetForCurrentView().GetString("NetworkError");
            }
            catch (XmlException)
            {
                result.Content = null;
                result.Successful = false;
                result.Message = ResourceLoader.GetForCurrentView().GetString("NetworkError");
            }
            catch
            {
                result.Content = null;
                result.Successful = false;
                result.Message = ResourceLoader.GetForCurrentView().GetString("ServerError");
            }

            return result;
        }


        /// <summary>
        /// Photos search method
        /// Used in 2 places:
        /// In SearchPage to show data when hitting search button.
        /// In MainPage to show search suggestions.
        /// </summary>
        private const string searchCacheKey = "SearchPhotosDataService_CacheKey";

        public Task<ServiceResult<Photos>> GetPhotosSearchResultAsync(int pageNumber, string text)
        {
            var Photos = _CacheService.GetData<Photos>(searchCacheKey + pageNumber + "_" + text);
            if (Photos != null)
                return Task.FromResult(new ServiceResult<Photos> { Successful = true, Content = Photos });
            else
                return SyncPhotosSearchResultAsync(pageNumber, text);
        }

        public async Task<ServiceResult<Photos>> SyncPhotosSearchResultAsync(int pageNumber, string text)
        {
            ServiceResult<Photos> result = new ServiceResult<Photos>();
            try
            {
                if (NetworkConnection.CheckNetwork())
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetAsync(new Uri
                            (Constants.ServiceURL + "method=flickr.photos.search&api_key=" + Constants.ApiKey + "&tags=" + text + "&page=" + pageNumber));

                        if (response.StatusCode == HttpStatusCode.Ok)
                        {
                            string photosXml = await response.Content.ReadAsStringAsync();

                            XmlSerializer serializer = new XmlSerializer(typeof(Models.Photo.Rsp));
                            StringReader reader = new StringReader(photosXml);
                            Models.Photo.Rsp resultingMessage = (Models.Photo.Rsp)serializer.Deserialize(reader);

                            _CacheService.SaveData(searchCacheKey + pageNumber + "_" + text, resultingMessage.Photos);
                            result.Successful = true;
                            result.Content = resultingMessage.Photos;
                        }
                        else
                        {
                            result.Content = null;
                            result.Successful = false;
                            result.Message = ResourceLoader.GetForCurrentView().GetString("NetworkError");
                        }
                    }
                }
                else
                {
                    result.Message = ResourceLoader.GetForCurrentView().GetString("NoNetworkConnection");
                    result.Successful = false;
                }
            }
            catch (COMException)
            {
                result.Content = null;
                result.Successful = false;
                result.Message = ResourceLoader.GetForCurrentView().GetString("NetworkError");
            }
            catch (XmlException)
            {
                result.Content = null;
                result.Successful = false;
                result.Message = ResourceLoader.GetForCurrentView().GetString("NetworkError");
            }
            catch
            {
                result.Content = null;
                result.Successful = false;
                result.Message = ResourceLoader.GetForCurrentView().GetString("ServerError");
            }

            return result;
        }


        /// <summary>
        /// Photo details method
        /// Used in DetailsPage to show selected Photo details.
        /// </summary>
        private const string detailsCacheKey = "PhotoDetailsDataService_CacheKey";

        public Task<ServiceResult<PhotoDetails>> GetPhotoDetailsResultAsync(string id)
        {
            var Photos = _CacheService.GetData<PhotoDetails>(searchCacheKey + id);
            if (Photos != null)
                return Task.FromResult(new ServiceResult<PhotoDetails> { Successful = true, Content = Photos });
            else
                return SyncPhotoDetailsResultAsync(id);
        }

        public async Task<ServiceResult<PhotoDetails>> SyncPhotoDetailsResultAsync(string id)
        {
            ServiceResult<PhotoDetails> result = new ServiceResult<PhotoDetails>();
            try
            {
                if (NetworkConnection.CheckNetwork())
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetAsync(new Uri
                            (Constants.ServiceURL + "method=flickr.photos.getInfo&api_key=" + Constants.ApiKey + "&photo_id=" + id));

                        if (response.StatusCode == HttpStatusCode.Ok)
                        {
                            string photosXml = await response.Content.ReadAsStringAsync();

                            XmlSerializer serializer = new XmlSerializer(typeof(Models.PhotoDetails.Rsp));
                            StringReader reader = new StringReader(photosXml);
                            Models.PhotoDetails.Rsp resultingMessage = (Models.PhotoDetails.Rsp)serializer.Deserialize(reader);

                            _CacheService.SaveData(searchCacheKey + id, resultingMessage.Photo);
                            result.Successful = true;
                            result.Content = resultingMessage.Photo;
                        }
                        else
                        {
                            result.Content = null;
                            result.Successful = false;
                            result.Message = ResourceLoader.GetForCurrentView().GetString("NetworkError");
                        }
                    }
                }
                else
                {
                    result.Message = ResourceLoader.GetForCurrentView().GetString("NoNetworkConnection");
                    result.Successful = false;
                }
            }
            catch (COMException)
            {
                result.Content = null;
                result.Successful = false;
                result.Message = ResourceLoader.GetForCurrentView().GetString("NetworkError");
            }
            catch (XmlException)
            {
                result.Content = null;
                result.Successful = false;
                result.Message = ResourceLoader.GetForCurrentView().GetString("NetworkError");
            }
            catch
            {
                result.Content = null;
                result.Successful = false;
                result.Message = ResourceLoader.GetForCurrentView().GetString("ServerError");
            }

            return result;
        }
    }
}
