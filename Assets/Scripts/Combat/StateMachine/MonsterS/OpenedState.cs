using UnityEngine;

public class OpenedState : IState
{
    public Monster monster;

    public OpenedState(Monster mon)
    {
        this.monster = mon;
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Process() { }

    public override void PhysicsProcess() { }

    public override void OnAreaEnter(Collider collision)
    {
        if (collision.CompareTag("Sword"))
        {
            //Receives Damage
            monster.BlinkDamage();

        }
    }
}