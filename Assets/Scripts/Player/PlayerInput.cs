using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerController playerController;

        public void OnMove(CallbackContext ctx)
        {
            if (ctx.performed)
                playerMovement.OnMove(ctx.ReadValue<Vector2>());
            else
                playerMovement.OnMove(Vector2.zero);
        }
        public void OnJump(CallbackContext ctx)
        {
            if (ctx.performed)
                playerMovement.OnJump(ctx.ReadValue<float>());
            else
                playerMovement.OnJump(0);
        }

        public void OnModeChange(CallbackContext ctx)
        {
            if (ctx.performed)
                playerController.OnModeChangeStart();
            if (ctx.canceled)
                playerController.OnModeChangeFinish();
        }

    }
}
