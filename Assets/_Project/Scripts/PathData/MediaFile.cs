namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Class that holds information about media file.
    /// </summary>
    public class MediaFile
    {
        public MediaFileType Type { get; }
        public MediaFileStorageType StorageType { get; }
        public string Path { get; }
        public string MediaId { get; }

        public MediaFile(string mediaId, MediaFileType type, MediaFileStorageType storageType, string path)
        {
            MediaId = mediaId;
            Type = type;
            StorageType = storageType;
            Path = path;
        }
    }
}