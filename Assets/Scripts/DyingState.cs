using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingState : BaseState
{
    PlayerController controller;
    
    public DyingState(PlayerController controller) : base("Dying") 
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        controller.playerAnimator.SetTrigger(name);
        controller.playerRigidbody.velocity = new Vector2(0f, 7f);
    }
}
