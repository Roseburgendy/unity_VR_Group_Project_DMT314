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
        player.transform.position = snapPointAboard.position;
        player.transform.rotation = snapPointAboard.rotation;
        player.transform.SetParent(snapPointAboard.parent);


        // Trigger car animation
        carAnimator.SetTrigger("DriveToGarage");

        // Set destination to garage
        carDestination = "garage";

        // Start coroutine to show the "Get Off" UI after animation ends(reach the garage)
        StartCoroutine(ShowUIAfterAnimation(driveToGarageClip.length, getOffUI));

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
        player.transform.position = snapPointAboard.position;
        player.transform.rotation = snapPointAboard.rotation;
        player.transform.SetParent(snapPointAboard.parent);


        // Trigger car animation
        carAnimator.SetTrigger("DriveToFarm");

        // Set destination to farm
        carDestination = "farm";

        // Start coroutine to show the "Aboard" UI after animation ends(drive back to farm)
        StartCoroutine(ShowUIAfterAnimation(driveBackClip.length, getOffUI));
    }
    private IEnumerator ShowUIAfterAnimation(float duration, GameObject uiElement)
    {
        // Wait for the animation duration
        yield return new WaitForSeconds(duration);

        // Show the specified UI element
        uiElement.SetActive(true);
    }
}
