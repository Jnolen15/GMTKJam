using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCursor : MonoBehaviour
{
    public bool Clicking = false;
    [SerializeField] private float MaxX; // area that the mouse can move in
    [SerializeField] private float MaxY;
    [SerializeField] private float MinClickTime; // time that the mouse spends stationary
    [SerializeField] private float MaxClickTime;
    [SerializeField] private float MinMouseSpeed; // time that the mouse spends moving for each movement
    [SerializeField] private float MaxMouseSpeed;
    private GameplayManager GManager;

    // Start is called before the first frame update
    void Start()
    {
        GManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameplayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Clicking)
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
            t = t * t * (3f - 2f * t);
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
