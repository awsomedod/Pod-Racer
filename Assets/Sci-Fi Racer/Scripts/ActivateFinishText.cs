using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateFinishText : MonoBehaviour {

	public Text RaceFinish;

	void Start () {
		
	}
	

	void Update ()
	{
		if (GameObject.Find("Sci-Fi Racer").GetComponent<ControlTimer> ().raceIsOn == false) 
		{
			RaceFinish.enabled = true;

		}
	}
}
