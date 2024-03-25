using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arowContrlor : MonoBehaviour
{
    public Transform HitPoint;
    public Rigidbody2D rb;
    public GameObject HitEff;
    public float damage;
    public float flySpeed;
    public int dir = 1;
    bool isHit = false;
    public AudioSource audioSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isHit)
            rb.velocity = new Vector2(flySpeed *dir, 0);
        else
            rb.velocity = Vector2.zero;
            


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isHit = true;
            Instantiate(HitEff, HitPoint.position, Quaternion.Euler(0f, dir>0? 0:180f, 0f));
            other.GetComponent<PlayerControler>().beDamged(damage);
            Destroy(this.gameObject);
            //Debug.Log("player");
        }
        if (other.CompareTag("ground"))
        {
            isHit = true;
            Instantiate(HitEff, HitPoint.position, Quaternion.Euler(0f, dir > 0 ? 0 : 180f, 0f));
            Debug.Log("ground");
            Destroy(this.gameObject);
        }
    }

}
