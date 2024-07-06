using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObseverManager : MonoBehaviour
{
    public static ObseverManager Instance;
    public List<IDameReciever> lstIDameRecievers = new List<IDameReciever>();
    public void Awake()
    {
        Instance = this;
    }
    public void AddObsever(IDameReciever dameReciever)
    {
        lstIDameRecievers.Add(dameReciever);
    }
    public void RemoveObsever(IDameReciever dameReciever)
    {
        lstIDameRecievers.Remove(dameReciever);
    }
    public void DamageReciever(int dame)
    {
        foreach (IDameReciever dameReciever in lstIDameRecievers)
        {
            dameReciever.DamageReciever(dame);
        }
    }
}
