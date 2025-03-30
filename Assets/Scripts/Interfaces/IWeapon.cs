using UnityEngine.InputSystem;

namespace Interfaces
{
    public interface IWeapon
    {
        public void OnAttack(InputAction.CallbackContext context);
    }
}