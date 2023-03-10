using EG.Tower.Game;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Missions
{
    public class MissionStepHeroUI : MonoBehaviour
    {
        public event Action<HeroModel> OnHeroSelectedEvent;

        [SerializeField] private Button _selectButton;
        [SerializeField] private Image _portraitImage;
        [SerializeField] private TMP_Text _heroNameLabel;

        private HeroModel _hero;

        private void Awake()
        {
            _selectButton.onClick.AddListener(OnSelectButtonClicked);
        }

        private void OnSelectButtonClicked()
        {
            if (_hero != null)
            {
                OnHeroSelectedEvent?.Invoke(_hero);
            }
            else
            {
                Debug.LogError("Hero model not set in UI selector");
            }
        }

        public void Init(int i, HeroModel hero)
        {
            _hero = hero;
            _portraitImage.sprite = hero.Portrait;
            _heroNameLabel.text = hero.Name;
        }
    }
}
