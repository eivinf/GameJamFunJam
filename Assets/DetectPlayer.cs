using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{

    public Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            timer.setFinished();
            StartCoroutine(GoToMenu());
        }
    }

    public IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(3f);
        Application.LoadLevel(0);
    }



}
