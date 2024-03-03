using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
    [Header("Base Button")]
    protected Button button;
    AudioManager audio;
    protected virtual void Start()
    {
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        LoadComponents();
        AddOnClickEvent();
    }

    protected virtual void LoadComponents()
    {
        button = GetComponent<Button>();
    }

    protected virtual void AddOnClickEvent()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    protected abstract void OnClick();
}
