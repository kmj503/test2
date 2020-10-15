using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Ubh shot ctrl.
/// </summary>
[AddComponentMenu("UniBulletHell/Controller/Shot Controller")]
public class UbhShotCtrl : UbhMonoBehaviour
{
    [Serializable]
    public class ShotInfo
    {
        // 샷 패턴 컴포넌트
        public UbhBaseShot _ShotObj;
        // 다음 패턴 딜레이
        public float _AfterDelay;
    }

   


    public UbhUtil.AXIS _AxisMove = UbhUtil.AXIS.X_AND_Y;
    public bool _StartOnAwake = true;
    public float _StartOnAwakeDelay = 1f;
    public bool _Loop = true;
    public bool _AtRandom = false;
    public List<ShotInfo> _ShotList = new List<ShotInfo>();
    bool _Shooting;

    IEnumerator Start ()
    {
        if (_StartOnAwake) {
            if (0f < _StartOnAwakeDelay) {
                yield return StartCoroutine(UbhUtil.WaitForSeconds(_StartOnAwakeDelay));
            }
            StartShotRoutine();
        }
    }

    void OnDisable ()
    {
        _Shooting = false;
    }

    /// <summary>
    /// Start the shot routine.
    /// </summary>
    public void StartShotRoutine ()
    {
        StartCoroutine(ShotCoroutine());
    }

    IEnumerator ShotCoroutine ()
    {
        if (_ShotList == null || _ShotList.Count <= 0) {
            Debug.LogWarning("Cannot shot because ShotList is not set.");
            yield break;
        }

        bool enableShot = false;
        for (int i = 0; i < _ShotList.Count; i++) {
            if (_ShotList[i]._ShotObj != null) {
                enableShot = true;
                break;
            }
        }

        bool enableDelay = false;
        for (int i = 0; i < _ShotList.Count; i++) {
            if (0f < _ShotList[i]._AfterDelay) {
                enableDelay = true;
                break;
            }
        }

        if (enableShot == false || enableDelay == false) {
            if (enableShot == false) {
                Debug.LogWarning("Cannot shot because all ShotObj of ShotList is not set.");
            }
            if (enableDelay == false) {
                Debug.LogWarning("Cannot shot because all AfterDelay of ShotList is zero.");
            }
            yield break;
        }

        if (_Shooting) {
            yield break;
        }
        _Shooting = true;

        var tmpShotInfoList = new List<ShotInfo>(_ShotList);

        int nowIndex = 0;

        while (true) {
            if (_AtRandom) {
                nowIndex = UnityEngine.Random.Range(0, tmpShotInfoList.Count);
            }

            if (tmpShotInfoList[nowIndex]._ShotObj != null) {
                tmpShotInfoList[nowIndex]._ShotObj.SetShotCtrl(this);
                tmpShotInfoList[nowIndex]._ShotObj.Shot();
            }

            if (0f < tmpShotInfoList[nowIndex]._AfterDelay) {
                yield return StartCoroutine(UbhUtil.WaitForSeconds(tmpShotInfoList[nowIndex]._AfterDelay));
            }

            if (_AtRandom) {
                tmpShotInfoList.RemoveAt(nowIndex);

                if (tmpShotInfoList.Count <= 0) {
                    if (_Loop) {
                        tmpShotInfoList = new List<ShotInfo>(_ShotList);
                    } else {
                        break;
                    }
                }

            } else {
                if (_Loop == false && tmpShotInfoList.Count - 1 <= nowIndex) {
                    break;
                }

                nowIndex = (int) Mathf.Repeat(nowIndex + 1f, tmpShotInfoList.Count);
            }
        }

        _Shooting = false;
    }

    /// <summary>
    /// Stop the shot routine.
    /// </summary>
    public void StopShotRoutine ()
    {
        StopAllCoroutines();
        _Shooting = false;
    }
}