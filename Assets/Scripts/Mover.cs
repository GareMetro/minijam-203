using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using System.Linq;
using UnityEditor;

public class Mover : MonoBehaviour
{
    [SerializeField] List<Transform> wayPoints = new();

    public void MoveObject(Transform toMove, float duration)
    {
        List<Vector3> positions = new()
        {
            toMove.position
        };

        foreach(Transform wayPoint in wayPoints)
            positions.Add(wayPoint.transform.position);

        toMove.DOPath(positions.ToArray(), duration).SetEase(Ease.Linear);
    }

    public IEnumerator MoveObjectRoutine(Transform toMove,  float duration)
    {
        MoveObject(toMove, duration);

        yield return new WaitForSeconds(duration);
    }
    

    private void OnDrawGizmos() {
        foreach (var item in wayPoints)
        {
            if(item)
                Gizmos.DrawSphere(item.position, 0.05f);
        }
    }
}
