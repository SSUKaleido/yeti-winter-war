using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public string ProjectileName;    // 투사체(물건) 이름
    public float range; // 사정거리 -> 뺼 수 도 있음.
    public float accuarcy;  // 정확도 -> 뺼 수 도 있음.22
    public float throwRate;  // 연사 속도 -> 시간당 몇개 쏠 수 있는지
    public float reloadTime;    // 재장전 속도
    public float throwingPower = 200.0f; // 투사체 날아가는 힘

    public int damamge; // 투사체 데미지

    public int reloadProjectileCount;  // 한번 재장전 수(재장전 시 빠져나갈 분량)
    public int currentProjectileCount;    // 현재 몇개가 남았는지, 몇번 더 던질 수 있는지
    public int maxProjectileCount;  // 최대투사체 소유 가능 개수
    public int carryProjectileCount;    // 갖고 있는 총 투사체 수


    // 포물선 관련 변수
    public Transform Target; // 투사 목적지 : 레이캐스트 -> Hit 맞은 것 transform 받아서 사용하기 -> 마우스 포인터(Vector3) 방향으로 힘 가하기로 변경
    //public float firingAngle = 45.0f;
    //public float gravity = 9.8f; -> 투사물 기본 중력 사용해보기

    //private Transform projectileTransform; // 투사물 위치 이동..
    Rigidbody projectileRigidbody;
    



    void Awake()
    {
        // projectileTransform = transform;        // 투사체 Transform 받아옴
        projectileRigidbody = GetComponent<Rigidbody>(); // 투사체 Rigidbody 받아옴.
        
    }

    public void projcetileThrow(Vector3 direction)
    {
        projectileRigidbody.AddForce(direction.normalized * throwingPower);

        

        //StartCoroutine(SimulateProjectile());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //we also add a debug log to know what the projectile touch
        Debug.Log("Projectile Collision with " + other.gameObject);
        Destroy(gameObject);
    }

    /*
    IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown
        // yield return new WaitForSeconds(1.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        projectileTransform.position = projectileTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(projectileTransform.position, Target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        // float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        //float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        //float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        projectileTransform.rotation = Quaternion.LookRotation(Target.position - projectileTransform.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            projectileTransform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }*/

    // 미리 눈덩이를 만들어서 기존 눈덩이 수에 합치게 할 것

    // public float retroActionForce; // 반동세기
    // public float retroActionSightForce; // 정조준시 반동 세기

    // public Vector3 fineSightOriginPos;  // 정조준시 초점의 위치

}
