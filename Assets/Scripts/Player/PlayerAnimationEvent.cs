using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public PlayerControler player;
    public Collider2D[] colliders;
    Animator animator;
    // Start is called before the first frame update
    
    public void getTiggerON()
    {
        player.canGetTigger = true;
    }

    public void xVelocity(float rate)
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x *rate, player.rb.velocity.y);
    }

    public void openHitBox(int n)
    {
        colliders[n].enabled = true;
    }

    public void closeHitBox(int n)
    {
        colliders[n].enabled = false;
    }
}
