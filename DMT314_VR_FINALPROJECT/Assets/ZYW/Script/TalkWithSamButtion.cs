using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // 用于更新 UI 文本
using UnityEngine.XR.Interaction.Toolkit;

public class TalkWithSamButton : MonoBehaviour
{
    public Text buttonText;  // 按钮上的文本
    public GameObject firstMessage;  // 第一段话的对象（可以是 Text 或其他 UI 元素）
    public GameObject secondMessage;  // 第二段话的对象
    private bool isFirstMessageShown = false;  // 判断是否显示了第一段话

    // XR 按钮交互
    private void OnEnable()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.onSelectEntered.AddListener(OnButtonPressed);
    }

    private void OnDisable()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.onSelectEntered.RemoveListener(OnButtonPressed);
    }

    // 按钮点击时的逻辑
    private void OnButtonPressed(XRBaseInteractor interactor)
    {
        if (!isFirstMessageShown)
        {
            // 显示第一段话，改变按钮文本为 Continue
            firstMessage.SetActive(true);
            buttonText.text = "Continue";
            isFirstMessageShown = true;
        }
        else
        {
            // 显示第二段话，隐藏按钮
            secondMessage.SetActive(true);
            gameObject.SetActive(false);  // 隐藏按钮
        }
    }
}
