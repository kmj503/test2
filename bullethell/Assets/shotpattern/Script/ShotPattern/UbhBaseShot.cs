using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 기본샷
/// </summary>
public abstract class UbhBaseShot : UbhMonoBehaviour
{
    // 총알 프리펩설정
    public GameObject _BulletPrefab;
    // 총알 수
    public int _BulletNum = 10;
    // 속도
    public float _BulletSpeed = 2f;
    // 가속도
    public float _AccelerationSpeed = 0f;
    // 회전 가속도
    public float _AccelerationTurn = 0f;
    // 시작 멈춤 타이밍
    public bool _UsePauseAndResume = false;
    // 총알 정지 시간
    public float _PauseTime = 0f;
    // 다시 재생 시간
    public float _ResumeTime = 0f;
    // 풀링 초기화
    public bool _InitialPooling = false;
    // 지정된 시간에 자동 활성화
    public bool _UseAutoRelease = false;
    // 자동 활성 시간
    public float _AutoReleaseTime = 10f;
    // 발사 이후 콜백 메서드 리시버
    public GameObject _CallbackReceiver;
    // 콜백 매서드 리시버 이름설정
    public string _CallbackMethod;

    protected UbhShotCtrl ShotCtrl
    {
        get
        {
            if (_ShotCtrl == null) {
                _ShotCtrl = transform.GetComponentInParent<UbhShotCtrl>();
            }
            return _ShotCtrl;
        }
    }
    UbhShotCtrl _ShotCtrl;

    protected bool _Shooting;

   
    // 클래스를 상속할 때 AWAKE 메서드 재정의에서 호출
    // 예시 : protected override void Awake () { base.Awake (); }
    
    protected virtual void Awake ()
    {
        if (_InitialPooling) {
            var goBulletList = new List<GameObject>();
            for (int i = 0; i < _BulletNum; i++) {
                var bullet = GetBullet(Vector3.zero, Quaternion.identity, true);
                if (bullet != null) {
                    goBulletList.Add(bullet.gameObject);
                }
            }
            for (int i = 0; i < goBulletList.Count; i++) {
                UbhObjectPool.Instance.ReleaseGameObject(goBulletList[i]);
            }
        }
    }

    //위와 같음
    protected virtual void OnDisable ()
    {
        _Shooting = false;
    }

    
    // 발사 메서드 함수
    public abstract void Shot ();

    
    // 발사 컨트롤 세터
    
    public void SetShotCtrl (UbhShotCtrl shotCtrl)
    {
        _ShotCtrl = shotCtrl;
    }

    
    // 발사 완료 함수
    protected void FinishedShot ()
    {
        if (_CallbackReceiver != null && string.IsNullOrEmpty(_CallbackMethod) == false) {
            _CallbackReceiver.SendMessage(_CallbackMethod, SendMessageOptions.DontRequireReceiver);
        }
        _Shooting = false;
    }

    
    //총알 오브젝트를 pool에서 가져옴
    protected UbhBullet GetBullet (Vector3 position, Quaternion rotation, bool forceInstantiate = false)
    {
        if (_BulletPrefab == null) {
            Debug.LogWarning("Cannot generate a bullet because BulletPrefab is not set.");
            return null;
        }

        // 총알 오브젝트를 pool에서 가져옴
        var goBullet = UbhObjectPool.Instance.GetGameObject(_BulletPrefab, position, rotation, forceInstantiate);
        if (goBullet == null) {
            return null;
        }

        //컴포넌트 추가
        var bullet = goBullet.GetComponent<UbhBullet>();
        if (bullet == null) {
            bullet = goBullet.AddComponent<UbhBullet>();
        }

        return bullet;
    }

    
    //총알 오브젝트 발사
    protected void ShotBullet (UbhBullet bullet, float speed, float angle,
                               bool homing = false, Transform homingTarget = null, float homingAngleSpeed = 0f,
                               bool wave = false, float waveSpeed = 0f, float waveRangeSize = 0f)
    {
        if (bullet == null) {
            return;
        }
        bullet.Shot(speed, angle, _AccelerationSpeed, _AccelerationTurn,
                    homing, homingTarget, homingAngleSpeed,
                    wave, waveSpeed, waveRangeSize,
                    _UsePauseAndResume, _PauseTime, _ResumeTime,
                    ShotCtrl != null ? ShotCtrl._AxisMove : UbhUtil.AXIS.X_AND_Y);
    }


    //총알 오브젝트 자동 활성화
    protected void AutoReleaseBulletGameObject (GameObject goBullet)
    {
        if (_UseAutoRelease == false || _AutoReleaseTime < 0f) {
            return;
        }
        UbhCoroutine.StartIE(AutoReleaseBulletGameObjectCoroutine(goBullet));
    }

    IEnumerator AutoReleaseBulletGameObjectCoroutine (GameObject goBullet)
    {
        float countUpTime = 0f;

        while (true) {
            if (goBullet == null || goBullet.activeInHierarchy == false) {
                yield break;
            }

            if (_AutoReleaseTime <= countUpTime) {
                break;
            }

            yield return 0;

            countUpTime += UbhTimer.Instance.DeltaTime;
        }

        UbhObjectPool.Instance.ReleaseGameObject(goBullet);
    }
}