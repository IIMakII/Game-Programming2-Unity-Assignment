using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shooting : MonoBehaviour
{
    [SerializeField] AudioClip hitSound, gunShot, EmptyClip, Reloading;
    [SerializeField] GameObject HitMarker;
    [SerializeField] int damage = 70, MaxAmmo = 5, blankShot = 0;
    public int currentAmmo = 5;
    bool isReloading = false;
    Vector3 newSize, ogSize;
    AudioSource ASource;
    Camera cam;
    RectTransform canTrans;
    float timediff = .8f;

    private void Awake()
    {
        currentAmmo = MaxAmmo;
        ASource = GetComponent<AudioSource>();
        cam = GetComponentInChildren<Camera>();
        canTrans = HitMarker.GetComponent<RectTransform>();
        newSize = canTrans.transform.localScale;
        ogSize= canTrans.transform.localScale;
        newSize.x = 1.7f;
        newSize.y = 1.7f;
    }
    

    private void Update()
    {  
        if(isReloading == false)
        {
            if (blankShot >= 4)
            {
                StartCoroutine(Reload());
            }
        }
        
        timediff += Time.deltaTime;
        if(Input.GetButtonDown("Fire1"))
        {
          if (isReloading == false)
          {
                if (.8 <= timediff)
                {
                    if (currentAmmo <= 0)
                    {
                        ASource.PlayOneShot(EmptyClip);
                        timediff = 0;
                        blankShot++;
                    }

                    else
                    {
                        currentAmmo--;
                        timediff = 0;
                        RaycastHit hit;
                        if (gunShot != null)
                        {
                            ASource.PlayOneShot(gunShot, .2f);
                        }
                        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
                        {
                            if (hit.transform.tag == "NPCBody")
                            {
                                hit.transform.GetComponentInParent<NPC_Script>().health -= damage;
                                hit.transform.GetComponentInParent<NPC_Script>().SetUIActive();
                                StartCoroutine(ActiveHitMarker());

                            }
                            if (hit.transform.tag == "NPCHead")
                            {
                                hit.transform.GetComponentInParent<NPC_Script>().health -= damage * 2;
                                hit.transform.GetComponentInParent<NPC_Script>().SetUIActive();
                                StartCoroutine(ActiveHitMarker(true));

                            }
                        }
                    }
                }

            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if(isReloading == false)
            {
                StartCoroutine(Reload());
            }
        }
    }

    IEnumerator Reload()
    {
        Debug.Log("reload press");
        if (EmptyClip != null)
        {
            ASource.PlayOneShot(Reloading);
        }
        isReloading = true;
        Debug.Log("reload started");
        yield return new WaitForSeconds(1.8f);
        Debug.Log("reload done");
        currentAmmo = MaxAmmo;
        isReloading = false;
        blankShot = 0;
    }
    IEnumerator ActiveHitMarker(bool critical = false)
    {
        if(HitMarker != null)
        {
            if(critical == true)
            {
                canTrans.transform.localScale = newSize; 
            }

            else
            {
                canTrans.transform.localScale = ogSize;
            }
            HitMarker.SetActive(true);
        }
        if (hitSound != null)
        {
            if (critical == true)
            {
                ASource.volume = 1f;
                ASource.pitch = 1.4f;
            }

            else
            {
                ASource.volume = .8f;
                ASource.pitch = .8f;
            }
            ASource.PlayOneShot(hitSound);
        }
        yield return new WaitForSeconds(.3f);

        if (HitMarker != null)
        {
            HitMarker.SetActive(false);
        }

    }
}
