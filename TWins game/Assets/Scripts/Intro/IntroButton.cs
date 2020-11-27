using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animation anim = GetComponent<Animation>();
        anim.Play("Intro"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartGame();
        }
    }

    public void StartGame()
    {   
        SceneManager.LoadScene("Game");
    }
}
