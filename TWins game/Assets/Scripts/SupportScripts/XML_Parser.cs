using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class XML_Parser
{
	XmlTextReader reader;

	bool SetReader(string filename)
	{
		TextAsset binary = Resources.Load<TextAsset>("/Resources/Dialogues/" + fileName + ".xml");
		reader = new XmlTextReader(new StringReader(binary.text));
    }

	DialogueItem GetToken()
    {
		int index = 0;
		while (reader.Read())
		{
			if (reader.IsStartElement("node"))
			{
				dialogue = new Dialogue();
				dialogue.answer = new List<Answer>();
				dialogue.npcText = reader.GetAttribute("npcText");
				node.Add(dialogue);

				XmlReader inner = reader.ReadSubtree();
				while (inner.ReadToFollowing("answer"))
				{
					answer = new Answer();
					answer.text = reader.GetAttribute("text");

					int number;
					if (int.TryParse(reader.GetAttribute("toNode"), out number)) answer.toNode = number; else answer.toNode = 0;

					bool result;
					if (bool.TryParse(reader.GetAttribute("exit"), out result)) answer.exit = result; else answer.exit = false;

					node[index].answer.Add(answer);
				}
				inner.Close();

				index++;
			}
		}

		reader.Close();
	}
}
