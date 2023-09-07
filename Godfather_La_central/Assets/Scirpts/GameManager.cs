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

    private void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(!startMusic && Input.anyKeyDown)
        {
            startMusic = true;
            scoller.hasStarted = true;
            foreach(AudioSource audiosources in audioGen)
            {
                audiosources.Play();
                audiosources.volume = 0;
            }
            audioGen[0].volume = 100;



        }
    }
    
    public void NodeHit()
    {
        /*audioGen.clip = clips[0];
        nbclip = 0;
        audioGen.Play();*/
        foreach (AudioSource audiosources in audioGen)
        {
            audiosources.volume = 0;
        }

        if (nbclip < 4)
        {
            audioGen[nbclip + 1].volume = 100;

        }
        else
            audioGen[nbclip].volume = 100;

        StartCoroutine(scoller.hashit());
        Debug.Log("hit");
    }
    public void NodeHitPerfect()
    {
        foreach (AudioSource audiosources in audioGen)
        {
            audiosources.volume = 0;
        }

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
            ScieAnim.SetBool("scie", true);
        else if(x < 2)
            tubeAnim.SetBool("tub", true);

        StartCoroutine(resetAnim());
    }
    IEnumerator resetAnim()
    {
        yield return new WaitForSeconds(1f);
        tubeAnim.SetBool("tub", false);
        ScieAnim.SetBool("scie", false);
        yield return null;
    }
    
    public void NodeFailed()
    {
        scoller.failedHit();
        Debug.Log("miss");
    }
}
