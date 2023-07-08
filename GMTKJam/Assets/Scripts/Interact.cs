using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    // ================= Refrences =================
    [SerializeField] private UnityEvent interactableEvent;

    public void InvokeEvent()
    {
        interactableEvent.Invoke();
        Debug.Log("Invoking event " + name);
    }
}
