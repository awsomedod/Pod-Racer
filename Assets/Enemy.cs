using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
	public Map map;
	public int currentNode;

	private XRIDefaultInputActions racerControls;

	Rigidbody m_body;
	float m_deadZone = 0.1f;
	Vector3 localVelocity;

	public float m_hoverForce = 9.0f;
	//Force of hover
	public float m_StabilizedHoverHeight = 2.0f;
	//Height of hover
	public GameObject[] HoverPointsGameObjects;
	//Points where hover will push down
	public float m_forwardAcl = 100.0f;
	//Foward Acceleation of car
	public float m_backwardAcl = 25.0f;
	//Backwords/reverse Acceleration of car
	public float m_currThrust = 0.0f;
	//Do not modify! Current speed
	public float m_turnStrength = 1f;
	//Strength of the turn
	float CurrentTurnAngle = 0.0f;
	//Current Turn Rotation
	private InputAction translateAnchor;
	private InputAction rotateAnchor;



	int m_layerMask;
	public Vector3 calculatedInput;
	public float accel;
	public float deccel;

	void Start()
	{
		map = FindObjectOfType<Map>();
		m_body = GetComponent<Rigidbody>();

		m_layerMask = 1 << LayerMask.NameToLayer("Characters");
		m_layerMask = ~m_layerMask;
		racerControls = new XRIDefaultInputActions();
		translateAnchor = racerControls.XRIRightHandInteraction.TranslateAnchor;
		translateAnchor.Enable();
		rotateAnchor = racerControls.XRIRightHandInteraction.RotateAnchor;
		rotateAnchor.Enable();
		agent.SetDestination(map.map[currentNode].transform.position);
	}

	void OnDrawGizmos()
	{

		//  Hover Force
		RaycastHit hit;
		for (int i = 0; i < HoverPointsGameObjects.Length; i++)
		{
			var hoverPoint = HoverPointsGameObjects[i];
			if (Physics.Raycast(hoverPoint.transform.position,
				-Vector3.up, out hit,
				m_StabilizedHoverHeight,
				m_layerMask))
			{
				Gizmos.color = Color.green;
				//Color if correctly alligned
				Gizmos.DrawLine(hoverPoint.transform.position, hit.point);
				Gizmos.DrawSphere(hit.point, 0.5f);
			}
			else
			{
				Gizmos.color = Color.red;
				//Color if incorrectly alligned
				Gizmos.DrawLine(hoverPoint.transform.position,
					hoverPoint.transform.position - Vector3.up * m_StabilizedHoverHeight);
			}
		}
	}
	public NavMeshAgent agent;
	public float nodeDistance;
	public float randomPos;
	void Update()
	{
		float distanceToNextNode = Vector3.Distance(transform.position, map.map[currentNode].transform.position);
		if (distanceToNextNode <= nodeDistance)
		{
			currentNode++;
			Vector3 offset = Random.insideUnitSphere * randomPos;
			offset.y = 0;
			agent.SetDestination(map.map[currentNode].transform.position + offset);
		}
	}

	void FixedUpdate()
	{
		//  Hover Force
		RaycastHit hit;
		for (int i = 0; i < HoverPointsGameObjects.Length; i++)
		{
			var hoverPoint = HoverPointsGameObjects[i];
			if (Physics.Raycast(hoverPoint.transform.position,
				-Vector3.up, out hit,
				m_StabilizedHoverHeight,
				m_layerMask))
				m_body.AddForceAtPosition(Vector3.up
					* m_hoverForce
					* (1.0f - (hit.distance / m_StabilizedHoverHeight)),
					hoverPoint.transform.position);
			else
			{
				if (transform.position.y > hoverPoint.transform.position.y)
					m_body.AddForceAtPosition(
						hoverPoint.transform.up * m_hoverForce,
						hoverPoint.transform.position);
				else
					//adding force to car
					m_body.AddForceAtPosition(
						hoverPoint.transform.up * -m_hoverForce,
						hoverPoint.transform.position);
			}
		}

		// Forward
		if (calculatedInput.y > 0)
		{
			m_body.AddRelativeForce(Vector3.forward * Mathf.Max(0f, m_forwardAcl));
		}
		//Backward
		if (calculatedInput.y < 0)
		{
			m_body.AddRelativeForce(Vector3.back * Mathf.Max(0f, m_backwardAcl));
		}

		//Reduce sideways slide
		localVelocity = m_body.transform.InverseTransformDirection(m_body.velocity);
		localVelocity.x *= 0.95f;
		m_body.velocity = m_body.transform.TransformDirection(localVelocity);

		// Turn
		if (CurrentTurnAngle > 0)
		{
			m_body.AddRelativeTorque(Vector3.up * CurrentTurnAngle * m_turnStrength);
		}
		else if (CurrentTurnAngle < 0)
		{
			m_body.AddRelativeTorque(Vector3.up * CurrentTurnAngle * m_turnStrength);


		}
	}
}