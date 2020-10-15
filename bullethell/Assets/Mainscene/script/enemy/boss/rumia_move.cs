using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rumia_move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        var possx = this.gameObject.transform.localPosition;
        possx.x -= 5f;
        this.gameObject.transform.localPosition = possx;
        if (possx.x < 1021)
        {
            var possf = this.gameObject.transform.localPosition;
            possx.x = 1021;
            this.gameObject.transform.localPosition = possf;
            

        }  

    }
}
