using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroller : MonoBehaviour
{
    public bool hasStarted = false;
    public bool first = true;
    public float tempo;
    public int nbCaisse = 3;
    public Transform spawnPoint;
    public GameObject caissePrefabs;
    public GameObject caisseObject;

    private void Start()
    {
        tempo /= 10;
    }
    // Update is called once per frame
    void Update()
    {
        if(hasStarted)
        {
            if(first)
            {
                first = false;
                caisseObject = Instantiate(caissePrefabs, spawnPoint);
            }
            caisseObject.transform.position += new Vector3(tempo * Time.deltaTime, 0, 0);
        }
    }

    public void hashit()
    {
        caisseObject = Instantiate(caissePrefabs, spawnPoint);
        
    }

    public void failedHit()
    {
        caisseObject = Instantiate(caissePrefabs, spawnPoint);
    }
}
