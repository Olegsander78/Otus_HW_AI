using System.Collections.Generic;
using Elementary;
using MonoOptimization;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay.Conveyors
{
    [AddComponentMenu("Gameplay/Conveyors/Conveyor Core Module")]
    public sealed class CoreModule : MonoModule
    {
        [Header("Storages")]
        [ShowInInspector, ReadOnly]
        public IntVariableLimited loadStorage = new();

        [ShowInInspector, ReadOnly]
        public IntVariableLimited unloadStorage = new();

        [Header("Enable")]
        [ShowInInspector, ReadOnly]
        public BoolVariable enableVariable = new();

        [Header("Work")]
        [ShowInInspector, ReadOnly]
        public Timer workTimer = new();

        [ShowInInspector, ReadOnly]
        private WorkMechanics workMechanics = new();

        public override IEnumerable<IMonoComponent> ProvideMonoComponents()
        {
            yield return this.workMechanics;
        }

        public override void ConstructSensor(MonoContextModular context)
        {
            this.ConstructWorkMechanics();
            
            var configModule = context.GetModule<ConfigModule>();
            var config = configModule.conveyourConfig;
            this.ConstructFromConfig(config);
        }

        private void ConstructWorkMechanics()
        {
            this.workMechanics.Construct(
                isEnable: this.enableVariable,
                loadStorage: this.loadStorage,
                unloadStorage: this.unloadStorage,
                workTimer: this.workTimer
            );
        }

        private void ConstructFromConfig(ScriptableConveyour config)
        {
            this.loadStorage.MaxValue = config.inputCapacity;
            this.unloadStorage.MaxValue = config.outputCapacity;
            this.workTimer.Duration = config.workTime;
        }
    }
}