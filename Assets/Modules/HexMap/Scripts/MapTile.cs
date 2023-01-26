using UnityEngine;
using UnityEngine.EventSystems;

public class MapTile : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;


    void OnMouseOver()
    {
        Debug.Log("Pointer over object!");

        _renderer.material.EnableKeyword("_EMISSION");
    }

    void OnMouseExit()
    {
        _renderer.material.DisableKeyword("_EMISSION");
    }
}
