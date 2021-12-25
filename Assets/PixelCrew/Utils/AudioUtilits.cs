using System.Collections;
using UnityEngine;

namespace Assets.PixelCrew.Utils
{
    public class AudioUtilits
    {
        public const string SfxSourceTag = "SfxAudioSource";

        public static AudioSource FindSfxSource()
        {
            return GameObject.FindWithTag(SfxSourceTag).GetComponent<AudioSource>();
        }
    }
}