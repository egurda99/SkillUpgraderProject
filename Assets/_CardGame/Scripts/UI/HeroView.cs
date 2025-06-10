using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    //Менять нельзя!
    public sealed class HeroView : MonoBehaviour
    {
        public event UnityAction OnClicked
        {
            add { button.onClick.AddListener(value); }
            remove { button.onClick.RemoveListener(value); }
        }

        [SerializeField] private Image heroImage;

        [SerializeField] private TMP_Text stats;

        [SerializeField] private Button button;

        [Header("Active")] [SerializeField] [Space]
        private Image activeImage;

        [SerializeField] private Sprite activeIcon;

        [SerializeField] private Sprite inactiveIcon;

        [SerializeField] private GameObject activeBlur;

        [Header("Attack")] [SerializeField] private RectTransform center;

        [SerializeField] private float forwardDuration = 0.2f;

        [SerializeField] private AnimationCurve attackCurve;

        [SerializeField] private AnimationCurve scaleCurve;

        [SerializeField] private float backDuration = 0.5f;

        [SerializeField] private AudioClip punchSFX;

        private Sequence attackAnimation;

        private AudioPlayer audioPlayer;

        private void Start()
        {
            audioPlayer = AudioPlayer.Instance;
        }

        [Button]
        public void SetIcon(Sprite icon)
        {
            heroImage.sprite = icon;
        }

        [Button]
        public void SetStats(string stats)
        {
            this.stats.text = stats;
        }

        [Button]
        public void SetActive(bool isActive)
        {
            activeImage.sprite = isActive ? activeIcon : inactiveIcon;
            activeBlur.SetActive(isActive);
        }

        [Button]
        public UniTask AnimateAttack(HeroView target)
        {
            if (attackAnimation != null)
            {
                return UniTask.CompletedTask;
            }

            var tcs = new UniTaskCompletionSource();

            var sourcePosition = center.position;
            var targetPosition = target.center.position;

            attackAnimation = DOTween
                .Sequence()
                .Append(center.DOMove(targetPosition, forwardDuration).SetEase(attackCurve))
                .Join(center.DOScale(1.25f, forwardDuration).SetEase(scaleCurve))
                .AppendCallback(() => audioPlayer.PlaySound(punchSFX))
                .Append(center.DOMove(sourcePosition, backDuration))
                .Join(center.DOScale(1, backDuration))
                .OnComplete(() =>
                {
                    attackAnimation = null;
                    tcs.TrySetResult();
                });

            return tcs.Task;
        }
    }
}
