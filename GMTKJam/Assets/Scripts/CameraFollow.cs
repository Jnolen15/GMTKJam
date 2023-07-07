using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // ================= Refrences =================
    [SerializeField] private Transform player;

    void Update()
    {
        /*float dist = Vector2.Distance(transform.position, player.position);
        Debug.Log(dist);
        if(dist > 2)
        {
            Vector3 pos = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.position = pos;
        }*/

        Vector3 pos = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = pos;
    }
}
