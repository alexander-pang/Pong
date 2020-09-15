using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteVisibility : MonoBehaviour
{
    SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        render = this.gameObject.GetComponent<SpriteRenderer> ();
    }

    // Update is called once per frame
    void Update()
    {
        //got this from a tutorial, maybe change press Q to be a start game button
        if (Input.GetKeyDown(KeyCode.Q)){
            render.enabled= !render.enabled;
        }
    }
}
