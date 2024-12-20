using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCarInteraction : MonoBehaviour
{
    public Transform snapPointAboard;
    public Transform snapPointGetOff;
    public GameObject player;
    public Animator carAnimator;
    public GameObject aboardUI;
    public GameObject getOffUI;
    public GameObject driveBackUI;

    public AnimationClip driveToGarageClip; // Assign the "Drive to Garage" animation clip in Inspector
    public AnimationClip driveBackClip;    // Assign the "Drive Back" animation clip in Inspector
    private string carDestination = "farm"; // Track current destination: "farm" or "garage"
    // Start is called before the first frame update
    void Start()
    {
        carAnimator = GetComponent<Animator>();
    }

    // Function to aboard(call by ui btn)
    public void Aboard()
    {
        // Hide the aboard button
        aboardUI.gameObject.SetActive(false);

        /* Snap the player position on to the snapPointAboard
        and set the player's parent to be the car in order to make
        the player move with the car
        */
        // Snap the player to the car and wait before starting animation
        StartCoroutine(SnapAndStartAnimation(snapPointAboard, "DriveToGarage", "garage", getOffUI, driveToGarageClip.length));

    }

    // Function to get off the car(call by ui btn)
    public void GetOff()
    {
         /* Snap the player position on to the snapPointGetOff
        and release player
        */
        player.transform.position=snapPointGetOff.position;
        player.transform.rotation=snapPointGetOff.rotation;
        player.transform.SetParent(null);

        // Hide the Get Off UI
        getOffUI.SetActive(false);

        if (carDestination == "garage")
        {
            // Show Drive Back UI if the car is at the garage
            driveBackUI.SetActive(true);
        }
        else if (carDestination == "farm")
        {
            // Show Aboard UI if the car is back at the farm
            aboardUI.SetActive(true);
        }
    }

    public void DriveBack()
    {
        // Hide the driveback UI
        driveBackUI.SetActive(false);

        /* Snap the player position on to the snapPointAboard
        and set the player's parent to be the car in order to make
        the player move with the car
        */
        // Snap the player to the car and wait before starting animation
        StartCoroutine(SnapAndStartAnimation(snapPointAboard, "DriveToFarm", "farm", getOffUI, driveBackClip.length));
    }

    private IEnumerator SnapAndStartAnimation(Transform snapPoint, string animationTrigger, string destination, GameObject uiAfterAnimation, float animationDuration)
    {
        // Snap player to the specified snap point
        player.transform.position = snapPoint.position;
        player.transform.rotation = snapPoint.rotation;
        player.transform.SetParent(snapPoint.parent);

        // Wait for 2 seconds before starting the animation
        yield return new WaitForSeconds(2f);

        // Trigger the car animation
        carAnimator.SetTrigger(animationTrigger);

        // Update the car's destination
        carDestination = destination;

        // Start coroutine to show the specified UI after the animation ends
        StartCoroutine(ShowUIAfterAnimation(animationDuration, uiAfterAnimation));

    }
    private IEnumerator ShowUIAfterAnimation(float duration, GameObject uiElement)
    {
        // Wait for the animation duration
        yield return new WaitForSeconds(duration);

        // Show the specified UI element
        uiElement.SetActive(true);
    }
}
