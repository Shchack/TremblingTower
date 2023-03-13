using EG.Tower.Game;
using EG.Tower.Missions;
using EG.Tower.Utils;
using System;
using UnityEngine;

namespace EG.Tower.Map
{
    public class MapEncounterController : MonoBehaviour
    {
        public event Action<MapEncounterMarker> OnMapMarkerHoverEvent;

        [SerializeField] private MapCameraController _mapCamera;

        private MapEncounterMarker[] _markers;

        private void Awake()
        {
            _markers = GetComponentsInChildren<MapEncounterMarker>();

            foreach (var marker in _markers)
            {
                marker.OnMarkerHoverEvent += HandleMarkerHoverEvent;
                marker.OnMarkerInteractEvent += HandleMarkerInteractEvent;
            }
        }

        private void HandleMarkerHoverEvent(MapEncounterMarker marker)
        {
            OnMapMarkerHoverEvent?.Invoke(marker);
        }

        private void HandleMarkerInteractEvent(MapEncounterMarker marker)
        {
            if (!string.IsNullOrWhiteSpace(marker._conversation))
            {
                LoadConversation(marker._conversation);
            }
            else
            {
                LoadMission(marker._missionData);
            }
        }

        private void LoadMission(MissionData missionData)
        {
            GameHub.One.NextMissionData = missionData;
            SceneHelper.LoadMissionScene();
        }

        private void LoadConversation(string conversation)
        {
            GameHub.One.DialogueSystem.SetNextConversation(conversation);
            SceneHelper.LoadDialogueScene();
        }
    }
}
