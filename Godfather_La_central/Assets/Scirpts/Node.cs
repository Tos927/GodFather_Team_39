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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keycode))
        {
            if(canBePressed)
            {
                nbcaisse++;
                gotPressed = true;
                GameManager.instance.nodeStart();
                if (nbcaisse == 3)
                {
                    sucess = true;
                    gameManager.cameraSwitch.CameraState += 1;
                    gameManager.cameraSwitch.DoCameraMoves();
                }
                
            }
            
        }

        if (nbBras == 3 && nbcaisse != 3) 
        {
            
            GameManager.instance.NodeHit();
            gameObject.SetActive(false);

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
