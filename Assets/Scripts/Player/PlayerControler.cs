using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class PlayerControler : MonoBehaviour
{

    public Animator animator;
    public Rigidbody2D rb;
    public CameraCtrl camCtrl;
    public AudioSource audioSource;
    public bool InputLock;
    
    public float xInput { get; private set; }
    public float yInput { get; private set; }
    public bool right;
    public int facingDir { get; private set; }
    public bool canGetTigger = true;



    [Header("UI")]
    public Image HpBar;
    public Image overBar;

    [Header("Agent")]
    PlayerAgent agent;
    public float maxHP;
    
    [Header("Controle")]
    public float jumpForce;
    public float walkSpeed;

    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCD;
    private SkillControlor dashCtrl;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    [SerializeField] protected float groundCheckDis;
    [SerializeField] protected LayerMask ground;
    public bool isGrounded = true;
    [Header("wall  Check")]
    public Transform wallCheckPos;
    [SerializeField] protected float wallCheckDis;
    [SerializeField] protected LayerMask wall;
    public bool isWall = false;

    [Header("attack")]
    public float damage01;
    public float damage02;
    public float damage03;
    public PolygonCollider2D hitBox01;
    public PolygonCollider2D hitBox02;
    public PolygonCollider2D hitBox03;
    public int pauseDura01;
    public int pauseDura02;
    public float shakeDura01;
    public float shakeDura02;
    public float shakeStrangth01;
    public float shakeStrangth02;

    [Header("audio")]

    public AudioClip jumpAudio;
    public AudioClip attackAudio01;
    public AudioClip attackAudio02;
    public AudioClip beattackAudio;

    //StateMachine

    public bool stateChangeLock = false;
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }


    public PlayerAttackState attackState01 { get; private set; }
    public PlayerAttackState attackState02 { get; private set; }
    public PlayerAttackState attackState03 { get; private set; }
    public PlayerBeAttackState beAttackState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    void Awake()
    {
        Time.timeScale = 1;
        agent = new PlayerAgent(maxHP);
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "playerIdel"); 
        moveState = new PlayerMoveState(this, stateMachine, "playerMove", walkSpeed);
        jumpState = new PlayerJumpState(this, jumpForce, stateMachine, "playerJump", jumpAudio);
        fallState = new PlayerFallState(this, stateMachine, "playerFall");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "wallSlide");
        //attack
        attackState03 = new PlayerAttackState(this, stateMachine, "playerAttack03", hitBox03, damage01, attackAudio02, pauseDura02, shakeDura02, shakeStrangth02,null);
        attackState02 = new PlayerAttackState(this, stateMachine, "playerAttack02", hitBox02, damage02, attackAudio01, pauseDura01, shakeDura01, shakeStrangth01, attackState03);
        attackState01= new PlayerAttackState(this, stateMachine, "playerAttack01", hitBox01, damage03, attackAudio01,pauseDura01,shakeDura01,shakeStrangth01, attackState02);

        
       

        //be attacked
        beAttackState = new PlayerBeAttackState(this, stateMachine, "playerBeAttack", 1,beattackAudio);
        deadState = new PlayerDeadState(this, stateMachine, "playerDead");

        //skill
        //dash
        dashCtrl = new SkillControlor();
        dashState = new PlayerDashState(this, dashCtrl, stateMachine, "playerDash", dashSpeed, dashDuration, dashCD);

        stateMachine.init(idleState);
        facingDir = right ? 1 : -1;

    }

    // Update is called once per frame
    void Update()
    {
        facingDir = right ? 1 : -1;
        groundCheck();
        wallCheck();
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        stateMachine.currState.Updata();
        if (!InputLock) {
            if (Input.GetKeyDown(KeyCode.LeftShift)) stateMachine.changeState(dashState);
        }
        
        if(rb.velocity.y< 0 && !isGrounded)
        {

            if (isWall) stateMachine.changeState(wallSlideState);
            else  stateMachine.changeState(fallState);
        }
        
        dashCtrl.update();
        
    }

    public void flip()
    {

        transform.Rotate(0, 180, 0);
        right = !right;

    }
    protected void groundCheck()
    {

       bool  newisGrounded = Physics2D.Raycast(groundCheckPos.position, Vector2.down, groundCheckDis, ground);
       //if (newisGrounded != isGrounded) Debug.Log("change");
       isGrounded = newisGrounded;
    }
    protected void wallCheck()
    {

        isWall = Physics2D.Raycast(wallCheckPos.position, Vector2.right*facingDir, wallCheckDis, ground);
        //Debug.Log(isWall);
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheckPos.position, new Vector3(groundCheckPos.position.x, groundCheckPos.position.y - groundCheckDis));
        Gizmos.DrawLine(wallCheckPos.position, new Vector3(wallCheckPos.position.x + wallCheckDis, wallCheckPos.position.y ));
    }

    public virtual bool beDamged(float damge)
    {
        if (stateMachine.currState == dashState) {
            return false;
        }
        else
        {
            agent.currHP -= damge;
            UI_Ctrl();
            if (agent.currHP  <= 0)
            {
                stateMachine.changeState(deadState);
            }
            else
            {
                
                if (stateMachine.currState == beAttackState)
                {
                    beAttackState.reAttack();
                }
                else
                {
                    stateMachine.changeState(beAttackState);
                }
            }

            


            return true;
        }

    }

    public virtual void recover(float re)
    {
        agent.currHP += re;
        agent.currHP = agent.currHP > this.maxHP ? maxHP : agent.currHP;

    }

    public void UI_Ctrl()
    {
        HpBar.fillAmount = agent.currHP / maxHP;
    }

    public void stop() { 
        rb.velocity = Vector3.zero;
        InputLock = true;
        animator.speed = 0;
    }

    public void restart()
    {
        
        InputLock = false;
        animator.speed = 1;
    }

}
