using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shooting : MonoBehaviour
{
    [SerializeField] AudioClip hitSound, gunShot, EmptyClip, Reloading;
    [SerializeField] GameObject HitMarker;
    [SerializeField] int damage = 0, MaxAmmo = 0, blankShot = 0;
    [SerializeField] float NpcDamagePoints = 0, CivilianKillPoint = -0, NpcKillBonusPoints = 0, CivilianOverkillPoint = -0, TimeTillNextShot = 0, ReloadTime = 0;
    protected RaycastHit hit;
    public int currentAmmo = 0;
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

    protected void autoReload()
    {
        timediff += Time.deltaTime;
        if (isReloading == false)
        {
            if (blankShot >= 4)
            {
                StartCoroutine(Reload());
            }
        }
    }

    protected void Shoot()
    {
        if (isReloading == false)
        {
            if (TimeTillNextShot <= timediff)
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
                            GetComponentInParent<PlayerScript>().UpdateScore(NpcDamagePoints);
                            if (hit.transform.GetComponentInParent<NPC_Script>().health <= 0)
                            {
                                GetComponentInParent<PlayerScript>().UpdateScore(NpcKillBonusPoints);
                            }

                        }
                        if (hit.transform.tag == "NPCHead")
                        {
                            hit.transform.GetComponentInParent<NPC_Script>().health -= damage * 2;
                            hit.transform.GetComponentInParent<NPC_Script>().SetUIActive();
                            StartCoroutine(ActiveHitMarker(true));
                            GetComponentInParent<PlayerScript>().UpdateScore(NpcDamagePoints * 2.5f);
                            if (hit.transform.GetComponentInParent<NPC_Script>().health <= 0)
                            {
                                GetComponentInParent<PlayerScript>().UpdateScore(NpcKillBonusPoints);
                            }

                        }
                        if (hit.transform.tag == "Civilian")
                        {
                            if (damage >= 1)
                            {
                                if (hit.transform.GetComponentInParent<Animator>().GetBool("Dead") == true)
                                {
                                    GetComponentInParent<PlayerScript>().UpdateScore(CivilianOverkillPoint);
                                }
                                else
                                {
                                    hit.transform.GetComponent<CivilianNpcScript>().Killed();
                                    GetComponentInParent<PlayerScript>().UpdateScore(CivilianKillPoint);
                                }

                            }

                            StartCoroutine(ActiveHitMarker());
                        }
                    }
                }
            }

        }
    }

    protected void StartReload()
    {
        if (isReloading == false)
        {
            StartCoroutine(Reload());
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
        yield return new WaitForSeconds(ReloadTime);
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
