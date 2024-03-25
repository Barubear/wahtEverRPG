using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillControlor 
{
    private float curTime = 0;
    private float duration;
    private float coolDown;
    private bool Isdoing = false;
    private bool InCD = false;
  
    // Update is called once per frame
    public void update()
    {

        
        curTime -= Time.deltaTime;
        if (Isdoing)
        {
            
            if (curTime <= 0)
            {
                Isdoing = false;
                InCD = true;
                curTime = coolDown;
            }
        }
        if (InCD)
        {
            InCD = (curTime > 0);
            
        }
        
    }
    public bool isDoing()
    {
        return Isdoing;
    }
    public void skillStart(float _duration, float _collDown)
    {
        
        
            Isdoing = true;
            curTime = duration;
            this.duration = _duration;
            this.coolDown = _collDown;
        
        
    }

    public bool startCheck()
    {
        return !InCD && !Isdoing;
    }
    public float getCD()
    {
        if (InCD)
        {
            return (curTime / coolDown);
        }
        else return 0;
    }


}
