using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingState : BaseState
{
    Vector2 currentVelocity = new Vector2();
    private PlayerController controller;
    private float lastKnownVelocityInsideLadder = 1f;

    public ClimbingState(PlayerController controller) : base("isClimbing") 
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        controller.playerRigidbody.gravityScale = 0;
        controller.playerAnimator.SetBool(name, true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (controller.CheckLadderContact())
        {
            currentVelocity = new Vector2(controller.playerRigidbody.velocity.x, controller.inputVector.y * controller.climbSpeed);
        }
        else 
        {
            if (Mathf.Abs(currentVelocity.y) > 1f)
            {
                lastKnownVelocityInsideLadder = currentVelocity.y;
            }

            currentVelocity = new Vector2(controller.playerRigidbody.velocity.x, -Mathf.Sign(lastKnownVelocityInsideLadder));
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
        controller.playerRigidbody.gravityScale = controller.gravityScale;
        controller.playerAnimator.SetBool(name, false);
    }
}
