using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCursor : MonoBehaviour
{
    public bool Clicking = false;
    public float MaxX; // area that the mouse can move in
    public float MaxY;
    public float MinClickTime; // time that the mouse spends stationary
    public float MaxClickTime;
    public float MinMouseSpeed; // time that the mouse spends moving for each movement
    public float MaxMouseSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Clicking)
        {

        }
        else
        {
            StartCoroutine(TimedClick(Random.Range(-MaxX, MaxX), Random.Range(-MaxY, MaxY), Random.Range(MinClickTime, MaxClickTime)));
        }
    }

    public void CameraLerp()
    {

    }

    IEnumerator TimedClick(float x, float y, float ClickTime)
    {
        // two parts, first lerps to location, then stays still and "clicks" for some time
        Clicking = true;
        float time = 0;
        float MouseSpeed = Random.Range(MinMouseSpeed, MaxMouseSpeed);

        while (time < MouseSpeed)
        {
            float t = time / MouseSpeed;
            // t = t * t * (3f - 2f * t);
            Vector3 target = new Vector3(x, y, 0) + this.transform.parent.transform.position;
            this.transform.position = Vector3.Lerp(this.transform.position, target, t);

            time += Time.deltaTime;
            yield return null;
        }

        time = 0;
        while (time < ClickTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        Clicking = false;
    }
}
