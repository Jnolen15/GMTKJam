using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    // ================= Variables =================
    [SerializeField] private GameObject EndScreen;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI ComboText;
    [SerializeField] private int Score;
    [SerializeField] private int Combo;
    [SerializeField] private float MultiplierVal;
    public List<GameObject> sabotagedList = new();
    public bool GameOver;

    public void AddToSabotagedList(GameObject obj)
    {
        if(!sabotagedList.Contains(obj))
            sabotagedList.Add(obj);
    }

    public void AddScore(int num)
    {
        Score += (int)(num * MultiplierVal);
        ScoreText.text = Score.ToString();
    }

    public void AddMultiplier(int num)
    {
        Combo += num;

        if (Combo == 0)
            MultiplierVal = 1;
        else if (Combo == 1)
            MultiplierVal = 1;
        else if (Combo == 2)
            MultiplierVal = 1.1f;
        else if (Combo == 3)
            MultiplierVal = 1.2f;
        else if (Combo == 4)
            MultiplierVal = 1.4f;
        else if (Combo == 5)
            MultiplierVal = 1.6f;
        else if (Combo == 6)
            MultiplierVal = 2f;

        ComboText.text = Combo.ToString();
    }

    public void Lose()
    {
        GameOver = true;
        EndScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Win()
    {
        GameOver = true;
        EndScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
