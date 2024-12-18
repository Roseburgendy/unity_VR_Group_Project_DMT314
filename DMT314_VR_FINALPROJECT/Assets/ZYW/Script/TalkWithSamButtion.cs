using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // ���ڸ��� UI �ı�
using UnityEngine.XR.Interaction.Toolkit;

public class TalkWithSamButton : MonoBehaviour
{
    public Text buttonText;  // ��ť�ϵ��ı�
    public GameObject firstMessage;  // ��һ�λ��Ķ��󣨿����� Text ������ UI Ԫ�أ�
    public GameObject secondMessage;  // �ڶ��λ��Ķ���
    private bool isFirstMessageShown = false;  // �ж��Ƿ���ʾ�˵�һ�λ�

    // XR ��ť����
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

    // ��ť���ʱ���߼�
    private void OnButtonPressed(XRBaseInteractor interactor)
    {
        if (!isFirstMessageShown)
        {
            // ��ʾ��һ�λ����ı䰴ť�ı�Ϊ Continue
            firstMessage.SetActive(true);
            buttonText.text = "Continue";
            isFirstMessageShown = true;
        }
        else
        {
            // ��ʾ�ڶ��λ������ذ�ť
            secondMessage.SetActive(true);
            gameObject.SetActive(false);  // ���ذ�ť
        }
    }
}
