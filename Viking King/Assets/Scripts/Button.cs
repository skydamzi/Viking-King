using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Button : MonoBehaviour
{
    public int num = 0;
    public Text t;

    public void ButtonPlus()
    {
        num += 100;
    }

    public void ButtonMinus()
    {
        num -= 50;
    }

    void Update()
    {
        t.text = num.ToString();
    }
}
