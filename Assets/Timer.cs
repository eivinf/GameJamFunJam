using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float startTime;
    public Text text;
    public bool finished = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(!finished)
        text.text = "" + (Time.time - startTime).ToString("F2");
    }

    public void setFinished()
    {
        finished = true;
    }

    
}
