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
    public GameObject Answer;
    public GameObject firstButton;
    public GameObject secondButton;
    public GameObject thirdButton;

    private SubDialogueItem currentDialogue;

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
        Answer.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (isDialogueActive)
            {
                isDialogueActive = false;
                hideDialog();
            }
            else
            {
                isDialogueActive = true;
                initDialogue();
            }
        }
    }

    private void initDialogue()
    {
        List<int> lottery = new List<int>();
        for (int i = 0; i < Dialogues.Count; i++)
        {
            if (Dialogues[i].wasRecently() == false)
            {
                lottery.Add(i);
            }    
        }
        int index;
        if (lottery.Count == 0)
        {
            for (int i = 0; i < Dialogues.Count; i++)
            {
                Dialogues[i].refresh();
            }
            index = (int)Random.Range(0, Dialogues.Count);
        }
        else
        {
            index = lottery[(int)Random.Range(0, lottery.Count)];
        }
        Dialogues[index].used();
        currentDialogue = Dialogues[index];
        showDialog();
    }

    private void showDialog()
    {
        Message.text = currentDialogue.Message;
        firstButton.GetComponentInChildren<Text>().text = currentDialogue.Options[0].Text;
        secondButton.GetComponentInChildren<Text>().text = currentDialogue.Options[1].Text;
        if (currentDialogue.Options.Count == 2)
        {
            thirdButton.SetActive(false);
            firstButton.transform.localPosition = new Vector2(0, -100);
            secondButton.transform.localPosition = new Vector2(0, -225);
        }
        else
        {
            thirdButton.SetActive(true);
            firstButton.transform.localPosition = new Vector2(0, -50);
            secondButton.transform.localPosition = new Vector2(0, -175);
            thirdButton.GetComponentInChildren<Text>().text = currentDialogue.Options[2].Text;
        }
        dialogueWindowUI.SetActive(true);
    }

    private void hideDialog()
    {
        dialogueWindowUI.SetActive(false);
    }

    public void OptionButtonPressed(int index)
    {
        isDialogueActive = false;
        Answer.SetActive(true);
        hideDialog();
        Answer.GetComponentInChildren<Text>().text = currentDialogue.Options[index].Answer;
        Energy.changeValue(currentDialogue.Options[index].EnergyChange);
        Faith.changeValue(currentDialogue.Options[index].FaithChange);
        Satiety.changeValue(currentDialogue.Options[index].SatietyChange);
        currentDialogue = currentDialogue.Options[index].NextSubDialogue; 
    }

    public void CloseMessage()
    {
        Answer.SetActive(false);
        if (currentDialogue != null)
        {
            showDialog();
            isDialogueActive = true;
        }
        else
        {
            isDialogueActive = false;
        }
    }
}