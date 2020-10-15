using UnityEngine;
using System.Collections;

/// <summary>
/// 리니어 샷
/// </summary>
[AddComponentMenu("UniBulletHell/Shot Pattern/Linear Shot")]
public class UbhLinearShot : UbhBaseShot
{
    // 발사 각도
    [Range(0f, 360f)]
    public float _Angle = 180f;
    // 초탄과 후탄의 딜레이
    public float _BetweenDelay = 0.1f;

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
        if (_BulletNum <= 0 || _BulletSpeed <= 0f) {
            Debug.LogWarning("총알 갯수 및 속도 설정이 안됨");
            yield break;
        }
        if (_Shooting) {
            yield break;
        }
        _Shooting = true;

        for (int i = 0; i < _BulletNum; i++) {
            if (0 < i && 0f < _BetweenDelay) {
                yield return StartCoroutine(UbhUtil.WaitForSeconds(_BetweenDelay));
            }

            var bullet = GetBullet(transform.position, transform.rotation);
            if (bullet == null) {
                break;
            }

            ShotBullet(bullet, _BulletSpeed, _Angle);

            AutoReleaseBulletGameObject(bullet.gameObject);
        }

        FinishedShot();
    }
}