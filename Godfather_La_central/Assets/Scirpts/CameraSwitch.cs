using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class CameraSwitch : MonoBehaviour
{
    /*    public Image cameraFade;*/

    GameManager gameManager;

    public float offset = 5;

    private AudioSource audioSource;
    public float TimeToChange = .2f;

    public float cameraSpeed = 1f;
    public List<Transform> cameraPoses = new List<Transform>();
    public AnimationCurve PosOverTime;
    public float duration = .5f;
    public float zoomduration = .2f;
    public float zoomForce = 4.7f;
    public float zoomForcePixel = 4.3f;
    private Vector3 OldPosition;
    private bool _doCameraMovs = false;


/*    private float _alpha = 1;
    private Texture2D _texture;
    private bool Fadein = false;
    private bool Fadeout = false;*/

    private float _time;
    private bool _isSwitching = false;
    [SerializeField] private CameraStates _cameraState = CameraStates.Camera3;
    public CameraStates CameraState { get => _cameraState; set => _cameraState = value;  }



    //public Decoup decoup;


    public enum CameraStates
    {
        Camera1 = 0,
        Camera2 = 1,
        Camera3 = 2,
        Camera4 = 3,
    }

    private void Start()
    {
        //print(CameraState);
        //decoup.Startmodule();
        gameManager = FindObjectOfType<GameManager>();
        _time = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gameManager.scoller.hasStarted)
        {
            //transform.position += Vector3.left * cameraSpeed * .01f * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.O)) StartCoroutine(ZoomInAndOut());

        if (Input.GetKeyDown(KeyCode.K)) 
        {
            _cameraState = CameraStates.Camera1;
            DoCameraMoves();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            _cameraState = CameraStates.Camera2;
            DoCameraMoves();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            _cameraState = CameraStates.Camera3;
            DoCameraMoves();
        }

    }
    public CameraStates SetCameraState(int state)
    {
        CameraStates c = (CameraStates)state;
        _cameraState = c;
        return c;
    }
    public CameraStates AddCameraState()
    {
        _cameraState++;
        return _cameraState;
    }
    public void DoCameraMoves()
    {
        if(GameManager.instance.sequence>=4)
        {
            StartCoroutine(GoTo(cameraPoses[0].position));
            GameManager.instance.sequence = 0;

        }
        else
            StartCoroutine(GoTo(cameraPoses[GameManager.instance.sequence].position));
    }

    public IEnumerator ZoomInAndOut()
    {
        float elapsed = 0;
        Vector3 startPosition = transform.position;

        while (elapsed <= zoomduration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / zoomduration;
            Camera.main.orthographicSize = Mathf.LerpUnclamped(5f, zoomForce, t);
            yield return null;
        }
        while (elapsed > 0)
        {
            elapsed -= Time.deltaTime;
            float t = elapsed / zoomduration;
            Mathf.Clamp01(t);
            Camera.main.orthographicSize = Mathf.LerpUnclamped(5f, zoomForce, t);
            yield return null;
        }
        Camera.main.orthographicSize = 5f;

    }

    public IEnumerator ZoomInAndOutPIXEL()
    {
        float elapsed = 0;
        Vector3 startPosition = transform.position;

        while (elapsed <= zoomduration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / zoomduration;
            Camera.main.orthographicSize = Mathf.LerpUnclamped(5f, zoomForcePixel, t);
            yield return null;
        }
        while (elapsed > 0)
        {
            elapsed -= Time.deltaTime;
            float t = elapsed / zoomduration;
            Mathf.Clamp01(t);
            Camera.main.orthographicSize = Mathf.LerpUnclamped(5f, zoomForcePixel, t);
            yield return null;
        }
        Camera.main.orthographicSize = 5f;

    }


    private IEnumerator GoTo(Vector3 endPosition)
    {
        float elapsed = 0;
        yield return new WaitForSeconds(0.4f);
        Vector3 startPosition = transform.position;

        while (elapsed <= duration)
        {
            _isSwitching = true;
            elapsed += Time.deltaTime;
            float t = PosOverTime.Evaluate(elapsed / duration);
            transform.position = Vector3.LerpUnclamped(startPosition, endPosition, t);
            //float distance = Vector3.Distance(transform.position, endPosition);
            yield return null;
        }
        _isSwitching = false;
        //print(_isSwitching);
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
