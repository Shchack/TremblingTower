using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game.Battle.UI
{
    public class BattleAttributesScreen : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TMP_Text _heroNameLabel;
        [SerializeField] private Button _beginBattleButton;
        [SerializeField] private BattleAttributeUI _attributeUIPrefab;
        [SerializeField] private RectTransform _attributesHolder;

        [SerializeField] private string _ultUseAttributeName;
        [SerializeField] private string _combatOrderAttributeName;

        private BattleController _battleController;

        private void Start()
        {
            _battleController = FindObjectOfType<BattleController>();
            _beginBattleButton.onClick.AddListener(HandleBeginButtonClick);
            GenerateItems();
            _canvas.enabled = true;
        }

        private void HandleBeginButtonClick()
        {
            _battleController.BeginBattle();
            _canvas.enabled = false;
        }

        private void GenerateItems()
        {
            Cleanup();
            BattleAttributesModel attributes = _battleController.GetAttributesModel();
            _heroNameLabel.text = attributes.HeroName;

            foreach (var item in attributes.Items)
            {
                var uiItem = Instantiate(_attributeUIPrefab, _attributesHolder);
                uiItem.InitMainAttribute(item);
            }

            var ultUseIiItem = Instantiate(_attributeUIPrefab, _attributesHolder);
            ultUseIiItem.InitAdditionalAttribute(_ultUseAttributeName, attributes.UltUseTimes);

            var orderUiItem = Instantiate(_attributeUIPrefab, _attributesHolder);
            orderUiItem.InitAdditionalAttribute(_combatOrderAttributeName, attributes.CombatOrder);
        }

        private void Cleanup()
        {
            var uiItems = _attributesHolder.GetComponentsInChildren<BattleAttributeUI>();

            for (int i = 0; i < uiItems.Length; i++)
            {
                Destroy(uiItems[i].gameObject);
            }
        }
    }
}