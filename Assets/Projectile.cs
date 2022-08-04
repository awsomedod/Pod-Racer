using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Projectile : MonoBehaviour
{
    public GameObject explosion;
    public NavMeshAgent agent;
    public float force;
    public bool hasSpawned;
    public Enemy myTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned)
            return;
        if(myTarget != null)
            agent.SetDestination(myTarget.transform.position);
        if (Vector3.Distance(transform.position, myTarget.transform.position) < 5f)
        {
            myTarget.HitMe();
            Explode();
        }
    }

    public IEnumerator startDestroy()
    {
        yield return new WaitForSeconds(1f);
        Explode();
    }

    public void ChaseTarget(Enemy target)
    {
        StartCoroutine(startDestroy());
        agent.SetDestination(target.transform.position);
        myTarget = target;
    }
    public void ChaseTarget(Vector3 target)
    {
        StartCoroutine(startDestroy());
        agent.SetDestination(target);
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
        print("explode");
    }
}
