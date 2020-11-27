using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipFlight : MonoBehaviour
{
    private List<Vector2> positions;
    private float step = 0.005f;
    private float current = 0f;
    private int curPositionNum = 0, destPositionNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector2>();
        positions.Add(new Vector2(150, 0));
        positions.Add(new Vector2(160, -10));
        positions.Add(new Vector2(140, 0));
        positions.Add(new Vector2(140, 10));
        positions.Add(new Vector2(145, 5));

        transform.position = positions[0];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(positions[curPositionNum], positions[destPositionNum], current);
        current += step;

        if (current > 1f)
        {
            curPositionNum = destPositionNum;
            destPositionNum = (destPositionNum + 1) % positions.Count;
            current = 0f;
        }
    }

    private void groupUpdate(List<Character> group, float w, float h)
    {
        foreach (Character item in group)
        {
            if (item.isntSet)
            {
                item.movementVector = 1f;
                item.step = 0.1f;
                item.destPlace = new Vector2(Random.Range(-w, w), Random.Range(-h, h));
                item.npc.transform.localScale = new Vector2(0.1f, 0.3f);
                item.npc.transform.position = item.destPlace;
                item.isntSet = false;
            }

            if (item.movementVector >= 1f)
            {
                item.curPlace = item.destPlace;
                item.destPlace = new Vector2(Random.Range(-w, w), Random.Range(-h, h));
                item.movementVector = 0f;
            }

            /*transform.position = Vector3.Lerp(item.curPlace, item.destPlace, item.movementVector);
            item.movementVector += item.step;*/
        }
    }
}
