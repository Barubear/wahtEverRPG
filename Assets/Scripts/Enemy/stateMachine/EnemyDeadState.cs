using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyState
{
    protected float duration;
    protected float currTime;
    public EnemyDeadState(EnemyControler _EC, EnemyStateMachine _stateMachine, List<string> _animStateName, float _duration) :base(_EC, _stateMachine, _animStateName)
    {
        this.duration = _duration;
        currTime = duration;
    }
    public override void Enter()
    {
        EC.animator.Play(animStateName[0]);
    }

    public override void Updata()
    {
        currTime -= Time.deltaTime;
        EC.rb.velocity = new Vector2(0, 0);
        if (currTime < 0)
            EC.dead();
    }
}
