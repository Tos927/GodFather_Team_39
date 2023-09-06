using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    float decalage = 0;
    //MARGE JOUEUR
    public bool isTempoRight()
    {
        float secondsPerBeat = 1; // 60.0f / 60.0f;
        float margin = 0.1f;

        float currentTime = _timeFromStart;
        //float currentTime = _timeFromStart - (int)_timeFromStart;
        float timeSinceLastBeat = currentTime % secondsPerBeat;
        print(timeSinceLastBeat);
        if (timeSinceLastBeat <= margin || timeSinceLastBeat >= secondsPerBeat - margin)
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

    // MARGE MACHINE
    public bool PerfectTempo()
    {
        float secondsPerBeat = 1;
        float margin = 0.01f;  

        float currentTime = _timeFromStart;

        float timeSinceLastBeat = currentTime % secondsPerBeat;

        if (timeSinceLastBeat <= margin || timeSinceLastBeat >= secondsPerBeat - margin)
        {
            print("true");
            decalage += timeSinceLastBeat - 1;
            print(decalage);
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
        while (!PerfectTempo())
        {
            print("oui");
            yield return null;
        }
        print("AAAAAAAAAAAAA");
        caisseObject = Instantiate(caissePrefabs, spawnPoint);
        
    }

    public void failedHit()
    {
        caisseObject = Instantiate(caissePrefabs, spawnPoint);
    }
}
