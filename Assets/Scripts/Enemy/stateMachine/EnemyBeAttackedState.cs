using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EnemyBeAttackedState : EnemyState
{
    float duration;
    float currTime;
    public EnemyBeAttackedState(EnemyControler _EC, EnemyStateMachine _stateMachine, List<string> _animStateName, float _duration, AudioClip _audioClip =null) : base(_EC, _stateMachine, _animStateName, _audioClip) 
    {
        this.duration = _duration;
    }
    public override void Enter()
    {

        currTime = duration;
        EC.animator.Play(animStateName[0]);
        if (audioClip != null) {
            EC.audioSource.clip = audioClip;
            EC.audioSource.Play();
        }

        

    }
    public override void Updata()
    {
        currTime -= Time.deltaTime;
        EC.rb.velocity = new Vector2(0, 0);
        if (currTime < 0) {
            // index 1 for deadState
            if (EC.agent.currHP <= 0) stateMachine.changeState(EC.stateList[1]);
            // index 0 for fightState
            else stateMachine.changeState(EC.stateList[0]);
        }
            
        
    }
    public void reAttack()
    {
        currTime = duration;
    }

    public override void Exit()
    {
        if (audioClip != null) EC.audioSource.Stop();
    }
}
