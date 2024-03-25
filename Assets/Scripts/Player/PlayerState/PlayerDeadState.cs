using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    Animator animator;
    AnimatorStateInfo animaInfo;
    public PlayerDeadState(PlayerControler _player, PlayerStateMachine _stateMachine, string _animStateName) : base(_player, _stateMachine, _animStateName)
    {
        animator = player.animator;
    }

    public override void Enter()
    {
        player.animator.Play(animStateName);
    }
    public override void Updata()
    {
        animaInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animaInfo.normalizedTime >= 1)
        {
            player.overBar.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
