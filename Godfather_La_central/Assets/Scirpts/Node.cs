using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    GameManager gameManager; 


    public KeyCode keycode;
    public bool canBePressed = false;
    public bool gotPressed = false;
    public int nbcaisse = 0;
    public int nbBras = 0;
    public bool sucess = false;


    void Start()
    {
        gameManager = GameManager.instance;
        nbBras = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keycode))
        {
            if(canBePressed)
            {
                GameManager.instance.nodeStart(nbBras);
                nbcaisse++;
                gotPressed = true;
                GameManager.instance.NodeHitPerfect();
                if (nbcaisse == 3)
                {
                    sucess = true;
                    nbBras = 0;
                    //gameManager.cameraSwitch.CameraState += 1;
                    //gameManager.cameraSwitch.DoCameraMoves();
                }
                
            }
            
        }


    }


   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "zone")
            canBePressed = true;

        if (collision.gameObject.tag == "stop" && !gotPressed)
        {
            GameManager.instance.nodeStop();
        }
        //hit


        //Debug.Log("touch�");
    }
    private void OnCollisionExit(Collision collision)
    {
        //out
        //Debug.Log("out");
        if (collision.gameObject.tag == "zone")
        {
            canBePressed = false;
            if(!gotPressed)
            {
                //GameManager.instance.NodeFailed();
            }
            nbBras++;
            gotPressed = false;
        }
    }
}
