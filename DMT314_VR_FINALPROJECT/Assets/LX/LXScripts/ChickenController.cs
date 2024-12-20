using UnityEngine;

public class ChickenController : MonoBehaviour
{
    public enum ChickenState { Idle, WalkToFeeder, Eating, WalkToStartPosition }
    public ChickenState currentState = ChickenState.Idle;

    public Transform feeder;         // 食槽的位置
    public Animator animator;        // 动画控制器
    public float walkSpeed = 2f;     // 鸡的移动速度
    public FeedChicken feedChicken; // 引用 FeedChicken 脚本
    public float eatingSpeed = 0.1f; // 吃的速度（每秒减少的量）

    private Vector3 startPosition;   // 起始位置
    private bool isFeederFull = false; // 食槽是否已满

    void Start()
    {
        // 记录鸡的起始位置
        startPosition = transform.position;
    }

    void Update()
    {
        // 根据当前状态执行对应的逻辑
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
        // 如果食槽满，切换到 WalkToFeeder 状态
        if (feedChicken.currentFill >= feedChicken.maxFill)
        {
            ChangeState(ChickenState.WalkToFeeder);
        }
    }

    void HandleWalkToFeederState()
    {
        // 鸡走向食槽
        Vector3 direction = (feeder.position - transform.position).normalized;
        transform.position += direction * walkSpeed * Time.deltaTime;

        // 面向食槽
        transform.LookAt(feeder);

        // 检查是否到达食槽
        if (Vector3.Distance(transform.position, feeder.position) < 0.5f)
        {
            ChangeState(ChickenState.Eating);
        }
    }

    void HandleEatingState()
    {
        // 鸡吃东西，减少食槽填充量
        if (feedChicken.currentFill > 0)
        {
            feedChicken.currentFill -= eatingSpeed * Time.deltaTime; // 减少饲料
            feedChicken.UpdateSlider(feedChicken.currentFill);       // 更新视觉进度条
            feedChicken.UpdateFeederVisual(feedChicken.currentFill);
        }
        else
        {
            // 食槽为空，返回起始位置
            ChangeState(ChickenState.WalkToStartPosition);
        }
    }

    void HandleWalkToStartPositionState()
    {
        // 鸡返回起始位置
        Vector3 direction = (startPosition - transform.position).normalized;
        transform.position += direction * walkSpeed * Time.deltaTime;

        // 面向起始位置
        transform.LookAt(startPosition);

        // 检查是否到达起始位置
        if (Vector3.Distance(transform.position, startPosition) < 0.5f)
        {
            
            ChangeState(ChickenState.Idle);
        }
    }
}
