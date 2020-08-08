﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoinsCount : MonoBehaviour
{
    public ScoreManager scoreManager;
    public Text scoretext;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = gameObject.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int score = scoreManager.GetCoins();
        scoretext.text = score.ToString();
    }
}
