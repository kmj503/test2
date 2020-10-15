using UnityEngine;
using System.Collections;

/// <summary>
/// Ubh homing shot.
/// </summary>
[AddComponentMenu("UniBulletHell/Shot Pattern/Homing Shot")]
public class UbhHomingShot : UbhBaseShot
{
    // 총알사이 딜레이
    public float _BetweenDelay = 0.1f;
    // 추적 각도
    public float _HomingAngleSpeed = 20f;
    //플레이어 태그 추적
    public bool _SetTargetFromTag = true;
    public string _TargetTagName = "Player";

    public Transform _TargetTransform;

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
            Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed is not set.");
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

            if (_TargetTransform == null && _SetTargetFromTag) {
                _TargetTransform = UbhUtil.GetTransformFromTagName(_TargetTagName);
            }

            float angle = UbhUtil.GetAngleFromTwoPosition(transform, _TargetTransform, ShotCtrl._AxisMove);

            ShotBullet(bullet, _BulletSpeed, angle, true, _TargetTransform, _HomingAngleSpeed);

            AutoReleaseBulletGameObject(bullet.gameObject);
        }

        FinishedShot();
    }
}