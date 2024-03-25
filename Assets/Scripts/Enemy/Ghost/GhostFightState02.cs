using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class GhostFightState02 : EnemyState
{
    private float cd;
    public  float currCD;
    private SelectNode selectNode;
   
    private Transform target;
    private float attckDis;
    private List<AudioClip> clips;


    public GhostFightState02(EnemyControler _EC, EnemyStateMachine _stateMachine, List<string> _animStateName, Transform _target, float _hoverTime, float _attackDis, float dashDis, float _damage, Collider2D hitBox , float skillCD, List<AudioClip> _clips = null) : base(_EC, _stateMachine, _animStateName)
    {
        this.cd = skillCD;
        this.stateMachine = _stateMachine;
        this.target = _target;
        this.attckDis = _attackDis;
        this.clips = _clips;
        selectNode = new SelectNode(ifSkill);
            SequencesNode rootSequencesNode = new SequencesNode();

                rootSequencesNode.chirldens.Add(new HoverNode(EC, target, _animStateName[0], _hoverTime));
                RandamSwtichNode randamSwtichNode = new RandamSwtichNode();
                    SequencesNode subSequencesNodeForAttack = new SequencesNode();
                        subSequencesNodeForAttack.chirldens.Add(new MoveToNode(EC, target, attckDis, _animStateName[0]));
                        subSequencesNodeForAttack.chirldens.Add(new AttackNode(EC, _animStateName[1], _damage, hitBox, false, clips[0]));
                    SequencesNode subSequencesNodeForDash = new SequencesNode();
                        subSequencesNodeForDash.chirldens.Add(new AttackNode(EC, _animStateName[1], _damage, hitBox, true, clips[0]));
                randamSwtichNode.chirldens.Add(subSequencesNodeForAttack);
                randamSwtichNode.chirldens.Add(subSequencesNodeForDash);
            
                rootSequencesNode.chirldens.Add(randamSwtichNode);
         selectNode.chirldens.Add(rootSequencesNode);
         selectNode.chirldens.Add(new SkillNode(EC, animStateName[2], clips[1]));
        parrolTree = new BHTree(selectNode);
    }
    public override void Enter()
    {
        Debug.Log("2ed State");
        currCD = -1;
    }
    public override void Updata()
    {
        parrolTree.tick();
        currCD -= Time.deltaTime;
    }

    public int ifSkill()
    {
        if (currCD < 0)
        {
            currCD = cd;
            return 1;
        }
        else
            return 0;
    }
    
}
