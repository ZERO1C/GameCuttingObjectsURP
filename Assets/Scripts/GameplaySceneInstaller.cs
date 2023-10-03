using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Scripts.Knife;

public class GameplaySceneInstaller : MonoInstaller
{
    public Controller Controller;
    public BakerManager BakerManager;
    public Knife Knife;

    public override void InstallBindings()
    {
        Container.Bind<Controller>().FromInstance(Controller).AsSingle().NonLazy();
        Container.Bind<BakerManager>().FromInstance(BakerManager).AsSingle().NonLazy();
        Container.Bind<Knife>().FromInstance(Knife).AsSingle().NonLazy();
    }
}
