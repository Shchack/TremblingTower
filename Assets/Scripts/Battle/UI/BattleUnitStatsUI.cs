using System;
using TMPro;
using UnityEngine;

namespace EG.Tower.Game.Battle.UI
{
    public class BattleUnitStatsUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _hpLabel;

        public void Init(int currentHp, int maxHp)
        {
            _hpLabel.text = $"{currentHp}/{maxHp}";
        }

        internal void Init(int hP, object maxHP)
        {
            throw new NotImplementedException();
        }
    }
}
