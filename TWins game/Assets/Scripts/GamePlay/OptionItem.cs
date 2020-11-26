using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionItem
{
    public string Text;
    public string Answer;

    public int EnergyChange;
    public int FaithChange;
    public int SatietyChange;

    public DialogueItem NextDialogue;

    public OptionItem(string text, string answer, int energyChange, int faithChange, int satietyChange, DialogueItem nextDialogue)
    {
        Text = text;
        Answer = answer;
        EnergyChange = energyChange; FaithChange = faithChange; SatietyChange = satietyChange;
        NextDialogue = nextDialogue;
    }
}
