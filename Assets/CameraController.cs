using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{

    private GameObject playercamera;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        this.playercamera = GameObject.Find("torus11");

        offset = transform.position - playercamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playercamera.transform.position + offset;
    }
}
