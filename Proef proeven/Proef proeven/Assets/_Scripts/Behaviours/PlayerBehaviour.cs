using System.Collections;
using Data;
using Managers;
using UnityEngine;

namespace Behaviours
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public AnimationCurve _heightAnimationCurve;

        private bool isAnimating = false;

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
                timer += Time.deltaTime * 2;

                Vector3 differenceVector = startPosition - nextPosition;

                Vector3 lerpedVector = startPosition - (differenceVector * timer);

                Vector3 finalVector = new Vector3(lerpedVector.x, startPosition.y + _heightAnimationCurve.Evaluate(timer), lerpedVector.z);

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
}