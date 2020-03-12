using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string interactDescription;
    public UnityEvent onInteract;

    public void Interact()
    {
        if (onInteract != null)
            onInteract.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
