using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionItem
{
    public string Text;
    public string Answer;

    public int EnergyChange;
    public int FaithChange;
    public int SatietyChange;

    public DialogueItem NextDialogue;

    public OptionItem(string text, string answer, int energyChange, int faithChange, int satietyChange)
    {
        Text = text;
        Answer = answer;
        EnergyChange = energyChange; FaithChange = faithChange; SatietyChange = satietyChange;
    }
}
