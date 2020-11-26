using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class XML_Parser
{
	XmlTextReader reader;

	bool SetReader(string fileName)
	{
		TextAsset binary = Resources.Load<TextAsset>("Dialogues/" + fileName + ".xml");
		reader = new XmlTextReader(new StringReader(binary.text));
		return reader.Read();
	}

	DialogueItem GetToken()
    {
		DialogueItem dialog = null;
		if (reader.Read())
		{
			dialog = new DialogueItem(
				int.Parse(reader.GetAttribute("EnergyMin")),
				int.Parse(reader.GetAttribute("EnergyMax")),
				int.Parse(reader.GetAttribute("FaithMin")),
				int.Parse(reader.GetAttribute("FaithMax")),
				int.Parse(reader.GetAttribute("SatietyMin")),
				int.Parse(reader.GetAttribute("SatietyMax")),
				reader.GetAttribute("Message"));

			XmlReader inner = reader.ReadSubtree();

			while (inner.Read())
			{
				dialog.Options.Add(new OptionItem(
					reader.GetAttribute("Text"),
					reader.GetAttribute("Answer"),
					int.Parse(reader.GetAttribute("EnergyChange")),
					int.Parse(reader.GetAttribute("FaithChange")),
					int.Parse(reader.GetAttribute("SatietyChange"))));
			}

			inner.Close();
		}
		else
        {
			reader.Close();
		}

		return dialog;
	}
		
}
