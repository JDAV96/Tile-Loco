using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected BaseState currentState;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
        {
            currentState.Enter();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateLogic();
        }
    }

    protected virtual void LateUpdate() 
    {
        if (currentState != null)
        {
            currentState.UpdatePhysics();
        }
    }

    public virtual void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }
}
