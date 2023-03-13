using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game
{
    public class GameScreenEffectController : MonoBehaviour
    {
        [SerializeField] private Image _flashImage;
        [SerializeField] private Image _blackMaskImage;
        [SerializeField] private LeanTweenType _type = LeanTweenType.linear;
        [SerializeField] private float _inSeconds = 0.1f;

        private void Start()
        {
            _flashImage.color = new Color(_flashImage.color.r, _flashImage.color.g, _flashImage.color.b, 0f);
        }

        public void ShowFlashEffect(Color color, float duration)
        {
            LeanTween.cancel(_flashImage.gameObject);
            _flashImage.color = new Color(_flashImage.color.r, _flashImage.color.g, _flashImage.color.b, 0f);

            _flashImage.color = new Color(color.r, color.g, color.b, 0f);
            var outSeconds = duration - _inSeconds;

            _flashImage.rectTransform.LeanAlpha(0.5f, _inSeconds).setEase(_type).setOnComplete(() =>
               _flashImage.rectTransform.LeanAlpha(0f, outSeconds).setEase(_type)
           );
        }

        public void FadeIn(float duration)
        {
            LeanTween.cancel(_blackMaskImage.gameObject);
            LeanTween.alpha(_blackMaskImage.gameObject, 1f, duration);
        }

        public void FadeOut(float duration)
        {
            LeanTween.cancel(_blackMaskImage.gameObject);
            LeanTween.alpha(_blackMaskImage.gameObject, 0f, duration);
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Flash"))
            {
                ShowFlashEffect(_flashImage.color, 3f);
            }
        }
    }
}
