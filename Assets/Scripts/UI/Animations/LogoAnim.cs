using UnityEngine;

namespace TalentCity.UI.Animations
{
    public class LogoAnim : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _alphaFrom = 0f;
        [SerializeField] private float _alphaTo = 1f;
        [SerializeField] private float _scaleFrom = 0.3f;
        [SerializeField] private float _scaleTo = 1f;
        [SerializeField] private float _duration = 1f;

        private float _currentTimer;
        private bool _isPlaying;
        
        private void Awake()
        {
            Play();
        }

        private void Play()
        {
            transform.localScale = Vector3.one * _scaleFrom;
            _canvasGroup.alpha = _alphaFrom;
            _currentTimer = 0f;
            _isPlaying = true;
        }

        private void Update()
        {
            if (!_isPlaying)
                return;

            _currentTimer = Mathf.Clamp(_currentTimer + Time.deltaTime, 0f, _duration);
            float progress = _currentTimer / _duration;
            float scaleDelta = _scaleTo - _scaleFrom;
            transform.localScale = Vector3.one * (_scaleFrom + scaleDelta * progress);
            float alphaDelta = _alphaTo - _alphaFrom;
            _canvasGroup.alpha = _alphaFrom + alphaDelta * progress;

            if (progress >= 1f)
            {
                _isPlaying = false;
            }
        }
        
    }
}

