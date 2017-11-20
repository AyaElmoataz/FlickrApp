using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrApp.Services.ServicesHelpers
{
    public class ServiceResult<T>
    {
        public T Content { get; set; }
        public bool Successful { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}
