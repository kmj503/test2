using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossshotcase : MonoBehaviour
{
    [SerializeField]
    GameObject _InitialPoolBulletPrefab;
    [SerializeField]
    int _InitialPoolCount = 1000;
    [SerializeField]
    GameObject[] _GoShotCtrlList;
    Rect _RectArea = new Rect(0, 0, 0, 0);
    int _NowIndex = 0;
    string _NowGoName;


    private rumiascript Rumia_S;

    void Start()
    {
        if (_InitialPoolBulletPrefab == null)
        {
            return;
        }
        // pooling bullet
        List<GameObject> goBulletList = new List<GameObject>();
        for (int i = 0; i < _InitialPoolCount; i++)
        {
            GameObject goBullet = UbhObjectPool.Instance.GetGameObject(_InitialPoolBulletPrefab, Vector3.zero, Quaternion.identity, true);
            if (goBullet == null)
            {
                break;
            }

            // add UbhBullet
            if (goBullet.GetComponent<UbhBullet>() == null)
            {
                goBullet.AddComponent<UbhBullet>();
            }

            goBulletList.Add(goBullet);
        }
        for (int i = 0; i < goBulletList.Count; i++)
        {
            UbhObjectPool.Instance.ReleaseGameObject(goBulletList[i]);
        }

        if (_GoShotCtrlList != null)
        {
            for (int i = 0; i < _GoShotCtrlList.Length; i++)
            {
                _GoShotCtrlList[i].SetActive(false);
            }
        }

        _NowIndex = -1;
        ChangeShot(true);
    }

    void Update()
    {
        /*
        if (Input.GetKeyUp (KeyCode.LeftArrow)) {
            ChangeShot (false);
            return;
        }
        if (Input.GetKeyUp (KeyCode.RightArrow)) {
            ChangeShot (true);
            return;
        }
        */
    }

  

    void ChangeShot(bool toNext)
    {
        
        if (_GoShotCtrlList == null)
        {
            return;
        }

        StopAllCoroutines();

        if (0 <= _NowIndex && _NowIndex < _GoShotCtrlList.Length)
        {
            _GoShotCtrlList[_NowIndex].SetActive(false);
        }

        if (toNext)
        {
            _NowIndex = (int)Mathf.Repeat(_NowIndex + 1f, _GoShotCtrlList.Length);
        }
        else
        {
            _NowIndex--;
            if (_NowIndex < 0)
            {
                _NowIndex = _GoShotCtrlList.Length - 1;
            }
        }

        if (0 <= _NowIndex && _NowIndex < _GoShotCtrlList.Length)
        {
            _GoShotCtrlList[_NowIndex].SetActive(true);

            _NowGoName = _GoShotCtrlList[_NowIndex].name;

            StartCoroutine(StartShot());
        }
    }

    IEnumerator StartShot()
    {
        float cntTimer = 0f;
        while (cntTimer < 1f)
        {
            cntTimer += UbhTimer.Instance.DeltaTime;
            yield return 0;
        }

        yield return 0;

        UbhShotCtrl shotCtrl = _GoShotCtrlList[_NowIndex].GetComponent<UbhShotCtrl>();
        if (shotCtrl != null)
        {
            shotCtrl.StartShotRoutine();
        }
    }
}
