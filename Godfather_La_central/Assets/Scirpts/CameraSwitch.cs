using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class CameraSwitch : MonoBehaviour
{
    /*    public Image cameraFade;*/

    private AudioSource audioSource;
    public float TimeToChange = .2f;

    public List<Transform> cameraPoses = new List<Transform>();
    public AnimationCurve PosOverTime;
    public float duration = .5f;
    private Vector3 OldPosition;
    private bool _doCameraMovs = false;


/*    private float _alpha = 1;
    private Texture2D _texture;
    private bool Fadein = false;
    private bool Fadeout = false;*/

    private float _time;

    public CameraState cameraState = CameraState.Camera1;

    public Decoup decoup;


    public enum CameraState
    {
        Camera1 = 0,
        Camera2 = 1,
        Camera3 = 2,
        Camera4 = 3,
    }

    private void Start()
    {
        decoup.Startmodule();
        _time = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            cameraState = CameraState.Camera1;
            DoCameraMoves();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameraState = CameraState.Camera2;
            DoCameraMoves();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cameraState = CameraState.Camera3;
            DoCameraMoves();
        }

    }
    void DoCameraMoves()
    {
        StartCoroutine(GoTo(cameraPoses[(int)cameraState].position));
    }
    private IEnumerator GoTo(Vector3 endPosition)
    {
        float elapsed = 0;

        Vector3 startPosition = transform.position;

        while (elapsed <= duration)
        {
            elapsed += Time.deltaTime;
            float t = PosOverTime.Evaluate(elapsed / duration);
            transform.position = Vector3.LerpUnclamped(startPosition, endPosition, t);
            //float distance = Vector3.Distance(transform.position, endPosition);
            yield return null;
        }
    }
    public void OnGUI()
    {
/*        if(Fadeout && Fadein) { Fadein = false;} //fails safe if forcing on input

        if (Fadein)
        {
            _time += Time.deltaTime;
            _alpha = FadeCurve.Evaluate(_time);
            //print("Fading IN / " + _time);
            cameraFade.color = new Color(0, 0, 0, _alpha);
            if (_alpha >= 1) Fadein = false;
        }
        if (Fadeout)
        {
            _time -= Time.deltaTime;
            _alpha = FadeCurve.Evaluate(_time);
            //print("Fading OUT / " + _time);
            cameraFade.color = new Color(0, 0, 0, _alpha);
            if (_alpha <= 0) Fadeout = false;
        }*/
    }


/*    IEnumerator Switch(GameObject Newcamera)
    {
        Fadein = true;
        audioSource.Play();
        yield return new WaitForSeconds(TimeToChange);
        Newcamera.SetActive(true);
        audioSource.Play();
        Fadeout = true;

    }*/
}
