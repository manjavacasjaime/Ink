using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject player;
    public new Camera camera;
    public int cameraSize = 6;
    public float cameraPlusX;
    public float cameraPlusY;

    // Start is called before the first frame update
    void Start()
    {
        camera.orthographicSize = cameraSize;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + cameraPlusX, player.transform.position.y + cameraPlusY, transform.position.z);
        Cursor.visible = false;
    }
}
