using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class EnemyFightState : EnemyState
{
    
    private SequencesNode sequencesNode;
    private Transform target;
    private float attckDis;
    private float decDis;
    
    public EnemyFightState(EnemyControler _EC, EnemyStateMachine _stateMachine, List<string> _animStateName,Transform _target, float _idelTime, float _attackDis,float _decDis,float _damage,Collider2D hitBox,AudioClip _audioClip = null) : base(_EC, _stateMachine, _animStateName, _audioClip)
    {
        this.stateMachine = _stateMachine;
        this.target = _target;
        this.decDis = _decDis;
        sequencesNode = new SequencesNode();
        sequencesNode.chirldens.Add(new MoveToNode(EC, target, _attackDis, _animStateName[0]));
        
        sequencesNode.chirldens.Add(new AttackNode(EC,_animStateName[1],_damage, hitBox,false,audioClip));
        
        sequencesNode.chirldens.Add(new IdleNode(EC, _animStateName[2], _idelTime,false));
        
        parrolTree = new BHTree(sequencesNode);
    }

    public override void Enter()
    {
        //Debug.Log("enter fight");
    }
    public override void Updata()
    {
        parrolTree.tick();
        if (Vector2.Distance(EC.transform.position, target.position) > decDis+3)
        {
            //Debug.Log(Vector2.Distance(EC.transform.position, target.position));
            stateMachine.changeState(EC.stateList[0]);
        }
    }

    public override void Exit()
    {
        parrolTree.reset();
    }
}
