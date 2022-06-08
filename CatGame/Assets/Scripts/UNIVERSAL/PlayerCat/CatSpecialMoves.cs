using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpecialMoves
{
	//list of Special Moves
	public enum SpecialMoves
	{
		Defecate,
		Meow,
		Bite,
	}
	
	//list of unlocked moves
	private List<SpecialMoves> unlockedMovesList;
	
	//activates moves list
	public void PlayerMoves()
	{
		unlockedMovesList = new List<SpecialMoves>();
	}
	
	//function for adding moves to unlocked move list
	public void UnlockMove(SpecialMoves move)
	{
		unlockedMovesList.Add(move);
	}
	
	
	public bool IsMoveUnlocked(SpecialMoves move)
	{
			return unlockedMovesList.Contains(move);
	}
	
	
	
	
	
}