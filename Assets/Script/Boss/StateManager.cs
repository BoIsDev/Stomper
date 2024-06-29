using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private IState currentState;
    public void ChangeState(IState newState)
    {
       if (currentState == newState) return;
        
       if(currentState != null)
       {
           currentState.Exit();
       }
        currentState = newState;
        if(currentState != null)
        {
            currentState.Enter();
        }
    }

    public void Update()
    {
        if(currentState != null)
        {
            currentState.Execute();
        }
    }
}
