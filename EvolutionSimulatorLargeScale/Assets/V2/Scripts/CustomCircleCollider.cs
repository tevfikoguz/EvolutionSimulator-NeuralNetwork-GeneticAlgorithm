﻿using UnityEngine;
using System.Collections;
using System;

public class CustomCircleCollider
{
    public float radius;

    public Vector3 position;
    public float rotation;
    
    public float veloForward;
    public float veloAngular;

    public float mass;

    private float worldDeltaTime;

    public CustomCircleCollider(float radius, Vector3 position, float angle, float veloForward, float veloAngular, float mass, float worldDetlaTime)
    {
        this.position = position;
        this.radius = radius;
        this.position = position;
        this.rotation = angle;
        this.veloAngular = veloAngular;
        this.veloForward = veloForward;
        this.mass = mass;
        this.worldDeltaTime = worldDetlaTime;
    }

    public void UpdateColliderPhysics(float accelForward, float accelAngular)
    {
        float unitAngle = rotation - 90f;
        Vector3 newUnit = new Vector3(Mathf.Cos(unitAngle * Mathf.Deg2Rad), Mathf.Sin(unitAngle * Mathf.Deg2Rad), 0f);

        /*veloForward += accelForward * worldDeltaTime;
        veloAngular += accelAngular * worldDeltaTime * 10f;
        veloForward *= (1f-0.04f);
        veloAngular *= (1f-0.004f);*/

        veloForward = accelForward * worldDeltaTime * 10f;
        veloAngular = accelAngular * worldDeltaTime * 2000f;

        position += newUnit * veloForward /** worldDeltaTime * 10f*/;
        rotation += veloAngular /** worldDeltaTime *100f*/;
    }

    public bool CollisionCheckWithPoint(Vector2 point)
    {
        float radiusSqr = Mathf.Pow(radius, 2f);
        float distanceBetweenPointAndCircle = Mathf.Pow((position.x - point.x), 2f) + Mathf.Pow((position.y - point.y), 2f);

        if (distanceBetweenPointAndCircle <= radiusSqr)
        {
            return true;
        }

        return false;
    }

    public bool CollisionCheckWithCircle(float circleRadius, Vector2 circlePoint)
    {
        float distance = Mathf.Sqrt(Mathf.Pow(circlePoint.x - position.x, 2f) + Mathf.Pow(circlePoint.y - position.y, 2f));


        if (distance <= (radius + circleRadius))
        {
            return true;
        }

        return false;
    }

    // Find the points of intersection.
    public bool IsLineIntersectingWithCircle( Vector2 point1, Vector2 point2)
    {
        float dx, dy, A, B, C, det;

        dx = point2.x - point1.x;
        dy = point2.y - point1.y;

        A = dx * dx + dy * dy;
        B = 2 * (dx * (point1.x - position.x) + dy * (point1.y - position.y));
        C = (point1.x - position.x) * (point1.x - position.x) +
            (point1.y - position.y) * (point1.y - position.y) -
            radius * radius;

        det = B * B - 4 * A * C;
        if ((A <= 0.0000001) || (det < 0))
        {
            return false;
        }

        return true;
    }
}
