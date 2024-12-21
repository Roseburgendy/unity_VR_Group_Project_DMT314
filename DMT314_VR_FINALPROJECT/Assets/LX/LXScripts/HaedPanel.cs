using UnityEngine;

public class HeadTiltPanelController : MonoBehaviour
{
    public Transform playerHead; // ���ͷ���� Transform��һ���������
    public GameObject panel;     // Ҫ����/���ص����
    public float activationAngle = 45f; // ��������̧ͷ�Ƕȣ���λ���ȣ�
    public float deactivationAngle = 15f; // ��������̧ͷ�Ƕȣ���λ���ȣ�
    public float distanceFromPlayer = 2f; // ��嵽��ҵľ���

    private bool isPanelActive = false; // ����Ƿ񼤻�

    void Update()
    {
        // ��ȡ���ͷ���ı�����ת�Ƕ�
        float headTiltX = playerHead.localEulerAngles.x;

        // ���Ƕȹ淶��Ϊ -180 �� 180 ��Χ
        if (headTiltX > 180)
        {
            headTiltX -= 360;
        }

        // �ж��Ƿ񼤻����
        if (!isPanelActive && headTiltX < -activationAngle)
        {
            ActivatePanel();
        }
        // �ж��Ƿ��������
        else if (isPanelActive && headTiltX > -deactivationAngle)
        {
            DeactivatePanel();
        }
    }

    void ActivatePanel()
    {
        if (!panel.activeSelf)
        {
            panel.SetActive(true);
            PositionPanel();
            isPanelActive = true;
        }
    }

    void DeactivatePanel()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            isPanelActive = false;
        }
    }

    void PositionPanel()
    {
        // �������Ӧ�ó��ֵ�λ��
        Vector3 forwardDirection = playerHead.forward.normalized;
        Vector3 targetPosition = playerHead.position + forwardDirection * distanceFromPlayer;

        // ��������λ��
        panel.transform.position = targetPosition;

        // �����ʼ���������
        panel.transform.LookAt(playerHead);

        // ����������ת���̶� X ��Ϊ -45 ��
        panel.transform.rotation = Quaternion.Euler(-45, panel.transform.rotation.eulerAngles.y+180, panel.transform.rotation.eulerAngles.z);
    }
}
