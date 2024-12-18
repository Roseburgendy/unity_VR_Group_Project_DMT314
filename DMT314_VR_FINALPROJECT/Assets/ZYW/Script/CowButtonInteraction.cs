using UnityEngine;
using UnityEngine.UI;  // ���� Slider ���
using UnityEngine.XR.Interaction.Toolkit; // ���� XR ����
using System.Collections;

public class ButtonInteraction : MonoBehaviour
{
    public Slider countdownSlider;  // ����ʱ������
    public GameObject targetObject; // Ŀ������
    public float countdownTime = 2f; // ����ʱʱ��

    private bool isCountingDown = false; // ����ʱ�Ƿ������

    // ��������ʱ
    public void StartCountdown()
    {
        if (!isCountingDown)
        {
            StartCoroutine(CountdownRoutine());
        }
    }

    // ����ʱЭ��
    private IEnumerator CountdownRoutine()
    {
        isCountingDown = true;
        float currentTime = 0f;

        // ���½�����
        while (currentTime < countdownTime)
        {
            currentTime += Time.deltaTime;
            countdownSlider.value = currentTime / countdownTime;  // ���½������������
            yield return null;
        }

        // ����ʱ��������ʾ���岢�Ŵ�
        if (targetObject != null)
        {
            targetObject.SetActive(true);  // ��ʾ����
            TargetObjectControl targetControl = targetObject.GetComponent<TargetObjectControl>();
            if (targetControl != null)
            {
                targetControl.ShowAndResizeObject();  // ���÷Ŵ�����ĺ���
            }
        }

        isCountingDown = false;  // ���õ���ʱ��־
    }
}

