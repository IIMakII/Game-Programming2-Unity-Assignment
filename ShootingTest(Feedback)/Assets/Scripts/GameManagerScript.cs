using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System;
using System.IO;

public class GameManagerScript : MonoBehaviour
{
      [SerializeField]  GameObject player;
    [SerializeField]  GameObject [] Civilians;
    [SerializeField] GameObject[] NPCs;
    public static bool ToResume = false;



    private void start()
    {
       
        Civilians = GameObject.FindGameObjectsWithTag("Civilian");
        NPCs = GameObject.FindGameObjectsWithTag("NPC");
        if(ToResume == true)
        {
            GetState();
            ToResume = false;
        }
       
    }

    public void ResumeGame()
    {
        ToResume = true;
        SceneManager.LoadScene(1);
    }

    public  void NewGame()
    {
        SceneManager.LoadScene(1);
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
        if(player != null)
        {
            PlayerPrefs.SetFloat("Xpos", player.transform.position.x);
            PlayerPrefs.SetFloat("Ypos", player.transform.position.y);
            PlayerPrefs.SetFloat("Zpos", player.transform.position.z);
            PlayerPrefs.SetInt("MainGunAmmo", player.GetComponentInParent<MainGun>().currentAmmo);
            PlayerPrefs.SetInt("BoopGunAmmo", player.GetComponentInParent<BoopGun>().currentAmmo);
            PlayerPrefs.SetFloat("Score", player.GetComponentInParent<PlayerScript>().points);
        }
    }

    private void GetState()
    {
        if(player != null)
        {
            player.transform.position = new Vector3(PlayerPrefs.GetFloat("Xpos"), PlayerPrefs.GetFloat("Ypos"), PlayerPrefs.GetFloat("Zpos"));
            player.GetComponentInParent<MainGun>().currentAmmo = PlayerPrefs.GetInt("MainGunAmmo");
            player.GetComponentInParent<BoopGun>().currentAmmo = PlayerPrefs.GetInt("BoopGunAmmo");
            player.GetComponentInParent<PlayerScript>().points = PlayerPrefs.GetFloat("Score");
        }
    }

}
