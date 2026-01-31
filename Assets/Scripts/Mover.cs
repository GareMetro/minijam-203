using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using System.Linq;
using UnityEditor;

public class Mover : MonoBehaviour
{
    [SerializeField] List<Transform> wayPoints;

    public void MoveObject(Transform toMove)
    {
        List<Vector3> positions = new()
        {
            toMove.position
        };

        foreach(Transform wayPoint in wayPoints)
            positions.Add(wayPoint.transform.position);
        
        toMove.DOPath(positions.ToArray(), Grid.GridInstance.TickDuration).SetEase(Ease.Linear);
    }
    

    private void OnDrawGizmos() {
        foreach (var item in wayPoints)
        {
            Gizmos.DrawSphere(item.position, 0.2f);
        }
    }
}
