using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Slider bar;
    private float changeTo;
    private float speed;

    private void Update()
    {
        if ((int)bar.value != (int)changeTo)
        {
            bar.value += speed;
        }
    }

    public float getValue()
    {
        return bar.value;
    }

    public void setValue(float v)
    {
        bar.value = v;
    }

    public void changeValue(float v)
    {
        if (v > 0)
        {
            speed = 0.05f;
        }
        else
        {
            speed = -0.05f;
        }
        changeTo = bar.value + v;
    }
}