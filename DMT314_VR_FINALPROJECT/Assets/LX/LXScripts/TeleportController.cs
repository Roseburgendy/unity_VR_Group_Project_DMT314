using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public Transform player; // 人物的 Transform
    public Transform[] teleportPoints; // 传送点的数组，包含 4 个地点

    void Start()
    {
        // 检查传送点是否正确设置
        if (teleportPoints.Length != 4)
        {
            Debug.LogError("Please assign exactly 4 teleport points!");
        }
    }

    // 传送到第一个地点
    public void TeleportToPoint1()
    {
        if (teleportPoints.Length >= 1)
        {
            player.position = teleportPoints[0].position;
            Debug.Log("Teleported to Point 1");
        }
    }

    // 传送到第二个地点
    public void TeleportToPoint2()
    {
        if (teleportPoints.Length >= 2)
        {
            player.position = teleportPoints[1].position;
            Debug.Log("Teleported to Point 2");
        }
    }

    // 传送到第三个地点
    public void TeleportToPoint3()
    {
        if (teleportPoints.Length >= 3)
        {
            player.position = teleportPoints[2].position;
            Debug.Log("Teleported to Point 3");
        }
    }

    // 传送到第四个地点
    public void TeleportToPoint4()
    {
        if (teleportPoints.Length >= 4)
        {
            player.position = teleportPoints[3].position;
            Debug.Log("Teleported to Point 4");
        }
    }
}
