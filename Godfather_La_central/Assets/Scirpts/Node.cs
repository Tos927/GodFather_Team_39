using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using static Blackboard;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class Node : MonoBehaviour
{
    GameManager gameManager; 


    public List<KeyCode> pressedkeycode = new List<KeyCode>();
    public bool IsKeyRight = false;

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
        if (!Input.anyKey) { pressedkeycode.Clear(); pressedkeycode = new List<KeyCode>(gameManager.actionList[gameManager.InputToGet].inputs.Count); }

        if (Input.GetKeyDown(KeyCode.J) && !pressedkeycode.Contains(KeyCode.J)) { pressedkeycode.Add(KeyCode.J); }
        if (Input.GetKeyDown(KeyCode.V) && !pressedkeycode.Contains(KeyCode.V)) { pressedkeycode.Add(KeyCode.V); }
        if (Input.GetKeyDown(KeyCode.L) && !pressedkeycode.Contains(KeyCode.L)) { pressedkeycode.Add(KeyCode.L); }
        if (Input.GetKeyDown(KeyCode.Alpha6) && !pressedkeycode.Contains(KeyCode.Alpha6)) { pressedkeycode.Add(KeyCode.Alpha6); }
        if (Input.GetKeyDown(KeyCode.Minus) && !pressedkeycode.Contains(KeyCode.Minus)) { pressedkeycode.Add(KeyCode.Minus); }

        if (Input.GetKeyDown(KeyCode.Q) && !pressedkeycode.Contains(KeyCode.Q)) { pressedkeycode.Add(KeyCode.Q); }
        if (Input.GetKeyDown(KeyCode.R) && !pressedkeycode.Contains(KeyCode.R)) { pressedkeycode.Add(KeyCode.R); }
        if (Input.GetKeyDown(KeyCode.G) && !pressedkeycode.Contains(KeyCode.G)) { pressedkeycode.Add(KeyCode.G); }
        if (Input.GetKeyDown(KeyCode.S) && !pressedkeycode.Contains(KeyCode.S)) { pressedkeycode.Add(KeyCode.S); }
        if (Input.GetKeyDown(KeyCode.F) && !pressedkeycode.Contains(KeyCode.F)) { pressedkeycode.Add(KeyCode.F); }
        if (Input.GetKeyDown(KeyCode.D) && !pressedkeycode.Contains(KeyCode.D)) { pressedkeycode.Add(KeyCode.D); }


        if (canBePressed && !gotPressed && AreInputsExacts() )
        {
            gameManager.nodeStart(nbBras);
            nbcaisse++;
            gotPressed = true;
            if (canPerfect())
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

    }

    public bool AreInputsExacts()
    {
        if (!Input.anyKey) { return false; }
        print("AAAAAAAAAAAAAAAAAAAAAAAAAAAA  " + gameManager.actionList[gameManager.InputToGet].inputs.Count);

        if(gameManager.actionList[gameManager.InputToGet].inputs.Count == 2)
        {

            KeyCode k = pressedkeycode[0];
            KeyCode k2 = pressedkeycode[1];

            LALIST K = GetCorrespondingLALIST(k);
            LALIST K2 = GetCorrespondingLALIST(k2);

            LALIST I = gameManager.actionList[gameManager.InputToGet].inputs[0];
            LALIST I2 = gameManager.actionList[gameManager.InputToGet].inputs[1];

            pressedkeycode.Add(KeyCode.None);
            pressedkeycode.Add(KeyCode.None);


            if (I == K && I2 == K2)
            {

                return true;
            }
            else if (I2 == K && I == K2)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            pressedkeycode.Add(KeyCode.None);
            pressedkeycode.Add(KeyCode.None);
            pressedkeycode.Add(KeyCode.None);



            KeyCode k = pressedkeycode[0];
            KeyCode k2 = pressedkeycode[1];
            KeyCode k3 = pressedkeycode[2];


            LALIST K = GetCorrespondingLALIST(k);
            LALIST K2 = GetCorrespondingLALIST(k2);
            LALIST K3 = GetCorrespondingLALIST(k3);


            LALIST I = gameManager.actionList[gameManager.InputToGet].inputs[0];
            LALIST I2 = gameManager.actionList[gameManager.InputToGet].inputs[1];
            LALIST I3 = gameManager.actionList[gameManager.InputToGet].inputs[2];

            if (I == K && I2 == K2 && I3 == K3)
            {
                return true;
            }
            else if (I3 == K && I == K2 && I2 == K3)
            {
                return true;
            }
            else if (I2 == K && I3 == K2 && I == K3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

/*        foreach(bool b in allgood)
        {
            print(b);
            if (b == false)
            {
                print("NOOOOOOOOOOO");
                return false; 
            }
        }
        print("Good");
        return true;*/
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
