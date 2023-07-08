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

    // ================= Refrences =================
    [SerializeField] private UnityEvent interactableEvent;

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
        if (!onCD)
            return;

        if (cd < cdTime)
            cd += Time.deltaTime * 1;
        else
            onCD = false;
    }
}
