using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoshtContr : EnemyControler
{
   

    [Header("fight")]
    public PlayerControler player;
    public float hoverTime;
    public float attackDis;
    public float dashDis;
    public  float damage;
    public Collider2D hitBox;
    public List<string> fightAnimStateName;
    public float skillCD;
    public bool isdead= false;

    [Header("be attacked")]
    public Image overbar;
    public Text text;

    [Header("be attacked")]
    public List<string> beAttackedAnimaton;
    public float beAttackedduration;
    public List<string> deadAnimaton;

    



    public List<string> bornAnimaton;
    private bool State2 = false;

    protected GhostFightState fightState;
    protected EnemyBeAttackedState beAttackedState;
    protected GhostDeadState deadState;
    protected GhostBornState bornState;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        stateMachine = new EnemyStateMachine();
        fightState = new GhostFightState(this, stateMachine, fightAnimStateName, player.gameObject.transform, hoverTime, attackDis, dashDis, damage, hitBox, audioClips.GetRange(1,2));

        beAttackedState = new EnemyBeAttackedState(this, stateMachine, beAttackedAnimaton, beAttackedduration, audioClips[0]);
        deadState = new GhostDeadState(this, stateMachine, deadAnimaton, 2,overbar ,text);
        bornState = new GhostBornState(this, stateMachine, bornAnimaton);


        //fight or idle must be indix 0
        //dead must be indix 1
        stateList.Add(fightState);
        stateList.Add(deadState);
        stateList.Add(beAttackedState);


        stateMachine.init(bornState);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!State2 && agent.currHP < 0.5 * maxHP)
        {
            stateList[0] = new GhostFightState02(this, stateMachine, fightAnimStateName, player.gameObject.transform, hoverTime, attackDis, dashDis, damage, hitBox, skillCD, audioClips.GetRange(1, 2));
            State2 = true;
            stateMachine.changeState(stateList[0]);
            
        }
        stateMachine.currState.Updata();
        dirSet();
        
        /*if (Input.GetMouseButtonDown(0))
        {
            animator.Play("Cast");
        }
        AnimatorStateInfo animaInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animaInfo.IsName("Cast"))
        {
            if (animaInfo.normalizedTime > 1)
                animator.Play("Idle");
            
        }*/
    }

    public override void beAttacked(float damge)
    {
        Debug.Log("beDamged");
        agent.currHP -= damge;
        UI_Ctrl();
        if (agent.currHP <= 0)
        {
            stateMachine.changeState(deadState);
            isdead = true;
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
