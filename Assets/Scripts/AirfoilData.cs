using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AirfoilData : ScriptableObject
{
    public AnimationCurve liftCoeff;
    public AnimationCurve dragCoeff;
}
