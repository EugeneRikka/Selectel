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

    public GameObject Zone1;
    public GameObject Zone2;

    private List<Character> zone1;
    private List<Character> zone2;

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

        List<string> pics = new List<string>(new string[] { "babka_1", "babka_2", "babka_3", "gopnik_1", "gopnik_2", "gopnik_3", "golub_1", "golub_2", "golub_3" });
        zone1 = generate(15, pics, "zone1_", Zone1);
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

        groupUpdate(zone1, 400f, 250f);
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
            index = Random.Range(0, Dialogues.Count);
        }
        else
        {
            index = lottery[Random.Range(0, lottery.Count)];
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

    private List<Character> generate(int num, List<string> pics, string prefix, GameObject parent)
    {
        List<Character> list = new List<Character>();

        for (int i = 0; i < num; i++)
        {
            Character current = new Character();

            GameObject currentObject = new GameObject(prefix + i.ToString());
            SpriteRenderer currentObjectSR = currentObject.AddComponent<SpriteRenderer>() as SpriteRenderer;

            current.type = Random.Range(0, pics.Count);

            Texture2D tex = Resources.Load<Texture2D>("Characters/" + pics[current.type]);
            //Animation anim = Resources.Load<Animation>("Animation/Rotate");

            current.type /= 3;

            currentObject.AddComponent<Animation>();

            currentObjectSR.sortingOrder = 1;

            currentObjectSR.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector3(0.1f, 0.1f, 0f), 200.0f);

            currentObject.transform.SetParent(parent.transform);

            current.npc = currentObject;
            current.isntSet = true;
            list.Add(current);
        }

        return list;
    }

    private void groupUpdate(List<Character> group, float w, float h)
    {
        foreach (Character item in group)
        {
            if (item.isntSet)
            {
                item.movementVector = 1f;
                item.step = 0.1f;
                item.destPlace = new Vector2(Random.Range(-w, w), Random.Range(-h, h));
                item.npc.transform.localScale = new Vector2(0.1f, 0.3f);
                item.npc.transform.position = item.destPlace;
                item.isntSet = false;
            }

            if (Random.Range(0f, 1f) < 0.00005f)
            {
                if (item.buttonExists == false)
                {
                    if (item.type == 0)
                    {
                        item.CreateButton(Faith);
                    }
                    else if (item.type == 1)
                    {
                        item.CreateButton(Faith);

                    }
                    else
                    {
                        item.CreateButton(Satiety);
                    }
                }
            }
            /*if (item.movementVector >= 1f)
            {
                item.curPlace = item.destPlace;
                item.destPlace = new Vector2(Random.Range(-w, w), Random.Range(-h, h));
                item.movementVector = 0f;
            }*/

                /*transform.position = Vector3.Lerp(item.curPlace, item.destPlace, item.movementVector);
                item.movementVector += item.step;*/
        }
    }
}

public class Character
{
    public GameObject npc;
    public float movementVector;
    public float step;
    public Vector2 curPlace;
    public Vector2 destPlace;
    public bool isntSet;

    public int type;

    public bool buttonExists = false;
    private GameObject button;

    public void CreateButton(Bar bar)
    {
        buttonExists = true;
        GameObject button = new GameObject("Bonus", typeof(Image), typeof(Button), typeof(LayoutElement));
        button.transform.SetParent(npc.transform);
        //button.image = 
        //button.GetComponent<LayoutElement>().minHeight = 35;
        //button.GetComponent<LayoutElement>().minHeight = 35;
        button.GetComponent<Button>().onClick.AddListener(delegate {press(bar);});
    }

    public void press(Bar bar)
    {
        GameObject.Destroy(npc.GetComponent<Button>());
        GameObject.Destroy(button);

        if (type == 0)
        {
            bar.changeValue(2);
        }
        else if (type == 1)
        {
            bar.changeValue(-2);
        }
        else 
        {
            bar.changeValue(2);
        }

        buttonExists = false;
    }
}