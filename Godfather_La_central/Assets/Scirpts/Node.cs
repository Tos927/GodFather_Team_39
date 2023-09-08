using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEditorInternal;
using UnityEngine;
using static Blackboard;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class Node : MonoBehaviour
{
    GameManager gameManager;

    public string inputs;

    public List<KeyCode> pressedkeycode = new List<KeyCode>();
    public bool IsKeyRight = false;

    public KeyCode keycode;
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    public bool canBePressed = false;
    public bool gotPressed = false;
    public int nbcaisse = 0; // zone pass� r�ussi
    public int nbBras = 0; // zone pass� meme si non r�ussi
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
    LALIST GetCorrespondingLALIST(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.J:
                return LALIST.LevierGHaut;
            case KeyCode.V:
                return LALIST.LevierGBas;
            case KeyCode.L:
                return LALIST.LevierDHaut;
            case KeyCode.Alpha6:
                return LALIST.LevierDBas;//DEPENDANCE CLAVIER FAIRE GAFFE
            case KeyCode.Minus:
                return LALIST.LevierDBas;
            case KeyCode.Q:
                return LALIST.Bouton1;
            case KeyCode.R:
                return LALIST.Bouton2;
            case KeyCode.G:
                return LALIST.Bouton3;
            case KeyCode.S:
                return LALIST.Bouton4;
            case KeyCode.F:
                return LALIST.Bouton5;
            case KeyCode.D:
                return LALIST.Bouton6;
            default:
                return LALIST.nulll;
        }
    }
    void Update()
    {
        inputs = "";

        float mouseDeltaX = Input.GetAxis("Mouse X");
        //print(mouseDeltaX);
        if(mouseDeltaX > 0) { inputs += "VHD"; }
        if (mouseDeltaX < 0) { inputs += "VBG"; }

        if (Input.GetKey(KeyCode.J)) { inputs += "LGH"; }
        if (Input.GetKey(KeyCode.V)) { inputs += "LGB"; }
        if (Input.GetKey(KeyCode.L)) { inputs += "LDH"; }
        

        if (Input.GetKey(KeyCode.Alpha6)) { inputs += "LDB"; }
        if (Input.GetKey(KeyCode.Minus)) { inputs += "LDB"; }

        if (Input.GetKey(KeyCode.Q)) { inputs += "B01"; }
        if (Input.GetKey(KeyCode.A)) { inputs += "B01"; }

        if (Input.GetKey(KeyCode.R)) { inputs += "B02"; }
        if (Input.GetKey(KeyCode.G)) { inputs += "B03"; }
        if (Input.GetKey(KeyCode.S)) { inputs += "B04"; }
        if (Input.GetKey(KeyCode.F)) { inputs += "B05"; }
        if (Input.GetKey(KeyCode.D)) { inputs += "B06"; }

        //print(inputs);


        if (canBePressed && !gotPressed)
        {
            if (AreInputsExacts())
            {
                gameManager.nodeStart(nbBras);
                nbcaisse++;
                gotPressed = true;
                if (canPerfect())
                {
                    gameManager.nodeStart(nbBras);
                    nbcaisse++;
                    gotPressed = true;
                    if (canPerfect())
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
                        sequence = GameManager.instance.sequence;
                        gameManager.cameraSwitch.CameraState += 1;
                        gameManager.cameraSwitch.DoCameraMoves();
                    }
                    else
                    {
                        gameManager.NodeHit();
                    }
                    if (nbcaisse == 3)
                    {
                        sucess = true;
                        nbBras = 0;
                        //gameManager.cameraSwitch.CameraState += 1;
                        //gameManager.cameraSwitch.DoCameraMoves();
                    }
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
                    sequence = GameManager.instance.sequence;
                    /*gameManager.cameraSwitch.CameraState += 1;
                    gameManager.cameraSwitch.DoCameraMoves();*/
                }

            this.GetComponentsInChildren<SpriteRenderer>()[4].enabled = true;
        }


            print("sequence " + sequence);
            sequencing();
        }
            
    }

    public void sequencing()
    {
        if (sequence == 1)
        {
            this.GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[2].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[4].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[3].enabled = false;

            this.GetComponentsInChildren<SpriteRenderer>()[4].enabled = true;
        }
        else if (sequence == 2)
        {
            this.GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[2].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[4].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[3].enabled = false;

            this.GetComponentsInChildren<SpriteRenderer>()[3].enabled = true;
        }
        else if (sequence == 3)
        {
            this.GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[2].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[4].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[3].enabled = false;

            this.GetComponentsInChildren<SpriteRenderer>()[2].enabled = true;
        }
        else if (sequence == 4)
        {
            this.GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[2].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[4].enabled = false;
            this.GetComponentsInChildren<SpriteRenderer>()[3].enabled = false;

            this.GetComponentsInChildren<SpriteRenderer>()[1].enabled = true;
        }
    }

    public bool AreInputsExacts()
    {
        print(inputs + " // " + gameManager.actionList[gameManager.InputToGet].Input);
        string str1 = "";
        string str2 = "";
        string str3 = "";

        int count = 0;
        foreach (char c in inputs)
        {
            if (count < 3)
            {
                str1 += c;
            }else if (count < 6)
            {
                str2 += c;
            }
            else
            {
                str3 += c;
            }
            count++;
        }

        string act1 = "";
        string act2 = "";
        string act3 = "";

        int count2 = 0;
        foreach (char c in gameManager.actionList[gameManager.InputToGet].Input)
        {
            if (count2 < 3)
            {
                act1 += c;
            }
            else if (count2 < 6)
            {
                act2 += c;
            }
            else
            {
                act3 += c;
            }
            count2++;
        }

        if (str1 == act1 && str2 == act2 && str3 == act3)
        {
            return true;
        }
        else if (str3 == act1 && str1 == act2 && str2 == act3)
        {
            return true;
        }
        else if (str2 == act1 && str3 == act2 && str1 == act3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnCollisionEnter(Collision collision)
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
    void OnCollisionExit(Collision collision)
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
