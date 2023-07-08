using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    // ================= Variables =================
    [SerializeField] private GameObject EndScreen;
    public List<GameObject> sabotagedList = new();
    public bool GameOver;

    public void AddToSabotagedList(GameObject obj)
    {
        if(!sabotagedList.Contains(obj))
            sabotagedList.Add(obj);
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
