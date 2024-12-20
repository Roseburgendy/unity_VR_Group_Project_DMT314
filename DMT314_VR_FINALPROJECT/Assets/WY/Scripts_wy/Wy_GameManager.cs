using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wy_GameManager : MonoBehaviour
{
    public int cultivatedFieldCount;
    public int sowedFieldCount;
    public static Wy_GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        cultivatedFieldCount = 0;
        sowedFieldCount = 0;
    }

    public void IncrementCultivatedFieldCount()
    {
        cultivatedFieldCount++;
        Debug.Log($"Cultivated Field Count: {cultivatedFieldCount}");
    }
    public void IncrementSowedFieldCount()
    {
        sowedFieldCount++;
        Debug.Log($"Sowed Field Count: {sowedFieldCount}");
    }
}
