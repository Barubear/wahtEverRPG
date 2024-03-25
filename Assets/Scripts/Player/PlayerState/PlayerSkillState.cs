using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerState
{
    public SkillControlor skillControlor;
    
    public float duration;
    public float collDown;
    

    public PlayerSkillState(PlayerControler _player, SkillControlor _skillControlor, PlayerStateMachine _stateMachine, string _animStateName, float _duration, float _collDown)
        :base(_player, _stateMachine, _animStateName)
    {
        this.skillControlor = _skillControlor;
        this.collDown = _collDown;
        this.duration = _duration;
        

    }
    public override void Enter() {
        skillControlor.skillStart(duration, collDown);
    }
    public override bool EnterCheck()
    {
        //Debug.Log(skillControlor.getCD());
        return skillControlor.startCheck();
    }


}
