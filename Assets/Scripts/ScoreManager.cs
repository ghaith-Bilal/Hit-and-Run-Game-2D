using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int Score;
    Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Score < 0)
            Score = 0;
        text.text = " " + Score;
    }
    public static void AddPoints(int PointToAdd)
    {
        Score += PointToAdd;
    }
    public static void Reset()
    {
        Score = 0;
    }
}