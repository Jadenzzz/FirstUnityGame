using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    private Text score;
    private void Awake()
    {
        score = transform.Find("ScoreText").GetComponent<Text>();
        
    }
    private void Start()
    {
        Player.GetInstance().OnDied += Player_OnDied;
        Hide();
    } 
    private void Player_OnDied(object sender, System.EventArgs e)
    {
        score.text = Level.GetInstance().GetScore().ToString();
        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
