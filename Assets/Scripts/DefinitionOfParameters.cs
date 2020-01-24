using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Скрипт для определения параметров квадратичного уровнения
public class DefinitionOfParameters : MonoBehaviour
{
    public float height;
    private Vector3 point1;
    private Vector3 point2;
    private Vector3 point3;
    private ZoneChecking check;

    private DisksManager disksManager;

    public float a { get; private set; }
    public float b { get; private set; }
    public float c { get; private set; }
    private float timer = 0.0f;

    //DEBUG
    public GameObject pointsPrefab;

    private void Start()
    {
        check = FindObjectOfType<ZoneChecking>().GetComponent<ZoneChecking>();
        if (check == null)
            Debug.LogError("ZoneChecking component not found");
        disksManager = FindObjectOfType<DisksManager>().GetComponent<DisksManager>();
        if (disksManager == null)
            Debug.LogError("DisksManager component not found");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && check.isInZone && !GetComponent<PlayerMovement>().isJumping && timer >= 1.0f)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.collider.gameObject.CompareTag("Ground"))
                    return;
                if (!canJump(hit.point))
                    return;
                Jump();
                disksManager.ReloadGoldenZone();
                GameManager.Instance.AddScore();
            }
        }
    }

    private bool canJump(Vector3 mousePosition)
    {
        point1 = transform.position;
        point2 = new Vector3(transform.position.x - ((transform.position.x - mousePosition.x) / 2), height, transform.position.z);
        point3 = new Vector3(mousePosition.x, transform.position.y, transform.position.z);

        if (point1.x < point3.x || (point1.x - point3.x < 3.5f))
            return false;
        else
            return true;
    }

    private void Jump()
    {
        timer = 0.0f;
        GetComponent<PlayerMovement>().isJumping = true;

        DefinitionOfCoefficients();
        GetComponent<MovingBetweenVerticles>().Calculate(a, b, c, point3);
    }


    //DEBUG
    public void Create(Vector3 point)
    {
        GameObject ball = Instantiate(pointsPrefab, point, Quaternion.identity);
        Destroy(ball, 2.0f);
    }

    private void DefinitionOfCoefficients()
    {
        float x1, x2, x3, y1, y2, y3;
        x1 = point1.x;
        x2 = point2.x;
        x3 = point3.x;
        y1 = point1.y;
        y2 = point2.y;
        y3 = point3.y;

        a = (y3 - (x3 * (y2 - y1) + x2 * y1 - x1 * y2) / (x2 - x1)) / (x3 * (x3 - x1 - x2) + x1 * x2);
        b = (y2 - y1) / (x2 - x1) - a * (x1 + x2);
        c = (x2 * y1 - x1 * y2) / (x2 - x1) + a * x1 * x2;

        float x = x1 - 0.5f;
        float y = a * x * x + b * x + c;
        Create(new Vector3(x, y, 0));
    }
}
