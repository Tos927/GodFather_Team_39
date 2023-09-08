using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
    public bool perfect = false;


    public BoxCollider boxCollider;
    public BoxCollider boxColliderPixel;

    public SubNode subNode;

    public bool canPerfect()
    {
        return subNode.isPerfect;
    }
    
    void Start()
    {
        gameManager = GameManager.instance;
        boxCollider = GetComponent<BoxCollider>();
        subNode = GetComponentInChildren<SubNode>();
        nbBras = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keycode))
        {
            if(canBePressed)
            {
                gameManager.nodeStart(nbBras);
                nbcaisse++;
                gotPressed = true;
                if(canPerfect()) 
                {
                    print("YA/GO/IT");
                    gameManager.NodeHitPerfect();
                }
                else
                {
                    gameManager.NodeHit();
                }
                if (nbcaisse == 3)
                {
                    sucess = true;
                    nbBras = 0;
                    GameManager.instance.sequence++;
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
