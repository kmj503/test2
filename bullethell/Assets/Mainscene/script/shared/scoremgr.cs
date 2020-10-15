using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoremgr : MonoBehaviour
{
    Text score;
    public static int scorevalue =0;
    // Start is called before the first frame update

    void Start()
    {
        score = GetComponent<Text>();
    }

    void Update()
    {
        score.text = "Score:" + scorevalue;
    }

}
