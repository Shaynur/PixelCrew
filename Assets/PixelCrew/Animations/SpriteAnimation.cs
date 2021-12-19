using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.PixelCrew.Animations
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] [Range(1, 30)] private int _framerate = 10;
        [SerializeField] private StringEvent _onComplete;
        [SerializeField] private AnimationClip[] _clips;

        private SpriteRenderer _renderer;
        private float _secPerFrame;
        private float _nextFrameTime;
        private int _currentClip;
        private int _currentFrame;
        private bool _isPlaying = true;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _secPerFrame = 1f / _framerate;
            _currentClip = 0;

            StartAnimation();
        }

        private void OnBecameVisible()
        {
            enabled = _isPlaying;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        public void SetClip(string clipName)
        {
            for (var i = 0; i < _clips.Length; i++)
            {
                if (_clips[i].Name == clipName)
                {
                    _currentClip = i;
                    StartAnimation();
                    return;
                }
            }
        }

        private void StartAnimation()
        {
            _nextFrameTime = Time.time;
            enabled = _isPlaying = true;
            _currentFrame = 0;
        }

        private void OnEnable()
        {
            _nextFrameTime = Time.time;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time || _clips.Length < 1) return;

            var clip = _clips[_currentClip];
            if (_currentFrame >= clip.Sprites.Length)

            {
                if (clip.Loop)
                {
                    _currentFrame = 0;
                }
                else
                {
                    enabled = _isPlaying = clip.AllowNextClip;
                    clip.OnComplete?.Invoke();
                    //_onComplete?.Invoke( clip.Name );         //???
                    if (clip.AllowNextClip)
                    {
                        _currentFrame = 0;
                        _currentClip = (int)Mathf.Repeat(_currentClip + 1, _clips.Length);
                    }
                    else
                    {
                        _onComplete?.Invoke(clip.Name);       //???
                    }
                }
                return;
            }
            _renderer.sprite = clip.Sprites[_currentFrame];
            _nextFrameTime += _secPerFrame;
            _currentFrame++;
        }

        [Serializable]
        public class AnimationClip
        {
            [SerializeField] private string _name;
            [SerializeField] private Sprite[] _sprites;
            [SerializeField] private bool _loop;
            [SerializeField] private bool _allowNextClip;
            [SerializeField] private UnityEvent _onComplete;

            public string Name => _name;
            public Sprite[] Sprites => _sprites;
            public bool Loop => _loop;
            public bool AllowNextClip => _allowNextClip;
            public UnityEvent OnComplete => _onComplete;
        }

        [Serializable]
        public class StringEvent : UnityEvent<string>
        {
        }
    }
}
