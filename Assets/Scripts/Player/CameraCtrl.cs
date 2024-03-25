using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public CinemachineBasicMultiChannelPerlin noiseProfile;
    // Start is called before the first frame update
    void Awake()
    {
        noiseProfile = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //hitShake

    public void hitShake(float dura, float shakeStrangth = 0.5f)
    {
        noiseProfile.m_AmplitudeGain = shakeStrangth;
        noiseProfile.m_FrequencyGain = shakeStrangth;
        Invoke("stopShaking", dura);
    }

    private void stopShaking()
    {
        noiseProfile.m_AmplitudeGain = 0;
        noiseProfile.m_FrequencyGain = 0;
    }
}
