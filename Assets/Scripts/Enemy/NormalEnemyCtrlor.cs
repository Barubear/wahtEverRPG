using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyCtrlor : EnemyControler
{
    // two state
    // patrol  -> fight


    [Header("player  Check")]
    
    public float decDis;
    public List<Transform> decPoints;
    public PlayerControler player;

    [Header("Attack")]
    public float attackDis;
    public float fightWaitTime;
    public float damage;
    public Collider2D hitBox;
    public List<string> FightAnimatones;
    

    [Header("patrol")]
    public float patrolWaitTime;
    public List<Transform> patrolPoints;
    protected List<Vector2> patrolVec;
    public List<string> PatrolAnimatones;

    [Header("be attacked")]
    public float beAttackedduration;
    public List<string> beAttackedAnimaton;

    [Header("dead")]
    public List<string> deadAnimaton;
    //state
    protected EnemyPatrolState patrolState;
    protected EnemyFightState fightState;
    protected EnemyBeAttackedState beAttackedState;
    protected EnemyDeadState deadState;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        
        setPatrolState();
        setFightState();
        setbeAttackedState();
        setDeadState();

        //fight or idle must be indix 0
        //dead must be indix 1
        stateList.Add(fightState);
        stateList.Add(deadState);
        stateList.Add(patrolState);
        stateList.Add(beAttackedState);
        

        stateMachine.init(patrolState);

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        stateMachine.currState.Updata();
        

    }



    public override void beAttacked(float damge)
    {
        //Debug.Log("beDamged");
        agent.currHP -= damge;
        UI_Ctrl();
       

            if (stateMachine.currState == beAttackedState)
            {
                beAttackedState.reAttack();
            }
            else
            {
                stateMachine.changeState(beAttackedState);
            }
        
    }


    public virtual void setPatrolState()
    {
        
        //PatrolState
        patrolVec = new List<Vector2>();
        
        foreach (Transform ponit in patrolPoints)
        {
            patrolVec.Add(ponit.position);
        }
        patrolState = new EnemyPatrolState(this, stateMachine, PatrolAnimatones, patrolVec, patrolWaitTime, decPoints, decDis);
    }

    public virtual void setFightState()
    {
        //FightState
         fightState = new EnemyFightState(this, stateMachine, FightAnimatones, player.transform, fightWaitTime, attackDis,decDis, damage, hitBox, audioClips[1]);
        
    }

    public virtual void setbeAttackedState()
    {
         beAttackedState = new EnemyBeAttackedState(this, stateMachine, beAttackedAnimaton, beAttackedduration, audioClips[0]);

        

    }
    public virtual void setDeadState()
    {
        deadState = new EnemyDeadState(this, stateMachine, deadAnimaton, 2);
    }
}
    
