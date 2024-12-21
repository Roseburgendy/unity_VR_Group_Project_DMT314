using UnityEngine;
using UnityEngine.UI;

public class MissionCheck : MonoBehaviour
{
    public EggBoxController eggBoxController; // 引用 EggBoxController 脚本
    public TomatoBoxController tomatoBoxController;
    public PotatoBoxController potatoBoxController;


    public GameObject checkmarkImage;         // 对勾图标
    public GameObject checkmarkImage2;         // 对勾图标
    public GameObject checkmarkImage3;         // 对勾图标
    public GameObject checkmarkImage4;         // 对勾图标

    void Start()
    {

    }

    void Update()
    {
        // 检查 currentEggs 是否达到 6
        if (eggBoxController != null && eggBoxController.currentEggs == 6)
        {
            // 激活对勾图标
            if (checkmarkImage != null && !checkmarkImage.activeSelf)
            {
                checkmarkImage.SetActive(true);
            }
        }

        if (tomatoBoxController.currentEggs == 3&&potatoBoxController.currentEggs == 3)
        {
            checkmarkImage2.SetActive(true);
        }

        
    }
}
