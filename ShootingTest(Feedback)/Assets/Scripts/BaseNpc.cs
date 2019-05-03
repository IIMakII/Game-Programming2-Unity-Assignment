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
        destinations.Insert(whereToAdd, this.gameObject);
    }

    // Update is called once per frame
    protected void StartNavMesh()
    {

        if (nav.stoppingDistance >= Vector3.Distance(this.transform.position, destinations[currentDestination].transform.position))
        {
            currentDestination = (currentDestination + 1) % destinations.Count;
            Debug.Log("new pos");
        }

        if (destinations.Count >= 1)
        {
            nav.SetDestination(destinations[currentDestination].transform.position);
        }
    }
}
