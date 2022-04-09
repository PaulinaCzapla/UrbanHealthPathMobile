using System;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Class that is able to parse JObject into MediaFile.
    /// </summary>
    public class MediaFileJsonParser : ValidatedJsonObjectParser<MediaFile>
    {
        private const string ID_KEY = "media_id";
        private const string TYPE_KEY = "type";
        private const string STORAGE_TYPE_KEY = "storage_type";
        private const string PATH_KEY = "path";
        
        public MediaFileJsonParser() : base(new [] {ID_KEY, TYPE_KEY, STORAGE_TYPE_KEY, PATH_KEY})
        {
        }

        protected override MediaFile ParseJsonObject(JObject json)
        {
            if (!Enum.TryParse(json[TYPE_KEY].Value<string>(), true, out MediaFileType mediaFileType))
            {
                throw new ParsingException();
            }
            
            if (!Enum.TryParse(json[STORAGE_TYPE_KEY].Value<string>(), true, out MediaFileStorageType mediaFileStorageType))
            {
                throw new ParsingException();
            }

            return new MediaFile(json[ID_KEY].Value<string>(), mediaFileType, mediaFileStorageType,
                json[PATH_KEY].Value<string>());
        }
    }
}