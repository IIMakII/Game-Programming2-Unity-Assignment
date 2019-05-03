using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class NPC_Script : BaseNpc
{
    private GameObject player;
    private Canvas _canvas;
    [SerializeField] GameObject _canvasGO;
    
   
    private Scrollbar scroll;
    public float health = 200;
    private float originalHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _canvas = GetComponentInChildren<Canvas>();
        originalHealth = health;
        scroll = GetComponentInChildren<Scrollbar>();
        _canvasGO.SetActive(false);
        AssignNavVariables();
    }

    // Update is called once per frame
    void Update()
    {
        StartNavMesh();

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }

        if(_canvasGO.activeInHierarchy == true)
        {
            scroll.size = Mathf.Lerp(0, 1, health / originalHealth);

            Vector3 direction = (player.transform.position - this.transform.position).normalized;
            direction.y = 0;

            Quaternion rotate = Quaternion.LookRotation(direction);
            _canvas.transform.rotation = rotate; 
        }
 
    }
    public void SetUIActive()
    {
        Debug.Log(" active");
        _canvasGO.SetActive(true);

        StartCoroutine(DeactivateUI());
    }

    IEnumerator DeactivateUI()
    {
        Debug.Log(" will soon unactive");
        
        yield return new WaitForSeconds(5);
        _canvasGO.SetActive(false);
        Debug.Log("unactive");

    }
}
