using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private FlippersController flippersControllerRight;
        [SerializeField] private FlippersController flippersControllerLeft;


        public void OnMove(CallbackContext ctx)
        {
            if (ctx.started)
                playerMovement.OnMove(ctx.ReadValue<Vector2>());
            else if (ctx.canceled)
                playerMovement.OnMove(Vector2.zero);
        }
        public void OnJump(CallbackContext ctx)
        {
            if (ctx.started)
                playerMovement.OnJump(ctx.ReadValue<float>());
            else if (ctx.canceled)
                playerMovement.OnJump(0);
        }

        public void OnModeChange(CallbackContext ctx)
        {
            if (ctx.performed)
                if (playerController.OnModeChangeStart())
                {
                    flippersControllerLeft.enabled = true;
                    flippersControllerRight.enabled = true;
                }
                else
                {
                    flippersControllerLeft.enabled = false;
                    flippersControllerRight.enabled = false;
                }
            if (ctx.canceled)
                playerController.OnModeChangeFinish();
        }

        public void OnFlipperUsedLeft(CallbackContext ctx)
        {
            if (ctx.performed)
                flippersControllerLeft.changeState(true);
            if (ctx.canceled)
                flippersControllerLeft.changeState(false);
        }
        public void OnFlipperUsedRight(CallbackContext ctx)
        {
            if (ctx.performed)
                flippersControllerRight.changeState(true);
            if (ctx.canceled)
                flippersControllerRight.changeState(false);
        }

    }
}
