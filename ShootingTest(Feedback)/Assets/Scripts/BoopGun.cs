using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoopGun : Shooting
{
    // Camera cam;
    [SerializeField] int knockBackForce = 20000;

    // Start is called before the first frame update
    void Start()
    {
      //  cam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        autoReload();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * knockBackForce);
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            StartReload();
        }
    }
}
