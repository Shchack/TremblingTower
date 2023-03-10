using EG.Tower.Game;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Missions
{
    public class MissionHeroSelectorUI : MonoBehaviour
    {
        public event Action<HeroModel> OnHeroSelectedEvent;

        [SerializeField] private Button _selectButton;
        [SerializeField] private Image _portrait;
        [SerializeField] private Image _plusIconImage;
        [SerializeField] private TMP_Text _selectTextLabel;
        [SerializeField] private RectTransform _optionsPanel;
        [SerializeField] private MissionStepHeroUI _heroUiPrefab;

        private void Awake()
        {
            _selectButton.onClick.AddListener(HandleSelectButtonClick);
            _optionsPanel.gameObject.SetActive(false);

            _portrait.color = new Color(_portrait.color.r, _portrait.color.g, _portrait.color.b, 0f);
            _plusIconImage.color = new Color(_plusIconImage.color.r, _plusIconImage.color.g, _plusIconImage.color.b, 1f);
        }

        private void HandleSelectButtonClick()
        {
            _optionsPanel.gameObject.SetActive(true);
        }

        public void Init(HeroModel[] heroes)
        {
            _portrait.color = new Color(_portrait.color.r, _portrait.color.g, _portrait.color.b, 0f);
            _plusIconImage.color = new Color(_plusIconImage.color.r, _plusIconImage.color.g, _plusIconImage.color.b, 1f);
            _selectTextLabel.text = "Select Character";

            var existingItems = _optionsPanel.GetComponentsInChildren<MissionStepHeroUI>();

            for (int i = 0; i < existingItems.Length; i++)
            {
                Destroy(existingItems[i].gameObject);
                existingItems[i].OnHeroSelectedEvent -= HandleHeroSelectedEvent;
            }

            for (int i = 0; i < heroes.Length; i++)
            {
                var heroUi = Instantiate(_heroUiPrefab, _optionsPanel.transform);
                heroUi.Init(i, heroes[i]);
                heroUi.OnHeroSelectedEvent += HandleHeroSelectedEvent;
            }
        }

        private void HandleHeroSelectedEvent(HeroModel hero)
        {
            _optionsPanel.gameObject.SetActive(false);

            if (hero != null)
            {
                _portrait.sprite = hero.Portrait;
                _portrait.color = new Color(_portrait.color.r, _portrait.color.g, _portrait.color.b, 1f);
                _plusIconImage.color = new Color(_plusIconImage.color.r, _plusIconImage.color.g, _plusIconImage.color.b, 0f);

                _selectTextLabel.text = hero.Name;
                OnHeroSelectedEvent?.Invoke(hero);
            }
            else
            {
                Debug.LogError("Skill check not set in UI option!");
            }
        }

        public void SetInteraction(bool isInteractable)
        {
            _selectButton.interactable = isInteractable;
        }
    }
}
