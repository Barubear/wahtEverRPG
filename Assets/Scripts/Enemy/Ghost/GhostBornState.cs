using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBornState : EnemyState
{
    Animator animator;
    AnimatorStateInfo animaInfo;
    public GhostBornState(EnemyControler _EC, EnemyStateMachine _stateMachine, List<string> _animStateName):base(_EC,_stateMachine,_animStateName)
    {
        animator = EC.animator;
        
    }

    public override void Enter()
    {
        animator.Play(animStateName[0]);
        
    }
    public override void Updata()
    {
        animaInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animaInfo.normalizedTime >= 1)
        {
            stateMachine.changeState(EC.stateList[0]);
        }
    }


}
