using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Lore;
    // Start is called before the first frame update
    void Start()
    {
        Lore.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Intro");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
