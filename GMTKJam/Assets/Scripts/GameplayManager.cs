using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    // ================= Variables =================
    [SerializeField] private List<GameObject> sabotagedList = new();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddToSabotagedList(GameObject obj)
    {
        if(!sabotagedList.Contains(obj))
            sabotagedList.Add(obj);
    }
}
