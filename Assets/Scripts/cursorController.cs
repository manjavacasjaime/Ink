using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorController : MonoBehaviour
{
    private Renderer rend;

    public GameObject pixelPrefab;
    private bool pixelPainted = false;
    private int i = 0;
    private int j = 0;

    private Vector3 mousePos;
    private Vector3 lastPaintedPixelPos;

    private GameObject firstPixel;
    private GameObject lastPixel;
    private ArrayList arrPixels;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        firstPixel = null;
        lastPixel = null;
        arrPixels = new ArrayList();

        rend.material.SetColor("_Color", Color.black);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;

        if (Input.GetMouseButton(0) && lastPaintedPixelPos != mousePos)
        {  
            paintNewPixel();
        } else if (!Input.GetKey(KeyCode.Mouse0) && pixelPainted)
        {
            groupSinglePixels();
        } else if (Input.GetMouseButton(1) && lastPaintedPixelPos != mousePos)
        {
            deletePixelInPosition(mousePos);
        }

        if (Input.GetKey(KeyCode.Escape) && arrPixels.Count > 0)
        {
            cancelActualDrawing();
        }
    }

    void paintNewPixel()
    {
        if (inkBarController.currentInk == 0) return;
        rend.material.SetColor("_Color", Color.grey);

        GameObject instantiatedPixel = Instantiate(pixelPrefab);
        instantiatedPixel.name = "blackPixel " + i.ToString();
        i++;

        if (firstPixel == null) {
            firstPixel = instantiatedPixel;
        }
        lastPixel = instantiatedPixel;

        arrPixels.Add(instantiatedPixel);
        lastPaintedPixelPos = mousePos;
        pixelPainted = true;

        //instantiatedPixel is painted grey
        //que el pixel final esté suficientemente cerca del inicial
        if (areTheseClose(firstPixel, lastPixel))
        {
            changeActualDrawingPixelsColor(Color.black);
        }
        else //lo dejo como antes
        {
            changeActualDrawingPixelsColor(Color.grey);
        }

        inkBarController.currentInk--;
        if (inkBarController.currentInk < 0) inkBarController.currentInk = 0;
    }

    void groupSinglePixels()
    {
        GameObject auxFirstPixel = firstPixel;
        GameObject auxLastPixel = lastPixel;

        pixelPainted = false;
        firstPixel = null;
        lastPixel = null;
        if (arrPixels.Count == 0) return;
        rend.material.SetColor("_Color", Color.black);
        
        //all the pixels from arrPixels are painted to black
        GameObject drawing = new GameObject("drawing " + j);
        j++;

        foreach (GameObject obj in arrPixels)
        {
            obj.transform.SetParent(drawing.transform);
            obj.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            obj.GetComponent<BoxCollider2D>().enabled = true;
        }

        if (areTheseClose(auxFirstPixel, auxLastPixel))
        {
            Rigidbody2D drawingRigidbody = drawing.AddComponent<Rigidbody2D>();
            drawingRigidbody.mass = arrPixels.Count;
        }

        arrPixels.Clear();
    }

    void cancelActualDrawing()
    {
        pixelPainted = false;
        firstPixel = null;
        lastPixel = null;
        rend.material.SetColor("_Color", Color.black);

        foreach (GameObject obj in arrPixels)
        {
            Destroy(obj);
        }

        inkBarController.currentInk += arrPixels.Count;
        arrPixels.Clear();
    }

    void changeActualDrawingPixelsColor(Color color)
    {
        foreach (GameObject obj in arrPixels)
        {
            obj.GetComponent<Renderer>().material.SetColor("_Color", color);
        }
    }

    bool areTheseClose(GameObject obj1, GameObject obj2)
    {
        int obj1Index = int.Parse(obj1.name.Substring(obj1.name.IndexOf(" ") + 1));
        int obj2Index = int.Parse(obj2.name.Substring(obj2.name.IndexOf(" ") + 1));
        if (obj2Index <= obj1Index + 15) return false;

        return Vector2.Distance(obj1.transform.position, obj2.transform.position) <= obj1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void deletePixelInPosition(Vector3 pos)
    {
        GameObject[] pixels = GameObject.FindGameObjectsWithTag("BlackPixel");

        foreach (GameObject pixel in pixels)
        {
            if (Vector2.Distance(pixel.transform.position, pos) <= pixel.GetComponent<SpriteRenderer>().bounds.size.x)
            {
                Destroy(pixel);
                inkBarController.currentInk++;
                if (inkBarController.currentInk > inkBarController.maxInk) inkBarController.currentInk = inkBarController.maxInk;
                break;
            }
        }
    }
}
