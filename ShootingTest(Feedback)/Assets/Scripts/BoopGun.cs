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
          
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            StartReload();
        }
    }

    private void FixedUpdate()
    {
        if (hit.rigidbody != null)
        {
           if (currentAmmo<=0)
            {
                hit.rigidbody.AddForce(cam.transform.forward * knockBackForce);
            }
        }
    }
}
