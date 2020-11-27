using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public GameObject Zone1;
    public GameObject Zone2;

    private List<Character> zone1;
    private List<Character> zone2;

    void Awake()
    {
        List<string> pics = new List<string>(new string[] { "babka_1", "babka_2", "babka_3", "gopnik_1", "gopnik_2", "gopnik_3", "golub_1", "golub_2", "golub_3" });

        zone1 = generate(30, pics, "zone1_", Zone1);

        //zone2 = generate(15, pics, "zone2_", Zone2);
    }
    

    void Update()
    {
        groupUpdate(zone1, 400f, 250f);
        //groupUpdate(zone2, 300f, 300f);
    } 

    private void groupUpdate(List<Character> group, float w, float h)
    {
        foreach (Character item in group)
        {
            if (item.isntSet)
            {
                item.movementVector = 1f;
                item.step = 0.1f;
                item.destPlace = new Vector3(Random.Range(-w, w), Random.Range(-h, h), 0.0f);
                item.npc.transform.localScale = new Vector3(0.1f, 0.3f, 0.0f);
                item.npc.transform.position = item.destPlace;
                item.isntSet = false;
            }

            if (item.movementVector >= 1f)
            {
                item.curPlace = item.destPlace;
                item.destPlace = new Vector3(Random.Range(-w, w), Random.Range(-h, h), 0.0f);
                item.movementVector = 0f;
            }

            /*transform.position = Vector3.Lerp(item.curPlace, item.destPlace, item.movementVector);
            item.movementVector += item.step;*/
        }
    }

    private List<Character> generate(int num, List<string> pics, string prefix, GameObject parent)
    {
        List<Character> list = new List<Character>();
        
        for (int i = 0; i < num; i++)
        {
            GameObject currentObject = new GameObject(prefix + i.ToString());
            SpriteRenderer currentObjectSR = currentObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
            Texture2D tex = Resources.Load<Texture2D>("Characters/" + pics[Random.Range(0, pics.Count - 1)]);
            Animation anim = Resources.Load<Animation>("Animation/Rotate");
            currentObject.AddComponent<Animation>();

            currentObjectSR.sortingOrder = 3;

            currentObjectSR.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector3(0.1f, 0.1f, 0f), 200.0f);

            currentObject.transform.SetParent(parent.transform);

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