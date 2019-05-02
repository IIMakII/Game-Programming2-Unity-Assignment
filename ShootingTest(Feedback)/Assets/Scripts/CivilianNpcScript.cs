using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianNpcScript : MonoBehaviour
{
    GameObject player;
    float distFromPlayer;
    [SerializeField] float PlayerDistToScare;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        distFromPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        if (Input.GetButtonDown("Fire1"))
        {
            if (PlayerDistToScare >= distFromPlayer)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
