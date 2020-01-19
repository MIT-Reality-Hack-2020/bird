using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class TriggerPressedEvent : UnityEvent<bool> { }

public class TriggerWatcher : MonoBehaviour
{
    public TriggerPressedEvent triggerPressed;
    public TriggerPressedEvent triggerReleased;
    public bool LeftHand = true;

    private bool lastTriggerPressedState = false;
    private List<InputDevice> devicesWithTrigger;

    private void Awake()
    {
        if (triggerPressed == null)
        {
            triggerPressed = new TriggerPressedEvent();
        }

        devicesWithTrigger = new List<InputDevice>();
    }

    void OnEnable()
    {
        List<InputDevice> handedDevices = new List<InputDevice>();
        InputDevices.GetDevices(handedDevices);

        if (LeftHand) {
            InputDevices.GetDevicesWithRole(InputDeviceRole.LeftHanded, handedDevices);
        }
        else {
            InputDevices.GetDevicesWithRole(InputDeviceRole.RightHanded, handedDevices);
        }
        foreach(InputDevice device in handedDevices)
            InputDevices_deviceConnected(device);

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
        devicesWithTrigger.Clear();
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        float discardedValue;
        if (device.TryGetFeatureValue(CommonUsages.trigger, out discardedValue))
        {
            devicesWithTrigger.Add(device); // Add any devices that have a primary button.
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (devicesWithTrigger.Contains(device))
            devicesWithTrigger.Remove(device);
    }

    void Update()
    {
        float tempState = 0f;
        foreach (var device in devicesWithTrigger)
        {
            float triggerState = 0f;
            tempState = device.TryGetFeatureValue(CommonUsages.trigger, out triggerState) ?// did get a value
                         triggerState // the value we got
                        : tempState; // cumulative result from other controllers
        }

        if ((tempState > .3f) != lastTriggerPressedState) // Button state changed since last frame
        {
            if (tempState > .3f) {
                triggerPressed.Invoke(true);
            }
            else {
                triggerReleased.Invoke(false);
            }
            lastTriggerPressedState = tempState > .3f;
        }
    }
}