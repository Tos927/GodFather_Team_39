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
        // Démarrez le timer au début (peut également être déclenché par un événement ou une action)
        StartTimer();
    }

    private void Update()
    {
        // Vérifiez si le timer est en cours d'exécution
        if (isTimerRunning)
        {
            // Réduire le temps restant
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
            // Vérifiez si le temps est écoulé
            else if (countdownTime <= 0)
            {
                lose = true;
                // Le temps est écoulé, exécutez votre action ici (par exemple, affichez un message)
                Debug.Log("Temps écoulé !");
                // Arrêtez le timer
                StopTimer();
                timeLeft.text = "0";

            }
        }
    }

    // Démarrez le timer
    public void StartTimer()
    {
        isTimerRunning = true;
    }

    // Arrêtez le timer
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

