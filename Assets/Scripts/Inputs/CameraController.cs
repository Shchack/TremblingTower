using Cinemachine;
using EG.Tower.Utils;
using UnityEngine;

namespace EG.Tower.Inputs
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameInputControls _input;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private BoxCollider _boundaries;

        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _rotateSpeed = 1f;
        [SerializeField] private float _zoomSpeed = 3f;
        [SerializeField] private FloatRange _zoomRange = new FloatRange(40f, 90f);

        private Transform _cameraTransform;

        private void Awake()
        {
            _input = new GameInputControls();
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

            var rotateInput = _input.Camera.Rotate.ReadValue<Vector2>();
            if (rotateInput != Vector2.zero)
            {
                Rotate(rotateInput);
            }

            float zoomInput = _input.Camera.Zoom.ReadValue<float>();
            if (zoomInput != 0)
            {
                Zoom(zoomInput);
            }
        }

        private void Move(Vector2 moveInput)
        {
            var direction = moveInput.x * GetCameraRight() + moveInput.y * GetCameraForward();
            direction = direction.normalized;

            Vector3 newPosition = _cameraTransform.position + direction * _moveSpeed * Time.deltaTime;
            var x = Mathf.Clamp(newPosition.x, _boundaries.bounds.min.x, _boundaries.bounds.max.x);
            var z = Mathf.Clamp(newPosition.z, _boundaries.bounds.min.z, _boundaries.bounds.max.z);
            newPosition = new Vector3(x, newPosition.y, z);

            _cameraTransform.position = newPosition;
        }

        private void Zoom(float increment)
        {
            float fov = _virtualCamera.m_Lens.FieldOfView;
            float targetZoom = Mathf.Clamp(fov + increment, _zoomRange.Min, _zoomRange.Max);
            _virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(fov, targetZoom, _zoomSpeed * Time.deltaTime);
        }

        private void Rotate(Vector2 rotateInput)
        {
            float rotation = 0f;

            if (rotateInput.x != 0f)
            {
                rotation = 1f;
            }
            else if (rotateInput.y != 0f)
            {
                rotation = -1f;
            }

            _cameraTransform.eulerAngles += new Vector3(0f, rotation * _rotateSpeed * Time.deltaTime, 0f);
        }

        private Vector3 GetCameraForward()
        {
            Vector3 forward = _cameraTransform.forward;
            forward.y = 0f;
            return forward;
        }

        private Vector3 GetCameraRight()
        {
            Vector3 right = _cameraTransform.right;
            right.y = 0f;
            return right;
        }
    }
}
