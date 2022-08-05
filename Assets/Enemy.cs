using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
	Rigidbody rb;
	public Map map;
	public RaceManager race;
	public int currentNode;

	public Vector3 calculatedInput;
	public float accel;
	public float deccel;

	bool hasRaceStarted;
	void Start()
	{
		map = FindObjectOfType<Map>();
		rb.GetComponent<Rigidbody>();
	}

	public void StartRace()
    {
		agent.SetDestination(map.map[currentNode].transform.position);
		hasRaceStarted = true;
	}
	public NavMeshAgent agent;
	public float nodeDetectionRadius;
	public float randomNodeOffset;
	void Update()
	{
		//DistanceLeftOnTrack();
		if (!hasRaceStarted)
			return;

		float distanceToNextNode = Vector3.Distance(transform.position, map.map[currentNode].transform.position);
		if (distanceToNextNode <= nodeDetectionRadius)
		{
			currentNode++;
			Vector3 offset = Random.insideUnitSphere * randomNodeOffset;
			offset.y = 0;
			agent.SetDestination(map.map[currentNode].transform.position + offset);
		}
	}

	public void ShootMe()
    {
		if (hitProcessCoroutine != null)
			StopCoroutine(hitProcessCoroutine);
		hitProcessCoroutine = StartCoroutine(HitProcess());

	}

	public void HitMe()
    {
		if (ragdollProcessCoroutine != null)
			StopCoroutine(ragdollProcessCoroutine);
		hitProcessCoroutine = StartCoroutine(RagdollProcess());
	}

	Coroutine ragdollProcessCoroutine;
	public float hitForce;
	public IEnumerator RagdollProcess()
	{
		agent.enabled = false;
		rb.AddExplosionForce(hitForce, transform.position, 5);
		yield return new WaitForSeconds(3f);
		agent.enabled = true;
		agent.SetDestination(map.map[currentNode].transform.position);
	}

	public void DistanceLeftOnTrack()
    {
		float returnVal = 0;
		for (int i = currentNode; i < map.map.Count; i++)
        {
			Debug.DrawLine(map.map[i].transform.position,map.map[i + 1].transform.position);
		}
    }

	Coroutine hitProcessCoroutine;
	public IEnumerator HitProcess()
	{
		agent.enabled = false;
		yield return new WaitForSeconds(1f);
		agent.enabled = true;
		agent.SetDestination(map.map[currentNode].transform.position);
	}
}