using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airfoil : MonoBehaviour
{
    private static float airDensity = 1.225f;
    public float span;
    public float chord;
    public float baseDrag;
    // public float liftCoeff;
    private float area;
    private float aspectRatio;
    public float deflectionCoeff;
    public float deflection;
    public AirfoilData data;

    // Start is called before the first frame update
    void Start()
    {
        area = span * chord;
        aspectRatio = span / chord;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 Aero(Rigidbody rb) {
        // Vector3 aero = Vector3.zero;
        Vector3 velocity = rb.GetPointVelocity(transform.position);
        velocity = transform.InverseTransformVector(velocity);
        float alpha = Mathf.Atan2(-velocity.y, velocity.z);
        float alphaDeg = alpha * Mathf.Rad2Deg;
        float cl = Mathf.Sign(alphaDeg) * data.liftCoeff.Evaluate(Mathf.Abs(alphaDeg)) + deflection * deflectionCoeff;
        if (gameObject.name.CompareTo("Wing") == 0) {
            print(alphaDeg);
            print(cl);
        }
        
        float cd = data.dragCoeff.Evaluate(Mathf.Abs(alphaDeg));//Mathf.Pow(cl, 2) / (Mathf.PI * aspectRatio) + baseDrag;
        float pbar = velocity.sqrMagnitude * airDensity / 2;
        float drag = cd * pbar * area;
        float lift = cl * pbar * area;

        float sa = Mathf.Sin(alpha);
        float ca = Mathf.Cos(alpha);
        Vector3 aero = new Vector3(0f, ca * lift + sa * drag, -ca * drag + sa * lift);
        aero = transform.TransformVector(aero);
        return aero;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(span, 0, chord));
        Gizmos.matrix = Matrix4x4.identity;
    }
}
