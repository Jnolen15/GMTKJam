using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPOVcam : MonoBehaviour
{

    public List<GameObject> Locations;
    public bool Observing = false;
    public GameObject CurrentlyObserving;
    public float MinObserveTime;
    public float MaxObserveTime;
    public float LookSpeed; // 0 - 1
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
            // Observe(Locations[nombre], ObserveTime);
        } else
        {
            int previousNombre = nombre;
            while(nombre == previousNombre) // prevents the same object from being observed twice
            {
                nombre = Random.Range(0, 3);
            }
            /*
            if (!Observing)
                Observing = true;
                TimeStamp = Time.time;
            */
            CurrentlyObserving = Locations[nombre];
            Debug.Log("Observing Nombre " + nombre);
            StartCoroutine(TimedObserve(CurrentlyObserving, Random.Range(MinObserveTime, MaxObserveTime)));
        }
    }

    public void Observe(GameObject obj, float time) // out of use, replaced by TimedObserve coroutine
    {

        // Debug.Log(TimeStamp + time);


        this.transform.position = Vector3.Lerp(this.transform.position, obj.transform.position, 0.01f);
        if (Time.time >= TimeStamp + time)
            Observing = false;
            // Debug.Log(TimeStamp);
    }

    public void CameraLerp()
    {

    }

    IEnumerator TimedObserve(GameObject obj, float ObserveTime)
    {
        // two parts, first lerps to obj, then a second, faster lerp to stick it to that obj
        Observing = true;
        float time = 0;

        while(time < LookSpeed)
        {
            float t = time / LookSpeed;
            t = t * t * (3f - 2f * t);
            this.transform.position = Vector3.Lerp(this.transform.position, obj.transform.position, t);

            time += Time.deltaTime;
            yield return null;
        }

        while (time < ObserveTime)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, obj.transform.position, 1);

            time += Time.deltaTime;
            yield return null;
        }

        Observing = false;
    }
        

}
