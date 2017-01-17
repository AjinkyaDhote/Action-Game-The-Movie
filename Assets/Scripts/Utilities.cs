using UnityEngine;
using System.Collections;

public static class Utilities
{

    private static readonly float THETA_SCALE = 0.01f;
    public static Vector3[] GenerateCirclePoints(float radius)
    {
        float theta = 0.0f;
        int circleSize = (int)((1f / THETA_SCALE) + 1f);
        Vector3[] circlePoints = new Vector3[circleSize];
        for (int j = 0; j < circleSize; j++)
        {
            theta += (2.0f * Mathf.PI * THETA_SCALE);
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);
            circlePoints[j] = new Vector3(x, y, 0);
        }
        return circlePoints;
    }
}
