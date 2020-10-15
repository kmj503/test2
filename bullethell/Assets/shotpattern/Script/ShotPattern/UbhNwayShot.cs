using UnityEngine;
using System.Collections;

/// <summary>
/// Ubh nWay shot.
/// </summary>
[AddComponentMenu("UniBulletHell/Shot Pattern/nWay Shot")]
public class UbhNwayShot : UbhBaseShot
{

    public int _WayNum = 5;
    [Range(0f, 360f)]
    public float _CenterAngle = 180f;
    [Range(0f, 360f)]
    public float _BetweenAngle = 10f;
    public float _NextLineDelay = 0.1f;

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
            Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed or WayNum is not set.");
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

            ShotBullet(bullet, _BulletSpeed, angle);

            AutoReleaseBulletGameObject(bullet.gameObject);

            wayIndex++;
        }

        FinishedShot();
    }
}