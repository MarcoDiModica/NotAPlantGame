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
        monster.mat.color = Color.red;
    }

    public override void Exit()
    {
        monster.mat.color = Color.white;
    }

    public override void Process() { }

    public override void PhysicsProcess() { }

    public override void OnAreaEnter(Collider collision)
    {
        if (collision.CompareTag("Sword"))
        {
            //Receives Damage
            SwordVibration.SendHapticImpulse( SettingsManager.Instance.hand , 1, 0.3f);
            monster.BlinkDamage();

            CallTransition(State.ATTACKING, this);

        }
    }
}