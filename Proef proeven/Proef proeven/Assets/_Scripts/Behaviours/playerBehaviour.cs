using System.Collections;
using UnityEngine;

public class playerBehaviour : MonoBehaviour
{
    public void MovePlayer(Vector3 toPosition)
    {
        StartCoroutine(MovePlayerCoroutine(toPosition));
    }

    private IEnumerator MovePlayerCoroutine(Vector3 nextPosition)
    {
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