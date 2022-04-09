using System;
using System.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath.MediaAccess
{
    /// <summary>
    /// Base class for all media file accessors that also checks whether the media file is of given accepted type.
    /// </summary>
    public abstract class MediaFileAccessor<T>
    {
        protected readonly MediaFile _mediaFile;
        private MediaFileType[] _acceptedMediaFileTypes;

        protected MediaFileAccessor(MediaFile mediaFile, MediaFileType[] acceptedMediaFileTypes)
        {
            _mediaFile = mediaFile ?? throw new ArgumentException("Attempt to access null media file!", nameof(mediaFile));
            _acceptedMediaFileTypes = acceptedMediaFileTypes;

            if (!_acceptedMediaFileTypes.Contains(mediaFile.Type))
            {
                throw new ArgumentException("Invalid MediaFile Type!", nameof(mediaFile));
            }
        }

        public T GetMedia()
        {
            if (_mediaFile.StorageType == MediaFileStorageType.Local)
            {
                return GetLocalMedia();
            }
            else
            {
                return GetRemoteMedia();
            }
        }

        protected abstract T GetLocalMedia();
        protected abstract T GetRemoteMedia();
    }
}