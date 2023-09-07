using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource audio;
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
            //Starter();
        }
    }
    public void Starter()
    {
        startMusic = true;
        scoller.hasStarted = true;
        audio.Play();
    }
    public void NodeHit()
    {
        StartCoroutine(scoller.hashit());
        //Debug.Log("hit");
    }
    public void nodeStop()
    {
        scoller.stop = true;
    }
    public void nodeStart()
    {
        scoller.stop = false;
    }
    public void NodeFailed()
    {
        scoller.failedHit();
        Debug.Log("miss");
    }
}
