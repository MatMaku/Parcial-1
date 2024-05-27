using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player_WallJump : Controller_Player
{
    private bool isWallSliding;
    public float WallSlidingSpeed = 1f;

    private bool isWallJumping;
    private float WallJumpingDirection;
    public float WallJumpingTime = 0.2f;
    private float WallJumpingCounter;
    public float WallJumpingDuration = 0.4f;
    private Vector2 WallJumpingPower = new Vector2(8f,12f);


    public override void FixedUpdate()
    {
        WallSliding();
        WallJump();

        animator.SetBool("EnPared", isWallSliding);

        if (!isWallJumping ) 
        {
            base.FixedUpdate();
        }
    }


    private void WallSliding()
    {
        if (SomethingRight() && !onFloor)
        {
            isWallSliding = true;
            rb.velocity = new Vector2 (rb.velocity.x, Mathf.Clamp(rb.velocity.y, -WallSlidingSpeed, float.MaxValue));
        }
        else if(SomethingLeft() && !onFloor)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -WallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }

    }

    public override void Jump()
    {
        if (!isWallSliding && !isWallJumping)
        {
            base.Jump();
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;

            if (SomethingRight())
            {
                WallJumpingDirection = -1;
                spriteRenderer.flipX = false;
            }
            else if (SomethingLeft())
            {
                WallJumpingDirection = 1;
                spriteRenderer.flipX = true;
            }

            WallJumpingCounter = WallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            WallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.W) && WallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(WallJumpingDirection * WallJumpingPower.x, WallJumpingPower.y);
            WallJumpingCounter = 0f;
        }

        Invoke(nameof(StopWallJumping), WallJumpingDuration);
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }
}
