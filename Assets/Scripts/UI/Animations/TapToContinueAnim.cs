using UnityEngine;

namespace TalentCity.UI.Animations
{
    public class TapToContinueAnim : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _delayBeforeChangeAlpha = 1f;
        [SerializeField] private float _alphaMin = 0.1f;
        [SerializeField] private float _alphaMax = 1f;
        [SerializeField] private float _oneWayAlphaDuration = 1f;

        private float _currentTimer = 0f;
        private float _alphaDelta;
        private bool _isAlphaChanging = false;
        private bool _isAlphaGrowing = false;
        
        private void Start()
        {
            _canvasGroup.alpha = 0f;
            _alphaDelta = _alphaMax - _alphaMin;
            ReplayAnimation();
        }

        public void ReplayAnimation()
        {
            _canvasGroup.alpha = 0f;
            _isAlphaChanging = false;
            CancelInvoke(nameof(StartChangeAlpha));
            Invoke(nameof(StartChangeAlpha), _delayBeforeChangeAlpha);
        }

        private void StartChangeAlpha()
        {
            _isAlphaChanging = true;
            _isAlphaGrowing = true;
        }

        private void Update()
        {
            if (!_isAlphaChanging)
                return;

            _currentTimer = _isAlphaGrowing
                ? Mathf.Clamp(_currentTimer + Time.deltaTime, 0, _oneWayAlphaDuration)
                : Mathf.Clamp(_currentTimer - Time.deltaTime, 0, _oneWayAlphaDuration);

            var currentProgress = _currentTimer / _oneWayAlphaDuration;
            _canvasGroup.alpha = _alphaMin + _alphaDelta * currentProgress;

            if (currentProgress < float.Epsilon || currentProgress >= 1f)
            {
                _isAlphaGrowing = !_isAlphaGrowing;
            }
        }
        
    }
}
