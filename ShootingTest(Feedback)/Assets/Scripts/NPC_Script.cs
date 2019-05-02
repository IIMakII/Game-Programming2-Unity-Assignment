using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class NPC_Script : MonoBehaviour
{
    private GameObject player;
    private Canvas _canvas;
    [SerializeField] GameObject _canvasGO;
    [SerializeField] List<GameObject> destinations;
    int currentDestination = 0;
    private Scrollbar scroll;
    public float health = 200;
    private float originalHealth;
    

    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _canvas = GetComponentInChildren<Canvas>();
        originalHealth = health;
        nav = GetComponent<NavMeshAgent>();
        scroll = GetComponentInChildren<Scrollbar>();
        _canvasGO.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
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

        if (nav.stoppingDistance >= Vector3.Distance(this.transform.position, destinations[currentDestination].transform.position))
        {
            currentDestination = (currentDestination + 1) %  destinations.Capacity;
            Debug.Log("new pos");
        }

        if (destinations.Count >= 1)
        {
            nav.SetDestination(destinations[currentDestination].transform.position);
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
