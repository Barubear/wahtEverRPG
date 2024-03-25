using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class stageManger : MonoBehaviour
{
    public GameObject ghost;
    public GameObject ghostHP;
    public AudioSource audioSource;
    public AudioClip bossBgm;
    public AudioClip clearBgm;
    private GoshtContr goshtContr;
    public PlayableDirector director;
    public PlayerControler player;
    public float timelineDura;
    public GameObject bossCamare;
    public Collider2D col;
    public void Start()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        col.gameObject.SetActive(false);
        ghost.SetActive(true);
        ghostHP.SetActive(true);
        player.stop();

        timeLineStart();


        goshtContr = ghost.GetComponent<GoshtContr>();
        Invoke("timeLineEnd", timelineDura);
    }

    public void Update()
    {
        
        if (goshtContr != null && goshtContr.isdead) {
            audioSource.clip = clearBgm;
            audioSource.loop = false;
            audioSource.Play();
        }
    }
    public void timeLineStart() {
        player.stop();
        audioSource.Stop();
        director.Play();
    }
    public void timeLineEnd() {
        director.Stop();
        player.restart();
        audioSource.clip = bossBgm;
        audioSource.Play();
        Destroy(bossCamare);
    }
}
