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

    private void ThrowRateCalc()
    {
        if (currentThrowRate > 0)
            currentThrowRate -= Time.deltaTime;
    }

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
        currentThrowRate = currentProjectile.throwRate;
        Shoot();
    }

    // 발사 : 실제로 투사체가 날아감
    private void Shoot()
    {

    }
}
