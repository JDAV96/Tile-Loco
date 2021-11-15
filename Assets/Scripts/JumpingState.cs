using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : BaseState
{
    Vector2 currentVelocity = new Vector2();
    PlayerController controller;
    private bool _onAir = false;
    private float bounceSpeed = 20f;

    public JumpingState(PlayerController controller) : base("isJumping")
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        _onAir = false;
        currentVelocity = new Vector2(controller.playerRigidbody.velocity.x, controller.jumpSpeed);
        controller.playerAnimator.SetBool(name, true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        float jumpDirection = controller.inputVector.x;

        if (jumpDirection == 0)
        {
            jumpDirection = Mathf.Sign(currentVelocity.x);
        }

        if (Mathf.Abs(currentVelocity.x) > Mathf.Epsilon)
        {
            controller.FlipSprite((int)currentVelocity.x);
        }

        if (_onAir)
        {
            currentVelocity = new Vector2(controller.inputVector.x * controller.playerSpeed, controller.playerRigidbody.velocity.y);
        }

        if (controller.CheckGroundContact() || controller.CheckBounceContact())
        {
            if (_onAir)
            {
                if (controller.CheckGroundContact())
                {
                    controller.ChangeState(controller._running);
                }
                else if (controller.playerRigidbody.velocity.y <= Mathf.Epsilon)
                {
                    currentVelocity = new Vector2(currentVelocity.x, bounceSpeed);
                }
            }
        }
        else if (!_onAir)
        {   
            _onAir = true;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        controller.playerRigidbody.velocity = currentVelocity;
    }

    public override void Exit()
    {
        base.Exit();
        controller.playerAnimator.SetBool(name, false);
    }
}
