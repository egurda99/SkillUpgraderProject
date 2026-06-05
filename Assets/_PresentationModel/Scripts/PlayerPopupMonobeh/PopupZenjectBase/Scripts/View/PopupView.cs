using UnityEngine;
using DG.Tweening;

namespace Modules.Popups
{
    public class PopupView : MonoBehaviour
    {
        [SerializeField]
        private float _animationDuration = 0.35f;

        [SerializeField]
        private Transform _animationRoot;

        private Tween _tween;

        public void Show() => this.gameObject.SetActive(true);

        public void Hide() => this.gameObject.SetActive(false);

        public void AnimateShow()
        {
            _tween?.Kill();
            this.Show();

            _animationRoot.localScale = Vector3.zero;

            _tween = _animationRoot
                .DOScale(1f, _animationDuration)
                .SetEase(Ease.OutBack);
        }
        
        public void AnimateHide(TweenCallback callback = null)
        {
            _tween?.Kill();

            _tween = _animationRoot
                .DOScale(0f, _animationDuration)
                .SetEase(Ease.InBack)
                .OnComplete(callback);
        }

        protected virtual void Reset()
        {
            _animationRoot = this.transform;
        }
    }
}