using TMPro;
using UnityEngine;

namespace EG.Tower.Game
{
    public class TraitUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _viceNameLabel;
        [SerializeField] private TMP_Text _virtueNameLabel;
        [SerializeField] private ProgressBarUI _progressBar;

        public string VirtueName => _trait != null ? _trait.Virtue : string.Empty;
        public string ViceName => _trait != null ? _trait.Vice : string.Empty;

        private Trait _trait;

        public void Init(Trait trait)
        {
            _trait = trait;
            _viceNameLabel.text = trait.Vice;
            _virtueNameLabel.text = trait.Virtue;
            _progressBar.Init(trait.MaxValue, trait.DefaultValue);
            _trait.OnValueChangedEvent += HandleValueChangedEvent;
        }

        private void HandleValueChangedEvent(int value)
        {
            _progressBar.SetCurrent(value);
        }
    }
}