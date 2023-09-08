using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.UIElements;

public class scroller : MonoBehaviour
{
    public bool hasStarted = false;
    public bool stop = false;
    public float tempo;
    public int beat = 0;
    public Transform spawnPoint;
    public Transform[] spawnPoints;
    public GameObject caissePrefabs;
    public GameObject caisseObject;

    public float _timeFromStart;

    public GameManager gameManager;



    //float decalage = 0;
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
        beat = 0;
        //StartCoroutine(DoBeat());
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
           caisseObject.transform.position += new Vector3(tempo * Time.fixedDeltaTime, 0, 0);
        }
    }

    public IEnumerator DoBeat()
    {
        if (hasStarted == false)
        {
            hasStarted = true;

            caisseObject = Instantiate(caissePrefabs, spawnPoint);


            //Camera.main.transform.parent = caisseObject.transform;
            /*Camera.main.transform.position =
                new Vector3 (
                    caisseObject.transform.position.x + Camera.main.GetComponent<CameraSwitch>().offset,
                    caisseObject.transform.position.y + 0.78f,
                    Camera.main.transform.position.z
                    );*/
        }
        yield return new WaitForSecondsRealtime(1f);
        beat++;
        if (beat >= 4 && caisseObject.GetComponent<Node>().nbBras >= 3)
        {
            beat = 0;
            if (caisseObject.GetComponent<Node>().sucess)
            {
                //REUSSITE
                
                caisseObject.GetComponent<Node>().sucess = false;
                if(caisseObject.GetComponent<Node>().sequence >= 4)
                {
                    gameManager.SetInputToGet(0);
                    gameManager.SetInputToShow(0);
                    Destroy(caisseObject);
                    caisseObject = Instantiate(caissePrefabs, spawnPoints[0]);
                }
                gameManager.cameraSwitch.AddCameraState();
                gameManager.cameraSwitch.DoCameraMoves();
            }
            else if (!caisseObject.GetComponent<Node>().sucess)
            {
                //FAIl
                print(caisseObject.GetComponent<Node>().sequence);
                int seq = caisseObject.GetComponent<Node>().sequence;
                Destroy(caisseObject);
                gameManager.AddInputToShow(-3);
                gameManager.AddInputToGet(-3);
                caisseObject = Instantiate(caissePrefabs, spawnPoints[caisseObject.GetComponent<Node>().sequence]);
                caisseObject.GetComponent<Node>().sequence = seq;
                caisseObject.GetComponent<Node>().sequencing();
                print("failed");
                //Camera.main.transform.parent = null;
                gameManager.InputToGet = 0;
                //gameManager.cameraSwitch.DoCameraMoves();
                //yield return new WaitForSeconds(gameManager.cameraSwitch.duration;
                //Camera.main.transform.position = 
                /*new Vector3 (
                caisseObject.transform.position.x + Camera.main.GetComponent<CameraSwitch>().offset,
                caisseObject.transform.position.y + .78f,
                Camera.main.transform.position.z
                );*/

            }
        }

         
        StartCoroutine(EndBeat());
    }
    public IEnumerator EndBeat()
    {
        yield return new WaitForSecondsRealtime(.002f);
        StartCoroutine(DoBeat());
    }

}
