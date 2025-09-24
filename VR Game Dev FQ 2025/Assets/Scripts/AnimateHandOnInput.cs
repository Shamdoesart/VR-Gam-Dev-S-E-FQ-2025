using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
   [Header("Input Actions")]
    public InputActionProperty pinchAction;  // float [0..1]
    public InputActionProperty gripAction;   // float [0..1]

    [Header("Animator")]
    public Animator handAnimator;            // expects "Trigger" and "Grip" floats

    void OnEnable()
    {
        if (pinchAction.action != null)
        {
            pinchAction.action.Enable();
            pinchAction.action.performed += OnPinchChanged;
            pinchAction.action.canceled  += OnPinchChanged; // set to 0 on release
        }
        if (gripAction.action != null)
        {
            gripAction.action.Enable();
            gripAction.action.performed += OnGripChanged;
            gripAction.action.canceled  += OnGripChanged;   // set to 0 on release
        }
    }

    void OnDisable()
    {
        if (pinchAction.action != null)
        {
            pinchAction.action.performed -= OnPinchChanged;
            pinchAction.action.canceled  -= OnPinchChanged;
            pinchAction.action.Disable();
        }
        if (gripAction.action != null)
        {
            gripAction.action.performed -= OnGripChanged;
            gripAction.action.canceled  -= OnGripChanged;
            gripAction.action.Disable();
        }
    }

    void OnPinchChanged(InputAction.CallbackContext ctx)
    {
        if (!handAnimator) return;
        float v = ctx.canceled ? 0f : ctx.ReadValue<float>();
        handAnimator.SetFloat("Trigger", v);
    }

    void OnGripChanged(InputAction.CallbackContext ctx)
    {
        if (!handAnimator) return;
        float v = ctx.canceled ? 0f : ctx.ReadValue<float>();
        handAnimator.SetFloat("Grip", v);
    }
}
