using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROF : MonoBehaviour
{
    public float Force = 3;
    public float timer = 3;

    RectTransform rectTransform;



    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        rectTransform.position += new Vector3(0,Mathf.Sin(Time.deltaTime* timer )/ Force,0);
    }
}
