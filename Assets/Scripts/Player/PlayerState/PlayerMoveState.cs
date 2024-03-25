using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private float speed;
    public PlayerMoveState(PlayerControler _player, PlayerStateMachine _stateMachine, string _animStateName, float _speed) : base(_player, _stateMachine, _animStateName)
    {
        this.speed = _speed;
    }

    public override void Enter() {
        //Debug.Log("MoveState Enter");
        player.animator.Play(animStateName);
    }
    public override void Updata() {
        
        if (player.right && player.xInput < 0)
        {
            player.flip();
        }
        else if (!player.right && player.xInput > 0)
        {
            player.flip();
        }
        player.rb.velocity = new Vector2(player.xInput * speed, player.rb.velocity.y);
        if (player.xInput == 0) stateMachine.changeState(player.idleState);
        if (Input.GetKeyDown(KeyCode.J)) stateMachine.changeState(player.attackState03);
        if (Input.GetKeyDown(KeyCode.Space)) stateMachine.changeState(player.jumpState);
        if (player.rb.velocity.y < 0 && !player.isGrounded) stateMachine.changeState(player.fallState);

    }
    public override void Exit() { }
}
