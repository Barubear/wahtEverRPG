using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class EnemyPatrolState : EnemyState
{
    
    private List<Vector2> patrolPoints;
    private SequencesNode sequencesNode;
    public float idelTime;
    public float decDis;
    public List<Transform> decPoints;
    LayerMask playerLayer;
    public EnemyPatrolState(EnemyControler _EC, EnemyStateMachine _stateMachine, List<string> _animStateName, List<Vector2> _patrolPoints, float idelTime,List<Transform> DecPoints, float _decDis) : base(_EC, _stateMachine, _animStateName)
    {
        this.patrolPoints = _patrolPoints;
        this.idelTime = idelTime;
        this.decPoints = DecPoints;
        this.decDis = _decDis;
        playerLayer = LayerMask.GetMask("player");
        sequencesNode = new SequencesNode();
        foreach (Vector2 point in patrolPoints)
        {
            sequencesNode.chirldens.Add(new MoveNode(EC, point, animStateName[0]));
            sequencesNode.chirldens.Add(new IdleNode(EC, animStateName[1], idelTime,true));
            //Debug.Log(animStateName[1]);
        }
        
        parrolTree = new BHTree(sequencesNode);
    }

    public override void Enter()
    {
        //Debug.Log("enter Patrol:" );
        
    }
    public override void Updata()
    {

        if(PlayerDec())
        {
            stateMachine.changeState(EC.stateList[0]);
            
        }
        
        parrolTree.tick();
    }

    public override void Exit()
    {
        parrolTree.reset();
    }

    public bool PlayerDec()
    {
        //Debug.Log("decing");
        bool dec = false;
        foreach (Transform pos in decPoints)
        {

            if(Physics2D.Raycast(pos.position, Vector2.right * EC.dir, decDis, playerLayer))
            {
                dec = true;
            }
            if (EC.isdebug)
            {
                Debug.DrawLine(pos.position, new Vector2((pos.position.x + EC.dir * decDis), pos.position.y), Color.red);
                
            }
            
            
        }
        return dec;
    }
}


