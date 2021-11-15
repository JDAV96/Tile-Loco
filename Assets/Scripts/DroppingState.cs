using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingState : BaseState
{
    Vector2 currentVelocity = new Vector2();
    PlayerController controller;

    public DroppingState(PlayerController controller) : base("isJumping")
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        controller.playerAnimator.SetBool(name, true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        float dropDirection = controller.inputVector.x;

        if (dropDirection == 0)
        {
            dropDirection = Mathf.Sign(currentVelocity.x);
        }

        if (Mathf.Abs(currentVelocity.x) > Mathf.Epsilon)
        {
            controller.FlipSprite((int)currentVelocity.x);
        }
        
        currentVelocity = new Vector2(controller.inputVector.x * controller.playerSpeed, controller.playerRigidbody.velocity.y);

        if (controller.CheckGroundContact())
        {
            controller.ChangeState(controller._running);
        }
        else if (controller.CheckBounceContact())
        {
            controller.ChangeState(controller._jumping);
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
