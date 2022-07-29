using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    [Tooltip("Target of camera.")]
    [SerializeField] Transform target;

    [Tooltip("It defines part of distance between target and mouse that camera will follow.\0 means camera on target; \n1 means camera on mouse.")]
    [Range(0, 1)]
    [SerializeField] float centerCoefficient = 1f / 3f;

    [Tooltip("It defines strength that camera will follow target.")]
    [SerializeField] float lerpCoefficient = 10f;

    [Tooltip("It defines how big shake does camera have.")]
    [SerializeField] float shakeCoef = 0.2f;


    void Update()
    {
        SetCameraPosition();

        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(1);
        }
    }

    void SetCameraPosition()
    {
        if (target != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Get Position between target and mouse;
            Vector3 targetMousePosition = target.position + (mousePosition - target.position) * centerCoefficient;


            Vector2 shakeVector = new(Random.Range(-shakeCoef, shakeCoef), Random.Range(-shakeCoef, shakeCoef));
            Vector3 newPosition = new(targetMousePosition.x + shakeVector.x, targetMousePosition.y + shakeVector.y, -10);

            transform.position = Vector3.Lerp(transform.position, newPosition, lerpCoefficient * Time.deltaTime);
        }
        else
        {
            StartCoroutine(BackToMenu());
        }
    }


    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
