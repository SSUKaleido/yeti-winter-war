using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string throwName;    // 투사체 이름
    public float range; // 사정거리
    public float accuarcy;  // 정확도
    public float throwRate;  // 발사 속도
    public float reloadTime;    // 재장전 속도

    public int damamge; // 투사체 데미지

    public int reloadTrowCount;  // 재장전 수 -> 필요할 지 모르겠음.
    public int currentTrowCount;    // 현재 몇번 더 던질 수 있는지
    public int maxThrowCount;  // 최대 소유 가능 개수

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
