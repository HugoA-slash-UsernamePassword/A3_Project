using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool test;
    public float speed = 4;
    public Vector2[] points;
    public int curPoint;
    private SimpleTween path;
    void Start()
    {
        //InvokeRepeating("Clockwise", 0, 2f / speed); //testing
        path = new SimpleTween(transform.position, points[0], Time.time, 1);
        transform.Rotate(transform.forward, 90);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, path.EndPos) > 0.01f)
        {
            float timeFraction = (Time.time - path.StartTime) / path.Duration;
            transform.position = Vector3.Lerp(path.StartPos, path.EndPos, timeFraction);
        }
        else
        {
            //transform.position = path.EndPos;
            curPoint++;
            if (curPoint == points.Length) curPoint = 0;
            path.Link(points[curPoint]);
            transform.Rotate(transform.forward, -90);
        }
        //transform.position = transform.position + (Vector3)dir;
        //transform.position = transform.position + (speed * Time.deltaTime * transform.right);
    }

    //void Clockwise() //testing
    //{
    //    transform.Rotate(transform.forward, -90);
    //}

    void Move(Vector2 dir)
    {
        //anims

        //movement
        //this.dir = dir;
    }
}
