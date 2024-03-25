using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState currState { get; private set; }
   
    public void init(PlayerState startState)
    {
        currState = startState;
        currState.Enter();

    }

    public bool changeState(PlayerState newState)
    {
        if (currState != newState)
        {
            bool check = newState.EnterCheck();
            //Debug.Log("change:" + check);
            if (check)
            {
                //Debug.Log("changed" );
                currState.Exit();
                currState = newState;
                currState.Enter();
            }
            return check;

        }
        else return false;
    }
}
