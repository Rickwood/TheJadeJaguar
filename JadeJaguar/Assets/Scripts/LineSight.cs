using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineSight : MonoBehaviour {

    public enum SightSensitivity {STRICT, LOOSE};
    public SightSensitivity Sensitivity = SightSensitivity.STRICT;
    public bool CanSeeTarget = false;
    public float FieldOfView = 360f;
    public Vector3 LastKnowSighting = Vector3.zero;

    private Transform Target = null;
    public Transform EyePoint = null;
    private Transform ThisTransform = null;
    private SphereCollider ThisCollider = null;

    void Awake()
    {
        ThisTransform = GetComponent<Transform>();
        ThisCollider = GetComponent<SphereCollider>();
        LastKnowSighting = ThisTransform.position;
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    
    bool InFOV()
    {
        Vector3 DirToTarget = Target.position - EyePoint.position;
        float Angle = Vector3.Angle(EyePoint.forward, DirToTarget);
        if (Angle <= FieldOfView)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool ClearLineofSight()
    {
        RaycastHit Info;
        if(Physics.Raycast(EyePoint.position, (Target.position - EyePoint.position).normalized, out Info, ThisCollider.radius))
        {
            if (Info.transform.CompareTag("Player"))
                return true;
        }
        return false;
    }

    void UpdateSight()
    {
        switch(Sensitivity)
        {
            case SightSensitivity.STRICT:
                CanSeeTarget = InFOV() && ClearLineofSight();
                break;
            case SightSensitivity.LOOSE:
                CanSeeTarget = InFOV() || ClearLineofSight();
                break;
        }
    }

    void OnTriggerStay(Collider other)
    {
        UpdateSight();
        
        if(CanSeeTarget)
        {
            SceneManager.LoadScene(3);
            LastKnowSighting = Target.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        CanSeeTarget = false;
    }
}
