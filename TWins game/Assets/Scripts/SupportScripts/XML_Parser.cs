using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class XML_Parser
{
	private XmlTextReader reader;
	private XmlReader inner;

	private Dictionary<string, SubDialogueItem> SubDialogues = new Dictionary<string, SubDialogueItem>();
	private List<DialogueItem> Dialogues = new List<DialogueItem>();

	public bool SetReader(string fileName)
	{
		TextAsset binary = Resources.Load<TextAsset>("Dialogues/" + fileName);
		reader = new XmlTextReader(new StringReader(binary.text));
		return reader.Read();
	}

	public void ReadSubDialogues()
	{
		while (reader.Read())
		{
			if (reader.IsStartElement("node"))
			{
				string index = reader.GetAttribute("DialogueIndex");
				SubDialogues.Add(index, getSubDialogueItem());

				inner = reader.ReadSubtree();

				while (inner.ReadToFollowing("Options"))
				{
					OptionItem option = getOptionItem();
					SubDialogues[index].Options.Add(option);
				}

				inner.Close();
			}
		}

		reader.Close();
	}

	public void ReadDialogues()
	{
		int index = 0;
		while (reader.Read())
		{
			if (reader.IsStartElement("node"))
			{
				Dialogues.Add(getDialogueItem());

				inner = reader.ReadSubtree();

				while (inner.ReadToFollowing("Options"))
				{
					OptionItem option = getOptionItem();
					string next = reader.GetAttribute("NextDialogue");
					if (next != "0")
                    {
						option.NextSubDialogue = SubDialogues[next];
					}
					Dialogues[index].Options.Add(option);
				}

				inner.Close();

				index++;
			}
		}

		reader.Close();
	}

	public List<DialogueItem> GetDialogues()
	{
		return Dialogues;
	}

	OptionItem getOptionItem()
    {
		return new OptionItem(
					reader.GetAttribute("Text"),
					reader.GetAttribute("Answer"),
					int.Parse(reader.GetAttribute("EnergyChange")),
					int.Parse(reader.GetAttribute("FaithChange")),
					int.Parse(reader.GetAttribute("SatietyChange")));
	}

	DialogueItem getDialogueItem()
	{
		return new DialogueItem(
				int.Parse(reader.GetAttribute("EnergyMin")),
				int.Parse(reader.GetAttribute("EnergyMax")),
				int.Parse(reader.GetAttribute("FaithMin")),
				int.Parse(reader.GetAttribute("FaithMax")),
				int.Parse(reader.GetAttribute("SatietyMin")),
				int.Parse(reader.GetAttribute("SatietyMax")),
				reader.GetAttribute("Message"));
	}

	SubDialogueItem getSubDialogueItem()
	{
		return new SubDialogueItem(reader.GetAttribute("Message"));
	}
}
