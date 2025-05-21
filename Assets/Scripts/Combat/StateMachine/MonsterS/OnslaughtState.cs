using System.Collections;
using UnityEngine;

public class OnslaughtState : IState
{
    public Monster monster;

    private float coolDownTimer;

    public float specialChance = 0.3f;



    public OnslaughtState(Monster mon)
    {
        this.monster = mon;
    }

    public override void Enter()
    {
        // monster calls routine bc non mono classes can't
        monster.StartOnslaughts();
    }

    public override void Exit()
    {

    }


  
    public override void Process()
    {

    }

    public override void PhysicsProcess() { }

}
