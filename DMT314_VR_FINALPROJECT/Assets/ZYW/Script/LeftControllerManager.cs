using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LeftControllerManager : MonoBehaviour
{
    public GameObject baseController;
    public GameObject RayCastController;

    public InputActionReference ActivateRayCast;
    public InputActionReference CancelRayCast;
    [Space]

    public UnityEvent onRayCastActivate;
    public UnityEvent onRayCastCancel;

    // Start is called before the first frame update
    void Start()
    {
        ActivateRayCast.action.performed += RayCastModeActivate;
        CancelRayCast.action.performed += RayCastModeCancel;
    }

    public void RayCastModeActivate(InputAction.CallbackContext obj)
    {
        onRayCastActivate.Invoke();
        
    }

    public void RayCastModeCancel(InputAction.CallbackContext obj)
    {
        //onRayCastCancel.Invoke();
        Invoke("DeactivateRayCast", 0.1f);
    }




    // Update is called once per frame
    void Update()
    {

    }
}
