using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireIdleState : State
{
    public Phase1State phase1State;

    public bool IsPlayerInRange;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public override State RunCurrentState()
    {

        if (IsPlayerInRange)
        {
            return phase1State;
        }
        return this;
    }
}
