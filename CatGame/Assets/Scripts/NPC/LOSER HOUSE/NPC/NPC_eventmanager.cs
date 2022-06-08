/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_eventmanager : MonoBehaviour
{
	
	//seperate event for chicken, chick, egg, etc?
	//
	//if a NPC is booped
	public delegate void NPCBooped();
    public static event NPCBooped NPCBoopedEvent;
	public static void NPCBoopedFunction()
	{
		if(NPCBooped!=null)
		{
			NPCBoopedEvent();
		}
	}
	
	//
	//if NPC is attacked
	public delegate void NPCAttacked();
    public static event NPCAttacked NPCAttackedEvent;
	public static void NPCAttackedFunction()
	{
		if(NPCAttacked!=null)
		{
			NPCAttackedEvent();
		}
	}
}
 */