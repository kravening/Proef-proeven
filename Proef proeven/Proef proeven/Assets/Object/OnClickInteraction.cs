using UnityEngine;

public class OnClickInteraction : MonoBehaviour
{
    public delegate void eventRaiser();
    public event eventRaiser onInteractionReceived;

    public void Interact()
    {
        onInteractionReceived();
    }
}
