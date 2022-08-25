using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuakeController : MonoBehaviour
{
    [SerializeField] AnimationCurve _earthQuakeGraph;

    [SerializeField, Range(0, 5)] float _earthQukaeTimeOn = 0;
    [SerializeField, Range(0, 50)] float _magnitude = 1;
    [SerializeField] GameObject _sphere;

    private void Start()
    {
        _sphere = transform.GetChild(0).gameObject;
        _earthQukaeTimeOn = 0;
    }
    
    private void AddForce3d(Rigidbody obj1, Vector3 direction)
    {
        obj1.velocity = direction;
    }
    
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("boom"+ other.name);
        _earthQukaeTimeOn = _earthQukaeTimeOn + Time.deltaTime / 15;
        var value = _earthQuakeGraph.Evaluate(_earthQukaeTimeOn);
        var direction = Vector3.one * value * _magnitude;
        _magnitude = Mathf.Clamp(12 + direction.x, 15, 35);
        //other.gameObject.GetComponent<Rigidbody>().AddRelativeForce(direction, ForceMode.Acceleration);
        AddForce3d(other.gameObject.GetComponent<Rigidbody>(), direction);
        _sphere.transform.localScale = direction.normalized * Mathf.Pow(direction.x, 2);

        GetComponent<SphereCollider>().radius = _magnitude;
        _sphere.transform.localScale = new Vector3(_sphere.transform.localScale.x, 0, _sphere.transform.localScale.z);
    }


}