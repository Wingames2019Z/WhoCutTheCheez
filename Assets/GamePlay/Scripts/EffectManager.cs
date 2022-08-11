using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] GameObject GasEffect;
    [SerializeField] GameObject NearMissEffect;
    [SerializeField] PointEffect PointEffect;
    [SerializeField] Transform InstantiatePoint;
    [SerializeField] float XPoint;
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
    public void InstantiateNearMissEffect(float xPoint)
    {
        Instantiate(NearMissEffect, new Vector3(XPositionNearMiss(xPoint), 1.2f, -1), Quaternion.identity);
    }
    float XPositionSet()
    {
        var x = XPoint;
        var num = Random.Range(0, 2);
        if(num == 1)
        {
            x = - XPoint;
        }
        x = Random.Range(-Range + x, x + Range);
        return x;
    }
    float XPositionNearMiss(float xPoint)
    {
        var x = XPoint;
        if (xPoint < 0)
        {
            x = -XPoint;
        }

        return x;
    }
}
