using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Скрипт для перемещения игрока по Вершинам параболы
public class MovingBetweenVerticles : MonoBehaviour
{
    private float step;
    private List<Vector3> verticles = new List<Vector3>();
    private Vector3 endPos;
    private float time = 0.15f;
    
    private float a;
    private float b;
    private float c;


    public void Calculate(float a, float b, float c, Vector3 endPos)
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.endPos = endPos;

        AddVerticles();
    }

    private void AddVerticles()
    {
        float x = transform.position.x;
        StepCount(x, endPos.x);

        while(x - step > endPos.x)
        {
            x -= step;
            float y = a * x * x + b * x + c;
            verticles.Add(new Vector3(x, y, 0.0f));
            GetComponent<DefinitionOfParameters>().Create(verticles[verticles.Count - 1]);
        }
        verticles.Add(endPos);
        GetComponent<DefinitionOfParameters>().Create(endPos);

        if (!GetComponent<Animator>())
        {
            Debug.LogError("The Player doesn't have an Animator component");
        }
        else
            GetComponent<Animator>().SetTrigger("Flip");

        
        StartCoroutine(MoveCoroutine());
    }

    private void StepCount(float x1, float x2)
    {
        step = (x1 - x2) / 10;

    }

    private IEnumerator MoveCoroutine()
    {
        while (verticles.Count != 0)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = verticles[0];
            for (float t = 0; t <= 1 * time; t += Time.deltaTime)
            {
                transform.position = Vector3.Lerp(startPos, endPos, t / time);
                yield return null;
            }
            verticles.RemoveAt(0);
        }

        transform.position = this.endPos;
        GetComponent<PlayerMovement>().isJumping = false;
    }
}
