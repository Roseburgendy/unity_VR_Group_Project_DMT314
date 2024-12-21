using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CultivationInteractionHandler : MonoBehaviour
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

        // Add listeners for hover events
        leftHandRayInteractor.hoverEntered.AddListener(OnObstacleHovered);
    }

    private void OnDestroy()
    {
        // Remove listeners
        rake.selectEntered.RemoveListener(OnRakeGrabbed);
        rake.selectExited.RemoveListener(OnRakeReleased);

        leftHandRayInteractor.hoverEntered.RemoveListener(OnObstacleHovered);
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

    private void OnObstacleHovered(HoverEnterEventArgs args)
    {
        // Ensure rake is grabbed before interacting
        if (!rakeGrabbed) return;

        // Check if the hovered object is an obstacle
        var obstacle = args.interactableObject.transform.GetComponent<Collider>();
        if (obstacle != null && ((1 << obstacle.gameObject.layer) & obstacleLayer) != 0)
        {
            // Destroy the obstacle
            Destroy(obstacle.gameObject);
            Debug.Log($"Destroyed obstacle: {obstacle.gameObject.name}");
        }
    }
}
