using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] GameObject GasEffect;
    [SerializeField] PointEffect PointEffect;
    [SerializeField] Transform InstantiatePoint;
    [SerializeField] float Range;
    public void InstantiateGas()
    {
        Instantiate(GasEffect, InstantiatePoint.position, Quaternion.identity);
    }
    public void InstantiatePointEffect(int point)
    {
        var pointEffect = Instantiate(PointEffect, new Vector3(XPositionSet(), 0.9f, -1), Quaternion.identity);
        pointEffect.InitialSet(point);
    }
    float XPositionSet()
    {
        var x = Random.Range(-Range, Range);
        return x;
    }
}
