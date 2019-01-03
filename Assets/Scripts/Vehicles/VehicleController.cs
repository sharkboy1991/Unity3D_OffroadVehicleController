using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    Rigidbody _rig;

    public WheelCollider[] _wC;

    public bool engineState = false;
    public float enginePower = 5000f;

    public float kph = 0;

    float vertical = 0;
    float horizontal = 0;

    [Header("Steering")]
    public float maxSteer = 25f;
    public float steerSpeed = 5f;

    public Transform[] wheelRot;
    public Transform[] wheelSpin;

    float curSteer = 0;
    float newSteerAngle;

    [Header("Audio")]
    public AudioSource engineSound;
    public GameObject startEngineSound;
    float enginePitch = 1;
    float engineVol = 1;

    void Start()
    {
        _rig = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //calculate vehicle speed
        kph = _rig.velocity.magnitude * 3.6f;

        GetInputs();

        Steer();

        if (engineState)
        {
            //calculate engine pitch and volume
            enginePitch = Mathf.Lerp(1.4f, .9f, (kph - 100) / (0 - 100));
            engineVol = Mathf.Lerp(1f, .2f, (kph - 50) / (0 - 50));

            engineSound.pitch = enginePitch;
            engineSound.volume = engineVol;

            //move vehicle
            AddTorque();
        }

    }

    void GetInputs()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.E))
        {
            engineState = !engineState;

            if (engineState)
            {
                //instantiate engine start sound prefab
                GameObject cloneSound = Instantiate(startEngineSound);
                cloneSound.transform.SetParent(transform);
                cloneSound.transform.localPosition = Vector3.zero;

                engineSound.gameObject.SetActive(true);
            }
            else
            {
                engineSound.gameObject.SetActive(false);
            }
        }
    }

    void Steer()
    {
        curSteer = maxSteer * horizontal;

        //lerp steer
        newSteerAngle = Mathf.MoveTowards(newSteerAngle, curSteer, Time.deltaTime * steerSpeed);

        wheelRot[0].localEulerAngles = new Vector3(0, 50 + (-newSteerAngle * -1), 0);
        wheelRot[1].localEulerAngles = new Vector3(0, 50 + (newSteerAngle * 1), 0);

        //steer wheel coliders
        for (int i = 0; i < 2; i++)
            _wC[i].steerAngle = newSteerAngle;

        //rotate mesh wheels
        for (int i = 0; i < 4; i++)
            wheelSpin[i].transform.Rotate(Vector3.right * Time.deltaTime * kph * 100);
    }

    void AddTorque()
    {
        _wC[0].motorTorque = enginePower * vertical;
        _wC[1].motorTorque = enginePower * vertical;
        _wC[2].motorTorque = enginePower * vertical;
        _wC[3].motorTorque = enginePower * vertical;
    }
}
