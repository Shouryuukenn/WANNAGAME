using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasActive : MonoBehaviour
{
    public GameObject Canvas;

    // Start is called before the first frame update
    void Start()
    {
        Canvas.SetActive(false);
    }

    public void GameOver()
    {
        Canvas.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
