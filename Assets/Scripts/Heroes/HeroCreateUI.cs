using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game
{
    public class HeroCreateUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private DropDownExtended _mainVirtueDropdown;
        [SerializeField] private DropDownExtended _mainViceDropdown;
        [SerializeField] private RectTransform _traitsPanelUI;
        [SerializeField] private TraitUI _traitUiPrefab;
        [SerializeField] private Button _createHeroButton;

        [SerializeField] private Color _errorColor = Color.red;
        [SerializeField] private float _errorTime = 1f;

        private HeroCreateController _controller;
        private List<TraitUI> _traitUiItems;

        private void Awake()
        {
            _controller = FindObjectOfType<HeroCreateController>();
            _traitUiItems = new List<TraitUI>();
        }

        private void Start()
        {
            _createHeroButton.onClick.AddListener(HandleCreateHeroButtonClick);
            _nameInput.onValueChanged.AddListener(HandleHeroNameChanged);
            InitDropdowns(_controller.TraitsData);
            InitTraitsUI(_controller);
        }

        private void HandleHeroNameChanged(string newName)
        {
            _controller.SetHeroName(newName);
        }

        private void InitDropdowns(TraitsData traitsData)
        {
            _mainVirtueDropdown.onValueChanged.AddListener(HandleMainVirtueChanged);
            _mainViceDropdown.onValueChanged.AddListener(HandleMainViceChanged);
            _mainVirtueDropdown.AddOptions(traitsData.Virtues);
            _mainViceDropdown.AddOptions(traitsData.Vices);
        }

        private void InitTraitsUI(HeroCreateController controller)
        {
            ClearTraits();
            foreach (var trait in controller.HeroTraits)
            {
                var traitUI = Instantiate(_traitUiPrefab, _traitsPanelUI);
                traitUI.Init(trait);
                _traitUiItems.Add(traitUI);
            }
        }

        private void HandleMainVirtueChanged(int index)
        {
            _mainViceDropdown.EnableAll();

            if (index > 0)
            {
                var virtue = _mainVirtueDropdown.GetText(index);
                _controller.SetMainVirtue(virtue);
                _mainViceDropdown.SetOptionEnabled(index, false);
            }
            else
            {
                _controller.ResetMainVirtue();
            }
        }

        private void HandleMainViceChanged(int index)
        {
            _mainVirtueDropdown.EnableAll();

            if (index > 0)
            {
                var vice = _mainViceDropdown.GetText(index);
                _controller.SetMainVice(vice);
                _mainVirtueDropdown.SetOptionEnabled(index, false);
            }
            else
            {
                _controller.ResetMainVice();
            }
        }

        private void ClearTraits()
        {
            var traitsUI = _traitsPanelUI.GetComponentsInChildren<TraitUI>();

            for (int i = 0; i < traitsUI.Length; i++)
            {
                Destroy(traitsUI[i].gameObject);
            }

            _traitUiItems.Clear();
        }

        private void HandleCreateHeroButtonClick()
        {
            bool isValid = IsValidName() & IsMainVirtueSelected() & IsMainViceSelected();
            if (isValid)
            {
                _controller.CreateHero();
            }
        }

        private bool IsValidName()
        {
            bool valid = !string.IsNullOrWhiteSpace(_nameInput.text) && _nameInput.text.Length <= _nameInput.characterLimit;
            if (!valid)
            {
                StartCoroutine(ShowValidationError(_nameInput));
            }

            return valid;
        }

        private bool IsMainVirtueSelected()
        {
            bool valid = _mainVirtueDropdown.value != 0;
            if (!valid)
            {
                StartCoroutine(ShowValidationError(_mainVirtueDropdown));
            }

            return valid;
        }

        private bool IsMainViceSelected()
        {
            bool valid = _mainViceDropdown.value != 0;
            if (!valid)
            {
                StartCoroutine(ShowValidationError(_mainViceDropdown));
            }

            return _mainViceDropdown.value != 0;
        }

        private System.Collections.IEnumerator ShowValidationError(Selectable uiSelectable)
        {
            var originalColor = uiSelectable.targetGraphic.color;
            uiSelectable.targetGraphic.color = _errorColor;
            float blendTime = 0f;

            while (blendTime <= _errorTime)
            {
                blendTime += Time.deltaTime;
                uiSelectable.targetGraphic.color = Color.Lerp(_errorColor, originalColor, blendTime / _errorTime);
                yield return null;
            }

            uiSelectable.targetGraphic.color = originalColor;
        }
    }
}