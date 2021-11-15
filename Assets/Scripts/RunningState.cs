using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState
{
    Vector2 currentVelocity = new Vector2();
    PlayerController controller;

    public RunningState(PlayerController controller) : base("isRunning")
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

        if (!controller.CheckGroundContact())
        {
            controller.ChangeState(controller._dropping);
        }

        currentVelocity = new Vector2(controller.inputVector.x * controller.playerSpeed, controller.playerRigidbody.velocity.y);

        if (Mathf.Abs(currentVelocity.x) > Mathf.Epsilon)
        {
            controller.FlipSprite((int)currentVelocity.x);
        }
        else 
        {
            controller.ChangeState(controller._idle);
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
