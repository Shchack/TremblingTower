using EG.Tower.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game
{
    public class GameplayDemoScreen : MonoBehaviour
    {
        [SerializeField] private Button _battleButton;

        private void Awake()
        {
            _battleButton.onClick.AddListener(BeginDemoBattle);
        }

        private void BeginDemoBattle()
        {
            SceneHelper.LoadMissionScene();
        }
    }
}