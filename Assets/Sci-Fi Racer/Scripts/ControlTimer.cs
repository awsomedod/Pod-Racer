using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTimer : MonoBehaviour {

	public LayerMask start;
	public LayerMask finish;
	public bool raceIsOn = true;
	public bool raceHasStarted = false;


	void Update ()

	{
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		if (Physics.Raycast(transform.position, fwd, 2, start ))

		{
			setTimer(1);
			raceHasStarted = true;
		}

		if (Physics.Raycast(transform.position, fwd, 2, finish ) && raceHasStarted == true)
		{
			setTimer(0);
			raceIsOn = false;

		}

	}

	void setTimer(int t){
		TimerCounter playerTimer = this.GetComponent<TimerCounter>();
		playerTimer.startTimer = t;
	}
}