using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class LeanTween
{
    private static Dictionary<string, object> sequences = new Dictionary<string, object>();

    public static Sequence CreateSequence(string name)
    {
        if (sequences.ContainsKey(name))
        {
            return (Sequence)sequences[name];
        }

        sequences[name] = DOTween.Sequence();

        return sequences[name] as Sequence;
    }

    public static Sequence GetSequence(string name)
    {
        if (sequences.ContainsKey(name))
        {
            return (Sequence)sequences[name];
        }

        throw new KeyNotFoundException();
    }

    public static Tween Scale(
        Transform target,
        float startValue,
        float endValue,
        float duration,
        int ease
    )
    {
        target.localScale = new Vector3(startValue, startValue, startValue);

        return target.DOScale(endValue, duration).SetEase((Ease)ease);
    }
}
