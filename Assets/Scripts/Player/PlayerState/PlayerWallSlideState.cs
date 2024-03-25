using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    float slideSpeed;
    public PlayerWallSlideState(PlayerControler _player, PlayerStateMachine _stateMachine, string _animStateName) : base(_player, _stateMachine, _animStateName)
    {
    }

    // Start is called before the first frame update
    public override void Enter()
    {
        player.animator.Play(animStateName);
        
    }
    public override void Updata()
    {
        if (player.isGrounded ) stateMachine.changeState(player.idleState);
        
        if (Input.GetKeyDown(KeyCode.Space)) {

            player.flip();
            player.rb.AddForce(new Vector2(150 * -player.facingDir, player.jumpForce));
            stateMachine.changeState(player.jumpState);
        }
        

        player.rb.velocity = new Vector2(0, player.rb.velocity.y *(player.yInput>=0 ? 0.98f: 1) );
    }
    public override void Exit() { }
}
