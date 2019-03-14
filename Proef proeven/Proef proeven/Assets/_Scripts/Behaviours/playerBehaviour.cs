using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public AnimationCurve _heightAnimationCurve;

    private bool isAnimating = false;

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            MovePlayer(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z));
        }
    }

    public void MovePlayer(Vector3 toPosition)
    {
        if (isAnimating)
        {
            return;
        }
        StartCoroutine(MovePlayerCoroutine(toPosition));
    }

    private IEnumerator MovePlayerCoroutine(Vector3 nextPosition)
    {
        float timer = 0;
        isAnimating = true;
        Vector3 startPosition = transform.position;

        while (timer < 1)
        {
            timer += Time.deltaTime;

            Vector3 differenceVector = startPosition - nextPosition;

            Vector3 LerpedVector = startPosition - (differenceVector * timer);

            Vector3 finalVector = new Vector3(LerpedVector.x, startPosition.y + _heightAnimationCurve.Evaluate(timer), LerpedVector.z);

            transform.position = finalVector;

            yield return null;
        }

        transform.position = nextPosition;
        isAnimating = false;
    }

    public void DestroyPlayer()
    {
        StartCoroutine(DestroyPlayerCoroutine());
    }

    private IEnumerator DestroyPlayerCoroutine()
    {
        // play animations and what not
        yield return new WaitForEndOfFrame();
        Destroy(this.gameObject);
    }
}