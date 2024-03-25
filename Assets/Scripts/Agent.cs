using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent
{
    protected float maxHP;
    public float currHP;

    public Agent(float _maxHP)
    {
        this.maxHP = _maxHP;
        this.currHP = this.maxHP;

    }

    
    
}
