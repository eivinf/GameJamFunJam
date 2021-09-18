using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Start is called before the first frame update
    [System.Serializable]
    public class SaveData
    {


        public string name;
        public float timePlayed;

        public SaveData( string nameStr, float timePlayedF)
        {
            name = nameStr;
            timePlayed = timePlayedF;
        }
    }


