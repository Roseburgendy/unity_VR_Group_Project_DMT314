using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceArea : MonoBehaviour
{
    public meatCowChase cowChaseScript;  // ţ׷��ű�
    private bool isInFence = false;  // �ж�ţ�Ƿ���դ����
    private bool hasStopped = false;  // ����Ƿ��Ѿ�ִ�й�ֹͣ׷���Э��

    private void OnTriggerEnter(Collider other)
    {
        // ��ţ����դ������
        if (other.CompareTag("MeatCow"))
        {
            Debug.Log("ţ����դ������");
            isInFence = true;  // ���ţ����դ������
            hasStopped = false; // ����ֹͣ״̬
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ��ţ�뿪դ������
        if (other.CompareTag("MeatCow"))
        {
            Debug.Log("ţ�뿪դ������");
            isInFence = false;  // ���ţ�뿪դ������
            hasStopped = false; // ����ֹͣ״̬
        }
    }

    private void Update()
    {
        // ���ţ����դ��������Э�̻�û��ִ�й�
        if (isInFence && !hasStopped)
        {
            Debug.Log("ţ��դ��������");
            StartCoroutine(StopChasingAfterDelay());
            hasStopped = true;  // �����Ѿ�ִ�й�Э�̣���ֹ�ظ�����
        }
    }

    // �ӳ�0.5���ֹͣţ��׷��
    IEnumerator StopChasingAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);  // �ȴ�0.5��
        Debug.Log("ţֹͣ׷��");
        cowChaseScript.StopChasing();  // ֹͣ׷��
    }
}
