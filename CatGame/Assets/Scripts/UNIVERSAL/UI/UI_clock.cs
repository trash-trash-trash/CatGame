using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_clock : MonoBehaviour
{
	private Transform clockHandTransform;
	
	private const float REAL_SECONDS_PER_INGAME_DAY = 30f;
	
	private float day;
	
	private void Awake() {
		clockHandTransform = transform.Find("clockHand");
	}
	
	private void FixedUpdate() {
		
		day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;
		
		float dayNormalized = day % 1f;
		
		float rotationDegreesPerDay = 360f;
		clockHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay);
	}
		
	
	


}
