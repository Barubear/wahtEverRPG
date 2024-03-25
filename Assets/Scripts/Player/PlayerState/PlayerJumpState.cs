using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private float jumpForce;
    public PlayerJumpState(PlayerControler _player, float _jumpForce,PlayerStateMachine _stateMachine, string _animStateName, AudioClip audioClip = null) : base(_player, _stateMachine, _animStateName, audioClip)
    {

        this.jumpForce = _jumpForce;
    }
    public override void Enter()
    {
        //Debug.Log("jumpState Enter");
        player.animator.Play(animStateName);
        player.audioSource.clip = audioClip;
        player.audioSource.Play();
        player.rb.velocity = new Vector2(player.rb.velocity.x, jumpForce);
    }
    public override void Updata()
    {
        if(player.rb.velocity.y < 0) stateMachine.changeState(player.idleState);
    }
    public override void Exit() { }
}
