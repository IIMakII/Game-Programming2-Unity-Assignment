using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianNpcScript : BaseNpc
{
    private GameObject player;
    private float distFromPlayer;
    [SerializeField] float PlayerDistToScare;
    private Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInParent<Animator>();
        anim.SetBool("Patrol", true);
        AssignNavVariables();
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetBool("Patrol") == true)
        {
            StartNavMesh();
        }
        distFromPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        if (Input.GetButtonDown("Fire1"))
        {
            if (PlayerDistToScare >= distFromPlayer)
            {
                anim.SetBool("Scared", true);
                anim.SetBool("Patrol", false);
                anim.SetBool("CivInContact", false);

                StartCoroutine(StopBeingScared());
            }
        }

    }
    IEnumerator StopBeingScared()
    {
        yield return new WaitForSeconds(5f);
        if (PlayerDistToScare >= distFromPlayer)
        {
            StartCoroutine(StopBeingScared());  
        }

        else
        {
            anim.SetBool("Scared", false);
            anim.SetBool("Patrol", true);
            anim.SetBool("CivInContact", false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Civilian")
        {
            anim.SetBool("Scared", false);
            anim.SetBool("Patrol", false);
            anim.SetBool("CivInContact", true);
        }
    }

    public void Killed()
    {
        anim.SetBool("Dead", true);
        anim.SetBool("Scared", false);
        anim.SetBool("Patrol", false);
        anim.SetBool("CivInContact", false);
    }
}
