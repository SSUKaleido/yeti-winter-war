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

    // ������
    private void Throw()
    {
        currentThrowRate = currentProjectile.throwRate;
        Shoot();
    }

    // �߻� : ������ ����ü�� ���ư�
    private void Shoot()
    {

    }
}
