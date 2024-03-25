using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcherAnimEvent : MonoBehaviour
{
    ArcherContror archer;
    public GameObject arow;
    public Transform shootPos;
    void Start()
    {
        archer = GetComponentInParent<ArcherContror>();
    }

    public void shoot()
    {
       GameObject arowIns =  Instantiate(arow, shootPos.position, Quaternion.Euler(0f, archer.dir > 0 ? 0 : 180f, 0f));
       arowIns.GetComponent<arowContrlor>().dir = archer.dir;
       archer.audioSource.clip = archer.audioClips[1];
       archer.audioSource.Play();
    }
}
