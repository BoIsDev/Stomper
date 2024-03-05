using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int coinCount = 0;
    public int levelCount = 0;
    public Text levelText;
   
    public Text cointext;



       // Update is called once per frame
    void Update()
    {
        cointext.text = "x "+ coinCount.ToString()+"/5";
        levelText.text = "level "+ levelCount.ToString();
    }
}
