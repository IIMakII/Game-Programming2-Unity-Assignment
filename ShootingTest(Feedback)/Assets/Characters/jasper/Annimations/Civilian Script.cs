using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianScript : MonoBehaviour
{
    GameObject player;
    float distFromPlayer;
    [SerializeField] float PlayerDistToScare;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        distFromPlayer = Vector3.Distance(player.transform.position, this.transform.position);

       
    }
}
