﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPathing : MonoBehaviour {

    public Transform[] target;
    public float speed;

    private int current;

    // Update is called once per frame
    void Update () {
        if (transform.position != target[current].position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else
        {
            transform.LookAt(target[current]);
            current = (current + 1) % target.Length;
        }

	}
}
