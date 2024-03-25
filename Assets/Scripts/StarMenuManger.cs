using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarMenuManger : MonoBehaviour
{
    
    public GameObject stopMenu;
    public Slider Bgmslider;
    public Slider Sfxslider;
    public float BgmVolume;
    public float SfxVolume;
    public AudioSource BgmSource;
    public List<AudioSource> sfxSources;
    public AudioSource buttonSource;
    public void Awake()
    {
        BgmVolume =  PlayerPrefs.GetFloat("BgmVolume", 0.5f);
        BgmSource.volume = BgmVolume;
        Bgmslider.value = BgmVolume;
        SfxVolume = PlayerPrefs.GetFloat("SfxVolume", 0.5f);
        Sfxslider.value = SfxVolume;
        if (sfxSources!= null) { 
            foreach (AudioSource sfx in sfxSources)
            {
                sfx.volume = SfxVolume;
            }
        }
        buttonSource.volume = SfxVolume;
        Bgmslider.onValueChanged.AddListener(OnBgmSliderValueChanged);
        Sfxslider.onValueChanged.AddListener(OnSfxSliderValueChanged);
    }


    public void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            windowSwtichWithTimeStop(stopMenu);
            
        }
        

    }
    public void loadMainSense()
    {
        buttonSource.Play();
        SceneManager.LoadScene(1);
        
    }

    public void exitGame()
    {
        buttonSource.Play();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        
        #else
               Application.Quit();
        #endif
    }

    public void windowSwtichWithTimeStop ( GameObject wid )
    {
        buttonSource.Play();
        wid.SetActive(!wid.activeSelf);
        
        Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
        
    }

    public void windowSwtich(GameObject wid)
    {
        buttonSource.Play();
        wid.SetActive(!wid.activeSelf);
       
    }

   
    private void OnBgmSliderValueChanged(float value)
    {

        PlayerPrefs.SetFloat("BgmVolume", value);
        BgmSource.volume = value;

    }
    private void OnSfxSliderValueChanged(float value)
    {
        PlayerPrefs.SetFloat("SfxVolume", value);
        if (sfxSources.Count != 0)
        {
            foreach (AudioSource sfx in sfxSources)
            {
                sfx.volume = value;
            }
        }
        buttonSource.volume = value;
    }


}
