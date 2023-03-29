using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Airplane))]
public class AirplaneEditor : Editor
{
    public override void OnInspectorGUI(){
        Airplane targetPlane = (Airplane)target;
        targetPlane.FindAirfoils();
        DrawDefaultInspector();
    }
}
