using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBeAttackedState : EnemyBeAttackedState { 
    // Start is called before the first frame update
    public GhostBeAttackedState(EnemyControler _EC, EnemyStateMachine _stateMachine, List<string> _animStateName, float _duration) : base(_EC, _stateMachine, _animStateName, _duration)
    {
    
    }

    void Update()
    {
        
    }
}


// Update is called once per frame

