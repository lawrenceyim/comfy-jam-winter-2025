using System;
using Godot;

public class TickTimer : ITick {
    public event Action TimedOut;
    private readonly Random _random = new Random();
    private int _minTicksInclusive;
    private int _maxTicksExclusive;
    private int _ticksLeft;
    private bool _loop;
    private bool _stopped = true;

    public TickTimer() { }

    public void StartFixedTimer(bool loop, int ticks) {
        _ticksLeft = ticks;
        _minTicksInclusive = ticks;
        _maxTicksExclusive = ticks + 1;
        _loop = loop;
        _stopped = false;
    }

    public void StartRandomTimer(bool loop, int minTicksInclusive, int maxTicksExclusive) {
        _minTicksInclusive = minTicksInclusive;
        _maxTicksExclusive = maxTicksExclusive;
        _loop = loop;
        _ticksLeft = _random.Next(_minTicksInclusive, _maxTicksExclusive);
        _stopped = false;
    }

    public void PhysicsTick() {
        if (_stopped) {
            return;
        }

        _ticksLeft--;
        // GD.Print($"Ticks left {_ticksLeft}");
        if (_ticksLeft == 0) {
            if (!_loop) {
                _stopped = true;
                TimedOut?.Invoke();
                return;
            }

            TimedOut?.Invoke();
            _ticksLeft = _random.Next(_minTicksInclusive, _maxTicksExclusive);
        }
    }

    public int GetTicksLeft() {
        return _ticksLeft;
    }

    public void Stop() {
        _stopped = true;
    }

    public void Start() {
        _stopped = false;
    }
}