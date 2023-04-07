using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireIdleState : State
{
    public Phase1State phase1State;

    public bool IsPlayerInRange;

    public GameObject wall1, wall2;

    public override State RunCurrentState()
    {
        if (IsPlayerInRange)
        {
            wall1.SetActive(true);
            wall2.SetActive(true);
            return phase1State;
        }
        return this;
    }
}
