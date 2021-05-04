using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform animationTransform = null;
    [SerializeField]
    private float animationSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        animationTransform.Rotate(new Vector3(0, 0, animationSpeed * Time.deltaTime));
    }
}
