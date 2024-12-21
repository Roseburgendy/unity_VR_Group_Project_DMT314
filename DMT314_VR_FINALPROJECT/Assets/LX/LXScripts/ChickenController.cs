using UnityEngine;

public class ChickenController : MonoBehaviour
{
    public enum ChickenState { Idle, WalkToFeeder, Eating, WalkToStartPosition }
    public ChickenState currentState = ChickenState.Idle;

    public Transform feeder;         // ʳ�۵�λ��
    public Animator animator;        // ����������
    public float walkSpeed = 2f;     // �����ƶ��ٶ�
    public FeedChicken feedChicken; // ���� FeedChicken �ű�
    public float eatingSpeed = 0.1f; // �Ե��ٶȣ�ÿ����ٵ�����

    private Vector3 startPosition;   // ��ʼλ��
    private bool isFeederFull = false; // ʳ���Ƿ�����

    void Start()
    {
        // ��¼������ʼλ��
        startPosition = transform.position;
    }

    void Update()
    {
        // ���ݵ�ǰ״ִ̬�ж�Ӧ���߼�
        switch (currentState)
        {
            case ChickenState.Idle:
                HandleIdleState();
                break;
            case ChickenState.WalkToFeeder:
                HandleWalkToFeederState();
                break;
            case ChickenState.Eating:
                HandleEatingState();
                break;
            case ChickenState.WalkToStartPosition:
                HandleWalkToStartPositionState();
                break;
        }
    }

    public void SetFeederFull(bool isFull)
    {
        isFeederFull = isFull;

        if (isFeederFull && currentState == ChickenState.Idle)
        {
            ChangeState(ChickenState.WalkToFeeder);
        }
    }

    void ChangeState(ChickenState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case ChickenState.Idle:
                animator.SetTrigger("idle");
                break;
            case ChickenState.WalkToFeeder:
                animator.SetTrigger("walk");
                break;
            case ChickenState.Eating:
                animator.SetTrigger("eat");
                break;
            case ChickenState.WalkToStartPosition:
                animator.SetTrigger("walk");
                break;
        }
    }

    void HandleIdleState()
    {
        // ���ʳ�������л��� WalkToFeeder ״̬
        if (feedChicken.currentFill >= feedChicken.maxFill)
        {
            ChangeState(ChickenState.WalkToFeeder);
        }
    }

    void HandleWalkToFeederState()
    {
        // ������ʳ��
        Vector3 direction = (feeder.position - transform.position).normalized;
        transform.position += direction * walkSpeed * Time.deltaTime;

        // ����ʳ��
        transform.LookAt(feeder);

        // ����Ƿ񵽴�ʳ��
        if (Vector3.Distance(transform.position, feeder.position) < 0.5f)
        {
            ChangeState(ChickenState.Eating);
        }
    }

    void HandleEatingState()
    {
        // ���Զ���������ʳ�������
        if (feedChicken.currentFill > 0)
        {
            feedChicken.currentFill -= eatingSpeed * Time.deltaTime; // ��������
            feedChicken.UpdateSlider(feedChicken.currentFill);       // �����Ӿ�������
            feedChicken.UpdateFeederVisual(feedChicken.currentFill);
        }
        else
        {
            // ʳ��Ϊ�գ�������ʼλ��
            ChangeState(ChickenState.WalkToStartPosition);
        }
    }

    void HandleWalkToStartPositionState()
    {
        // ��������ʼλ��
        Vector3 direction = (startPosition - transform.position).normalized;
        transform.position += direction * walkSpeed * Time.deltaTime;

        // ������ʼλ��
        transform.LookAt(startPosition);

        // ����Ƿ񵽴���ʼλ��
        if (Vector3.Distance(transform.position, startPosition) < 0.5f)
        {
            
            ChangeState(ChickenState.Idle);
        }
    }
}
