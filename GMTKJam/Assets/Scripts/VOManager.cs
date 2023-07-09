using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VOManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> sabotaged;
    [SerializeField] private List<AudioClip> cowSus;
    [SerializeField] private List<AudioClip> cameraMove;
    [SerializeField] private List<AudioClip> foundOut;
    [SerializeField] private List<AudioClip> normal;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void PlaySabotaged()
    {
        audioSource.PlayOneShot(sabotaged[Random.Range(0, sabotaged.Count)]);
    }

    public void PlayCowSus()
    {
        audioSource.PlayOneShot(cowSus[Random.Range(0, cowSus.Count)]);
    }

    public void PlayCameraMove()
    {
        audioSource.PlayOneShot(cameraMove[Random.Range(0, cameraMove.Count)]);
    }

    public void PlayFoundOut()
    {
        audioSource.PlayOneShot(foundOut[Random.Range(0, foundOut.Count)]);
    }

    public void PlayNormal()
    {
        audioSource.PlayOneShot(normal[Random.Range(0, normal.Count)]);
    }
}
