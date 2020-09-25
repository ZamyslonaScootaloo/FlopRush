﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    void Update()
    {
        
    }


    public IEnumerator DestroyBullet()
	{
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
	}
}
