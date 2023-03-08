using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public string ProjectileName;    // ����ü(����) �̸�
    public float range; // �����Ÿ� -> �E �� �� ����.
    public float accuarcy;  // ��Ȯ�� -> �E �� �� ����.22
    public float throwRate;  // ���� �ӵ� -> �ð��� � �� �� �ִ���
    public float reloadTime;    // ������ �ӵ�
    public float throwingPower = 200.0f; // ����ü ���ư��� ��

    public int damamge; // ����ü ������

    public int reloadProjectileCount;  // �ѹ� ������ ��(������ �� �������� �з�)
    public int currentProjectileCount;    // ���� ��� ���Ҵ���, ��� �� ���� �� �ִ���
    public int maxProjectileCount;  // �ִ�����ü ���� ���� ����
    public int carryProjectileCount;    // ���� �ִ� �� ����ü ��


    // ������ ���� ����
    public Transform Target; // ���� ������ : ����ĳ��Ʈ -> Hit ���� �� transform �޾Ƽ� ����ϱ� -> ���콺 ������(Vector3) �������� �� ���ϱ�� ����
    //public float firingAngle = 45.0f;
    //public float gravity = 9.8f; -> ���繰 �⺻ �߷� ����غ���

    //private Transform projectileTransform; // ���繰 ��ġ �̵�..
    Rigidbody projectileRigidbody;
    



    void Awake()
    {
        // projectileTransform = transform;        // ����ü Transform �޾ƿ�
        projectileRigidbody = GetComponent<Rigidbody>(); // ����ü Rigidbody �޾ƿ�.
        
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

    // �̸� �����̸� ���� ���� ������ ���� ��ġ�� �� ��

    // public float retroActionForce; // �ݵ�����
    // public float retroActionSightForce; // �����ؽ� �ݵ� ����

    // public Vector3 fineSightOriginPos;  // �����ؽ� ������ ��ġ

}
