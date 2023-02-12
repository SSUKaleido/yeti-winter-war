using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    [SerializeField]
    private Projectile currentProjectile;

    private float currentThrowRate;

    // Update is called once per frame
    void Update()
    {
        ThrowRateCalc();
        TryThrow();
    }

    // 발사 속도 계산
    private void ThrowRateCalc()
    {
        if (currentThrowRate > 0)
            currentThrowRate -= Time.deltaTime;
    }
    
    // 밠
    private void TryThrow()
    {
        if (Input.GetButton("Throw") && currentThrowRate <= 0)
        {
            Throw();
        }
    }

    // 던지기
    private void Throw()
    {
        
        Shoot();
    }

    // 발사 : 실제로 투사체가 날아감
    private void Shoot()
    {
        //currentProjectile
        //currentThrowRate = currentProjectile.throwRate; // 발사 속도 재계산
    }
}
