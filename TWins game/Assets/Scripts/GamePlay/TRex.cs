using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRex : MonoBehaviour
{
    public GameObject TRexObject;

    private int isAppeared;
    private Vector2 place;
    private Vector2 dest;
    private float current;

    // Start is called before the first frame update
    void Start()
    {
        isAppeared = 0;
    }

    private int SpawnMark = 100;

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) < 10)
        {
            if (isAppeared == SpawnMark)
            {
                isAppeared++;

                if (Random.Range(0, 2) == 0)
                {
                    place = new Vector2(-1100, 400);
                    dest = new Vector2(1100, 300);
                }
                else
                {
                    place = new Vector2(1100, 400);
                    dest = new Vector2(-1100, 300);
                }
                TRexObject.transform.position = place;
            }
            else 
            {
                isAppeared++;
            }

        }
        if (isAppeared > SpawnMark)
        {
            TRexObject.transform.position = Vector2.Lerp(place, dest, current);
            current += 0.0005f;

            if (current >= 1f)
            {
                isAppeared = 0;
                current = 0;
            }
        }
    }
}
