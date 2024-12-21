using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public Transform player; // ����� Transform
    public Transform[] teleportPoints; // ���͵�����飬���� 4 ���ص�

    void Start()
    {
        // ��鴫�͵��Ƿ���ȷ����
        if (teleportPoints.Length != 4)
        {
            Debug.LogError("Please assign exactly 4 teleport points!");
        }
    }

    // ���͵���һ���ص�
    public void TeleportToPoint1()
    {
        if (teleportPoints.Length >= 1)
        {
            player.position = teleportPoints[0].position;
            Debug.Log("Teleported to Point 1");
        }
    }

    // ���͵��ڶ����ص�
    public void TeleportToPoint2()
    {
        if (teleportPoints.Length >= 2)
        {
            player.position = teleportPoints[1].position;
            Debug.Log("Teleported to Point 2");
        }
    }

    // ���͵��������ص�
    public void TeleportToPoint3()
    {
        if (teleportPoints.Length >= 3)
        {
            player.position = teleportPoints[2].position;
            Debug.Log("Teleported to Point 3");
        }
    }

    // ���͵����ĸ��ص�
    public void TeleportToPoint4()
    {
        if (teleportPoints.Length >= 4)
        {
            player.position = teleportPoints[3].position;
            Debug.Log("Teleported to Point 4");
        }
    }
}
