using FlickrApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FlickrApp.Models.PhotoDetails
{
    [XmlRoot(ElementName = "owner")]
    public class Owner
    {
        [XmlAttribute(AttributeName = "nsid")]
        public string Nsid { get; set; }
        [XmlAttribute(AttributeName = "username")]
        public string Username { get; set; }
        [XmlAttribute(AttributeName = "realname")]
        public string Realname { get; set; }
        [XmlAttribute(AttributeName = "location")]
        public string Location { get; set; }
        [XmlAttribute(AttributeName = "iconserver")]
        public string Iconserver { get; set; }
        [XmlAttribute(AttributeName = "iconfarm")]
        public string Iconfarm { get; set; }
        [XmlAttribute(AttributeName = "path_alias")]
        public string Path_alias { get; set; }
    }

    [XmlRoot(ElementName = "visibility")]
    public class Visibility
    {
        [XmlAttribute(AttributeName = "ispublic")]
        public string Ispublic { get; set; }
        [XmlAttribute(AttributeName = "isfriend")]
        public string Isfriend { get; set; }
        [XmlAttribute(AttributeName = "isfamily")]
        public string Isfamily { get; set; }
    }

    [XmlRoot(ElementName = "dates")]
    public class Dates
    {
        [XmlAttribute(AttributeName = "posted")]
        public string Posted { get; set; }
        [XmlAttribute(AttributeName = "taken")]
        public string Taken { get; set; }
        [XmlAttribute(AttributeName = "takengranularity")]
        public string Takengranularity { get; set; }
        [XmlAttribute(AttributeName = "takenunknown")]
        public string Takenunknown { get; set; }
        [XmlAttribute(AttributeName = "lastupdate")]
        public string Lastupdate { get; set; }
    }

    [XmlRoot(ElementName = "editability")]
    public class Editability
    {
        [XmlAttribute(AttributeName = "cancomment")]
        public string Cancomment { get; set; }
        [XmlAttribute(AttributeName = "canaddmeta")]
        public string Canaddmeta { get; set; }
    }

    [XmlRoot(ElementName = "publiceditability")]
    public class Publiceditability
    {
        [XmlAttribute(AttributeName = "cancomment")]
        public string Cancomment { get; set; }
        [XmlAttribute(AttributeName = "canaddmeta")]
        public string Canaddmeta { get; set; }
    }

    [XmlRoot(ElementName = "usage")]
    public class Usage
    {
        [XmlAttribute(AttributeName = "candownload")]
        public string Candownload { get; set; }
        [XmlAttribute(AttributeName = "canblog")]
        public string Canblog { get; set; }
        [XmlAttribute(AttributeName = "canprint")]
        public string Canprint { get; set; }
        [XmlAttribute(AttributeName = "canshare")]
        public string Canshare { get; set; }
    }

    [XmlRoot(ElementName = "people")]
    public class People
    {
        [XmlAttribute(AttributeName = "haspeople")]
        public string Haspeople { get; set; }
    }

    [XmlRoot(ElementName = "tag")]
    public class Tag
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "author")]
        public string Author { get; set; }
        [XmlAttribute(AttributeName = "authorname")]
        public string Authorname { get; set; }
        [XmlAttribute(AttributeName = "raw")]
        public string Raw { get; set; }
        [XmlAttribute(AttributeName = "machine_tag")]
        public string Machine_tag { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "tags")]
    public class Tags
    {
        [XmlElement(ElementName = "tag")]
        public List<Tag> Tag { get; set; }
    }

    [XmlRoot(ElementName = "url")]
    public class Url
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "urls")]
    public class Urls
    {
        [XmlElement(ElementName = "url")]
        public Url Url { get; set; }
    }

    [XmlRoot(ElementName = "photo")]
    public class PhotoDetails
    {
        [XmlElement(ElementName = "owner")]
        public Owner Owner { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "visibility")]
        public Visibility Visibility { get; set; }
        [XmlElement(ElementName = "dates")]
        public Dates Dates { get; set; }
        [XmlElement(ElementName = "editability")]
        public Editability Editability { get; set; }
        [XmlElement(ElementName = "publiceditability")]
        public Publiceditability Publiceditability { get; set; }
        [XmlElement(ElementName = "usage")]
        public Usage Usage { get; set; }
        [XmlElement(ElementName = "comments")]
        public string Comments { get; set; }
        [XmlElement(ElementName = "notes")]
        public string Notes { get; set; }
        [XmlElement(ElementName = "people")]
        public People People { get; set; }
        [XmlElement(ElementName = "tags")]
        public Tags Tags { get; set; }
        [XmlElement(ElementName = "urls")]
        public Urls Urls { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "secret")]
        public string Secret { get; set; }
        [XmlAttribute(AttributeName = "server")]
        public string Server { get; set; }
        [XmlAttribute(AttributeName = "farm")]
        public string Farm { get; set; }
        [XmlAttribute(AttributeName = "dateuploaded")]
        public string Dateuploaded { get; set; }
        [XmlAttribute(AttributeName = "isfavorite")]
        public string Isfavorite { get; set; }
        [XmlAttribute(AttributeName = "license")]
        public string License { get; set; }
        [XmlAttribute(AttributeName = "safety_level")]
        public string Safety_level { get; set; }
        [XmlAttribute(AttributeName = "rotation")]
        public string Rotation { get; set; }
        [XmlAttribute(AttributeName = "originalsecret")]
        public string Originalsecret { get; set; }
        [XmlAttribute(AttributeName = "originalformat")]
        public string Originalformat { get; set; }
        [XmlAttribute(AttributeName = "views")]
        public string Views { get; set; }
        [XmlAttribute(AttributeName = "media")]
        public string Media { get; set; }

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

    [XmlRoot(ElementName = "rsp")]
    public class Rsp
    {
        [XmlElement(ElementName = "photo")]
        public PhotoDetails Photo { get; set; }
        [XmlAttribute(AttributeName = "stat")]
        public string Stat { get; set; }
    }    
   
}
