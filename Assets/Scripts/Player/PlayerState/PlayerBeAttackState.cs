using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeAttackState : PlayerState
{
    float duration;
    float currTime;
    public PlayerBeAttackState(PlayerControler _player, PlayerStateMachine _stateMachine, string _animStateName, float _duration,AudioClip _audioClip = null) : base(_player, _stateMachine, _animStateName, _audioClip)
    {
        this.duration = _duration;
    }

    public override void Enter()
    {

        currTime = duration;
        player.animator.Play(animStateName);
        player.audioSource.clip = audioClip;
        player.audioSource.Play();
        
    }

    public override void Updata()
    {
        currTime -= Time.deltaTime;
        player.rb.velocity = new Vector2(0 , 0);
        if (currTime < 0)
            stateMachine.changeState(player.idleState);
    }

    public override bool EnterCheck()
    {
        if (stateMachine.currState == player.dashState)
            return false;
        else return true;
    }
    public override void Exit()
    {
        player.rb.velocity = new Vector2(0, 0);
    }
    public void reAttack()
    {
        currTime = duration;
    }
}
