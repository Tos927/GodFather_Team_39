using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    [SerializeField] GameObject cube;
    bool isWin = false;
    bool isLose = false;
    public KeyCode key;
    
    void Start()
    {
        if (cube == null)
            Debug.Log("Pas de cube.");
        cube.tag = "first";
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && isWin == false && isLose == false)
            allsteps();
    }


    public void allsteps()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKeyDown(KeyCode.LeftShift) && cube.tag == "first")
        {
            cube.transform.Rotate(cube.transform.position, 90);
            cube.tag = "second";
        }
        else if(Input.GetKeyDown(KeyCode.LeftShift) && cube.tag == "second")
        {
            cube.transform.localScale = cube.transform.localScale / 2;
            cube.tag = "third";
        }
        else if (Input.GetKeyDown(KeyCode.RightShift) && cube.tag == "third")
        {
            cube.transform.Translate(new Vector3(0, 2, 0));
            GetComponent<timer>().win = true;
            isWin = true;

        }
        else
        {
            //lose
            GetComponent<timer>().lose = true;
            isLose = true;
            Debug.Log("Lose");
        }
    }



}
