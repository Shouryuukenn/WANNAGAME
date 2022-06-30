using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBallMoving : MonoBehaviour
{
    private Vector3 WindBallPos;


    // Start is called before the first frame update
    void Start()
    {
        WindBallPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float T = 30.0f;
        float f = 1.0f / T;
        // X方向に周期的に動かす
        transform.position = new Vector3(Mathf.Sin(2 * Mathf.PI * f * Time.time) * 20.0f + WindBallPos.x, WindBallPos.y, WindBallPos.z);
    }
}
