using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTween
{
    public Vector2 StartPos { get; private set; }
    public Vector2 EndPos { get; private set; }
    public float StartTime { get; private set; }
    public float Duration { get; private set; }
    public SimpleTween(Vector2 StartPos, Vector2 EndPos, float StartTime, float Duration)
    {
        this.StartPos = StartPos;
        this.EndPos = EndPos;
        this.StartTime = StartTime;
        this.Duration = Duration;
    }
    public void Link(Vector2 newPos)
    {
        StartPos = EndPos;
        EndPos = newPos;
        StartTime = Time.time;
    }
}
