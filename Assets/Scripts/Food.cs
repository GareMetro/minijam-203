using DG.Tweening;
using UnityEngine;

public class Food : MonoBehaviour
{

    public BaseIngredient baseIngredient;

    public void Disapear(float duration = 0.25f)
    {
        DOTween.Sequence()
        .Append(transform.DOScale(0, duration))
        .SetEase(Ease.InSine)
        .Join(transform.DOShakeRotation(duration,50f, 5))
        .AppendCallback(() => Destroy(gameObject));
    }

    public void DisapearFalling(Vector3 moveDir)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.linearVelocity = 1.2f * moveDir * Grid.GridInstance.tileSize * Grid.GridInstance.TickDuration;
        rb.angularVelocity = Random.onUnitSphere * 1.5f;

        DOTween.Sequence().Append(transform.DOScale(0, 0.8f)).SetEase(Ease.InBack).AppendCallback(() => Destroy(gameObject));
    }

    public void BadBoing()
    {
        transform.DOShakeRotation(0.3f,15f);
    }
}
