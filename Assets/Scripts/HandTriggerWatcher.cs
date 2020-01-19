using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class HandTriggerPressedEvent : UnityEvent<bool> { }

public class HandTriggerWatcher : MonoBehaviour
{
    public HandTriggerPressedEvent handTriggerPressed;
    public HandTriggerPressedEvent handTriggerReleased;
    public bool LeftHand = true;

    private bool lastTriggerPressedState = false;
    private List<InputDevice> devicesWithHandTrigger;

    private void Awake()
    {
        if (handTriggerPressed == null)
        {
            handTriggerPressed = new HandTriggerPressedEvent();
        }

        devicesWithHandTrigger = new List<InputDevice>();
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
        devicesWithHandTrigger.Clear();
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        bool discardedValue;
        if (device.TryGetFeatureValue(CommonUsages.gripButton, out discardedValue))
        {
            devicesWithHandTrigger.Add(device); // Add any devices that have a primary button.
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (devicesWithHandTrigger.Contains(device))
            devicesWithHandTrigger.Remove(device);
    }

    void Update()
    {
        bool tempState = false;
        foreach (var device in devicesWithHandTrigger)
        {
            bool handTriggerState = false;
            tempState = device.TryGetFeatureValue(CommonUsages.gripButton, out handTriggerState) // did get a value
                        && handTriggerState // the value we got
                        || tempState; // cumulative result from other controllers
        }

        if (tempState != lastTriggerPressedState) // Button state changed since last frame
        {
            if (tempState) {
                handTriggerPressed.Invoke(true);
            }
            else {
                handTriggerReleased.Invoke(false);
            }
            lastTriggerPressedState = tempState;
        }
    }
}