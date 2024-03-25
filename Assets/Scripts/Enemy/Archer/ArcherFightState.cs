using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class ArcherFightState : EnemyState
{
    private SequencesNode sequencesNode;
    private Transform target;
    private float attackDis;
    public ArcherFightState(EnemyControler _EC, EnemyStateMachine _stateMachine, List<string> _animStateName, Transform _target, float _idelTime, float _attackDis, AudioClip _audioClip = null) : base(_EC, _stateMachine, _animStateName, _audioClip)
    {
        this.stateMachine = _stateMachine;
        this.target = _target;
        sequencesNode = new SequencesNode();
        this.attackDis = _attackDis;
        sequencesNode.chirldens.Add(new MoveToNode(EC, target, attackDis, _animStateName[0]));

        sequencesNode.chirldens.Add(new ShootNode(EC, _animStateName[1],audioClip));

        sequencesNode.chirldens.Add(new IdleNode(EC, _animStateName[2], _idelTime, false));

        parrolTree = new BHTree(sequencesNode);
    }
    public override void Enter()
    {
        //Debug.Log("enter fight");
    }
    public override void Updata()
    {
        parrolTree.tick();
        if (Vector2.Distance(EC.transform.position, target.position) > attackDis+2)
        {
            stateMachine.changeState(EC.stateList[0]);
        }
    }

    public override void Exit()
    {
        parrolTree.reset();
    }

}
