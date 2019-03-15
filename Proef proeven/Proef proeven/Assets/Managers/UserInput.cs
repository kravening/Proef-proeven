using UnityEngine;

namespace Managers
{
    public class UserInputManager : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            Tap();
        }

        public void Tap()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Camera.main == null)
                {
                    return;
                }

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    hit.transform.GetComponent<OnClickInteraction>().Interact();
                }
            }
        }
    }
}