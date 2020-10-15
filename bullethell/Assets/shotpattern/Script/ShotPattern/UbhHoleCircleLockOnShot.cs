using UnityEngine;
using System.Collections;

/// <summary>
/// Ubh hole circle lock on shot.
/// </summary>
[AddComponentMenu("UniBulletHell/Shot Pattern/Hole Circle Shot (Lock On)")]
public class UbhHoleCircleLockOnShot : UbhHoleCircleShot
{

    //플레이어 추적 태그 설정
    public bool _SetTargetFromTag = true;
    public string _TargetTagName = "Player";
    public Transform _TargetTransform;

    protected override void Awake ()
    {
        base.Awake();
    }

    public override void Shot ()
    {
        if (_TargetTransform == null && _SetTargetFromTag) {
            _TargetTransform = UbhUtil.GetTransformFromTagName(_TargetTagName);
        }
        if (_TargetTransform == null) {
            Debug.LogWarning("Cannot shot because TargetTransform is not set.");
            return;
        }

        _HoleCenterAngle = UbhUtil.GetAngleFromTwoPosition(transform, _TargetTransform, ShotCtrl._AxisMove);

        base.Shot();
    }
}