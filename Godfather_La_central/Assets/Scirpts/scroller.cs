using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroller : MonoBehaviour
{
    public bool hasStarted = false;
    public bool first = true;
    public bool stop = false;
    public float tempo;
    public int nbCaisse = 3;
    public Transform spawnPoint;
    public GameObject caissePrefabs;
    public GameObject caisseObject;

    public float _timeFromStart;

    public bool isTempoRight()
    {
        float tempo = _timeFromStart - Mathf.Floor(_timeFromStart);
        if (_timeFromStart >= 0.9f || _timeFromStart <=  0.1f)
        {
            print("true");
            return true;
        }
        else
        {
            print("false");
            return false;
        }
    }

    private void Start()
    {
        tempo /= 10;
    }
    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            _timeFromStart += Time.deltaTime;
        }

        if(hasStarted)
        {
            if(first)
            {
                first = false;
                caisseObject = Instantiate(caissePrefabs, spawnPoint);
            }

            if(!stop)
                caisseObject.transform.position += new Vector3(tempo * Time.deltaTime, 0, 0);
        }
    }

    public IEnumerator hashit()
    {
        while (!isTempoRight())
        {
            yield return null;
        }
        caisseObject = Instantiate(caissePrefabs, spawnPoint);
        
    }

    public void failedHit()
    {
        caisseObject = Instantiate(caissePrefabs, spawnPoint);
    }
}
