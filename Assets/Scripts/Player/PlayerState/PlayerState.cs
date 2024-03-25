using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected PlayerControler player;
    protected AudioClip audioClip;
    public  string animStateName { get; private set; }
    public PlayerState(PlayerControler _player , PlayerStateMachine _stateMachine, string _animStateName, AudioClip audioClip = null)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animStateName = _animStateName;
        this.audioClip = audioClip;
    }

    public virtual void Enter() {}
    public virtual void Updata() { }
    public virtual void Exit() { }
    public virtual bool EnterCheck() {
        
        return true;
    }
}
