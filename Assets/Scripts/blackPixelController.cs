using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackPixelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
