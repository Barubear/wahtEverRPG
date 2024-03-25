using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerControler _player, PlayerStateMachine _stateMachine, string _animStateName) : base(_player, _stateMachine, _animStateName)
    {
        
    }

    public override void Enter() {
        //Debug.Log("IdleState Enter");
        player.animator.Play(animStateName);
    }
    public override void Updata() {
        if (!player.InputLock) {
            if (player.xInput != 0) stateMachine.changeState(player.moveState);
            if (Input.GetKeyDown(KeyCode.LeftShift)) stateMachine.changeState(player.dashState);
            if (Input.GetKeyDown(KeyCode.J)) stateMachine.changeState(player.attackState01);
            if (Input.GetKeyDown(KeyCode.Space)) stateMachine.changeState(player.jumpState);
        }
        
        if (player.rb.velocity.y < 0) stateMachine.changeState(player.fallState);
        

    }
    public override void Exit() { }
}
