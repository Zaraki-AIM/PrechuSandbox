using UnityEngine;
using Zenject;

public class ExampleInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Foo>().AsCached();
    }
}