using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inkBarController : MonoBehaviour
{
    private Image inkBar;
    public static float maxInk = 500f;
    public static float currentInk = maxInk; // esta se modifica desde cursorcontroller

    // Start is called before the first frame update
    void Start()
    {
        inkBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        inkBar.fillAmount = currentInk / maxInk;
    }
}
