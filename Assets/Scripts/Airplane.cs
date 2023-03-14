using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Airplane : MonoBehaviour
{
    public List<Airfoil> airfoils;
    public Airfoil wing;
    public Airfoil tail;
    public Airfoil[] ailerons;
    public float thrust;
    private Rigidbody rb;
    private Vector3 input;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
        FindAirfoils();
        print(rb.inertiaTensor);
        print(rb.centerOfMass);
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate() {
        tail.deflection = input.y;
        foreach (Airfoil airfoil in ailerons) {
            airfoil.deflection = input.x;
        }
        // print(transform.InverseTransformVector(rb.velocity));
        // print(wing.Aero(rb));
        foreach (Airfoil airfoil in airfoils) {
            Vector3 aero = airfoil.Aero(rb) * Time.fixedDeltaTime;
            // print(aero);
            // aero = transform.InverseTransformVector(aero) * Time.fixedDeltaTime;
            
            rb.AddForceAtPosition(aero, airfoil.transform.position, ForceMode.Impulse);
        }

        rb.AddForce(transform.TransformVector(Vector3.forward) * thrust * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    void FindAirfoils() {
        foreach (Airfoil airfoil in GetComponentsInChildren<Airfoil>()) {
            airfoils.Add(airfoil);
        }
    }
}
