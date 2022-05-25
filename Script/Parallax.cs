using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform Camera;

    public float moveRate;
    private float startPointX, startPointY;
    public bool lockY;
    // Start is called before the first frame update
    void Start()
    {
        startPointX = transform.position.x;
        startPointY = transform.position.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lockY){
            transform.position = new Vector2(startPointX + Camera.position.x * moveRate, transform.position.y);
        } else 
        {
            transform.position = new Vector2(startPointX + Camera.position.x * moveRate, startPointY + Camera.position.y * moveRate);
        }
        
    }
}
