using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool test;
    public float speed = 4;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Clockwise", 0, 2f / speed);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + (Vector3)dir;
        transform.position = transform.position + (speed * Time.deltaTime * transform.right);
    }

    void Clockwise()
    {
        transform.Rotate(transform.forward, -90);
    }
    //void Move(Vector2 dir)
    //{
    //    //anims
    //    this.dir = dir;
    //}
}
