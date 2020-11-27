using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Lore;
    private bool firstGame = true;

    // Start is called before the first frame update
    void Start()
    {
        Lore.SetActive(false);
    }

    public void PlayGame()
    {
        if (firstGame)
        {
            SceneManager.LoadScene("Intro");
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
