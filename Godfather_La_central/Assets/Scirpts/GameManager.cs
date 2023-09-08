using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource[] audioGen;
    public AudioSource audioTube;
    public AudioClip[] clips;
    public Animator tubeAnim;
    public Animator ScieAnim;
    public int nbclip = 0;
    public bool startMusic = false;
    public static GameManager instance;
    public bool pass = false;
    public scroller scoller;
    public Steps steps;
    public CameraSwitch cameraSwitch;


    public int Cycles = 0;
    public bool IsCyclesPerfect = true;
    public int PerfectCombo = 0;


    public int ComboStart = 2; //6
    public bool IsInCombo = false;

    public int ComboFolieStart = 4; //24 // 2 Cycles
    public bool IsInComboFolie = false;


    public int ComboCredits = 6; //48 3*4*4 // 4 cycles 
    public bool IsInComboCredits = false;

    public Blackboard Blackboard;
    public List<Blackboard.Action> actionList = new List<Blackboard.Action>();
    public int InputToGet = 0;

    public int AddInputToGet()
    {
        return InputToGet++;
    } 

    private void Start()
    {
        Blackboard = FindObjectOfType<Blackboard>();
        actionList = Blackboard.actionList;
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(!startMusic && Input.anyKeyDown)
        {
            Starter();
        }
    }
    public void Starter()
    {
        startMusic = true;
        StartCoroutine(scoller.DoBeat());
        foreach (AudioSource audiosources in audioGen)
        {
            audiosources.Play();
            audiosources.volume = 0;
        }
        audioGen[0].volume = 100;
    }
    public void NodeHit()
    {
        /*audioGen.clip = clips[0];
        nbclip = 0;
        audioGen.Play();*/

        //looseCombo
        PerfectCombo = 0;
        foreach (AudioSource audiosources in audioGen)
        {
            audiosources.volume = 0;
        }
        StartCoroutine(cameraSwitch.ZoomInAndOut());
        if (nbclip < 4)
        {
            audioGen[nbclip + 1].volume = 100;

        }
        else
            audioGen[nbclip].volume = 100;

        //Debug.Log("hit");
    }
    public void NodeHitPerfect()
    {

        //COMBO
        PerfectCombo++;
        if(PerfectCombo == ComboStart)
        {
            IsInCombo = true;
        }
        if (PerfectCombo == ComboFolieStart)
        {
            IsInComboFolie = true;
        }
        if (PerfectCombo == ComboCredits)
        {
            IsInComboCredits = true;
        }



        foreach (AudioSource audiosources in audioGen)
        {
            audiosources.volume = 0;
        }
        StartCoroutine(cameraSwitch.ZoomInAndOutPIXEL());

        if (nbclip < 4)
        {
            audioGen[nbclip + 1 ].volume = 100;

        }
        else
            audioGen[nbclip].volume = 100;

    }
    public void nodeStop()
    {
        scoller.stop = true;
    }
    public void nodeStart(int x)
    {
        if(x == 0)
            tubeAnim.SetBool("tub", true);
        else if(x == 1)
            ScieAnim.SetBool("tub", true);
        else if(x >= 2)
            ScieAnim.SetBool("scie", true);

        StartCoroutine(resetAnim(x));
    }
    IEnumerator resetAnim(int x)
    {
        yield return new WaitForSeconds(1f);
        if (x == 0)
            tubeAnim.SetBool("tub", false);
        /*else if (x == 1)
            ScieAnim.SetBool("scie", false);*/
        else if (x >= 2)
            ScieAnim.SetBool("scie", false);
        yield return null;
    }
    

}
