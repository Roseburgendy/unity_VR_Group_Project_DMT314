using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class SnapTurnToggle : MonoBehaviour
{
    public InputActionReference ToggleSnapTurn;
    bool SnapToggle = false;
    [Space] 
    public UnityEvent onSnapTurn;
    public UnityEvent onContinuousTurn;
    // Start is called before the first frame update
    void Start()
    {
        ToggleSnapTurn.action.performed += ToggleTurnType;
    }

    public void ToggleTurnType(InputAction.CallbackContext obj)
    {
        SnapToggle = !SnapToggle;
        if(SnapToggle == true) {
            onSnapTurn.Invoke();
        }
        else
        {
            onContinuousTurn.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
