using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
public class RacerShoot : MonoBehaviour
{
    public Transform shootPos;
    public GameObject projectile;
    private XRIDefaultInputActions racerControls;
    private InputAction buttonAction;
    public float fireRate;
    float lastfired;
    Enemy[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        racerControls = new XRIDefaultInputActions();
        buttonAction = racerControls.XRIRightHandInteraction.Activate;
        buttonAction.Enable();
        enemies = FindObjectsOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonAction.IsPressed())
        {
            float closestDistance = Mathf.Infinity;
            Enemy closestEnemy = null;
            foreach (Enemy enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy <= closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
            if (Time.time - lastfired > 1 / fireRate)
            {
                if (Vector3.Dot(transform.forward, closestEnemy.transform.position - transform.position) >= 0.5f)
                {

                    GameObject instantiatedProjectile = Instantiate(projectile, shootPos.position, projectile.transform.rotation);
                    instantiatedProjectile.transform.forward = transform.forward;
                    instantiatedProjectile.GetComponent<Projectile>().ChaseTarget(closestEnemy);
                    instantiatedProjectile.GetComponent<Projectile>().hasSpawned = true;
                }
                else
                {
                    GameObject instantiatedProjectile = Instantiate(projectile, shootPos.position, projectile.transform.rotation);
                    instantiatedProjectile.transform.forward = transform.forward;
                    NavMesh.SamplePosition(transform.position + transform.forward * 100f, out NavMeshHit hit, 20f, NavMesh.AllAreas);
                    instantiatedProjectile.GetComponent<Projectile>().ChaseTarget(hit.position);
                    instantiatedProjectile.GetComponent<Projectile>().hasSpawned = true;
                }
                lastfired = Time.time;
            }
        }
    }
}
