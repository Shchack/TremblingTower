using EG.Tower.Game.Battle.Models;
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

        [SerializeField] private string _hpAttributeName = "Hit points";
        [SerializeField] private string _combatOrderAttributeName = "Combat Order";

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
            BattleAttributesModel attributes = _battleController.Attributes;
            _heroNameLabel.text = attributes.HeroName;

            GenerateAdditionalItem(_hpAttributeName, attributes.HP);
            GenerateAdditionalItem(_combatOrderAttributeName, attributes.CombatOrder);
            GenerateAdditionalItem(attributes.Inspiration.CombatActionName, attributes.Inspiration.Value);

            foreach (var item in attributes.Items)
            {
                var uiItem = Instantiate(_attributeUIPrefab, _attributesHolder);
                uiItem.InitMainAttribute(item);
            }
        }

        private void GenerateAdditionalItem(string name, float value)
        {
            var uiItem = Instantiate(_attributeUIPrefab, _attributesHolder);
            uiItem.InitAdditionalAttribute(name, value);
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