using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private MenuScript _menuScript;
    [SerializeField] private ResourcesManagerScript _resourcesManagerScript;
    [SerializeField] private ArmyScript _armyScript;
    [SerializeField] private VisualBattleScript _visualBattleScript;
    [SerializeField] private EventsScript _eventsScript;
    [SerializeField] private BattleScript _battleScript;
    [SerializeField] private EnemyArmyScript _enemyArmyScript;
    [SerializeField] private SeasonUIScript _seasonUIScript;
    [SerializeField] private ModifierScript _modifierScript;
    [SerializeField] private BuildingsScript _buildingsScript;
    [SerializeField] private SoundScript _soundScript;
    [SerializeField] private TradeScript _tradeScript;
    [SerializeField] private TimeScript _timeScript;
    [SerializeField] private EnemyArmyData _enemyArmyData;
    [SerializeField] private ArmyData _armyData;
    [SerializeField] private FortressData _fortressData;
    [SerializeField] private SaveAndLoadScript _saveAndLoadScript;

    public override void InstallBindings()
    {
        Container.BindInstance(_timeScript).AsSingle();
        Container.BindInstance(_resourcesManagerScript).AsSingle();
        Container.BindInstance(_modifierScript).AsSingle();
        Container.BindInstance(_enemyArmyScript).AsSingle();
        Container.BindInstance(_enemyArmyData).AsSingle();
        Container.BindInstance(_armyScript).AsSingle();
        Container.BindInstance(_armyData).AsSingle();
        Container.BindInstance(_buildingsScript).AsSingle();
        Container.BindInstance(_soundScript).AsSingle();
        Container.BindInstance(_battleScript).AsSingle();
        Container.BindInstance(_seasonUIScript).AsSingle();
        Container.BindInstance(_visualBattleScript).AsSingle();
        Container.BindInstance(_eventsScript).AsSingle();
        Container.BindInstance(_tradeScript).AsSingle();
        Container.BindInstance(_fortressData).AsSingle();
        Container.BindInstance(_menuScript).AsSingle();
        Container.BindInstance(_saveAndLoadScript).AsSingle();
    }
}
