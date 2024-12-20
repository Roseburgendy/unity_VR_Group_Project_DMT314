using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WateringInteractionHandler : MonoBehaviour
{
    public XRDirectInteractor rightHandBaseInteractor;
    public XRRayInteractor leftHandRayInteractor;
    public XRGrabInteractable rake;
    public LayerMask obstacleLayer;
    //public GameObject destroyEffect;

    private bool rakeGrabbed=false;

    private void Start()
    {
        // Add events to rake on runtime
        rake.selectEntered.AddListener(OnRakeGrabbed);
        rake.selectExited.AddListener(OnRakeReleased);
    }

    private void OnDestroy()
    {
        // Remove listeners
        rake.selectEntered.RemoveListener(OnRakeGrabbed);
        rake.selectExited.RemoveListener(OnRakeReleased);
    }

    private void Update()
    {
        // Condition to cultivation interaction:
        // 1. grab the rake
        // 2. activate ray in left hand
        if(rakeGrabbed && leftHandRayInteractor.enabled)
        {
            Debug.Log("Rake Grabbed!");
            HandleCultivationInteraction();
        }
    }


    private void OnRakeGrabbed(SelectEnterEventArgs args)
    {
        rakeGrabbed = true;
    }

    private void OnRakeReleased(SelectExitEventArgs args)
    {
        rakeGrabbed = false;
    }

    // Method to handle cultivation interaction
    private void HandleCultivationInteraction()
    {
        // Raycast from left hand ray interactor
        Ray ray = new Ray(leftHandRayInteractor.transform.position, leftHandRayInteractor.transform.forward);
        // Detect obstacle
        if(Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity, obstacleLayer))
        {
            GameObject obstacle = hit.collider.gameObject;
            
            // Destroy obstacles if detected
            Destroy(obstacle);
            Debug.Log($"Destroyed obstacle: {obstacle.name}");
        }

    }
}
