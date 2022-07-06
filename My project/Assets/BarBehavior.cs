using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BarBehavior : MonoBehaviour
{
    public HogeBehavior _hoge;

    [Inject]
    public void Constructor(HogeBehavior hoge)
    {
        _hoge = hoge;
    }

}
