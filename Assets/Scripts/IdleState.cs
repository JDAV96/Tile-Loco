using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    PlayerController controller;

    public IdleState(PlayerController controller) : base("Idle")
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        controller.playerRigidbody.velocity = new Vector2(0f,0f);
    }
}
