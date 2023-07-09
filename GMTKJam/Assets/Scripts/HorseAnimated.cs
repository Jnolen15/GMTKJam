using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HorseAnimated : MonoBehaviour
{
    public void Animate()
    {
        transform.DOJump(transform.position, Random.Range(0.8f, 1.4f), 1, Random.Range(0.4f, .6f)).SetLoops(-1, LoopType.Restart);
    }

    public void StopAnimating()
    {
        transform.DOKill();
    }
}
