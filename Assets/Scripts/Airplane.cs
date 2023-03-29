using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Airplane : MonoBehaviour
{
    public List<Airfoil> airfoils;
    public float thrust;
    private Rigidbody rb;
    private Vector3 input;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
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
        foreach (Airfoil airfoil in airfoils) {
            switch (airfoil.channel) {
                case Airfoil.Channel.NONE:
                    break;
                case Airfoil.Channel.AILERON:
                    airfoil.deflection = input.x;
                    break;
                case Airfoil.Channel.ELEVATOR:
                    airfoil.deflection = input.y;
                    break;
                default:
                    break;
            }
            Vector3 aero = airfoil.Aero(rb) * Time.fixedDeltaTime;
            rb.AddForceAtPosition(aero, airfoil.transform.position, ForceMode.Impulse);
        }

        rb.AddForce(transform.TransformVector(Vector3.forward) * thrust * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    public void FindAirfoils() {
        airfoils.Clear();
        foreach (Airfoil airfoil in GetComponentsInChildren<Airfoil>()) {
            airfoils.Add(airfoil);
        }
    }
}
