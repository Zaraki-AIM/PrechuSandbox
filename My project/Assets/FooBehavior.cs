using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FooBehavior : MonoBehaviour
{
    private Foo _foo;
    [Inject]
    public void Construct(Foo foo)
    {
        _foo = foo;
        print(_foo);
    }
}

public class Foo { }
