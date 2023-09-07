using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.UIElements;

public class scroller : MonoBehaviour
{
    public bool hasStarted = false;
    public bool first = true;
    public bool stop = false;
    public float tempo;
    public int beat;
    public bool isbeat = false;
    public int nbCaisse = 3;
    public Transform spawnPoint;
    public GameObject caissePrefabs;
    public GameObject caisseObject;

    public float _timeFromStart;

    public GameManager gameManager;



    float decalage = 0;
    //MARGE JOUEUR
    public bool isTempoRight()
    {
        float secondsPerBeat = 1; // 60.0f / 60.0f;
        float margin = 0.1f;

        float currentTime = _timeFromStart;
        //float currentTime = _timeFromStart - (int)_timeFromStart;
        float timeSinceLastBeat = currentTime % secondsPerBeat;
        //print(timeSinceLastBeat);
        if (timeSinceLastBeat <= margin || timeSinceLastBeat >= secondsPerBeat - margin)
        {
            //print("true");
            return true;
        }
        else
        {
            //print("false");
            return false;
        }
    }

    // MARGE MACHINE
    public bool isPerfectTempo()
    {
        float secondsPerBeat = 1;
        float margin = 0.01f;  

        float currentTime = _timeFromStart;

        float timeSinceLastBeat = currentTime % secondsPerBeat;
        //print(timeSinceLastBeat);
        if (timeSinceLastBeat <= margin || timeSinceLastBeat >= secondsPerBeat - margin)
        {
            //print("true");
            return true;
        }
        else
        {
            //print("false");
            return false;
        }
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        stop = false;
        StartCoroutine(DoBeat());
        tempo /= 10;
    }
    private void FixedUpdate()
    {
        if (hasStarted)
        {
            _timeFromStart += Time.fixedDeltaTime;
        }
        //isPerfectTempo();

        if (hasStarted)
        {
            if (!stop)
                caisseObject.transform.position += new Vector3(tempo * Time.fixedDeltaTime, 0, 0);
        }
    }

    public IEnumerator DoBeat()
    {
        if (hasStarted == false)
        {
            while (!Input.anyKeyDown)
            {
                yield return null;
            }
            gameManager.Starter();
            first = false;
            caisseObject = Instantiate(caissePrefabs, spawnPoint);
            Camera.main.transform.parent = caisseObject.transform;
            Camera.main.transform.position =
                new Vector3 (
                    caisseObject.transform.position.x + Camera.main.GetComponent<CameraSwitch>().offset,
                    caisseObject.transform.position.y + .78f,
                    Camera.main.transform.position.z
                    );
        }
        yield return new WaitForSecondsRealtime(.99f);
        isbeat = true;
        beat++;

        if (beat == 4)
        {
            beat = 0;
            if (caisseObject.GetComponent<Node>().sucess)
            {
                //REUSSITE
            }
            if (!caisseObject.GetComponent<Node>().sucess)
            {
                //FAIl
                Camera.main.transform.parent = null;
                Destroy(caisseObject);
                caisseObject = Instantiate(caissePrefabs, spawnPoint);
                Camera.main.transform.parent = caisseObject.transform;
                Camera.main.transform.position = 
                    new Vector3 (
                    caisseObject.transform.position.x + Camera.main.GetComponent<CameraSwitch>().offset,
                    caisseObject.transform.position.y + .78f,
                     Camera.main.transform.position.z
                    );
                print("JEXISTE");

            }
        }

         
        print(beat);
        StartCoroutine(EndBeat());
    }
    public IEnumerator EndBeat()
    {
        yield return new WaitForSecondsRealtime(.02f);
        isbeat = false;
        StartCoroutine(DoBeat());
    }
    public IEnumerator hashit()
    {
        while (!isbeat)
        {
            print("oui");
            yield return null;
        }
        //print("AAAAAAAAAAAAA");
        caisseObject = Instantiate(caissePrefabs, spawnPoint);
        
    }

    public void failedHit()
    {
        caisseObject = Instantiate(caissePrefabs, spawnPoint);
    }
}
