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
    }

    private void OnDestroy()
    {
        // Remove listeners
        wateringCan.selectEntered.RemoveListener(OnWateringCanGrabbed);
        wateringCan.selectExited.RemoveListener(OnWateringCanReleased);
    }

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


    private void OnWateringCanGrabbed(SelectEnterEventArgs args)
    {
        wateringCanGrabbed = true;
    }

    private void OnWateringCanReleased(SelectExitEventArgs args)
    {
        wateringCanGrabbed = false;
    }

    // Method to handle cultivation interaction
    private void HandleWateringInteraction()
    {
        // Raycast from left hand ray interactor
        Ray ray = new Ray(leftHandRayInteractor.transform.position, leftHandRayInteractor.transform.forward);
        // Detect obstacle
        if(Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity, fieldLayer))
        {
            Wy_GameManager.instance.wateringEffect.SetActive(true);
            GameObject field = hit.collider.gameObject;
            field.gameObject.GetComponentInParent<WheatField>().TriggerWateringEffect();           
        }

    }
}
