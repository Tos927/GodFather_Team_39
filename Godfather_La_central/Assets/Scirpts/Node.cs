using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    GameManager gameManager; 


    public KeyCode keycode;
    public bool canBePressed;
    public bool gotPressed;
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
                if (nbcaisse == 3)
                {
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
        //hit


        Debug.Log("touché");
    }
    private void OnCollisionExit(Collision collision)
    {
        //out
        Debug.Log("out");
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
