using FlickrApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FlickrApp.Models.Photo
{
    [XmlRoot(ElementName = "photo")]
    public class Photo
    {
        [XmlAttribute(AttributeName = "farm")]
        public string Farm { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "isfamily")]
        public string Isfamily { get; set; }

        [XmlAttribute(AttributeName = "isfriend")]
        public string Isfriend { get; set; }

        [XmlAttribute(AttributeName = "ispublic")]
        public string Ispublic { get; set; }

        [XmlAttribute(AttributeName = "owner")]
        public string Owner { get; set; }

        [XmlAttribute(AttributeName = "secret")]
        public string Secret { get; set; }

        [XmlAttribute(AttributeName = "server")]
        public string Server { get; set; }

        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }

        public string PhotoUrl
        {
            get
            {
                if (Farm != null && Server != null && Secret != null && Id != null)
                {                    
                    string[] items = { Farm, Server, Id, Secret };
                    var url = string.Format(Constants.PhotoUrlFormat, items);
                    return url;
                }
                else
                    return "ms-appx:///Assets/Square150x150Logo.scale-400.jpg";
            }
        }

    }

    [XmlRoot(ElementName = "photos")]
    public class Photos
    {
        [XmlAttribute(AttributeName = "page")]
        public string Page { get; set; }

        [XmlAttribute(AttributeName = "pages")]
        public string Pages { get; set; }

        [XmlAttribute(AttributeName = "perpage")]
        public string Perpage { get; set; }

        [XmlElement(ElementName = "photo")]
        public List<Photo> Photo { get; set; }

        [XmlAttribute(AttributeName = "total")]
        public string Total { get; set; }
    }

    [XmlRoot(ElementName = "rsp")]
    public class Rsp
    {
        [XmlElement(ElementName = "photos")]
        public Photos Photos { get; set; }

        [XmlAttribute(AttributeName = "stat")]
        public string Stat { get; set; }
    }
}
