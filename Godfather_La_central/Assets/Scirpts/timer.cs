using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{
    public float countdownTime = 180.0f; // Temps en secondes
    public float countdownTimeBase = 180.0f; // Temps en secondes
    private bool isTimerRunning = false;
    public TextMeshProUGUI timeLeft;
    public bool win = false;    
    public bool lose = false;    

    private void Start()
    {
        // D�marrez le timer au d�but (peut �galement �tre d�clench� par un �v�nement ou une action)
        StartTimer();
    }

    private void Update()
    {
        // V�rifiez si le timer est en cours d'ex�cution
        if (isTimerRunning)
        {
            // R�duire le temps restant
            countdownTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(countdownTime / 60);
            int seconds = Mathf.FloorToInt(countdownTime % 60);
            timeLeft.text = minutes + ":" + seconds;
            if(lose)
            {
                StopTimer();
                Debug.Log("Vous avez mal fait");

            }
            else if(win)
            {
                StopTimer();
                Debug.Log("Win ! ");
            }
            // V�rifiez si le temps est �coul�
            else if (countdownTime <= 0)
            {
                lose = true;
                // Le temps est �coul�, ex�cutez votre action ici (par exemple, affichez un message)
                Debug.Log("Temps �coul� !");
                // Arr�tez le timer
                StopTimer();
                timeLeft.text = "0";

            }
        }
    }

    // D�marrez le timer
    public void StartTimer()
    {
        isTimerRunning = true;
    }

    // Arr�tez le timer
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void ResetTimer()
    {
        countdownTime = countdownTimeBase;
        timeLeft.text = countdownTimeBase.ToString();
        isTimerRunning = true;
    }
}

