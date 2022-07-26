using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCounter : MonoBehaviour {

	[SerializeField] private Text timerLabel;
	public float startTimer = 0;

	private float time;

	void Update() {


		if (startTimer > 0) {
			time += Time.deltaTime;

			var minutes = Mathf.Floor (time / 60);
			var seconds = time % 60;//Use the euclidean division for the seconds.
			var fraction = (time * 100) % 100;

			//update the label value
			timerLabel.text = string.Format ("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
		} 
	}



}
