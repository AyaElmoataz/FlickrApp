using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrApp.Common
{
    public static class Constants
    {
        public const string ServiceURL = "https://api.flickr.com/services/rest/?";        
        public const string ApiKey = "6d7ad01225fa20c4e7aae4dd7e25a6bc";
        public const string PhotoUrlFormat = "https://farm{0}.staticflickr.com/{1}/{2}_{3}.jpg";
        public const string LastSearchKey = "LastSearch";
    }
}
