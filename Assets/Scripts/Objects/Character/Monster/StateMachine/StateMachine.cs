using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public interface IState
{
    public void Enter();
    public void Exit();

    public void Update();
    
}
public abstract class StateMachine
{
    public IState curState;
    
    public void ChangeState(IState state)
    {
        curState?.Exit();
        curState = state;
        curState?.Enter();
    }

    public void Update()
    {
        curState?.Update();
    }

}
