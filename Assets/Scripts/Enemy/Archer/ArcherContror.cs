using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherContror : NormalEnemyCtrlor
{
    // Start is called before the first frame update
    ArcherFightState archerFight;
    void Start()
    {
        dirSet();
        agent = new EnemyAgent(maxHP);
        stateList = new List<EnemyState>();
        stateMachine = new EnemyStateMachine();

        setPatrolState();
        setFightState();
        setbeAttackedState();
        setDeadState();

        //fight or idle must be indix 0
        //dead must be indix 1
       
        stateList.Add(archerFight);
        stateList.Add(deadState);
        stateList.Add(beAttackedState);
        stateList.Add(patrolState);
        stateMachine.init(patrolState);
    }

    // Update is called once per frame
    void Update()
    {
        dirSet();
        
        stateMachine.currState.Updata();
    }
    public override void setFightState()
    {
        //FightState
        archerFight = new ArcherFightState(this, stateMachine, FightAnimatones, player.transform, fightWaitTime, attackDis, audioClips[1]);

    }

    public override void beAttacked(float damge)
    {
        Debug.Log("beDamged");
        agent.currHP -= damge;
        UI_Ctrl();
        if (agent.currHP <= 0)
        {
            stateMachine.changeState(deadState);
        }
        else
        {
            
            if (stateMachine.currState == beAttackedState)
            {
                beAttackedState.reAttack();
            }
            else
            {
                stateMachine.changeState(beAttackedState);
            }
        }
    }
}
