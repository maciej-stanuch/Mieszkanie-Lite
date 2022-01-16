using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class LineRendererSettings : MonoBehaviour
{
    [SerializeField] LineRenderer rend;

    Vector3[] points;
    public LayerMask layerMask;
    public GameObject panel;
    private Image img;
    private Button button;

    private InputDevice rightController;
    private InputDevice leftController;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<LineRenderer>();
        points = new Vector3[2];
        points[0] = Vector3.zero;
        points[1] = transform.position + new Vector3(0, 0, 30);   

        rend.SetPositions(points);
        rend.enabled = true;

        img = panel.GetComponent<Image>();

        List<InputDevice> leftControllers = new List<InputDevice>();
        List<InputDevice> rightControllers = new List<InputDevice>();

        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;

        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, rightControllers);
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, leftControllers);

        if (rightControllers.Count > 0) {
            rightController = rightControllers[0];
        }

        if (leftControllers.Count > 0) {
            leftController = leftControllers[0];
        }
    }

    public bool AlignLineRenderer(LineRenderer rend) {
        bool hitButton = false;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, layerMask)) {
            rend.startColor = Color.blue;
            rend.endColor = Color.blue;

            points[1] = transform.forward + new Vector3(0, 0, hit.distance);
            button = hit.collider.gameObject.GetComponent<Button>();
            hitButton = true;
        } else {
            rend.startColor = Color.green;
            rend.endColor = Color.green;

            points[1] = transform.forward + new Vector3(0, 0, 30);
            hitButton = false;
        }

        rend.SetPositions(points);
        rend.material.color = rend.startColor;
        return hitButton;
    }

    public void ChangeColorOnClick() {
        img.color = Color.yellow;
        if (button != null) {
            img.color = Color.red;
            if (button.name == "Start") {
                SceneManager.LoadScene(1);
            } else {
                Application.Quit();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (AlignLineRenderer(rend) && primaryButtonValue) {
            button.onClick.Invoke();
        }
    }
}
