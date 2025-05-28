using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] 
    public string[] letters = { "@", "[" ,"{","%"};

    List<float> numbers = new List<float>();

    public float time = 250f;

    public TextMeshProUGUI timertext;
    private float result = 0;

    public TextMeshProUGUI computertext;
    private string pctxt;

    // Start is called before the first frame update

    public TextMeshProUGUI paperUI;

    void Start()
    {
        numbers.Add(Mathf.Round(5f));

        numbers.Add(Mathf.Round((numbers[0]+1)/2));

        numbers.Add(Mathf.Round((-(numbers[1] * 3)+19)/numbers[0]));
  
        numbers.Add(Mathf.Round(-((numbers[2] * 2) / (numbers[1] * 2)) + 7));

        result = numbers[1]+numbers[1]+numbers[2]+numbers[3];
        Debug.Log(result);
        paperUI.text = "@ + @ = " + numbers[0]*2 + "\n\n @ - [x2 = -1 \n\n [x3 + @x{ = 19";
        for (int i = 0; i < letters.Length; i++) {
            if (i == letters.Length-1)
            {
                pctxt += letters[i];
            }
            else
            {
                pctxt += letters[i] + " + ";
            }
        }
        pctxt += " =";
        computertext.text = pctxt;
    }
    public void Inputvalue(string value)
    {
        computertext.text = pctxt + " " +value;
    }
    public void CheckResult(string value)
    {
        float value2 = Convert.ToInt32(value);

        if (value2 == result)
        {
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Incorrect");
        }
 
    }
    private void Update()
    {
       
        time -= Time.deltaTime;
        timertext.text = "Timer: " + Mathf.Round(time).ToString();
        if ( time <= 0)
        {
            SceneManager.LoadScene("TitleScreen");
        }
        
    }
}
