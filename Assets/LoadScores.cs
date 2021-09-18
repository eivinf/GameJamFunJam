using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class LoadScores : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        SaveData data = GameFileManagement.LoadFile();
        scoreText.text = data.name + "\n" + data.timePlayed;
        //Debug.Log(GameFileManagement.LoadFile().name);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
