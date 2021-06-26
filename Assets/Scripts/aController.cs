using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aController : MonoBehaviour
{
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        StartCoroutine("FallingLoop");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FallingLoop()
    {
        yield return new WaitForSeconds(2.5f);
        transform.position = startPos;
        StartCoroutine("FallingLoop");
    }
}
