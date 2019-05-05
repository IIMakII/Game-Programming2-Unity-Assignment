using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGun : Shooting
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        autoReload();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartReload();
        }

    }
}
