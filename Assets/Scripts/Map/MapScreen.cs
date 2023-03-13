using TMPro;
using UnityEngine;

namespace EG.Tower.Map
{
    public class MapScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform _missionInfoPanel;
        [SerializeField] private TMP_Text _missionNameLabel;
        [SerializeField] private TMP_Text _missionDescriptionLabel;

        private MapEncounterController _mapController;

        private void Awake()
        {
            _missionInfoPanel.gameObject.SetActive(false);
            _mapController = FindObjectOfType<MapEncounterController>();
            _mapController.OnMapMarkerHoverEvent += HandleMapMarkerHoverEvent;
        }

        private void HandleMapMarkerHoverEvent(MapEncounterMarker marker)
        {
            if (marker != null)
            {
                ShowMissionInfo(marker);
            }
            else
            {
                HideMissionInfo();
            }
        }

        public void ShowMissionInfo(MapEncounterMarker marker)
        {
            _missionNameLabel.text = marker._name;
            _missionDescriptionLabel.text = marker._description;
            _missionInfoPanel.gameObject.SetActive(true);
        }

        public void HideMissionInfo()
        {
            _missionInfoPanel.gameObject.SetActive(false);
        }
    }
}
