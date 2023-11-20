using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PirateShooter
{
    public class PlayerMovement : Movement
    {
        public void OnMovement(InputAction.CallbackContext context)
        {
            _movementDirection = context.ReadValue<Vector2>();
        }
    }
}