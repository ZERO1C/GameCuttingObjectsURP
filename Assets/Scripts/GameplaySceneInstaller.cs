using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    public GameManager GameManager;
    public BakerManager BakerManager;

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(GameManager).AsSingle().NonLazy();
        Container.Bind<BakerManager>().FromInstance(BakerManager).AsSingle().NonLazy();
    }
}
