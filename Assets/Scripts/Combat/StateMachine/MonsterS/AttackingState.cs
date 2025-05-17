using UnityEngine;

public class AttackingState : IState
{
    public Monster monster;

    private float coolDownTimer;

    public AttackingState(Monster mon)
    {
        this.monster = mon;
    }

    public override void Enter()
    {
        coolDownTimer = 2f;
        monster.Attacking();
    }

    public override void Exit()
    {

    }

    void HandleAttack()
    {
        coolDownTimer -= Time.deltaTime;
        if (coolDownTimer < 0)
        {
            coolDownTimer = monster.baseTimer + Random.Range(-monster.timerVariation.x, +monster.timerVariation.y);
            //PerformAtk
            monster.atkEvent?.Invoke((AtkDirection)Random.Range(0, 3));

            if(Random.value <= monster.chanceForOpening)
            {
                CallTransition(State.OPENED, this);
            }

        }
    }

    public override void Process() 
    { 
        HandleAttack(); 
    }

    public override void PhysicsProcess() { }
}