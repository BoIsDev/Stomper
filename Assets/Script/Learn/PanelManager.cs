using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public Button btnAdd;
    public Button btnRemove;

    public Transform Content;
    void Start()
    {
        ClickAddButton();
        ClickRemoveButton();
    }


    public void ClickAddButton()
    {
         btnAdd.GetComponent<Button>().onClick.AddListener(delegate {

            MenuManager.Instance.CreateButton();
            Debug.Log(MenuManager.Instance.lstItems.Count);
       });
    }


    public void ClickRemoveButton()
    {
        btnRemove.GetComponent<Button>().onClick.AddListener(delegate {
        MenuManager.Instance.RemoveButton();
            Debug.Log(MenuManager.Instance.lstItems.Count);
       });
    }

  
   
    

}
