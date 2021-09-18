using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveName : MonoBehaviour
{

    public static string name;


    public void Save(Text text)
    {
        name = text.text;
    }
}
