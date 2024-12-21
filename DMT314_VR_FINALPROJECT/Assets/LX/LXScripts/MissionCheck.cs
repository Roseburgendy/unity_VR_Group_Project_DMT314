using UnityEngine;
using UnityEngine.UI;

public class MissionCheck : MonoBehaviour
{
    public EggBoxController eggBoxController; // ���� EggBoxController �ű�
    public TomatoBoxController tomatoBoxController;
    public PotatoBoxController potatoBoxController;


    public GameObject checkmarkImage;         // �Թ�ͼ��
    public GameObject checkmarkImage2;         // �Թ�ͼ��
    public GameObject checkmarkImage3;         // �Թ�ͼ��
    public GameObject checkmarkImage4;         // �Թ�ͼ��

    void Start()
    {

    }

    void Update()
    {
        // ��� currentEggs �Ƿ�ﵽ 6
        if (eggBoxController != null && eggBoxController.currentEggs == 6)
        {
            // ����Թ�ͼ��
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
