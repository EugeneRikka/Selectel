using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class DialogueGenerator : MonoBehaviour
{

	public string fileName = "Dialogues"; // имя генерируемого файла (без разрешения)
	public DialogueItemGen[] node;

	public void Generate()
	{
		string path = Application.dataPath + "/Resources/Dialogues/" + fileName + ".xml";

		XmlDocument xmlDoc = new XmlDocument();

		XmlNode rootNode = xmlDoc.CreateElement("dialogue");
		xmlDoc.AppendChild(rootNode);

		for (int j = 0; j < node.Length; j++)
		{
			XmlElement DialogueElement = xmlDoc.CreateElement("node");

			createDialogueItem(DialogueElement, node[j]);

			for (int i = 0; i < node[j].Options.Length; i++)
			{
				XmlElement OptionElement = xmlDoc.CreateElement("Options");

				createOptionItem(OptionElement, node[j].Options[i]);

				DialogueElement.AppendChild(OptionElement);
			}

			rootNode.AppendChild(DialogueElement);
		}

		xmlDoc.Save(path);
		Debug.Log(this + " Создан XML файл диалога [ " + fileName + " ] по адресу: " + path);
	}

	public void createDialogueItem(XmlElement element, DialogueItemGen dialogue)
	{
		element.SetAttribute("DialogueIndex", dialogue.DialogueIndex.ToString());

		element.SetAttribute("EnergyMin", dialogue.EnergyMin.ToString());
		element.SetAttribute("EnergyMax", dialogue.EnergyMax.ToString());

		element.SetAttribute("FaithMin", dialogue.FaithMin.ToString());
		element.SetAttribute("FaithMax", dialogue.FaithMax.ToString());

		element.SetAttribute("SatietyMin", dialogue.SatietyMin.ToString());
		element.SetAttribute("SatietyMax", dialogue.SatietyMax.ToString());

		element.SetAttribute("Message", dialogue.Message);
	}

	public void createOptionItem(XmlElement element, OptionItemGen option)
	{
		element.SetAttribute("Text", option.Text);
		element.SetAttribute("Answer", option.Answer);

		element.SetAttribute("EnergyChange", option.EnergyChange.ToString());
		element.SetAttribute("FaithChange", option.FaithChange.ToString());
		element.SetAttribute("SatietyChange", option.SatietyChange.ToString());
		element.SetAttribute("NextDialogue", option.NextDialogue.ToString());
	}
}

[System.Serializable]
public class DialogueItemGen
{
	private bool WasRecently;

	public int DialogueIndex;

	public int EnergyMin, EnergyMax;
	public int FaithMin, FaithMax;
	public int SatietyMin, SatietyMax;

	public string Message;

	public OptionItemGen[] Options;
};

[System.Serializable]
public class OptionItemGen
{
	public string Text;
	public string Answer;

	public int EnergyChange;
	public int FaithChange;
	public int SatietyChange;

	public string NextDialogue;
}
