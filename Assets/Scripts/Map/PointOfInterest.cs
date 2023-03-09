using EG.Tower.Missions;
using EG.Tower.Utils;
using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;

namespace EG.Tower.Game
{
    public class PointOfInterest : MonoBehaviour
    {
        [ConversationPopup(false, true)]
        [SerializeField] private string _conversation;

        [SerializeField] private MissionData _missionData;

        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Transform _hoverUI;
        [SerializeField] private TMP_Text _nameLabel;
        [SerializeField] private TMP_Text _descriptionLabel;

        [SerializeField] private string _name;
        [SerializeField] private string _description;

        private void Start()
        {
            _hoverUI.gameObject.SetActive(false);
            _nameLabel.text = _name;
            _descriptionLabel.text = _description;
        }

        private void Interact()
        {
            if (string.IsNullOrWhiteSpace(_conversation))
            {
                LoadMission();
            }
            else
            {
                LoadConversation();
            }
        }

        private void LoadMission()
        {
            GameHub.One.NextMissionData = _missionData;
            SceneHelper.LoadMissionScene();
        }

        private void LoadConversation()
        {
            GameHub.One.DialogueSystem.SetNextConversation(_conversation);
            SceneHelper.LoadDialogueScene();
        }

        public void OnMouseEnter()
        {
            Debug.Log("Pointer over object!");

            _renderer.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            ShowInfo(true);
        }

        public void OnMouseExit()
        {
            _renderer.transform.localScale = new Vector3(1f, 1f, 1f);
            ShowInfo(false);
        }

        public void OnMouseUp()
        {
            Interact();
        }

        private void ShowInfo(bool isShown)
        {
            _hoverUI.gameObject.SetActive(isShown);
        }
    }
}
