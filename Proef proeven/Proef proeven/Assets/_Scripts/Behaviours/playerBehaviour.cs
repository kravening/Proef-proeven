using System.Collections;
using UnityEngine;

public class playerBehaviour : MonoBehaviour
{
    public AnimationCurve _heightAnimationCurve;

    public void MovePlayer(Vector3 toPosition)
    {
        StartCoroutine(MovePlayerCoroutine(toPosition));
    }

    private IEnumerator MovePlayerCoroutine(Vector3 nextPosition)
    {
        transform.position = nextPosition;
        float timer = 0;
        
        Vector3 startPosition = transform.position;

        while (timer < 1)
        {
            timer += Time.deltaTime;

            Vector3 addHeightToPosition = new Vector3(transform.position.x, startPosition.y + _heightAnimationCurve.Evaluate(timer), transform.position.z);
            Vector3 addHeightToNextPosition = new Vector3(nextPosition.x, nextPosition.y + _heightAnimationCurve.Evaluate(timer), nextPosition.z);

            Vector3.Lerp(addHeightToPosition, addHeightToNextPosition, 1f);

            yield return new WaitForSeconds(0);
        }

        transform.position = nextPosition;

        yield return new WaitForEndOfFrame();
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