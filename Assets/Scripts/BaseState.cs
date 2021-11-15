using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    public string name;

    public BaseState(string name) 
    {
        this.name = name;
    }

    public virtual void Enter() { }

    public virtual void UpdateLogic() { }

    public virtual void UpdatePhysics() { }

    public virtual void Exit() { }
}
