using UnityEngine;

public class RotateAtTick : MonoBehaviour
{
    [SerializeField] private Vector3 rotationPerTick;

    // Update is called once per frame
    void Update()
    {
        Vector3 newRotation = transform.eulerAngles;
        newRotation.y += rotationPerTick.y;
        transform.rotation = Quaternion.Euler(newRotation);
    }
}
