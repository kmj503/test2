using UnityEngine;
using System.Collections;

/// <summary>
/// Ubh nway lock on shot.
/// </summary>
[AddComponentMenu("UniBulletHell/Shot Pattern/nWay Shot (Lock On)")]
public class UbhNwayLockOnShot : UbhNwayShot
{

    public bool _SetTargetFromTag = true;
    public string _TargetTagName = "Player";
    public Transform _TargetTransform;
    public bool _Aiming;

    protected override void Awake ()
    {
        base.Awake();
    }

    public override void Shot ()
    {
        if (_Shooting) {
            return;
        }

        AimTarget();

        if (_TargetTransform == null) {
            Debug.LogWarning("Cannot shot because TargetTransform is not set.");
            return;
        }

        base.Shot();

        if (_Aiming) {
            StartCoroutine(AimingCoroutine());
        }
    }

    void AimTarget ()
    {
        if (_TargetTransform == null && _SetTargetFromTag) {
            _TargetTransform = UbhUtil.GetTransformFromTagName(_TargetTagName);
        }
        if (_TargetTransform != null) {
            _CenterAngle = UbhUtil.GetAngleFromTwoPosition(transform, _TargetTransform, ShotCtrl._AxisMove);
        }
    }

    IEnumerator AimingCoroutine ()
    {
        while (_Aiming) {
            if (_Shooting == false) {
                yield break;
            }

            AimTarget();

            yield return 0;
        }
    }
}