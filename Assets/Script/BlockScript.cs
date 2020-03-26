﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{

    public Sprite[] sprites;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite =
            sprites[Random.Range(0, sprites.Length)];
    }

    void Update()
    {
        
    }
}
