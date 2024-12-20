using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WateringInteractionHandler : MonoBehaviour
{
    public XRDirectInteractor rightHandBaseInteractor;
    public XRRayInteractor leftHandRayInteractor;
    public XRGrabInteractable wateringCan;
    public LayerMask fieldLayer;
    //public GameObject destroyEffect;

    private bool wateringCanGrabbed=false;

    private void Start()
    {
        // Add events to wateringCan on runtime
        wateringCan.selectEntered.AddListener(OnWateringCanGrabbed);
        wateringCan.selectExited.AddListener(OnWateringCanReleased);

        // Add events to left-hand ray interactor for watering
        leftHandRayInteractor.hoverEntered.AddListener(OnFieldHovered);
        leftHandRayInteractor.hoverExited.AddListener(OnFieldHoverExited);
    }

    private void OnDestroy()
    {
        // Remove listeners
        wateringCan.selectEntered.RemoveListener(OnWateringCanGrabbed);
        wateringCan.selectExited.RemoveListener(OnWateringCanReleased);

        leftHandRayInteractor.hoverEntered.RemoveListener(OnFieldHovered);
        leftHandRayInteractor.hoverExited.RemoveListener(OnFieldHoverExited);
    }

    /*
    private void Update()
    {
        // Condition to watering interaction:
        // 1. grab the wateringCan
        // 2. activate ray in left hand
        if (wateringCanGrabbed && leftHandRayInteractor.enabled)
        {
            Debug.Log("WateringCan Grabbed!");
            HandleWateringInteraction();
        }
    }
    */

    private void OnWateringCanGrabbed(SelectEnterEventArgs args)
    {
        wateringCanGrabbed = true;
        Debug.Log("Watering can grabbed!");
    }

    private void OnWateringCanReleased(SelectExitEventArgs args)
    {
        wateringCanGrabbed = false;
        Debug.Log("Watering can released!");
    }


    private void OnFieldHovered(HoverEnterEventArgs args)
    {
        if (!wateringCanGrabbed) return; // Only allow interaction if watering can is grabbed

        // Ensure the interactable is a WheatField
        var field = args.interactableObject.transform.GetComponent<WheatField>();
        if (field != null)
        {
            Debug.Log("Field selected for watering!");
            field.StartWatering();
        }
    }

    private void OnFieldHoverExited(HoverExitEventArgs args)
    {
        if (!wateringCanGrabbed) return; // Only allow interaction if watering can is grabbed
        // Ensure the interactable is a WheatField
        var field = args.interactableObject.transform.GetComponent<WheatField>();
        if (field != null)
        {
            Debug.Log("Stopped watering field!");
            field.StopWatering();
        }
    }
}
