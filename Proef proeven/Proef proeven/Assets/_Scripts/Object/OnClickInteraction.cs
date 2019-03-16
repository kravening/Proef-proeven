using UnityEngine;

public class OnClickInteraction : MonoBehaviour
{
    public delegate void eventRaiser();
    public event eventRaiser OnInteractionReceived;

    public void Interact()
    {
        OnInteractionReceived();
    }
}
