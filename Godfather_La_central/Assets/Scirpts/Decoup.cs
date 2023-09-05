using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoup : MonoBehaviour
{
    public float TimeToSwap = 1f;
    
    public GameObject LeftArrow;
    public GameObject RightArrow;

    Coroutine lasctCoroutine;

    bool _isLeft;

    public void Startmodule()
    {
        ///Rand Int to decide L || R
        int LeftRight = Random.Range(0, 2);

        print(LeftRight);

        if (LeftRight == 0) { _isLeft = true; }
        if (LeftRight == 1) { _isLeft = false;}

        if(_isLeft) lasctCoroutine = StartCoroutine(ArrowOn(LeftArrow));
    }

    public void Start()
    {
        Startmodule();
    }

    IEnumerator ArrowOn(GameObject Arrow)
    {
        Arrow.SetActive(false);
        yield return new WaitForSeconds(TimeToSwap);
        
    }

    IEnumerator ArrowOff(GameObject Arrow)
    {
        Arrow.SetActive(true);
        yield return new WaitForSeconds(TimeToSwap);
    }
}
