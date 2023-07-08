using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPOVcam : MonoBehaviour
{
    // ================= Variables =================
    public List<GameObject> Locations;
    public bool Observing = false;
    public bool ObservingPlayer = false;
    public GameObject CurrentlyObserving;
    public float MinObserveTime;
    public float MaxObserveTime;
    public float LookSpeed; // 0 - 1
    private int nombre;
    [SerializeField] private float suspicion;
    [SerializeField] private float maxSuspicion;
    [SerializeField] private float suspicionGainRate;

    // ================= Refrences =================
    private GameplayManager GManager;

    // ================= Setup =================
    // Subscribe to player sabotage event
    private void Awake()
    {
        PlayerController.OnSabotage += SusOut;
    }

    private void OnDisable()
    {
        PlayerController.OnSabotage -= SusOut;
    }

    void Start()
    {
        GManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameplayManager>();
    }

    // ================= Core Function =================
    void Update()
    {
        if (GManager.GameOver)
            return;

        // Player Monitoring
        if (ObservingPlayer)
        {
            if (suspicion < maxSuspicion)
                suspicion += suspicionGainRate * Time.deltaTime;
            else
                SusOut();
        } else
        {
            if(suspicion > 0)
                suspicion -= suspicionGainRate * Time.deltaTime;
        }

        // Location Switching
        if(Observing)
        {
            
        } else
        {
            int previousNombre = nombre;
            while(nombre == previousNombre) // prevents the same object from being observed twice
            {
                nombre = Random.Range(0, 3);
            }
            CurrentlyObserving = Locations[nombre];
            Debug.Log("Observing Nombre " + nombre);
            StartCoroutine(TimedObserve(CurrentlyObserving, Random.Range(MinObserveTime, MaxObserveTime)));
        }
    }

    // Lose Condition, Trigged bu full sus meter
    // Or by witnessing the player sabotage something
    private void SusOut()
    {
        Debug.Log("MAX SUS");
        GManager.Lose();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ObservingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ObservingPlayer = false;
        }
    }


    // =============== LERP STUFF ===============
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
