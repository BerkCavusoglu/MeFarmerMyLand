using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private JoystickController joystickController;
    private CharacterController chcon;
    [SerializeField] private PlayerAnimator playerAnimator;
    Vector3 moveVector;
    [Header("Settings")]
    [SerializeField] private int moveSpeed;
    private float gravity = -9.81f;
    private float gravityMultiplier = 3f;
    private float gravityVelocity;
    void Start()
    {
        chcon = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        moveVector = joystickController.GetMovePosition() * moveSpeed * Time.deltaTime / Screen.width;

        moveVector.z = moveVector.y;
        moveVector.y = 0;
        playerAnimator.ManageAnimations(moveVector);
        ApplyGravity();
        chcon.Move(moveVector);
    }
    private void ApplyGravity()
    {
        if (chcon.isGrounded && gravityVelocity < 0.0f)
        {
            gravityVelocity = -1f;
        }
        else
        {
            gravityVelocity *= gravity * gravityMultiplier * Time.deltaTime;
        }
        moveVector.y = gravityVelocity; 
    }
}
