using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControler : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public EnemyStateMachine stateMachine;
    public Animator animator;
    public List<EnemyState> stateList;
    public float maxHP;
    public EnemyAgent agent;
    public bool isdebug = false;
    
    [Header("Move")]
    public bool right;
    public float moveSpeed;
    [Header("UI")]
    public Image HpBar;

    [Header("audio")]
    public AudioSource audioSource;
    public List <AudioClip> audioClips;
    

    public int dir { get; private set; }  
    // Start is called before the first frame update
     protected void Start()
    {
        dirSet();
        agent = new EnemyAgent(maxHP);
        stateList = new List<EnemyState>();
        stateMachine = new EnemyStateMachine();
        
    }

    // Update is called once per frame
    protected void Update()
    {
        dirSet();
        UI_Ctrl();
    }

    public void flip()
    {

        transform.Rotate(0, 180, 0);
        right = !right;

    }
    public void dirSet()
    {
        dir = (right ? 1 : -1);
    }

    public virtual void beAttacked(float damage)
    {

    }



    public virtual void dead()
    {
        Destroy(this.gameObject);
    }

    public void UI_Ctrl()
    {
        HpBar.fillAmount = agent.currHP / maxHP;
    }
}
