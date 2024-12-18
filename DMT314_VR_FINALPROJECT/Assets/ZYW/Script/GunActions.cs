using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunActions : MonoBehaviour
{
    public float Speed = 50;
    public GameObject bullet;
    public Transform Silencer;
    public AudioSource gunSource;

    public void Fire()
    {
        GameObject spawnBullet = Instantiate(bullet, Silencer.position, Silencer.rotation);
        spawnBullet.GetComponent<Rigidbody>().velocity = Speed * Silencer.forward;
        gunSource.Play();
        Destroy(spawnBullet, 2);
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
