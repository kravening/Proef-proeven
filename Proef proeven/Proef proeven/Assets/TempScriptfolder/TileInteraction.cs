using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<OnClickInteraction>().onInteractionReceived += OnClick;
    }

    public void OnClick()
    {
        
    }
}
