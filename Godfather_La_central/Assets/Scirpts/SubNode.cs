using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubNode : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
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
            gameManager.AddInputToGet();
            gameManager.Blackboard.DecodeInputs(gameManager.actionList[gameManager.InputToGet].inputs);

            isPerfect = false;

        }
    }

}
