using UnityEngine;
using System.Collections;

/// <summary>
/// Ubh bullet.
/// </summary>
public class UbhBullet : UbhMonoBehaviour
{
    public bool _Shooting
    {
        get;
        private set;
    }

    void OnBecameInvisible() //화면밖으로 나가 보이지 않게 되면 호출
    {
        Destroy(this.gameObject); //객체를 삭제
    }

    void OnDisable ()
    {
        StopAllCoroutines();
        transform.ResetPosition();
        transform.ResetRotation();
        _Shooting = false;
    }

    /// <summary>
    /// Bullet Shot
    /// </summary>
    public void Shot (float speed, float angle, float accelSpeed, float accelTurn,
                      bool homing, Transform homingTarget, float homingAngleSpeed,
                      bool wave, float waveSpeed, float waveRangeSize,
                      bool pauseAndResume, float pauseTime, float resumeTime, UbhUtil.AXIS axisMove)
    {
        if (_Shooting) {
            return;
        }
        _Shooting = true;

        StartCoroutine(MoveCoroutine(speed, angle, accelSpeed, accelTurn,
                                     homing, homingTarget, homingAngleSpeed,
                                     wave, waveSpeed, waveRangeSize,
                                     pauseAndResume, pauseTime, resumeTime, axisMove));
    }

    IEnumerator MoveCoroutine (float speed, float angle, float accelSpeed, float accelTurn,
                               bool homing, Transform homingTarget, float homingAngleSpeed,
                               bool wave, float waveSpeed, float waveRangeSize,
                               bool pauseAndResume, float pauseTime, float resumeTime, UbhUtil.AXIS axisMove)
    {
        if (axisMove == UbhUtil.AXIS.X_AND_Z) {
            // x,z 각
            transform.SetEulerAnglesY(-angle);
        } else {
            // x,y각
            transform.SetEulerAnglesZ(angle);
        }

        float selfFrameCnt = 0f;
        float selfTimeCount = 0f;

        while (true) {
            if (homing) {
                // 추적
                if (homingTarget != null && 0f < homingAngleSpeed) {
                    float rotAngle = UbhUtil.GetAngleFromTwoPosition(transform, homingTarget, axisMove);
                    float myAngle = 0f;
                    if (axisMove == UbhUtil.AXIS.X_AND_Z) {
                        // x,z각
                        myAngle = -transform.eulerAngles.y;
                    } else {
                        // x,y각
                        myAngle = transform.eulerAngles.z;
                    }

                    float toAngle = Mathf.MoveTowardsAngle(myAngle, rotAngle, UbhTimer.Instance.DeltaTime * homingAngleSpeed);

                    if (axisMove == UbhUtil.AXIS.X_AND_Z) {
                        // x,z각
                        transform.SetEulerAnglesY(-toAngle);
                    } else {
                        // x,y각
                        transform.SetEulerAnglesZ(toAngle);
                    }
                }

            } else if (wave) {
                // 가속 회전
                angle += (accelTurn * UbhTimer.Instance.DeltaTime);
                // 웨이브
                if (0f < waveSpeed && 0f < waveRangeSize) {
                    float waveAngle = angle + (waveRangeSize / 2f * Mathf.Sin(selfFrameCnt * waveSpeed / 100f));
                    if (axisMove == UbhUtil.AXIS.X_AND_Z) {
                        // X and Z axis
                        transform.SetEulerAnglesY(-waveAngle);
                    } else {
                        // X and Y axis
                        transform.SetEulerAnglesZ(waveAngle);
                    }
                }
                selfFrameCnt++;

            } else {
                // acceleration turning.
                float addAngle = accelTurn * UbhTimer.Instance.DeltaTime;
                if (axisMove == UbhUtil.AXIS.X_AND_Z) {
                    // X and Z axis
                    transform.AddEulerAnglesY(-addAngle);
                } else {
                    // X and Y axis
                    transform.AddEulerAnglesZ(addAngle);
                }
            }

            // acceleration speed.
            speed += (accelSpeed * UbhTimer.Instance.DeltaTime);

            // move.
            if (axisMove == UbhUtil.AXIS.X_AND_Z) {
                // X and Z axis
                transform.position += transform.forward.normalized * speed * UbhTimer.Instance.DeltaTime;
            } else {
                // X and Y axis
                transform.position += transform.up.normalized * speed * UbhTimer.Instance.DeltaTime;
            }

            yield return 0;

            selfTimeCount += UbhTimer.Instance.DeltaTime;

            // pause and resume.
            if (pauseAndResume && pauseTime >= 0f && resumeTime > pauseTime) {
                while (pauseTime <= selfTimeCount && selfTimeCount < resumeTime) {
                    yield return 0;
                    selfTimeCount += UbhTimer.Instance.DeltaTime;
                }
            }
        }
    }
}