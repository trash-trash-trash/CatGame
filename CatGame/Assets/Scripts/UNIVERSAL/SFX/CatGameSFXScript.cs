using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatGameSFXScript : MonoBehaviour
{
	//SFX script is a script that handles SFX! :)

	//
	//declares each and every audio clip in "Resources" folder
	public static AudioClip Meow01;
	public static AudioClip Hiss01;

	static AudioSource audioSrc;


	// 
	//all SFX's are loaded at Game Start but not played
	void Start()
	{
		Meow01 = Resources.Load<AudioClip>("Meow01");
		Hiss01 = Resources.Load<AudioClip>("Hiss01");

		audioSrc = GetComponent<AudioSource>();
	}

	//
	//the corresponding SFX are played when called for in other scripts
	//there's a SFX for Game Over even tho it will never be called (unless you are VERY dedicated)
	public static void PlaySound(string clip)
	{
		switch (clip)
		{
			case "Meow01":
				audioSrc.PlayOneShot(Meow01);
				break;
			case "Hiss01":
				audioSrc.PlayOneShot(Hiss01);
				break;

		}
	}




}
