using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class KeyBindingsManager : MonoBehaviour
{
    [System.Serializable]
    public class KeyBinding
    {
        public string actionName;
        public Button button;
        public Text keyText;
        public KeyCode keyCode;
    }

    public List<KeyBinding> keyBindings;

    private KeyBinding currentBinding;
    private Dictionary<string, KeyCode> actionKeyBindings;

    void Start()
    {
        actionKeyBindings = new Dictionary<string, KeyCode>();

        foreach (var binding in keyBindings)
        {
            if (PlayerPrefs.HasKey(binding.actionName))
            {
                binding.keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(binding.actionName));
                binding.keyText.text = binding.keyCode.ToString();
            }
            else
            {
                binding.keyCode = KeyCode.None;
                binding.keyText.text = "None";
            }

            actionKeyBindings[binding.actionName] = binding.keyCode;
            binding.button.onClick.AddListener(() => OnClickBindingButton(binding));
        }
    }

    void Update()
    {
        if (currentBinding != null)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    currentBinding.keyCode = keyCode;
                    currentBinding.keyText.text = keyCode.ToString();
                    PlayerPrefs.SetString(currentBinding.actionName, keyCode.ToString());
                    actionKeyBindings[currentBinding.actionName] = keyCode;
                    Debug.Log("Key binding updated: " + currentBinding.actionName + " -> " + keyCode);
                    currentBinding = null;
                    break;
                }
            }
        }
    }

    void OnClickBindingButton(KeyBinding binding)
    {
        currentBinding = binding;
        Debug.Log("Awaiting new key binding for action: " + binding.actionName);
    }

    public KeyCode GetKeyCodeForAction(string actionName)
    {
        if (actionKeyBindings.TryGetValue(actionName, out KeyCode keyCode))
        {
            return keyCode;
        }
        return KeyCode.None;
    }
}
