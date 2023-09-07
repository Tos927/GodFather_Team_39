using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    public List<GameObject> Sprites;

    private Coroutine lastCoroutine;
    public float timeToSwap = .1f;

    public List<Action> actionList = new List<Action>();

    [System.Serializable]
    public struct Action
    {
        public List<LALIST> inputs;
    }

    [System.Serializable]
    public enum LALIST
    {
        VolantHaut,
        VolantBas,
        LevierGHaut,
        LevierGBas,
        LevierDHaut,
        LevierDBas,
        Bouton1,
        Bouton2,
        Bouton3,
        Bouton4,
        Bouton5,
        Bouton6
    }

    void DecodeInputs(List<LALIST> Actions)
    {
        foreach(LALIST I in Actions)
        {
            if((int)I <= 1) Sprites[12].SetActive(true);
            else if((int)I <= 3) Sprites[13].SetActive(true);
            else if((int)I <= 5) Sprites[14].SetActive(true);

            switch(I)
            {
                /*case 0: Sprites[0].SetActive(true); break;
                case 1: Sprites[1].SetActive(true); break;
                case 2: Sprites[0].SetActive(true); break;
                case 3: Sprites[1].SetActive(true); break;
                case 4: Sprites[0].SetActive(true); break;
                case 5: Sprites[1].SetActive(true); break;
                case 6: Sprites[1].SetActive(true); break;
                case 7: Sprites[0].SetActive(true); break;
                case 8: Sprites[1].SetActive(true); break;
                case 9: Sprites[0].SetActive(true); break;
                case 10: Sprites[1].SetActive(true); break;*/
            }

        }
    }

    void Start()
    {
        GameObject a = null;
        StartCoroutine(ArrowOn(a));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ArrowOn(GameObject Arrow)
    {
        Arrow.SetActive(true);
        yield return new WaitForSeconds(timeToSwap);
        StopCoroutine(lastCoroutine);
        //lastCoroutine = StartCoroutine(ArrowOff(Arrow));
    }

    IEnumerator ArrowOff(GameObject Arrow)
    {
        Arrow.SetActive(false);
        yield return new WaitForSeconds(timeToSwap);
        StopCoroutine(lastCoroutine);
        //lastCoroutine = StartCoroutine(ArrowOn(Arrow));
    }
}
