using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSkillCTrl : MonoBehaviour
{
    public Animator animator;
    
    public bool canDemage = true;
    AnimatorStateInfo animaInfo;
    public void Update()
    {
        
        animaInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animaInfo.normalizedTime >= 1)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(canDemage)
                other.GetComponent<PlayerControler>().beDamged(40);
        }
    }
}
