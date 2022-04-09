using System;
using PolSl.UrbanHealthPath.PathData;
using UnityEngine;
using UnityEngine.Video;

namespace PolSl.UrbanHealthPath.MediaAccess
{
    /// <summary>
    /// Class responsible for accessing video files as Unity's VideoClips.
    /// </summary>
    public class VideoFileAccessor : MediaFileAccessor<VideoClip>
    {
        public VideoFileAccessor(MediaFile mediaFile) : base(mediaFile, new [] {MediaFileType.Video})
        {
        }

        protected override VideoClip GetLocalMedia()
        {
            return Resources.Load<VideoClip>(_mediaFile.Path);
        }

        protected override VideoClip GetRemoteMedia()
        {
            throw new NotImplementedException();
        }
    }
}