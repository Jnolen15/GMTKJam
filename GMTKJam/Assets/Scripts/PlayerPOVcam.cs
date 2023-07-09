using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPOVcam : MonoBehaviour
{
    // ================= Variables =================
    [SerializeField] private GameObject Player;
    [SerializeField] private List<GameObject> Locations;
    public bool Observing = false;
    public bool FixingSabo = false;
    [SerializeField] private bool ObservingPlayer = false;
    [SerializeField] private GameObject CurrentlyObserving;
    [SerializeField] public float MinObserveTime;
    [SerializeField] public float MaxObserveTime;
    [SerializeField] private float LookSpeed;
    [SerializeField] private float DistanceMultiplier; // increses the time looking takes based on distance
    private int nombre;
    [SerializeField] private int lookToPlayerCountdown;
    [SerializeField] private float suspicion;
    [SerializeField] private float maxSuspicion;
    [SerializeField] private float suspicionGainRate;
    private bool reverseSuspicion;

    // ================= Refrences =================
    private GameplayManager GManager;
    [SerializeField] private Image susMeterImage;
    [SerializeField] private GameObject Exclamation;
    [SerializeField] private GameObject Question;

    // ================= Setup =================
    // Subscribe to player sabotage event
    private void Awake()
    {
        PlayerController.OnSabotage += SusOut;
        PlayerController.OnActNormal += LowerSus;
    }

    private void OnDisable()
    {
        PlayerController.OnSabotage -= SusOut;
        PlayerController.OnActNormal -= LowerSus;
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
        // Drop sus meter to 0 if cow was observed being normal
        if (reverseSuspicion)
        {
            if (suspicion > 0)
                suspicion -= (suspicionGainRate * 1.5f) * Time.deltaTime;
            else
                reverseSuspicion = false;
        }
        else if (ObservingPlayer)
        {
            if (suspicion < maxSuspicion)
                suspicion += suspicionGainRate * Time.deltaTime;
            else
                SusOut();
        } else
        {
            //if(suspicion > 0)
            //    suspicion -= suspicionGainRate * Time.deltaTime;
        }

        UpdateSusMeter();

        // Location Switching
        if (!Observing)
        {
            // Pick Player
            if (lookToPlayerCountdown == 0)
            {
                CurrentlyObserving = Player;
                lookToPlayerCountdown = Random.Range(2, 4);
            }
            // Pick random building
            else
            {
                int previousNombre = nombre;
                while (nombre == previousNombre) // prevents the same object from being observed twice
                {
                    nombre = Random.Range(0, Locations.Count);
                }
                CurrentlyObserving = Locations[nombre];
                lookToPlayerCountdown--;
            }

            // Change view
            Debug.Log("Observing " + CurrentlyObserving.name);
            StartCoroutine(TimedObserve(CurrentlyObserving, Random.Range(MinObserveTime, MaxObserveTime)));
        }
    }

    // Lower Suspicion if player did something normal
    public void LowerSus()
    {
        reverseSuspicion = true;
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
        Question.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ObservingPlayer = false;
        }
        Question.SetActive(false);
    }


    // =============== LERP STUFF ===============
    IEnumerator TimedObserve(GameObject obj, float ObserveTime)
    {
        // two parts, first lerps to obj, then a second, faster lerp to stick it to that obj
        Observing = true;
        float time = 0;

        LookSpeed = Vector3.Distance(this.transform.position, obj.transform.position) / DistanceMultiplier;
        Debug.Log("Look Speed " + LookSpeed);
        Vector3 startPosition = transform.position;
        while (time < LookSpeed)
        {
            float t = time / LookSpeed;
            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(startPosition, obj.transform.position, t);

            time += Time.deltaTime;
            yield return null;
        }
        transform.position = obj.transform.position;

        if (GManager.sabotagedList.Contains(CurrentlyObserving))
        {
            // code for doing things when something is sabatoged go here
            // waitTime *= 2;
            Debug.Log("Observer noticed sabatogedList");
            FixingSabo = true;
            Exclamation.SetActive(true);
            suspicion += 2;
        }

        time = 0;
        Debug.Log("Waiting for " + ObserveTime);
        while (time < ObserveTime)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, obj.transform.position, 1);

            time += Time.deltaTime;
            yield return null;
        }

        if (FixingSabo)
        {
            if (CurrentlyObserving.GetComponent<Interact>() != null)
            {
                CurrentlyObserving.GetComponent<Interact>().ResetInteractable();
                GManager.AddMultiplier(-1);
            }

            Debug.Log("Fixed: " + CurrentlyObserving.name);

            GManager.sabotagedList.Remove(CurrentlyObserving);
            Exclamation.SetActive(false);

            FixingSabo = false;
        }

        Observing = false;
    }
}
