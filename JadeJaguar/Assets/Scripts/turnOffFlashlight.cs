using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOffFlashlight : MonoBehaviour {

    Light flashlight;

    private void Start()
    {
        flashlight = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update ()
    {
		if(Input.GetKeyDown("f"))
        {
                flashlight.enabled = !flashlight.enabled;
        }
       
	}
}
