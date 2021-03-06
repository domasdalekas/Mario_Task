﻿using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public ScoreManager scoreManager;
    public Text scoretext;
    public Text coinsText;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = gameObject.GetComponent<ScoreManager>();
    }
    // Update is called once per frame
    void Update()
    {
        int score = scoreManager.GetScore();
        scoretext.text = score.ToString();
        coinsText.text = scoreManager.GetCoins().ToString();
    }
}
