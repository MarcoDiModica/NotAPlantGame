using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterStateMachine : StateMachine
{
    public Monster monster;

    public override void Start()
    {
        monster = GetComponent<Monster>();

        if (initialized) { return; }
        initialized = true;

        states = new Dictionary<State, IState>();

        foreach (var state in stateTypes)
        {
            IState new_state = null;

            switch (state)
            {
                case State.ATTACKING:
                    new_state = new AttackingState(monster);
                    break;
                case State.OPENED:
                    new_state = new OpenedState(monster);
                    break;
            }

            // each state should subscribe to the OnChild Transition
            // therefore when Transitioned is meitted by any state, the exit() and enter() occur automatically
            new_state.Transition += OnChildTransitionEvent;
            states[state] = new_state;
        }

        if (states[startingState] != null)
        {
            currentState = states[startingState];
            states[startingState].Enter();
        }
    }

}
