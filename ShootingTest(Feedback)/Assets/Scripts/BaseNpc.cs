using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseNpc : MonoBehaviour
{
    [SerializeField] List<GameObject> destinations;
    int currentDestination = 0;
    NavMeshAgent nav;

    protected void AssignNavVariables()
    {
        nav = GetComponent<NavMeshAgent>();
        int whereToAdd = destinations.Count;
    }

    // Update is called once per frame
    protected void StartNavMesh()
    { 
        if (nav.stoppingDistance >= Vector3.Distance(this.transform.position, destinations[currentDestination].transform.position))
        {
            currentDestination = (currentDestination + 1) % destinations.Count;
            Debug.Log("new pos");
        }

        if (destinations != null)
        {
            nav.SetDestination(destinations[currentDestination].transform.position);
        }
        Vector3 direction = (destinations[currentDestination].transform.position - this.transform.position).normalized; //gets the direction
        direction.y = 0;

        Quaternion rotate = Quaternion.LookRotation(direction);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotate, Time.deltaTime * 2);
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 2);
    }
}
