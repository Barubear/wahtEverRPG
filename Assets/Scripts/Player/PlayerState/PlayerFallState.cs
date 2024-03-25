using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(PlayerControler _player, PlayerStateMachine _stateMachine, string _animStateName) : base(_player, _stateMachine, _animStateName)
    {

    }
    // Start is called before the first frame update
    public override void Enter()
    {
        //Debug.Log("fall state");
        player.animator.Play(animStateName);
    }
    public override void Updata()
    {
        if (player.isGrounded ) {
            
            stateMachine.changeState(player.idleState);
        }
        

    }
    public override void Exit() {
        player.rb.velocity = new Vector2(0, 0);
    }
    public override bool EnterCheck()
    {
        return stateMachine.currState != player.deadState;
    }
}
