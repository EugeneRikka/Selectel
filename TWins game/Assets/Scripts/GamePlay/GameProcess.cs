using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProcess : MonoBehaviour
{
    public GameObject dialogueWindowUI;
    private bool isDialogueActive = false;

    private List<DialogueItem> dialogues = new List<DialogueItem>();
    private Dictionary<string, DialogueItem> subDialogues = new Dictionary<string, DialogueItem>();

    public Bar Energy;
    public Bar Faith;
    public Bar Satiety;

    // Start is called before the first frame update
    void Start()
    {
        Energy.setValue(50);
        Faith.setValue(0);
        Satiety.setValue(50);
        dialogueWindowUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (isDialogueActive)
            {
                hideDialog();
                isDialogueActive = false;
            }
            else
            { 
                showDialog();
                isDialogueActive = true;
            }
        }
    }


    private void showDialog()
    {
        dialogueWindowUI.SetActive(true);

    }

    private void hideDialog()
    {
        dialogueWindowUI.SetActive(false);
    }
}