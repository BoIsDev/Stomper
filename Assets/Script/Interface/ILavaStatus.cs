using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILavaStatus
{
    void OnLavaEnter(bool isOnLava);
    void OnLavaExit(bool isOnLava);
}
