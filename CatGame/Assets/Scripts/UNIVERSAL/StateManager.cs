﻿/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    
	State currentState;


    void Update()
    {
        RunStateMachine();
    }
	
	
	
	
	private void RunStateMachine()
	{
		State nextState = currentState?.RunCurrentState();
		
		if (nextState != null)
		{
			SwitchNextState(nextState);
		}
	}
	
	private void SwitchNextState(State nextState)
	{
		currentState = nextState;
	}
	
}
 */