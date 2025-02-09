﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianControlerVisualization : Civilian
{
    public List<Vector2> positions;
    public float velocity = 3f;
    public float smoothFactor = 0.75f;

    private int positionsIndex = 0;
    private Vector3 nextPosition;
    private float distance;

    private const float EPSILON = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = transform.position;
    }

    private void RotateView()
    {
        Vector3 direction = (nextPosition - transform.position).normalized;

        if (System.Math.Abs(direction.x - 1) < EPSILON)
        {
            transform.Rotate(new Vector3(0, 0, 270) - transform.rotation.eulerAngles);
        }
        else if (System.Math.Abs(direction.x - (-1)) < EPSILON)
        {
            transform.Rotate(new Vector3(0, 0, 90) - transform.rotation.eulerAngles);
        }
        else if (System.Math.Abs(direction.y - 1) < EPSILON)
        {
            transform.Rotate(new Vector3(0, 0, 360) - transform.rotation.eulerAngles);
        }
        else if (System.Math.Abs(direction.y - (-1)) < EPSILON)
        {
            transform.Rotate(new Vector3(0, 0, 180) - transform.rotation.eulerAngles);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (!AreSameVectors(nextPosition, transform.position))
            {
                UpdateMovement();
            }
            else if (AreSameVectors(nextPosition, transform.position))
            {
                NextMovement();
                RotateView();
            }
        }
    }

    private void UpdateMovement()
    {
        float step = velocity * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, step);
    }

    private void NextMovement()
    {
        nextPosition = transform.position + (Vector3)positions[positionsIndex];

        distance = Mathf.Abs((transform.position - nextPosition).magnitude);

        positionsIndex = (positionsIndex + 1) % positions.Count;
    }

    private bool AreSameVectors(Vector3 a, Vector3 b)
    {
        return Mathf.Abs((a - b).magnitude) < 0.05f;
    }
}
