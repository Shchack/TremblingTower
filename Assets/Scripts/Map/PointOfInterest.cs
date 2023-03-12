using EG.Tower.Missions;
using EG.Tower.Utils;
using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EG.Tower.Game
{
    public class PointOfInterest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [ConversationPopup(false, true)]
        [SerializeField] private string _conversation;

        [SerializeField] private MissionData _missionData;

        [SerializeField] private Image _pointer;
        [SerializeField] private Transform _hoverUI;
        [SerializeField] private TMP_Text _nameLabel;
        [SerializeField] private TMP_Text _descriptionLabel;

        [SerializeField] private string _name;
        [SerializeField] private string _description;

        [SerializeField] private float _floatDistance = 15f;
        [SerializeField] private float _speedTime = 2f;

        private void Start()
        {
            _hoverUI.gameObject.SetActive(false);
            _nameLabel.text = _name;
            _descriptionLabel.text = _description;

            _pointer.rectTransform.LeanMoveLocalY(_floatDistance, _speedTime).setEaseInOutSine().setLoopPingPong();
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

            _pointer.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            ShowInfo(true);
        }

        public void OnMouseExit()
        {
            _pointer.rectTransform.localScale = new Vector3(1f, 1f, 1f);
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

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Pointer over object!");

            _pointer.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            ShowInfo(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _pointer.rectTransform.localScale = new Vector3(1f, 1f, 1f);
            ShowInfo(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Interact();
        }
    }
}
