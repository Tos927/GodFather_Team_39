using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource audio;
    public bool startMusic = false;
    public static GameManager instance;
    public scroller scoller;

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
            audio.Play();
        }
    }

    public void NodeHit()
    {
        scoller.hashit();
        Debug.Log("hit");
    }

    public void NodeFailed()
    {
        scoller.failedHit();
        Debug.Log("miss");
    }
}
