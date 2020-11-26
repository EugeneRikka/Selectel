/*public bool skipText = false;
private bool isPrint = false;

public Text message;

StartCoroutine(TextPrint(message.text, "3066 год - Солнце стало красным гигантом.\nЧерез несколько лет невыносимая жара уничтожила все живое на планете.", 0.1f, skipText));
 
IEnumerator TextPrint(ref string output, string input, float delay, ref bool skip)
{
	if (isPrint) return;
	isPrint = true;
	//вывод текста побуквенно
	for (int i = 1; i <= input.Length(); i++) 
	{
		if (skip) 
		{ 
			output = input; 
			return; 
		}
		output = input.SubString(1, i);
		yield return new WaitForSeconds(delay);
	}
	isPrint = false;
}*/