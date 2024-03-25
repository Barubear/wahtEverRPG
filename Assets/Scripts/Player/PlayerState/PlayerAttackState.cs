using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private float damage;
    private bool nexTigger;
    private AnimatorStateInfo stateInfo;
    private PlayerAttackState nextAttack;
    private PolygonCollider2D hitBox;
    private List<Collider2D> colliderList = new List<Collider2D>();
    private bool isShaking;
    private bool isPausing;
    private int pauseDura;
    private float shakeDura;
    private float shakeStrangth;


    private ContactFilter2D contactFilter = new ContactFilter2D();
    
    public PlayerAttackState(PlayerControler _player, PlayerStateMachine _stateMachine, string _animStateName, PolygonCollider2D _hitBox, float _damage, AudioClip _audioClip,
        int _pauseDura, float _shakeDura, float _shakeStrangth,PlayerAttackState _nextAttack = null) : base(_player, _stateMachine, _animStateName, _audioClip)
    {

        this.nextAttack = _nextAttack;
        this.hitBox = _hitBox;
        this.damage = _damage;
        contactFilter.useLayerMask = true;
        contactFilter.layerMask = LayerMask.GetMask("enemy");
        pauseDura = _pauseDura;
        shakeDura = _shakeDura;
        shakeStrangth = _shakeStrangth;
    }

    public override void Enter() {
       
        player.animator.Play(animStateName);
        player.audioSource.clip = audioClip;
        player.audioSource.Play();
        player.canGetTigger = false;
        nexTigger = false;
    }
    public override void Updata() {
        //hitbox
        Collider2D[] colliders = new Collider2D[5];
        
        int n  = hitBox.OverlapCollider(contactFilter, colliders);
        if(n != 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (!colliderList.Contains(collider)&& collider != null)
                {
                    if (collider.CompareTag("enemy"))
                    {
                        if (!isPausing) player.StartCoroutine(hitPause(pauseDura));
                        player.camCtrl.hitShake(shakeDura,shakeStrangth);
                        EnemyControler enemy = collider.gameObject.GetComponent<EnemyControler>();

                        enemy.beAttacked(damage);
                        //Debug.Log(animStateName);
                        colliderList.Add(collider);
                       

                    }
                    
                }
            }

        }
        


        //combo
        stateInfo = player.animator.GetCurrentAnimatorStateInfo(0);
        
        if (player.canGetTigger)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                nexTigger = true;
            }
        }
        if (stateInfo.normalizedTime>=1)
        {
            if(nexTigger && nextAttack != null) stateMachine.changeState(nextAttack);
            else stateMachine.changeState(player.idleState);

        }
    }

    // hitPause

    IEnumerator hitPause(int dura) {
        float time = dura / 60f;
        isPausing = true;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
        isPausing = false;

    }

    
    public override void Exit() {

        player.canGetTigger = false;
        nexTigger = false;
        player.rb.velocity = new Vector2(0, 0);
        colliderList.Clear();
    }
    public override bool EnterCheck()
    {
        return true;
    }

    
}
