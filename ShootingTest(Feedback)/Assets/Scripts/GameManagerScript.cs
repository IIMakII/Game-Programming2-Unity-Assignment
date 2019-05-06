using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;
using System.IO;

public class GameManagerScript : MonoBehaviour
{
      [SerializeField] GameObject player;
    [SerializeField]  GameObject [] Civilians;
    [SerializeField] GameObject[] NPCs;


    // Start is called before the first frame update
    private void start()
    {
       
        Civilians = GameObject.FindGameObjectsWithTag("Civilian");
        NPCs = GameObject.FindGameObjectsWithTag("NPC");
        GetState();
    }

    // Update is called once per frame
    private void OnApplicationQuit()
    {
        SetState();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
            SetState();
        }
        else
        {
            GetState();
        }
    }

    private void Update()
    {
        SetState();

    }

    private void SetState()
    {
        PlayerPrefs.SetFloat("Xpos", player.transform.position.x);
        PlayerPrefs.SetFloat("Ypos", player.transform.position.y);
        PlayerPrefs.SetFloat("Zpos", player.transform.position.z);
        PlayerPrefs.SetInt("MainGunAmmo", player.GetComponentInParent<MainGun>().currentAmmo);
        PlayerPrefs.SetInt("BoopGunAmmo", player.GetComponentInParent<BoopGun>().currentAmmo);
        PlayerPrefs.SetFloat("Score", player.GetComponentInParent<PlayerScript>().points);
    }

    private void GetState()
    {
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("Xpos"), PlayerPrefs.GetFloat("Ypos"), PlayerPrefs.GetFloat("Zpos"));
        player.GetComponentInParent<MainGun>().currentAmmo = PlayerPrefs.GetInt("MainGunAmmo");
        player.GetComponentInParent<BoopGun>().currentAmmo = PlayerPrefs.GetInt("BoopGunAmmo");
        player.GetComponentInParent<PlayerScript>().points = PlayerPrefs.GetFloat("Score");
    }

}
