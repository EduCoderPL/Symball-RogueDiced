using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    public float lerpCoefficient = 10f;
    public float centerCoefficient = 1f / 3f;
    public float randShakeCoef = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 centerPosition = target.position + (mousePosition - target.position) * centerCoefficient;
        Vector2 randomShakeVector = new Vector2(Random.Range(-randShakeCoef, randShakeCoef), Random.Range(-randShakeCoef, randShakeCoef));
        Vector3 newPosition = new Vector3(centerPosition.x + randomShakeVector.x, centerPosition.y + randomShakeVector.y, -10);

        transform.position = Vector3.Lerp(transform.position, newPosition, lerpCoefficient * Time.deltaTime);
    }
}
