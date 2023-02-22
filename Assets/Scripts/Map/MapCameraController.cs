using UnityEngine;

namespace EG.Tower.Game.Map
{
    public class MapCameraController : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private void Start()
        {
            TargetPoint();
        }

        private void TargetPoint()
        {
            transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
        }
    }
}
