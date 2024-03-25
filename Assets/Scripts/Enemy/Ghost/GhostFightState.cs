using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class GhostFightState : EnemyState
{
    public  SequencesNode rootSequencesNode;
    private Transform target;
    private float attckDis;
    private float DashAttckDis;
    private List<AudioClip> clips;

    private float hoverTime;

    public GhostFightState(EnemyControler _EC, EnemyStateMachine _stateMachine, List<string> _animStateName, Transform _target, float _hoverTime, float _attackDis, float dashDis, float _damage, Collider2D hitBox, List<AudioClip> _clips = null) : base(_EC, _stateMachine, _animStateName)
    {
        this.stateMachine = _stateMachine;
        this.target = _target;
        this.attckDis = _attackDis;
        this.clips = _clips;
        rootSequencesNode = new SequencesNode();
        rootSequencesNode.chirldens.Add(new HoverNode(EC, target,_animStateName[0], _hoverTime));
            RandamSwtichNode randamSwtichNode = new RandamSwtichNode();
                SequencesNode subSequencesNodeForAttack = new SequencesNode();
                    subSequencesNodeForAttack.chirldens.Add(new MoveToNode(EC, target, attckDis, _animStateName[0]));
                    subSequencesNodeForAttack.chirldens.Add(new AttackNode(EC, _animStateName[1], _damage, hitBox, false, clips[0]));
                SequencesNode subSequencesNodeForDash = new SequencesNode();
                    //subSequencesNodeForDash.chirldens.Add(new MoveToNode(EC, _target, dashDis, _animStateName[0]));
                    subSequencesNodeForDash.chirldens.Add(new AttackNode(EC, _animStateName[1], _damage, hitBox,true, clips[0]));
            randamSwtichNode.chirldens.Add(subSequencesNodeForAttack);
            randamSwtichNode.chirldens.Add(subSequencesNodeForDash);
        rootSequencesNode.chirldens.Add(randamSwtichNode);




        parrolTree = new BHTree(rootSequencesNode);
    }


    public override void Updata()
    {
        parrolTree.root.tick();
    }



}
