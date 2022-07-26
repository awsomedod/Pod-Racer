using UnityEngine;
using UnityEngine.AI;

public class MyPath : MonoBehaviour
{
    public float nodeDistance;
    public int currentNode;
    Map map;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        map = FindObjectOfType<Map>();
        agent.speed = 0;
        agent.SetDestination(map.map[currentNode].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToNextNode = Vector3.Distance(transform.position, map.map[currentNode].transform.position);
        if (distanceToNextNode < nodeDistance)
        {
            currentNode++;
            agent.velocity = (map.map[currentNode].transform.position) - transform.position;
        }
    }
}
