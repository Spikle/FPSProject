using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public abstract class State
{
    protected StateType stateType;
    protected EnemyController controller;

    public StateType Type => stateType;

    public State(StateType stateType, EnemyController controller)
    {
        this.stateType = stateType;
        this.controller = controller;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
