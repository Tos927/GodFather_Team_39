using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    public List<GameObject> Sprites;
    private List<GameObject> SpritesToLit = new List<GameObject>();

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
        if(lastCoroutine != null) StopCoroutine(lastCoroutine);

        SpritesToLit.Clear();
        foreach (GameObject g in Sprites) { g.SetActive(false); }
        foreach(LALIST I in Actions)
        {
            if((int)I <= 1) SpritesToLit.Add(Sprites[12]);
            else if((int)I <= 3) SpritesToLit.Add(Sprites[13]);
            else if((int)I <= 5) SpritesToLit.Add(Sprites[14]);

            switch (I)
            {
                case (LALIST)0: SpritesToLit.Add(Sprites[0]); break;
                case (LALIST)1: SpritesToLit.Add(Sprites[1]); break;
                case (LALIST)2: SpritesToLit.Add(Sprites[2]); break;
                case (LALIST)3: SpritesToLit.Add(Sprites[3]); break;
                case (LALIST)4: SpritesToLit.Add(Sprites[4]); break;
                case (LALIST)5: SpritesToLit.Add(Sprites[5]); break;
                case (LALIST)6: SpritesToLit.Add(Sprites[6]); break;
                case (LALIST)7: SpritesToLit.Add(Sprites[7]); break;
                case (LALIST)8: SpritesToLit.Add(Sprites[8]); break;
                case (LALIST)9: SpritesToLit.Add(Sprites[9]); break;
                case (LALIST)10: SpritesToLit.Add(Sprites[10]); break;
                case (LALIST)11: SpritesToLit.Add(Sprites[11]); break;

            }
            lastCoroutine = StartCoroutine(ArrowOn());
        }
    }

    void Start()
    {
        //DecodeInputs(actionList[0].inputs);
    }

    // Update is called once per frame
    void OnValidate()
    {
        DecodeInputs(actionList[0].inputs);
    }

    IEnumerator ArrowOn()
    {
        foreach(GameObject go in SpritesToLit) { go.SetActive(true); }
        yield return new WaitForSeconds(timeToSwap);
        StopCoroutine(lastCoroutine);
        lastCoroutine = StartCoroutine(ArrowOff());
    }

    IEnumerator ArrowOff()
    {
        foreach (GameObject go in SpritesToLit) { go.SetActive(false); }
        yield return new WaitForSeconds(timeToSwap);
        StopCoroutine(lastCoroutine);
        lastCoroutine = StartCoroutine(ArrowOn());
    }
}
