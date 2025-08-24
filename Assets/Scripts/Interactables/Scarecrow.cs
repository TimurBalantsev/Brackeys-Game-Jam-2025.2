﻿using System;
using UnityEngine;

public class Scarecrow : MonoBehaviour, Interactable
{
    [SerializeField] private string scareMessage;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color highlightColor = Color.yellow;
    private Color defaultColor;

    private void Start()
    {
        defaultColor = spriteRenderer.color;
    }

    public void Interact(Player player)
    {
        Debug.Log(scareMessage);
    }

    public void Select(Player player, bool isSelected)
    {
        if (isSelected)
        {
            spriteRenderer.color = highlightColor;
        }
        else
        {
            spriteRenderer.color = defaultColor;
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
