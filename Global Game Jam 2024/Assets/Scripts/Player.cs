using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody;
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashCooldown = 1f;

    private bool isDashing;
    private bool canDash = true;
    private float dashTimer;
    private LayerMask groundLayer;

    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        HandleMovement();
        HandleDash();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Dashing
        if (Input.GetButtonDown("Horizontal") && canDash)
        {
            canDash = true;
            dashTimer = 0f;
        }

        if (canDash && dashTimer < dashDuration)
        {
            _rigidbody.velocity = new Vector2(horizontalInput * dashSpeed, _rigidbody.velocity.y);
            dashTimer += Time.deltaTime;
        }
        else
        {
            canDash = false;
            _rigidbody.velocity = new Vector2(horizontalInput * moveSpeed, _rigidbody.velocity.y);
        }

        // Flip the character based on the direction
        if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (horizontalInput > 0)
            transform.localScale = new Vector3(1, 1, 1);

        // Cooldown for dash
        if (!canDash && dashTimer >= dashDuration)
        {
            if (dashTimer >= dashCooldown)
            {
                canDash = true;
            }
        }
    }

    void HandleDash()
    {
        if (Input.GetButtonDown("Horizontal") && canDash)
        {
            isDashing = true;
            dashTimer = 0f;
        }

        if (dashTimer < dashDuration)
        {
            canDash = false;
            dashTimer += Time.deltaTime;
        }
        else if (!canDash)
        {
            canDash = true;
        }
    }

}
