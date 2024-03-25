using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerSkillState
{
    AnimatorStateInfo stateInfo;
    float dashSpeed;
    float currentTime;
    
    
    public PlayerDashState(PlayerControler _player, SkillControlor _skillControlor,PlayerStateMachine _stateMachine, string _animStateName, float _dashSpeed, float _duration , float _coolDown) 
        : base(_player, _skillControlor,_stateMachine, _animStateName, _duration, _coolDown)
    {
        this.dashSpeed = _dashSpeed;
        this.skillControlor = _skillControlor;
    }

    public override void Enter()
    {   
        base.Enter();
        //Debug.Log("DashState Enter");
        player.animator.Play(animStateName);
    }

    public override void Updata()
    {
        stateInfo = player.animator.GetCurrentAnimatorStateInfo(0);
        currentTime = stateInfo.normalizedTime * stateInfo.length;
        player.rb.velocity = new Vector2(dashSpeed * player.facingDir, player.rb.velocity.y);
        if (currentTime >= duration) stateMachine.changeState(player.idleState);
        skillControlor.update();
        

    }
    public override void Exit() {
        player.rb.velocity = new Vector2(0, player.rb.velocity.y);
    }
    public override bool EnterCheck()
    {
        return stateMachine.currState != player.deadState;
    }
}
