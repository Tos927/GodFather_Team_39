using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource[] audioGen;
    public AudioSource audioTube;
    public AudioClip[] clips;
    public Animator tubeAnim;
    public Animator ScieAnim;
    public Animator marteauAnim;
    public Animator perceuseGaucheAnim;
    public Animator perceusedroiteAnim;
    public Animator perceusedroitedroiteAnim;
    public Animator simAnim;
    public Animator flamboAnim;
    public Animator mainAnim;
    public Animator mainAnim2;
    public Animator scotchAnim;
    public Animator oiseauAnim;
    public int nbclip = 0;
    public bool startMusic = false;
    public static GameManager instance;
    public bool pass = false;
    public scroller scoller;
    public Steps steps;
    public CameraSwitch cameraSwitch;

    public int sequence = 0;
    public int Cycles = 0;
    public bool IsCyclesPerfect = true;
    public int PerfectCombo = 0;


    public int ComboStart = 2; //6
    public bool IsInCombo = false;

    public int ComboFolieStart = 4; //24 // 2 Cycles
    public bool IsInComboFolie = false;


    public int ComboCredits = 6; //48 3*4*4 // 4 cycles 
    public bool IsInComboCredits = false;

    public List<Blackboard> Blackboards;
    public List<Blackboard.Action> actionList = new List<Blackboard.Action>();
    public int InputToGet = 0;

    public int AddInputToGet(int i)
    {
        InputToGet += i;
        foreach(Blackboard b in Blackboards)
        {
            b.DecodeInputs(actionList[InputToGet].inputs);
        }
        return InputToGet;
    }
    public int SetInputToGet(int i)
    {
        foreach (Blackboard b in Blackboards)
        {
            b.DecodeInputs(actionList[i].inputs);
        }
        return InputToGet = i;
    }

    private void Start()
    {
        Blackboards = FindObjectsOfType<Blackboard>().ToList();
        actionList = Blackboards[0].actionList;
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
        if (sequence == 0)
        {
            if (x == 0)
                tubeAnim.SetBool("tub", true);
            else if (x == 1)
                marteauAnim.SetBool("bool", true);
            else if (x >= 2)
                ScieAnim.SetBool("scie", true);
        }
        else if (sequence == 1)
        {
            if (x == 0)
                perceuseGaucheAnim.SetBool("bool", true);
            else if (x == 1)
                perceusedroiteAnim.SetBool("bool", true);
            else if (x >= 2)
                perceusedroiteAnim.SetBool("droite", true);
        }
        else if (sequence == 2)
        {
            if (x == 0)
                simAnim.SetBool("bool", true);
            else if (x == 1)
                flamboAnim.SetBool("bool", true);
            else if (x >= 2)
                mainAnim.SetBool("bool", true);

        }
        else if (sequence == 3)
        {
              if (x == 0)
                mainAnim2.SetBool("bool", true);
            else if (x == 1)
                scotchAnim.SetBool("bool", true);
            else if (x >= 2)
                oiseauAnim.SetBool("bool", true);
        }
        
            StartCoroutine(resetAnim(x));
    }
    IEnumerator resetAnim(int x)
    {
        yield return new WaitForSeconds(0.8f);

        if (sequence == 0)
        {
            if (x == 0)
                tubeAnim.SetBool("tub", false);
            else if (x == 1)
                marteauAnim.SetBool("bool", false);
            else if (x >= 2)
                ScieAnim.SetBool("scie", false);
        }
        else if (sequence == 1)
        {
            if (x == 0)
                perceuseGaucheAnim.SetBool("bool", false);
            else if (x == 1)
                perceusedroiteAnim.SetBool("bool", false);
            else if (x >= 2)
                perceusedroiteAnim.SetBool("droite", false);
        }
        else if (sequence == 2)
        {
            if (x == 0)
                simAnim.SetBool("bool", false);
            else if (x == 1)
                flamboAnim.SetBool("bool", false);
            else if (x >= 2)
                mainAnim.SetBool("bool", false);

        }
        else if (sequence == 3)
        {
            if (x == 0)
                mainAnim2.SetBool("bool", false);
            else if (x == 1)
                scotchAnim.SetBool("bool", false);
            else if (x >= 2)
                oiseauAnim.SetBool("bool", false);
        }

        yield return null;
    }
    

}
