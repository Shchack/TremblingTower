using System;
using UnityEngine;

namespace EG.Tower.Game.Common
{
    public class ObjectHighlighter : MonoBehaviour
    {
        public event Action OnObjectClickEvent;

        private Renderer[] _renderers;

        private void Awake()
        {
            _renderers = GetComponentsInChildren<Renderer>();
        }

        private void OnMouseOver()
        {
            Debug.Log("Pointer over object!");

            foreach (var renderer in _renderers)
            {
                renderer.material.EnableKeyword("_EMISSION");
            }

        }

        private void OnMouseExit()
        {
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
