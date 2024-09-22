using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateMachine
{
    public NPCState CurrentNPCState;

    public void Initialize(NPCState _startingState)
    {
        CurrentNPCState = _startingState;
        CurrentNPCState.EnterState();
    }

    public void ChangeState(NPCState _newState)
    {
        CurrentNPCState.ExitState();
        CurrentNPCState = _newState;
        CurrentNPCState.EnterState();
    }
}
