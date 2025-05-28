using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    // Start is called before the first frame update
    public string number;

    private LevelManager lvlmnger;

    private void Start()
    {
        lvlmnger = gameObject.GetComponent<LevelManager>();
    }
    public void ButtonClick(string value)
    {
        if (value == "Delete")
        {
            number = "";
        }
        else if(value == "Enter")
        {
            lvlmnger.CheckResult(number);
        }
        else
        {
            number += value;
        }

        lvlmnger.Inputvalue(number);
    }
}
