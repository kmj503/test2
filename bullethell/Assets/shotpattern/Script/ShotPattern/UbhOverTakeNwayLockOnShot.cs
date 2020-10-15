using UnityEngine;
using System.Collections;

/// <summary>
/// Ubh over take nway lock on shot.
/// </summary>
[AddComponentMenu("UniBulletHell/Shot Pattern/Over Take nWay Shot (Lock On)")]
public class UbhOverTakeNwayLockOnShot : UbhOverTakeNwayShot
{
  
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

        _CenterAngle = UbhUtil.GetAngleFromTwoPosition(transform, _TargetTransform, ShotCtrl._AxisMove);

        base.Shot();
    }
}