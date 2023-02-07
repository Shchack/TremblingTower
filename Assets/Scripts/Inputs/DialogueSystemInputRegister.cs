using PixelCrushers;
using UnityEngine;

namespace EG.Tower.Inputs
{
    public class DialogueSystemInputRegister : MonoBehaviour
    {
        [SerializeField] private GameInputControls controls;

        protected static bool isRegistered = false;
        private bool didIRegister = false;

        void Awake()
        {
            controls = new GameInputControls();
        }
        void OnEnable()
        {
            if (!isRegistered)
            {
                isRegistered = true;
                didIRegister = true;
                controls.Enable();
                InputDeviceManager.RegisterInputAction("Interact", controls.Gameplay.Interact);
            }
        }
        void OnDisable()
        {
            if (didIRegister)
            {
                isRegistered = false;
                didIRegister = false;
                controls.Disable();
                InputDeviceManager.UnregisterInputAction("Interact");
            }
        }

    }
}
