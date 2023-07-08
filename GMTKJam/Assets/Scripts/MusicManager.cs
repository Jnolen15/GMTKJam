using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource Source1;
    [SerializeField] private AudioSource Source2;
    [SerializeField] private AudioSource Source3;
    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private float Track2Min = 2; // mimimun buidlings on tire to start 2nd track, 3rd track is track2min+depth
    [SerializeField] private float TrackDepth = 3; // accross how many buildijngs sabotaged the track increases in volume
    private GameplayManager Manager;

    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameplayManager>();
        Source1.Play();
        Source2.Play();
        Source3.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float chaos = Manager.sabotagedList.Count;
        Debug.Log("Chaos lvl = " + chaos);
        if (chaos < Track2Min)
        {
            Source2.volume = 0;
        } else if(chaos > Track2Min + TrackDepth)
        {
            Source2.volume = 1;
        } else
        {
            Source2.volume = (chaos - Track2Min) / TrackDepth;
        }

        if (chaos < Track2Min + TrackDepth)
        {
            Source3.volume = 0;
        }
        else if (chaos > Track2Min + TrackDepth*2)
        {
            Source3.volume = 1;
        }
        else
        {
            Source3.volume = (chaos - Track2Min * 2) / TrackDepth;
        }

    }
}
