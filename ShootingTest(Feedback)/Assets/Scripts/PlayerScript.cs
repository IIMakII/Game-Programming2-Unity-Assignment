using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Text ammoUI,pointsUI;
    int ammo;
    private Camera cam;
    float camOgFOF, timeDiff = 0, points = 0;
    bool aim = false, notAiming = false; 
    [SerializeField] float camNewFOF = 40;
 
    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        camOgFOF = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        ammo = GetComponent<MainGun>().currentAmmo;
        ammoUI.text = "" + ammo;
        timeDiff += Time.deltaTime;
     
        if(Input.GetButtonDown("Fire2"))
        {
            timeDiff = 0;
            aim = true;
            notAiming = false;
        }

        if(Input.GetButtonUp("Fire2"))
        {
            timeDiff = 0;
            aim = false;
            notAiming = true;
        }
 
       if(aim == true)
       {
            if (timeDiff <= .5)
            {
                    cam.fieldOfView = Mathf.Lerp(camOgFOF, camNewFOF, timeDiff * 2);
                    
            }
       }
        
 
       if(notAiming == true)
       {
           if (timeDiff <= .5f)
           {
                    cam.fieldOfView = Mathf.Lerp(camNewFOF, camOgFOF, timeDiff * 2);
                   
           }
       }
        
    }

    public void UpdateScore(float toAdd)
    {
        points += toAdd;
        pointsUI.text = points.ToString();
    }

}
