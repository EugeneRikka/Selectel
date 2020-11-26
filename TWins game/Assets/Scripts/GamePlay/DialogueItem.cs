using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueItem
{
    private bool WasRecently; 

    public int DialogueIndex;
    
    public int EnergyMin, EnergyMax;
    public int FaithMin, FaithMax;
    public int SatietyMin, SatietyMax;

    public string Message;

    public List<OptionItem> Options;

    public DialogueItem(int dialogueNum, int energyMin, int energyMax, int faithMin, int faithMax, int satietyMin, int satietyMax, string message)
    {
        WasRecently = false;

        DialogueIndex = dialogueNum;
        
        EnergyMin = energyMin; EnergyMax = energyMax;
        FaithMin = faithMin; FaithMax = faithMax;
        SatietyMin = satietyMin; SatietyMax = satietyMax;
        
        Message = message;

        Options = new List<OptionItem>();
    }

    void used()
    {
        WasRecently = true;
    }

    bool wasRecently()
    {
        return WasRecently;
    }

    void refresh()
    {
        WasRecently = false;
    }
}
