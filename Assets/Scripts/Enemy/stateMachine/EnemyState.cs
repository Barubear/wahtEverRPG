using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected EnemyControler EC;
    protected BHTree parrolTree;
    protected AudioClip audioClip;
    public List<string> animStateName { get; private set; }
    public EnemyState(EnemyControler _EC, EnemyStateMachine _stateMachine, List<string> _animStateName, AudioClip _audioClip = null)
    {
        this.stateMachine = _stateMachine;
        this.EC = _EC;
        this.animStateName = _animStateName;
        this.audioClip = _audioClip;
    }

    public virtual void Enter() { }
    public virtual void Updata() { }
    public virtual void Exit() { }
    public virtual bool EnterCheck()
    {
        return true;
    }
}
