using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        public Vector2 MoveComposite { get; set; }
    
        private Controls _controls;
    
        private void OnEnable()
        {
            if (_controls != null)
                return;

            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
            _controls.Player.Enable();
        }
    
        public void OnDisable()
        {
            _controls.Player.Disable();
        }

    
        public void OnMovement(InputAction.CallbackContext context)
        {
            MoveComposite = context.ReadValue<Vector2>();
        }
    }
}
