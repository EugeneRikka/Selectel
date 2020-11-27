using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public GameObject Zone1;

    GameObject babka;
    private List<Character> zone1;

    float current;

    void Awake()
    {
        List<string> pics = new List<string>(new string[] { "babka_1", "babka_2", "babka_3" });
        zone1 = generate(5, pics,"zone1_");
    }

    void Update()
    {
        foreach (Character item in zone1)
        {
            if (item.isntSet)
            {
                item.movementVector = 1f;
                item.step = 0.1f;
                item.destPlace = new Vector3(Random.Range(0f, 1.5f), Random.Range(0f, 1.5f), 0.0f);
                item.npc.transform.localScale = new Vector3(0.1f, 0.4f, 0.0f);
                item.npc.transform.position = item.destPlace;
                item.isntSet = false;
            }

            if (item.movementVector >= 1f)
            {
                item.curPlace = item.destPlace;
                item.destPlace = new Vector3(Random.Range(-200f, 150f), Random.Range(-100f, 100f), 0.0f);
                item.movementVector = 0f;
            }

            transform.position = Vector3.Lerp(item.curPlace, item.destPlace, item.movementVector);
            item.movementVector += item.step;
        }
    }

    private List<Character> generate(int num, List<string> pics, string prefix)
    {
        List<Character> list = new List<Character>();
        
        for (int i = 0; i < num; i++)
        {
            GameObject currentObject = new GameObject(prefix + i.ToString());
            SpriteRenderer currentObjectSR = currentObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
            Texture2D tex = Resources.Load<Texture2D>("Characters/" + pics[Random.Range(0, pics.Count - 1)]);

            currentObjectSR.sortingOrder = 3;

            currentObjectSR.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector3(0.5f, 0.5f, 0f), 100.0f);

            currentObject.transform.SetParent(Zone1.transform);

            Character current = new Character();
            current.npc = currentObject;
            current.isntSet = true;
            list.Add(current);
        }

        return list;    
    }
}

public class Character
{
    public GameObject npc;
    public float movementVector;
    public float step;
    public Vector3 curPlace;
    public  Vector3 destPlace;
    public bool isntSet;
}