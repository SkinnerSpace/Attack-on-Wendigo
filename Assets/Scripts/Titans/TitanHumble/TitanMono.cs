using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TitanMono : MonoBehaviour
{
    [SerializeField] public TitanSetup setup;
    private Titan titan;

    private void Awake()
    {
        TitanData data = PackData(); 
        titan = TitansFactory.CreateTitan(data);
    }

    private TitanData PackData()
    {
        TitanData data = new TitanData(setup);
        data.transform = new TransformProxy(transform);

        return data;
    }

    private void Update()
    {
        titan.Update();
    }
}
