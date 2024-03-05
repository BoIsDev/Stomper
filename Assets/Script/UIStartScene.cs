using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartScene : MonoBehaviour
{
     public GameObject btPlayGame = null;
    
    public GameObject btCredit = null;

    public GameObject btExit = null;
      private void Awake()
    {
        btPlayGame.SetActive(true);
        btExit.SetActive(true);
        btCredit.SetActive(true);
    }
}
