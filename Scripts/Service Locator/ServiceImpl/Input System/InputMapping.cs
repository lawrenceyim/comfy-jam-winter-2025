using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using FileSystem;
using Godot;

public class InputMapping {
    # region Consts

    private const string _moveLeft = "MoveLeft";
    private const string _moveUp = "MoveUp";
    private const string _leftClick = "LeftClick";

    # endregion

    private static readonly Dictionary<string, InputBinding> _defaultKey = new Dictionary<string, InputBinding> {
        { _moveLeft, new InputBinding(Key.A) },
        { _moveUp, new InputBinding(Key.W) },
        { _leftClick, new InputBinding(MouseButton.Left) },
    };

    public static void InitInput() {
        GD.Print("Input Mapping binded.");
        if (!_LoadInputBindingFromJson()) {
            ResetToDefault();
            SaveInputBinding();
        }
    }

    private static bool _LoadInputBindingFromJson() {
        GD.Print("Loading key bindings.");
        try {
            string json = JsonUtils.GetJsonFromFile(FilePath.InputMappingJson, "Input Mapping JSON error");
            Dictionary<string, InputBinding> inputBinding = JsonSerializer.Deserialize<Dictionary<string, InputBinding>>(json);
            foreach (KeyValuePair<string, InputBinding> kvp in inputBinding) {
                ResetAndAddKey(kvp.Key, kvp.Value);
            }

            return true;
        }
        catch (Exception) {
            GD.Print("Binding do not exist exception");
            return false;
        }
    }

    public static void SaveInputBinding() {
        GD.Print("Saving bindings");

        Dictionary<string, InputBinding> bindings = [];
        Godot.Collections.Array<StringName> actions = InputMap.GetActions();
        foreach (StringName action in actions) {
            if (!_defaultKey.ContainsKey(action)) {
                continue;
            }

            GD.Print($"Action StringName: {action}");
            Godot.Collections.Array<InputEvent> events = InputMap.ActionGetEvents(action);

            foreach (InputEvent e in events) {
                if (e is InputEventKey k) {
                    GD.Print($"{k} is key.");
                    bindings.Add(action, new InputBinding(k.Keycode));
                } else if (e is InputEventMouseButton mb) {
                    GD.Print($"{mb} is mousebutton.");
                    bindings.Add(action, new InputBinding(mb.ButtonIndex));
                } else {
                    GD.Print($"{e} is neither.");
                }
            }

            GD.Print("\n");
        }

        JsonUtils.WriteToJsonFile(FilePath.InputMappingJson, JsonSerializer.Serialize(bindings));
    }

    public static void ResetToDefault() {
        GD.Print("Restoring default bindings");
        foreach (KeyValuePair<string, InputBinding> kvp in _defaultKey) {
            ResetAndAddKey(kvp.Key, kvp.Value);
        }
    }

    private static void ResetAndAddKey(string actionName, InputBinding inputBinding) {
        if (InputMap.HasAction(actionName)) {
            InputMap.EraseAction(actionName);
        }

        InputMap.AddAction(actionName);
        InputMap.ActionAddEvent(actionName, inputBinding.ToInputEvent());
    }
}

public class InputBinding {
    [JsonInclude]
    [JsonPropertyName("type")]
    private InputType _type;

    [JsonInclude]
    [JsonPropertyName("code")]
    private int _code;

    public InputBinding() { }

    public InputBinding(MouseButton mouseButton) {
        _type = InputType.Mouse;
        _code = (int)mouseButton;
    }

    public InputBinding(Key key) {
        _type = InputType.Key;
        _code = (int)key;
    }

    public InputEvent ToInputEvent() {
        return _type switch {
            InputType.Key => new InputEventKey { Keycode = (Key)_code },
            InputType.Mouse => new InputEventMouseButton { ButtonIndex = (MouseButton)_code },
            _ => null
        };
    }
}