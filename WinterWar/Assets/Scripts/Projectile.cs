using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string throwName;    // ����ü �̸�
    public float range; // �����Ÿ�
    public float accuarcy;  // ��Ȯ��
    public float throwRate;  // �߻� �ӵ�
    public float reloadTime;    // ������ �ӵ�

    public int damamge; // ����ü ������

    public int reloadTrowCount;  // ������ �� -> �ʿ��� �� �𸣰���.
    public int currentTrowCount;    // ���� ��� �� ���� �� �ִ���
    public int maxThrowCount;  // �ִ� ���� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
