using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SplineCreator : MonoBehaviour
{
    [SerializeField] Transform splineTarget;
    [SerializeField] LineRenderer lineRenderer;

    [Foldout("Info"), SerializeField, ReadOnly] Vector3[] previousPoints;
    [Foldout("Info"), SerializeField, ReadOnly] float activeCurvePointCount;
    [Foldout("Info"), SerializeField, ReadOnly] float distance;

    [Foldout("Values"), SerializeField] float segmentLength = 10f;
    [Foldout("Values"), SerializeField] int maxCurvePointCount = 7;
    [Foldout("Values"), SerializeField, MinMaxSlider(0, 3)] Vector2 timeMultiplierX = new Vector2(0.5f, 1.5f);
    [Foldout("Values"), SerializeField, MinMaxSlider(0, 3)] Vector2 timeMultiplierY = new Vector2(0.4f, 1.6f);
    [Foldout("Values"), SerializeField, MinMaxSlider(-5, 5)] Vector2 OffsetMultiplierX = new Vector2(-3, 4f);
    [Foldout("Values"), SerializeField, MinMaxSlider(-5, 5)] Vector2 OffsetMultiplierY = new Vector2(-4f, 3f);
    [Foldout("Values"), SerializeField] AnimationCurve yOffsetStrengthByDistance = AnimationCurve.EaseInOut(0, 0, 100, 33);
    [Foldout("Values"), SerializeField, Range(0, 1)] float globalCurveStrenthMultiplier = 0.1f;
    [Foldout("Values"), SerializeField, Range(0, 5)] int smoothLoopCount = 3;

    private void Update()
    {
        List<Vector3> points = new List<Vector3>();

        Vector2 start = transform.position;
        Vector2 end = splineTarget.position;

        Vector2 forward = (end - start).normalized;


        distance = Vector2.Distance(start, end);
        activeCurvePointCount = Mathf.RoundToInt(distance / segmentLength);

        points.Add(start);

        for (int i = 0; i < maxCurvePointCount; i++)
        {
            Vector2 raw = end;

            //raw
            if (i < activeCurvePointCount)
            {
                bool isEven = (i % 2 == 0);
                float curveValue = Mathf.Sin((float)i / (maxCurvePointCount) * Mathf.PI) * distance * globalCurveStrenthMultiplier;
                float yOffset = (isEven ? 1 : -1) * yOffsetStrengthByDistance.Evaluate(distance) + curveValue;

                Random.InitState(i);

                float randTimeMultiplierX = Random.Range(this.timeMultiplierX.x, this.timeMultiplierX.y);
                float randTimeMultiplierY = Random.Range(this.timeMultiplierY.x, this.timeMultiplierY.y);

                Vector2 randomOffset = new Vector2(Mathf.Sin(Time.time * randTimeMultiplierX) * Random.Range(OffsetMultiplierX.x, OffsetMultiplierX.y), Mathf.Sin(Time.time * randTimeMultiplierY) * Random.Range(OffsetMultiplierY.x, OffsetMultiplierY.y));

                raw = Vector2.Lerp(start, end, (float)((i + 0.5f) / maxCurvePointCount)) + Vector2.Perpendicular(forward) * yOffset + randomOffset;
            }

            //lerp
            if (previousPoints != null && previousPoints.Length > i)
            {
                raw = Vector2.Lerp(previousPoints[Mathf.Min(i + 1, previousPoints.Length - 1)], raw, Time.deltaTime);
            }

            points.Add(raw);
        }

        points.Add(end);

        previousPoints = points.ToArray();

        for (int i = 0; i < smoothLoopCount; i++)
            points = Smooth(points);


        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Vector3 point in previousPoints)
        {
            Gizmos.DrawWireSphere(point, 1f);
        }
    }

    private List<Vector3> Smooth(List<Vector3> points)
    {
        List<Vector3> result = new List<Vector3>();

        result.Add(points[0]);

        for (int i = 1; i < points.Count; i++)
        {
            Vector2 previous = points[i - 1];
            Vector2 current = points[i];

            result.Add(Vector2.Lerp(previous, current, 0.2f));
            result.Add(Vector2.Lerp(previous, current, 0.8f));
        }

        result.Add(points[points.Count - 1]);

        return result;
    }
}
