using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Node : MonoBehaviour
{
    GameManager gameManager; 


    public KeyCode keycode;
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    public bool canBePressed = false;
    public bool gotPressed = false;
    public int nbcaisse = 0;
    public int nbBras = 0;
    public int sequence = 0;
    public bool sucess = false;
    public bool perfect = false;


    public BoxCollider boxCollider;

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
                    nbcaisse = 0;
                    GameManager.instance.sequence++;
                    sequence = GameManager.instance.sequence++;
                    //gameManager.cameraSwitch.CameraState += 1;
                    //gameManager.cameraSwitch.DoCameraMoves();
                }

            }
            if(sequence == 1)
            {
                this.GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
                this.GetComponentsInChildren<SpriteRenderer>()[4].enabled = true;
            }
            else if(sequence == 2 && nbcaisse == 1) 
            {
                this.GetComponentsInChildren<SpriteRenderer>()[4].enabled = false;
                this.GetComponentsInChildren<SpriteRenderer>()[3].enabled = true;
            }
            else if (sequence == 3)
            {
                this.GetComponentsInChildren<SpriteRenderer>()[3].enabled = false;
                this.GetComponentsInChildren<SpriteRenderer>()[1].enabled = true;
            }
            else if (sequence == 4 && nbcaisse == 2)
            {
                this.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
                this.GetComponentsInChildren<SpriteRenderer>()[2].enabled = true;
            }
        }
            
    }


    


   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "zone")
        {
            canBePressed = true;
        }
        else if (collision.gameObject.tag == "stop")
        {
            nbBras = 0;
        }
            
    }
    private void OnCollisionExit(Collision collision)
    {
        //out
        //Debug.Log("out");
        if (collision.gameObject.tag == "zone")
        {
                print(collision.gameObject.name);
                canBePressed = false;
                nbBras++;
                gotPressed = false;
            
        }
    }
}
