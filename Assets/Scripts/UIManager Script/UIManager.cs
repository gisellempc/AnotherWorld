using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   
    public TMPro.TextMeshProUGUI _scoreTxt;
    public Image[] _hpIndicators;
    public GameObject loseScreen;
    public TMPro.TextMeshProUGUI finalScoreTxt;
    public TMPro.TextMeshProUGUI winScoreTxt;
    public GameObject winScreen;
    public void SetScore(int newScore)
    {
        _scoreTxt.text = "Score: " + newScore.ToString();
    }

    public void SetHp(int newHp)
    {
        for (int i = 0; i < _hpIndicators.Length; i++) 
        { 
            if (newHp > i)
                _hpIndicators[i].gameObject.SetActive(true);
            else
                _hpIndicators[i].gameObject.SetActive(false);
        }
    }

    public void ShowLooseScreen(int score)
    {
        loseScreen.SetActive(true);
        finalScoreTxt.text = "Final Score: " + score;
    }

    public void ShowWinScreen(int score)
    {
        winScreen.SetActive(true);
        winScoreTxt.text = "Final Score: " + score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }
}
