using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
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
	}

	public void StartRace()
    {
		agent.SetDestination(map.map[currentNode].transform.position);
		hasRaceStarted = true;
	}
	public NavMeshAgent agent;
	public float nodeDistance;
	public float randomPos;
	void Update()
	{
		if (!hasRaceStarted)
			return;

		float distanceToNextNode = Vector3.Distance(transform.position, map.map[currentNode].transform.position);
		if (distanceToNextNode <= nodeDistance)
		{
			currentNode++;
			Vector3 offset = Random.insideUnitSphere * randomPos;
			offset.y = 0;
			agent.SetDestination(map.map[currentNode].transform.position + offset);
		}
	}

	public void HitMe()
    {
		if (hitProcessCoroutine != null)
			StopCoroutine(hitProcessCoroutine);
		hitProcessCoroutine = StartCoroutine(HitProcess());

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