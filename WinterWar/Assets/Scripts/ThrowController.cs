using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowController : MonoBehaviour
{


    [SerializeField]
    private GameObject projectilePrefab;

    private Projectile currentProjectile;   // Projectile 가져오기 - 던질 물체

    private float currentThrowRate; // 연사 속도 계산

    private bool isReload = false; // 상태 변수 - 재장전

    //private Vector3 originPos;  // 본래 포지션 값
    private GameObject holder;
    private Transform holderTransform;  // 홀더 객체 받아서 transform 받기

    // private RaycastHit hitInfo; // 충돌 객체 정보 받아오기

    //[SerializeField]
    //private Camera playerCam; // 플레이어 시점 가운데에서 탄 발사 알아보기 위함


    public int reloadProjectileCount;  // 한번 재장전 수(재장전 시 빠져나갈 분량)
    public int currentProjectileCount;    // 현재 몇개가 남았는지, 몇번 더 던질 수 있는지
    public int maxProjectileCount;  // 최대투사체 소유 가능 개수
    public int carryProjectileCount;    // 갖고 있는 총 투사체 수


    private void Awake()
    {
        holder = GameObject.Find("Holder");
        holderTransform = holder.transform;     // 던지기 시작점 transform_필수

        currentProjectile = projectilePrefab.GetComponent<Projectile>();
        //originPos = Vector3.zero;
    }

    void Update()
    {
        ThrowRateCalc();
        TryThrow();
        TryReload();
    }

    // 발사 속도 계산
    private void ThrowRateCalc()
    {
        if (currentThrowRate > 0)
            currentThrowRate -= Time.deltaTime; // 0보다 작으면 발사
            
    }
    
    // 발사 시도
    private void TryThrow()
    {
       // Debug.Log("TT");
        if (Input.GetMouseButton(0) && (currentThrowRate<= 0) && !isReload)
        {
            Throw();
           
        }
    }

    // 던지기 : 날아가기 전
    private void Throw()
    {
        if (!isReload)
        {
            if ( currentProjectileCount > 0)
            {
                Shoot();
            }
            else
                StartCoroutine(ReloadCorutine());
        }
    }

    // 발사 : 실제로 투사체가 날아감
    private void Shoot()
    {
         currentProjectileCount--;
        currentThrowRate = currentProjectile.throwRate; // 연사 속도 재계산
        GameObject projectileObject = Instantiate(projectilePrefab, holderTransform.position, Quaternion.identity);

        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y, -Camera.main.transform.position.z));
        Debug.Log("마우스 : " + point.ToString());
        currentProjectile.projcetileThrow(point);

        //Hit();  // 타겟 확인
        // Debug.Log("발사함");

    }
  

    //private void Hit() // Raycast -> 마우스 포인터 클릭 시 그 방향 투척으로 수정
    //{
        // 마우스 클릭 좌표 point
        
        

        /*
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hitInfo,  range))
        {
            Debug.Log(hitInfo.transform.name);

             Target = hitInfo.transform; // 목적지 위치

             projcetileThrow(); // addForce로 수정 해보자
        }*/
    //}

    // 재장전 시도
    private void TryReload()
    {
        if(Input.GetKeyDown(KeyCode.R) && !isReload && currentProjectileCount < reloadProjectileCount)  // 방금 재장전한 경우도 제외
        {
            StartCoroutine(ReloadCorutine());
        }
    }

    // 재장전
    IEnumerator ReloadCorutine() //재장전 되는 동안 대기시간을 만듦
    {
        if( carryProjectileCount > 0)
        {
            isReload = true;

            carryProjectileCount += currentProjectileCount; // 현재 소지중인 투사체 수를 전체 투사체 수에 합침
             currentProjectileCount = 0;

            yield return new WaitForSeconds(currentProjectile.reloadTime);

            if( carryProjectileCount >= reloadProjectileCount)
            {
                 currentProjectileCount = reloadProjectileCount;
                 carryProjectileCount -= reloadProjectileCount;
            }
            else
            {
                 currentProjectileCount = carryProjectileCount;
                 carryProjectileCount = 0;
            }
        }
        else
        {
            Debug.Log("소유한 " + currentProjectile.ProjectileName + "가 없습니다.");
        }

        isReload= false;
    }
}
