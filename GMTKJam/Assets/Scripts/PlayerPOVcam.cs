using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPOVcam : MonoBehaviour
{

    public List<GameObject> Locations;
    public bool Observing = false;
    public float ObserveTime;
    public float LerpFloat; // 0 - 1
    private float TimeStamp;
    private int nombre;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Observing)
        {
            Observe(Locations[nombre], ObserveTime);
        } else
        {
            nombre = Random.Range(0, 3);
            if (!Observing)
                Observing = true;
                TimeStamp = Time.time;
            Debug.Log("Observing Nombre " + nombre);
        }
        
    }

    public void Observe(GameObject obj, float time)
    {

        // Debug.Log(TimeStamp + time);


        this.transform.position = Vector3.Lerp(this.transform.position, obj.transform.position, LerpFloat);
        if (Time.time >= TimeStamp + time)
            Observing = false;
            // Debug.Log(TimeStamp);
    }

    public void CameraLerp()
    {

    }

}
