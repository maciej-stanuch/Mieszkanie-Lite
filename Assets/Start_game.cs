using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Start_game : MonoBehaviour, IPointerDownHandler
{
    public bool isStart;
    public bool isExit;
    private Renderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        myRenderer.material.color = Color.white;
    }

    public void OnMouseEnter() {
        myRenderer.material.color = Color.blue;
    }

    public void OnMouseExit() {
        myRenderer.material.color = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (isStart) {
            SceneManager.LoadScene(1);
        } else if (isExit) {
            Application.Quit();
        }
    }

    // Update is called once per frame
    void OnMouseUp()
    {
        if (isStart) {
            SceneManager.LoadScene(1);
        } else if (isExit) {
            Application.Quit();
        }
    }
}
