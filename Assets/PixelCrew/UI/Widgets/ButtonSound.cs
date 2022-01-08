using Assets.PixelCrew.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.PixelCrew.UI.Widgets
{
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip _audioClip;

        private AudioSource _source;
        public void OnPointerClick(PointerEventData eventData)
        {
            _source = _source != null ? _source : AudioUtilits.FindSfxSource();
            _source.PlayOneShot(_audioClip);
        }
    }
}