using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour
{
    public Collider2D[] colliders;
    public EnemyControler Ec;
    
    public void openHitBox(int n)
    {
        colliders[n].enabled = true;
    }

    public void closeHitBox(int n)
    {
        colliders[n].enabled = false;
    }

    public void toDestroy()
    {
        Destroy(this.gameObject);
    }


}
