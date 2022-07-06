using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fraction : MonoBehaviour
{
    public Text denominator;
    public Text enumerator;

    private void Awake()
    {
    }

    public void SetDenominator(string deno)
    {
        denominator.text = deno;
    }

    public void SetEnumerator(string enu)
    {
        enumerator.text = enu;
    }

    public void SetFraction(string deno, string enu)
    {
        SetDenominator(deno);
        SetEnumerator(enu);
    }
}
