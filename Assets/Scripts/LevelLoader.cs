using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Start()
    {
        GameManager.Instance.StartScene();
    }
    public void ReloadScene()
    {
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        PlayerPrefs.DeleteKey("Score");
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitApplication()
    {
        StartCoroutine(Quit());
    }

    private IEnumerator Quit()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
}
