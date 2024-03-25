using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currState;
    public void init(EnemyState startMachine)
    {

        currState = startMachine;
        currState.Enter();
    }

    public bool changeState(EnemyState newMachine)
    {
        if (currState != newMachine)
        {
            bool check = newMachine.EnterCheck();
            
            if (check)
            {

                currState.Exit();
                currState = newMachine;
                currState.Enter();
            }
            return check;

        }
        else return false;
    }
}
