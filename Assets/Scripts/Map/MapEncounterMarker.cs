using EG.Tower.Missions;
using PixelCrushers.DialogueSystem;
using System;
using UnityEngine;

namespace EG.Tower.Map
{
    public class MapEncounterMarker : MonoBehaviour
    {
        public event Action<MapEncounterMarker> OnMarkerHoverEvent;
        public event Action<MapEncounterMarker> OnMarkerInteractEvent;

        [ConversationPopup(false, true)]
        public string _conversation;

        public MissionData _missionData;

        public string _name;
        public string _description;

        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Vector3 _rotationAxis = new Vector3(0f, 1f, 0f);
        [SerializeField] private float _rotationSpeed = 45f;

        private void Update()
        {
            transform.Rotate(_rotationAxis, _rotationSpeed * Time.deltaTime);
        }

        public void OnMouseEnter()
        {
            Debug.Log("Pointer over object!");

            _renderer.material.EnableKeyword("_EMISSION");
            OnMarkerHoverEvent?.Invoke(this);
        }

        public void OnMouseExit()
        {
            _renderer.material.DisableKeyword("_EMISSION");
            OnMarkerHoverEvent?.Invoke(null);
        }

        public void OnMouseUp()
        {
            OnMarkerInteractEvent?.Invoke(this);
        }

        //public void OnPointerEnter(PointerEventData eventData)
        //{
        //    Debug.Log("Pointer over object!");

        //    _pointer.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        //    ShowInfo(true);
        //}

        //public void OnPointerExit(PointerEventData eventData)
        //{
        //    _pointer.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        //    ShowInfo(false);
        //}

        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    Interact();
        //}
    }
}
