using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private Image susMeterImage;

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

        UpdateSusMeter();

        // Location Switching
        if (Observing)
        {
            
        } else
        {
            int previousNombre = nombre;
            while(nombre == previousNombre) // prevents the same object from being observed twice
            {
                nombre = Random.Range(0, Locations.Count);
            }
            CurrentlyObserving = Locations[nombre];
            Debug.Log("Observing " + CurrentlyObserving.name);
            StartCoroutine(TimedObserve(CurrentlyObserving, Random.Range(MinObserveTime, MaxObserveTime)));
        }
    }

    // Lose Condition, Trigged bu full sus meter
    // Or by witnessing the player sabotage something
    private void SusOut()
    {
        if (!ObservingPlayer)
            return;

        Debug.Log("MAX SUS");
        GManager.Lose();
    }

    private void UpdateSusMeter()
    {
        susMeterImage.fillAmount = (suspicion / maxSuspicion);
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
        float waitTime = ObserveTime;

        while(time < LookSpeed)
        {
            float t = time / LookSpeed;
            t = t * t * (3f - 2f * t);
            this.transform.position = Vector3.Lerp(this.transform.position, obj.transform.position, t);

            time += Time.deltaTime;
            yield return null;
        }

        bool FixingSabo = false;
        if(GManager.sabotagedList.Contains(CurrentlyObserving))
        {
            // code for doing things when something is sabatoged go here
            waitTime *= 2;
            Debug.Log("Player noticed sabatogedList");
            FixingSabo = true;
        }

        time = 0;
        while (time < waitTime)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, obj.transform.position, 1);

            time += Time.deltaTime;
            yield return null;
        }

        if (FixingSabo)
        {
            GManager.sabotagedList.Remove(CurrentlyObserving);
        }

        Observing = false;
    }
}
