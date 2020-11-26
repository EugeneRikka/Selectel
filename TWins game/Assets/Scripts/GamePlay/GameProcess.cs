using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProcess : MonoBehaviour
{
    public GameObject dialogueWindowUI;
    private bool isDialogueActive = false;

    public Bar Energy;
    public Bar Faith;
    public Bar Satiety;

    private List<DialogueItem> Dialogues;

    public Text Message;
    public Button firstButton;
    public Button secondButton;
    public Button thirdButton;

    // Start is called before the first frame update
    void Start()
    {
        XML_Parser parser = new XML_Parser();

        parser.SetReader("SubDialogues");
        parser.ReadSubDialogues();

        parser.SetReader("Dialogues");
        parser.ReadDialogues();
        
        Dialogues = parser.GetDialogues();
        
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
        int index = 1;
        Message.text = Dialogues[index].Message;
        firstButton.GetComponentInChildren<Text>().text = Dialogues[index].Options[0].Text;
        secondButton.GetComponentInChildren<Text>().text = Dialogues[index].Options[1].Text;
        if (Dialogues[index].Options.Count == 2)
        {

        }
        else
        {
            thirdButton.GetComponentInChildren<Text>().text = Dialogues[index].Options[2].Text;
        }
        dialogueWindowUI.SetActive(true);
    }

    private void hideDialog()
    {
        dialogueWindowUI.SetActive(false);
    }
}