using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class LoadScores : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    string text = "";
    // Start is called before the first frame update
    void Start()
    {
        foreach(SaveData element in GameFileManagement.LoadFile())
        {
            text += element.name + ": " + element.timePlayed.ToString("F2") + "\n";
        }
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = text;
        //Debug.Log(GameFileManagement.LoadFile().name);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
