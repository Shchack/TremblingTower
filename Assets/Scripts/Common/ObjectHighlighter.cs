using System;
using UnityEngine;

namespace EG.Tower.Game.Common
{
    public class ObjectHighlighter : MonoBehaviour
    {
        public event Action OnObjectClickEvent;

        private Renderer[] _renderers;

        private bool _isDisabled = false;

        private void Awake()
        {
            _renderers = GetComponentsInChildren<Renderer>();
        }

        private void OnMouseOver()
        {
            Debug.Log("Pointer over object!");

            if (_isDisabled)
            {
                return;
            }

            foreach (var renderer in _renderers)
            {
                renderer.material.EnableKeyword("_EMISSION");
            }

        }

        private void OnMouseExit()
        {
            if (_isDisabled)
            {
                return;
            }

            foreach (var renderer in _renderers)
            {
                renderer.material.DisableKeyword("_EMISSION");
            }
        }

        public void Disable()
        {
            _isDisabled = true;
            foreach (var renderer in _renderers)
            {
                renderer.material.DisableKeyword("_EMISSION");
            }
        }

        private void OnMouseUp()
        {
            OnObjectClickEvent?.Invoke();
        }
    }
}
