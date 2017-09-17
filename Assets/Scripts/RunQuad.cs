using UnityEngine;
using System.Collections;

public class RunQuad : MonoBehaviour {

    public float speed = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0f, -Time.time * speed);
    }
}
