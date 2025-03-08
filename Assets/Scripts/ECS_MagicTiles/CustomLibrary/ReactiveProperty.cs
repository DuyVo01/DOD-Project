using System;
using UnityEngine;

public class ReactiveProperty<T>
    where T : IEquatable<T>
{
    private T value;
    private event Action<T> OnValueChanged;

    public T Value
    {
        get => value;
        set
        {
            if (!value.Equals(this.value))
            {
                this.value = value;
                OnValueChanged?.Invoke(value);
            }
        }
    }

    public void Subscribe(Action<T> observer)
    {
        OnValueChanged += observer;
        observer?.Invoke(value);
    }

    public void Unsubscribe(Action<T> observer)
    {
        OnValueChanged -= observer;
    }
}

public class ReactiveValue<T>
    where T : IEquatable<T>
{
    [SerializeField]
    protected T _value;

    private ReactiveProperty<T> reactiveProperty = new ReactiveProperty<T>();

    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            reactiveProperty.Value = value;
        }
    }

    public void Subscribe(Action<T> observer) => reactiveProperty.Subscribe(observer);

    public void Unsubscribe(Action<T> observer) => reactiveProperty.Unsubscribe(observer);

    public void OnChangeValidatedInInpsector()
    {
        Value = _value;
    }
}
