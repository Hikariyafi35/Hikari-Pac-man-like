using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    private int _score;
    private int _maxscore;

    public void UpdateUi()
    {
        _scoreText.text = "Score: " + _score + " / " + _maxscore;
    }
    public void SetMaxScore(int value)
    {
        _maxscore = value;
        UpdateUi();
    }
    public void AddScore(int value)
    {
        _score+= value;
        UpdateUi();
    }
    private void Start() {
        UpdateUi();
    }
}
