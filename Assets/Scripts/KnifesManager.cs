using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifesManager : MonoBehaviour {

    public static int Knifes;
    Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        Knifes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Knifes < 0)
            Knifes = 0;
        text.text = " " + Knifes;
    }
    public static void AddKnifes(int KnifesToAdd)
    {
        Knifes += KnifesToAdd;
    }
    public static void Reset()
    {
        Knifes = 0;
    }
}
