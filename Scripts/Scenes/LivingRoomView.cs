using Godot;
using System;

public partial class LivingRoomView : Node2D {
    [Export]
    private Button _storeButton;

    [Export]
    private Button _kitchenButton;

    [Export]
    private Label _dayLabel;

    [Export]
    private HSlider _healthSlider;
}