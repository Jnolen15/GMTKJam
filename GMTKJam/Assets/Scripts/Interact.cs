using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    // ================= Variables =================
    public float interactTime;
    public bool isSabotage;

    [SerializeField] private float cdTime;
    [SerializeField] private float cd;
    public bool onCD;

    [TextArea]
    public string description;

    public int ScoreVal;

    // ================= Refrences =================
    [SerializeField] private UnityEvent interactableEvent;
    [SerializeField] private UnityEvent eventReset;

    public void InvokeEvent()
    {
        if (onCD)
            return;
        else
        {
            cd = 0;
            onCD = true;
        }

        interactableEvent.Invoke();
        Debug.Log("Invoking event " + name);
    }

    private void Update()
    {
        if (!onCD || isSabotage)
            return;

        if (cd < cdTime)
            cd += Time.deltaTime * 1;
        else
            onCD = false;
    }

    public void ResetInteractable()
    {
        onCD = false;
        eventReset.Invoke();
    }
}
