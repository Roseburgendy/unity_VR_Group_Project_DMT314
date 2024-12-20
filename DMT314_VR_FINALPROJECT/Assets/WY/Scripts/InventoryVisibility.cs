using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryVisibility : MonoBehaviour
{
    public GameObject[] tools;
    public GameObject[] snapZones;
    public GameObject[] uiImages;
    public GameObject[] texts;
  
   // Method the toggle the visibility of UI without affect the functionality of the snapzone
   public void SetUIVisibility(bool isVisible)
    {
        // Toggle tool visibility
        foreach(var tool in tools)
        {
            // Access all the renderers in the gameobject and its children
            var renderers = tool.GetComponentsInChildren<Renderer>();
            foreach(var renderer in renderers)
            {
                renderer.enabled = isVisible;
            }
        }

        // Toggle zone visibility
        foreach (var zone in snapZones)
        {
            // Access all the renderers in the gameobject and its children
            var renderers = zone.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                renderer.enabled = isVisible;
            }
        }

        // Toggle ui elements visibility
        foreach (var image in uiImages)
        {
            // Access all the renderers in the gameobject and its children
            var imgobjs = image.GetComponentsInChildren<Image>();
            foreach (var img in imgobjs)
            {
                img.enabled = isVisible;
            }
        }

        // Toggle ui elements visibility
        foreach (var text in texts)
        {
            text.SetActive(isVisible);
        }
    }
}
