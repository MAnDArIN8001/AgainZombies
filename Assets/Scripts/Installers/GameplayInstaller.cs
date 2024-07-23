using Zenject;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MainInput>().FromNew().AsSingle().NonLazy();
    }
}
