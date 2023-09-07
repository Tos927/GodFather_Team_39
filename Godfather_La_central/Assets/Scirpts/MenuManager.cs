using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public Button play;
    public Button options;
    public Button credit;
    public Button quit;
    public Button quitOptions;
    public Button quitCredits;
    public GameObject optionsMenu;
    public GameObject creditMenu;
    // Update is called once per frame
    void Start()
    {
        // Ajoutez des �couteurs de clic aux boutons
        play.onClick.AddListener(PlayButtonClick);
        options.onClick.AddListener(OptionsButtonClick);
        credit.onClick.AddListener(CreditsButtonClick);
        quitOptions.onClick.AddListener(QuitOptions);
        quitCredits.onClick.AddListener(QuitCredit);
        quit.onClick.AddListener(quitButtonClick);
        optionsMenu.SetActive(false);
        creditMenu.SetActive(false);

    }

    void QuitCredit()
    {
        creditMenu.SetActive(false);
    }

    void QuitOptions()
    {
        optionsMenu.SetActive(false);
    }

    void PlayButtonClick()
    {
        // Chargez la sc�ne du jeu (remplacez "NomDeLaScene" par le nom de votre sc�ne de jeu)
        SceneManager.LoadScene("Clemeent_Scene");
    }

    void OptionsButtonClick()
    {
        // Chargez la sc�ne des options (remplacez "OptionsScene" par le nom de votre sc�ne d'options)
        optionsMenu.SetActive(true);
    }

    void CreditsButtonClick()
    {
        // Chargez la sc�ne des cr�dits (remplacez "CreditsScene" par le nom de votre sc�ne de cr�dits)
        creditMenu.SetActive(true);

    }
    void quitButtonClick()
    {
        Application.Quit();
    }
}
