using Cinemachine;
using EG.Tower.Common;
using UnityEngine;

namespace EG.Tower.Inputs
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private DefaultInputControls _input;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private BoxCollider _boundaries;

        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _zoomSpeed = 3f;
        [SerializeField] private FloatRange _zoomRange = new FloatRange(40f, 90f);

        private Transform _cameraTransform;

        private void Awake()
        {
            _input = new DefaultInputControls();
            _cameraTransform = _virtualCamera.VirtualCameraGameObject.transform;
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Update()
        {
            var moveInput = _input.Camera.Move.ReadValue<Vector2>();
            if (moveInput != Vector2.zero)
            {
                Move(moveInput);
            }

            float zoomInput = _input.Camera.Zoom.ReadValue<float>();
            if (zoomInput != 0)
            {
                Zoom(zoomInput);
            }
        }

        private void Move(Vector2 moveInput)
        {
            var direction = new Vector3(moveInput.x, 0f, moveInput.y);
            Vector3 newPosition = _cameraTransform.position + direction * _moveSpeed;
            var x = Mathf.Clamp(newPosition.x, _boundaries.bounds.min.x, _boundaries.bounds.max.x);
            var z = Mathf.Clamp(newPosition.z, _boundaries.bounds.min.z, _boundaries.bounds.max.z);
            var targetPosition = new Vector3(x, newPosition.y, z);
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, targetPosition, Time.deltaTime);
        }

        private void Zoom(float increment)
        {
            float fov = _virtualCamera.m_Lens.FieldOfView;
            float targetZoom = Mathf.Clamp(fov + increment, _zoomRange.Min, _zoomRange.Max);
            _virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(fov, targetZoom, _zoomSpeed * Time.deltaTime);
        }
    }
}
