using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimContrl : EnemyAnimEvent
{
    
    public Transform target;
    public GameObject skillEff;
    

    public void doSkill() {

        Instantiate(skillEff, new Vector2(target.position.x, -26f),Quaternion.identity);
    }

   

    
}
