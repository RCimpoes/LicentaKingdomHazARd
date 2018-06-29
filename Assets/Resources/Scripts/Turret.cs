using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;

    [Header("Adjutables")]

    public float range = 15f;
    public float fireRate = 1f;
    public float power = 30f;

    public List<float> FireRateValues;
    public List<float> RangeValues;
    public List<float> PowerValues;

    private float fireCountdown = 0f;

    [Header("Setup Fields References")]

    public string enemyTag = "NavAgent";
    public GameObject bulletPrefab;
    public Transform firePoint;

    private int FireRateLevel;
    private int RangeLevel;
    private int PowerLevel;

   


    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        InvokeRepeating("UpdateStats", 0f, 1f);

        FireRateLevel = 1;
        RangeLevel = 1;
        PowerLevel = 1;
   
    }
       

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {         
            target = nearestEnemy.transform;
        }

    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.power = power;
     
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    //Upgrade Logic

    private void UpdateStats()
    {
        fireRate = FireRateValues[FireRateLevel-1];
        range = RangeValues[RangeLevel-1];
        power = PowerValues[PowerLevel-1];
    }

    public int getFireRateLevel()
    {
        return FireRateLevel;
    }

    public int getRangeLevel()
    {
        return RangeLevel;
    }

    public int getPowerLevel()
    {
        return PowerLevel;
    }

    public void levelUpFireRate()
    {
        if (FireRateLevel < 5)
        {
            FireRateLevel++;
        }
    }

    public void levelUpRange()
    {
        if(RangeLevel < 5)
        {
            RangeLevel++;
        }
    }

    public void levelUpPower()
    {
        if(PowerLevel < 5)
        {
            PowerLevel++;
        }
    }
    

}

