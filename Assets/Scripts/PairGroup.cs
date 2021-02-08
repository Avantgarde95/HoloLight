using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class PairGroup : MonoBehaviour
{
    private int pairCount = 4;
    private List<Pair> pairs = new List<Pair>();

    void Start()
    {
        for (var i = 1; i <= pairCount; i++)
        {
            pairs.Add(new Pair("Pair" + i));
        }
    }

    void Update()
    {
        foreach (var pair in pairs)
        {
            pair.mapVideoToImage();
            pair.mapVideoToModel();
            pair.animateModel();
        }
    }
}
