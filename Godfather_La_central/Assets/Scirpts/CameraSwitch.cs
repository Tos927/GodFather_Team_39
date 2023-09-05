using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class CameraSwitch : MonoBehaviour
{
    public Image cameraFade;
    public List<GameObject> cameras = new List<GameObject>();

    public float TimeToChange = .2f;

    private AudioSource audioSource;


    public AnimationCurve FadeCurve;
    private float _alpha = 1;
    private Texture2D _texture;
    private bool Fadein = false;
    private bool Fadeout = false;

    private float _time;

    public static CameraState cameraState;

    public enum CameraState
    {
        Camera1,
        Camera2,
        Camera3,
        Camera4,
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cameraFade.color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cameraState = CameraState.Camera1;
            GameObject camera = cameras[0];
            StartCoroutine(Switch(camera));

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameraState = CameraState.Camera2;
            GameObject camera = cameras[1];
            StartCoroutine(Switch(camera));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cameraState = CameraState.Camera3;
            GameObject camera = cameras[2];
            StartCoroutine(Switch(camera));
        }

    }
    public void OnGUI()
    {
        if(Fadeout && Fadein) { Fadein = false;}

        if (Fadein)
        {
            _time += Time.deltaTime;
            _alpha = FadeCurve.Evaluate(_time);
            print("Fading IN / " + _time);
            cameraFade.color = new Color(0, 0, 0, _alpha);
            if (_alpha >= 1) Fadein = false;
        }
        if (Fadeout)
        {
            _time -= Time.deltaTime;
            _alpha = FadeCurve.Evaluate(_time);
            print("Fading OUT / " + _time);
            cameraFade.color = new Color(0, 0, 0, _alpha);
            if (_alpha <= 0) Fadeout = false;
        }

    }

    IEnumerator Switch(GameObject Newcamera)
    {
        Fadein = true;
        audioSource.Play();
        yield return new WaitForSeconds(TimeToChange);
        foreach (GameObject cam in cameras)
        {
            cam.SetActive(false);
        }
        Newcamera.SetActive(true);
        audioSource.Play();
        Fadeout = true;

    }
}
