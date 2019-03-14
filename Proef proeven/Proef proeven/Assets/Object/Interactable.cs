using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public delegate void eventRaiser();
    public event eventRaiser interactionReceived;

    public void Interact()
    {
        interactionReceived();
    }
}
