using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCollide : MonoBehaviour {

	public GameObject spark;
	[SerializeField] private AudioClip groundHit;
	private AudioSource groundHitSource;


	void Awake()
	{
		groundHitSource = gameObject.AddComponent<AudioSource> ();
		groundHitSource.clip = groundHit;
		groundHitSource.volume = 0.1f;
	}
	void OnCollisionEnter (Collision hit)
	{
		if (hit.gameObject.layer == 10)
		{
			Instantiate (spark, transform.position, Quaternion.identity);
			groundHitSource.Play();

		}
	}
}
