using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostDeadState : EnemyDeadState
{
    Image overbar;
    Text text;

    public GhostDeadState(EnemyControler _EC, EnemyStateMachine _stateMachine, List<string> _animStateName, float _duration, Image overbar, Text text) : base(_EC, _stateMachine, _animStateName, _duration)
    {
        this.text = text;
        this.overbar = overbar;
    }

    public override void Updata()
    {
        currTime -= Time.deltaTime;
        EC.rb.velocity = new Vector2(0, 0);
        if (currTime < 0)
        {
            
            if (!overbar.gameObject.activeSelf)
            {
                EC.dead();
                text.text = "Congratulations";
                text.fontSize = 150;
                overbar.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
                
        }
            
    }
}
