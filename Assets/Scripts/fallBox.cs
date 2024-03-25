using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallBox : MonoBehaviour
{
    public PlayerControler player;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        player.beDamged(player.maxHP + 10);
    }
}
