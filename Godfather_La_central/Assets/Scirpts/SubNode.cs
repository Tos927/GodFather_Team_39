using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubNode : MonoBehaviour
{

    public bool isPerfect = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "pixel")
        {
            isPerfect = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "pixel")
        {
            isPerfect = false;
        }
    }

}
