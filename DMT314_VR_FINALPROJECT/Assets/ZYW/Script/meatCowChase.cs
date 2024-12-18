using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class meatCowChase : MonoBehaviour
{

    private enum CowState
    {
        Wait, Chase, Stay,
    }
    public Transform cow;  
    public Transform apple;  
    public Animator cowAnimator;  
    public float chaseDistance = 4f;  


    private CowState cowState;

    private bool infense;
    private float fenseTime;
    void Update()
    {
       
        Vector3 direction = new Vector3(apple.position.x - cow.position.x, 0, apple.position.z - cow.position.z);
        float distance = direction.magnitude;  

        switch (cowState)
        {
            case CowState.Wait:
                if (distance < chaseDistance)
                {
                    StartChasing();
                    cowState = CowState.Chase;
                }
                break;
            case CowState.Chase:
                if (distance > 1)
                {
                    ChaseApple(direction);
                }
                else
                {
                    apple.gameObject.SetActive(false); 
                    StopChasing();
                    cowState = CowState.Stay;
                    break;
                }
                if (!infense && IsInFense())
                {
                    fenseTime = Time.time;
                    infense = true;
                }
                if (infense && (Time.time - fenseTime > 0.5f))
                {
                    StopChasing();
                    cowState = CowState.Stay;
                }

                break;
            case CowState.Stay:
                break;
        }


    }

   
    void StartChasing()
    {
        cowAnimator.SetBool("IsRunning", true);  
    }

  
    void ChaseApple(Vector3 direction)
    {
        direction.Normalize();  
        cow.forward = direction;


        cow.Translate(direction * Time.deltaTime);
    }
    bool IsInFense()
    {
        return transform.position.x > 183 && transform.position.x < 198 && transform.position.z > 2 && transform.position.z < 25;
    }
 
    public void StopChasing()
    {
        cowAnimator.SetBool("IsRunning", false);  
    }
}
