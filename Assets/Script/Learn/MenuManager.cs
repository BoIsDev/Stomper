    using System.Collections;
    using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
    using UnityEngine.UI;
    public class item
    {
        public string name;
        public string img;
        public item(string name, string img)
        {
            this.name = name;
            this.img = img;
        }
    }


public class MenuManager : MonoBehaviour
{
    private static MenuManager instance;
    public static MenuManager Instance => instance;
    public GameObject btnPlay;
    //public ScrollRect ScrollMenuManager; // tại sao là khai báo ScrollRect??
    public Transform Content;
    public Transform PanelManager;
    protected GameObject _newBtnUI;
    private Object[] textures;
    public List<item> lstItems = new List<item>();
    Texture2D texture = null;


    protected virtual void Awake()
    {
        if (MenuManager.instance != null)
        {
            Debug.LogWarning(" Only one instance MenuManager can be created");
        }
        else
        {
            MenuManager.instance = this;
        }
        textures = Resources.LoadAll("Textures", typeof(Texture2D));

        InitButton("Play", "sritePlay");
        InitButton("Map", "spriteMap");
        InitButton("Credit", "spriteCredit");
        InitButton("Exit", "spriteExit");
    }


    public virtual void InitButton(string nameObj, string imgObj)
    {
        lstItems.Add(new item(nameObj, imgObj));
        GameObject _newBtnUI = Instantiate(btnPlay, Content, true);
        _newBtnUI.name = nameObj;
        _newBtnUI.SetActive(true);
        _newBtnUI.transform.GetChild(0).GetComponent<Text>().text = nameObj;
        for (int j = 0; j < textures.Length; j++)
        {
            if (textures[j].name == imgObj)
            {
                texture = textures[j] as Texture2D;
                Rect rec = new Rect(0, 0, texture.width, texture.height);
                _newBtnUI.transform.GetChild(1).GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
                break;
            }
        }
        _newBtnUI.GetComponent<Button>().onClick.AddListener(() => ChangeImg(imgObj));

    }

    public virtual void CreateButton()
    {
        int itemObj = Random.Range(0, lstItems.Count);
        for (int i = 0; i < lstItems.Count; i++)
        {
            if (i == itemObj)
            {
                string nameObj = lstItems[i].name;
                string imgObj = lstItems[i].img;
                lstItems.Add(new item(nameObj, imgObj));
                GameObject _newBtnUI = Instantiate(btnPlay, Content, true);
                _newBtnUI.name = nameObj;
                _newBtnUI.SetActive(true);
                _newBtnUI.transform.GetChild(0).GetComponent<Text>().text = nameObj;

                for (int j = 0; j < textures.Length; j++)
                {
                    if (textures[j].name == imgObj)
                    {
                        texture = textures[j] as Texture2D;
                        Rect rec = new Rect(0, 0, texture.width, texture.height);
                        _newBtnUI.transform.GetChild(1).GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
                        break;
                    }

                }
                _newBtnUI.GetComponent<Button>().onClick.AddListener(() => ChangeImg(imgObj));

            }

        }
    }

    public virtual void RemoveButton()
    {
        // int count = Content.transform.ChildCount();


        int count = Content.childCount;
        Destroy(Content.GetChild(count - 1).gameObject);
    }

    public virtual void ChangeImg(string imgObj)
    {
        for (int j = 0; j < textures.Length; j++)
        {
            if (textures[j].name == imgObj)
            {
                texture = textures[j] as Texture2D;
                Rect rec = new Rect(0, 0, texture.width, texture.height);
                PanelManager.GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
                break;
            }

        }

    }

   
}