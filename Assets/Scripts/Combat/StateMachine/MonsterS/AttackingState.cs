using NUnit.Framework.Constraints;
using UnityEngine;

public class AttackingState : IState
{
    public Monster monster;

    private float coolDownTimer;

    public float specialChance = 0.3f;
    private Animator animator;
    public AttackingState(Monster mon)
    {
        this.monster = mon;
        animator = mon.GetComponentInChildren<Animator>();  
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

            if (Random.value < specialChance)
            {
                CallTransition(State.ONSLAUGHT, this);
            }
            else
            {

                coolDownTimer = monster.baseTimer + Random.Range(-monster.timerVariation.x, +monster.timerVariation.y);
                //PerformAtk
                int direction = Random.Range(0, 3);
                monster.atkEvent?.Invoke((AtkDirection) direction, 0.9f);
                monster.SetAtkAnim((AtkDirection) direction);

                if (Random.value <= monster.chanceForOpening)
                {
                    CallTransition(State.OPENED, this);
                }
            }
        }
    }

    public override void Process() 
    { 
        HandleAttack(); 
    }



    public override void PhysicsProcess() { }
}