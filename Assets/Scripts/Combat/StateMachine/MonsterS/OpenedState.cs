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
        monster.Opened();
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
            SwordVibration.SendHapticImpulse( UnityEngine.XR.XRNode.RightHand , 1, 0.3f);
            monster.BlinkDamage();

            CallTransition(State.ATTACKING, this);

        }
    }
}