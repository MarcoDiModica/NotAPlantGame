using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;

public abstract class StateMachine : MonoBehaviour
{

    [SerializeField] protected State[] stateTypes;

    protected Dictionary<State, IState> states;

    public State startingState;

    public IState currentState;

    public State checkState;

    protected bool initialized = false;

    public float debugSpeed = 0;

    public event Action<State> OnTransition;

    // Start is called before the first frame update
    public virtual void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentState?.Process();
    }

    void FixedUpdate()
    {
        currentState?.PhysicsProcess();
    }

    // Call sparingly from non-state classes , be careful
    public void OnChildTransitionEvent(State new_state_type)
    {

        OnTransition?.Invoke(new_state_type);
        currentState?.Exit();
        states[new_state_type]?.Enter();
        checkState = new_state_type;
        currentState = states[new_state_type];
        return;
    }


    protected void OnChildTransitionEvent(State new_state_type, IState prev_state)
    {
        if (currentState != prev_state) { return; }

        prev_state.Exit();
        OnTransition?.Invoke(new_state_type);
        states[new_state_type]?.Enter();
        checkState = new_state_type;
        currentState = states[new_state_type];

    }

    protected void OnTriggerEnter(Collider collision)
    {
        currentState?.OnAreaEnter(collision);
    }

    protected void OnTriggerExit(Collider collision)
    {
        currentState?.OnAreaExit(collision);
    }

    protected void OnTriggerStay(Collider collision)
    {

    }

    protected void OnCollisionEnter(Collision collision)
    {
        currentState?.OnBodyEnter(collision.collider);
    }
    protected void OnCollisionStay(Collision collision)
    {
        currentState?.OnBodyStay(collision.collider);
    }

    public State GetCurrentState()
    {
        foreach (var entry in states)
        {
            if (entry.Value == currentState)
            {
                return entry.Key;
            }
        }

        return State.ATTACKING;
    }

}