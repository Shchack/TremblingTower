using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game
{
    public class GameScreenEffectController : MonoBehaviour
    {
        [SerializeField] private Image _flashImage;
        [SerializeField] private LeanTweenType _type = LeanTweenType.linear;
        [SerializeField] private float _inSeconds = 0.1f;
        [SerializeField] private float _outSeconds = 0.5f;

        private void Start()
        {
            _flashImage.color = new Color(_flashImage.color.r, _flashImage.color.g, _flashImage.color.b, 0f);
        }

        public void ShowFlashEffect(Color color, float duration)
        {
            _flashImage.color = new Color(color.r, color.g, color.b, 0f);
            var outSeconds = duration - _inSeconds;

            _flashImage.rectTransform.LeanAlpha(0.5f, _inSeconds).setEase(_type).setOnComplete(() =>
               _flashImage.rectTransform.LeanAlpha(0f, outSeconds).setEase(_type)
           );
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
