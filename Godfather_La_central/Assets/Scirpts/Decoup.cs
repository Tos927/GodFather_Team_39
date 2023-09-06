using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Decoup : MonoBehaviour
{
    public CameraSwitch CameraMan;

    public float TimeToSwap = 1f;
    


    Coroutine lastCoroutine;
    bool stopCoroutines = false;

    bool _isLeft;
    bool _ispining;

    float MouseWheel;


    #region Things in screen

    public GameObject LeftArrow;
    public GameObject RightArrow;

    public GameObject saw;

    #endregion
    public void Startmodule()
    {
        //RESET
        saw.transform.eulerAngles = Vector3.zero;
        _ispining= false;
        stopCoroutines = false;

        ///Rand Int to decide L || R
        int LeftRight = Random.Range(0, 2);

        print(LeftRight);

        if (LeftRight == 0) { _isLeft = true; }
        if (LeftRight == 1) { _isLeft = false;}

        if(_isLeft)  lastCoroutine = StartCoroutine(ArrowOn(LeftArrow));
        if(!_isLeft) lastCoroutine = StartCoroutine(ArrowOn(RightArrow));
    }

    public void Start()
    { 
    }


    //RIGHT IS UP ON MOUSE WHEEL
    private void Update()
    {
        bool turningLeft = false;
        bool isGood;


        MouseWheel = Input.GetAxis("Mouse ScrollWheel");
        
        
        //print(MouseWheel);
        if ((int)CameraMan.cameraState == 1)
        {
            if(MouseWheel != 0)
            {
                if (MouseWheel > 0)
                {
                    turningLeft = false;
                    saw.transform.eulerAngles += new Vector3(0, 0, MouseWheel);
                }
                if (MouseWheel < 0) 
                { 
                    turningLeft = true;
                    saw.transform.eulerAngles += new Vector3(0, 0, MouseWheel);

                }
                isGood = (turningLeft, _isLeft) switch
                {
                    (true , false) => true,
                    (false, true ) => true,
                    (true , true ) => false,
                    (false, false) => false
                };
                if(!isGood) { print("Game Over"); }
                else /* Sucess */
                {
                    StartCoroutine(SawRotateSucess());
                }
            }

            if (_ispining)
            {
                if (_isLeft)
                {
                    saw.transform.eulerAngles += new Vector3(0, 0, 2);
                }
                else
                {
                    saw.transform.eulerAngles -= new Vector3(0, 0, 2);
                }
            }

        }
    }


    IEnumerator SawRotateSucess()
    {
        yield return new WaitForSeconds(.5f);
        _ispining = true;
        stopCoroutines = true;
    }


    //Clignotant
    IEnumerator ArrowOn(GameObject Arrow)
    {


        //print("Working TWICE");
        Arrow.SetActive(false);
        yield return new WaitForSeconds(TimeToSwap);
        StopCoroutine(lastCoroutine);
        lastCoroutine = StartCoroutine(ArrowOff(Arrow));
    }

    IEnumerator ArrowOff(GameObject Arrow)
    {

        //print("Working");
        Arrow.SetActive(true);
        yield return new WaitForSeconds(TimeToSwap);
        if (stopCoroutines)
        {
            StopCoroutine(lastCoroutine);
            stopCoroutines = false;
            yield return null;
        }
        StopCoroutine(lastCoroutine);
        lastCoroutine = StartCoroutine(ArrowOn(Arrow));
    }
}
