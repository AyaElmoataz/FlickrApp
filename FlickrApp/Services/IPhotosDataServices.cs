using FlickrApp.Models;
using FlickrApp.Models.Photo;
using FlickrApp.Models.PhotoDetails;
using FlickrApp.Services.ServicesHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrApp.Services
{
    public interface IPhotosDataServices
    {
        Task<ServiceResult<Photos>> GetPhotosAsync(int pageNumber);
        Task<ServiceResult<Photos>> SyncPhotosAsync(int pageNumber);

        Task<ServiceResult<Photos>> GetPhotosSearchResultAsync(int pageNumber, string text);
        Task<ServiceResult<Photos>> SyncPhotosSearchResultAsync(int pageNumber, string text);

        Task<ServiceResult<PhotoDetails>> GetPhotoDetailsResultAsync(string photoId);
        Task<ServiceResult<PhotoDetails>> SyncPhotoDetailsResultAsync(string photoId);
    }
}
