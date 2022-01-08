using Assets.PixelCrew.Utils;
using UnityEngine;

namespace Assets.PixelCrew.Components.Audio
{
    public class PlaySfxSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        private AudioSource _source;

        public void Play()
        {
            _source = _source != null ? _source : AudioUtilits.FindSfxSource();
            _source.PlayOneShot(_clip);
        }
    }
}