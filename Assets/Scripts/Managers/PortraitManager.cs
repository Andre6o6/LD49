using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Portraits")]
public class PortraitManager : ScriptableObject
{
    public List<Sprite> Portraits;
    private List<Sprite> _cached;

    private void OnEnable()
    {
        CachePortraits();
    }

    public void CachePortraits()
    {
        System.Random rnd = new System.Random();
        _cached = Portraits.OrderBy(x => rnd.Next()).Take(3).ToList();
    }
    
    public Sprite PortraitForMinister(MinisterSuite suite)
    {
        return _cached[(int) suite - 1];
    }
}
    
