using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputManager : Singleton<InputManager>
{
    private Controls controls;

    private Vector2 mousePosition;

    protected override void Awake()
    {
        base.Awake();

        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.Player.Select.performed += OnSelect;
        controls.Player.Point.performed += OnPoint;
    }

    private void OnDisable()
    {
        controls.Disable();

        controls.Player.Select.performed -= OnSelect;
        controls.Player.Point.performed -= OnPoint;
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log($"{hit.collider.transform.parent.name} selected");

            var selectable = hit.collider.GetComponentInParent<ISelectable>();
            if (selectable != null) selectable.OnSelect();
        }
    }

    private void OnPoint(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
}
