using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScrollManager : MonoBehaviour
{
    public float scrollSpeed = 5.0f;
    public GameObject Map;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Map.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
    }
}
