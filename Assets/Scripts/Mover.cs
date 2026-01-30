using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using System.Linq;

public class Mover : MonoBehaviour
{
    [SerializeField] List<Transform> wayPoints;

    public void MoveObject(Transform transform)
    {
        Vector3[] positions = new Vector3[wayPoints.Count + 1];
        positions.Append(transform.position);
        foreach(Transform wayPoint in wayPoints)
            positions.Append(wayPoint.transform.position);

        transform.DOPath(positions, Grid.GridInstance.TickDuration);
    }
}
