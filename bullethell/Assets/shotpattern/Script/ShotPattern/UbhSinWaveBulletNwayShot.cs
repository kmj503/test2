using UnityEngine;
using System.Collections;

/// <summary>
/// 웨이브 샷
/// </summary>
[AddComponentMenu("UniBulletHell/Shot Pattern/Sin Wave Bullet nWay Shot")] //컴포넌트 메뉴 추가
public class UbhSinWaveBulletNwayShot : UbhBaseShot
{
    // 발사되는 앵글 및 projectile에 대한 설정
    public int _WayNum = 4;
   // 줄의 수
    [Range(0f, 360f)]
    public float _CenterAngle = 48f;
    //센터 앵글 설정
    [Range(0f, 360f)]
    public float _WaveRangeSize = 40f;
   //웨이브의 길이
    [Range(0f, 30f)]
    public float _WaveSpeed = 10f;      
    //속도
    [Range(0f, 360f)]
    public float _BetweenAngle = 10f;
    // 총알 사이의 거리
    public float _NextLineDelay = 0.1f;
    // 그다음 발사의 딜레이
    protected override void Awake ()
    {
        base.Awake();
    }

    public override void Shot ()
    {
        StartCoroutine(ShotCoroutine());
    }

    IEnumerator ShotCoroutine ()
    {
        if (_BulletNum <= 0 || _BulletSpeed <= 0f || _WayNum <= 0) {
            Debug.LogWarning("총알 숫자, 줄의 수 , 총알의 속도 등의 문제로 인한 오류");
            yield break;
        }
        if (_Shooting) {
            yield break;
        }
        _Shooting = true;

        int wayIndex = 0;

        for (int i = 0; i < _BulletNum; i++) {
            if (_WayNum <= wayIndex) {
                wayIndex = 0;

                if (0f < _NextLineDelay) {
                    yield return StartCoroutine(UbhUtil.WaitForSeconds(_NextLineDelay));
                }
            }

            var bullet = GetBullet(transform.position, transform.rotation);
            if (bullet == null) {
                break;
            }

            float baseAngle = _WayNum % 2 == 0 ? _CenterAngle - (_BetweenAngle / 2f) : _CenterAngle;

            float angle = UbhUtil.GetShiftedAngle(wayIndex, baseAngle, _BetweenAngle);

            ShotBullet(bullet, _BulletSpeed, angle, false, null, 0f, true, _WaveSpeed, _WaveRangeSize);

            AutoReleaseBulletGameObject(bullet.gameObject);

            wayIndex++;
        }

        FinishedShot();
    }
}