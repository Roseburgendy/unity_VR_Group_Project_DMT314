using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PuzzleManager : MonoBehaviour
{
    [Header("Letter GameObjects")]
    public GameObject letter_W;
    public GameObject letter_H;
    public GameObject letter_E;
    public GameObject letter_A;
    public GameObject letter_T;

    [Header("Snap Zones")]
    public XRSocketInteractor snapZone_W;
    public XRSocketInteractor snapZone_H;
    public XRSocketInteractor snapZone_E;
    public XRSocketInteractor snapZone_A;
    public XRSocketInteractor snapZone_T;

    [Header("Wheat Fields")]
    public List<WheatField> wheatFields; // List of all wheat fields

    private Dictionary<XRSocketInteractor, string> snapZoneMapping; // Map snap zones to correct letters
    private Dictionary<XRSocketInteractor, bool> snapZoneState; // Track if each snap zone is complete

    [Header("Puzzle UIs")]
    public GameObject QuestionUI;

    private void Start()
    {
        // Initialize snap zone mapping
        snapZoneMapping = new Dictionary<XRSocketInteractor, string>
        {
            { snapZone_W, "W" },
            { snapZone_H, "H" },
            { snapZone_E, "E" },
            { snapZone_A, "A" },
            { snapZone_T, "T" }
        };

        // Initialize snap zone state
        snapZoneState = new Dictionary<XRSocketInteractor, bool>();
        foreach (var snapZone in snapZoneMapping.Keys)
        {
            snapZone.selectEntered.AddListener(OnLetterSnapped);
            snapZoneState[snapZone] = false; // Initially all zones are incomplete
        }

        // Deactivate letters at the start
        letter_W.SetActive(false);
        letter_H.SetActive(false);
        letter_E.SetActive(false);
        letter_A.SetActive(false);
        letter_T.SetActive(false);
    }

    private void OnDestroy()
    {
        foreach (var snapZone in snapZoneMapping.Keys)
        {
            snapZone.selectEntered.RemoveListener(OnLetterSnapped);
        }
    }

    private void Update()
    {
        // Check if all fields are growing
        if (AllFieldsGrowing())
        {
            Debug.Log("All fields are growing! Activating letters for the puzzle.");
            ActivatePuzzleAndLetters();
        }
    }

    private void ActivatePuzzleAndLetters()
    {
        QuestionUI.SetActive(true);
        letter_W.SetActive(true);
        letter_H.SetActive(true);
        letter_E.SetActive(true);
        letter_A.SetActive(true);
        letter_T.SetActive(true);

        Debug.Log("Letters W, H, E, A, T activated in the field.");
    }

    private void OnLetterSnapped(SelectEnterEventArgs args)
    {
        var snappedObject = args.interactableObject.transform.gameObject;

        foreach (var snapZone in snapZoneMapping.Keys)
        {
            if (snapZone.hasSelection && snapZone.GetOldestInteractableSelected().transform.gameObject == snappedObject)
            {
                string expectedLetter = snapZoneMapping[snapZone];
                string snappedLetter = snappedObject.name; // Ensure letter GameObjects are named appropriately

                if (snappedLetter.StartsWith(expectedLetter))
                {
                    Debug.Log($"Correct letter {snappedLetter} snapped into {snapZone.name}!");
                    snapZoneState[snapZone] = true;

                    // Check if all letters are snapped correctly
                    if (AllLettersSnapped())
                    {
                        CompletePuzzle();
                    }
                }
                else
                {
                    Debug.LogError($"Incorrect letter {snappedLetter} snapped into {snapZone.name}!");
                }

                break;
            }
        }
    }

    private bool AllFieldsGrowing()
    {
        foreach (var field in wheatFields)
        {
            if (!field.isGrowing) // Add `IsGrowing` method to WheatField
            {
                return false;
            }
        }
        return true;
    }

    private bool AllLettersSnapped()
    {
        foreach (var state in snapZoneState.Values)
        {
            if (!state) return false;
        }
        return true;
    }

    private void CompletePuzzle()
    {
        Debug.Log("Puzzle completed! Instantly maturing all wheat.");
        foreach (var field in wheatFields)
        {
            field.MatureAllWheat(); //mature all wheat
        }
        // Deactivate letters at the last
        letter_W.SetActive(false);
        letter_H.SetActive(false);
        letter_E.SetActive(false);
        letter_A.SetActive(false);
        letter_T.SetActive(false);
        QuestionUI.SetActive(false);
    }
}
