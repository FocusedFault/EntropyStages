using RoR2;
using RoR2.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace EntropyStages
{
    public class CrystalController : MonoBehaviour
    {
        public SpawnCard crystalSpawnCard = Addressables.LoadAssetAsync<SpawnCard>((object)"RoR2/Base/WeeklyRun/bscTimeCrystal.asset").WaitForCompletion();
        public uint crystalCount = 3;
        public uint crystalsRequiredToKill = 3;
        private List<OnDestroyCallback> crystalActiveList = new List<OnDestroyCallback>();

        public uint crystalsKilled => (uint)(this.crystalCount - (ulong)this.crystalActiveList.Count);

        public void Start()
        {
            DirectorPlacementRule placementRule = new DirectorPlacementRule();
            placementRule.placementMode = DirectorPlacementRule.PlacementMode.Random;
            for (int index = 0; index < this.crystalCount; ++index)
                this.crystalActiveList.Add(OnDestroyCallback.AddCallback(DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(this.crystalSpawnCard, placementRule, Run.instance.stageRng)), component => this.crystalActiveList.Remove(component)));
            ObjectivePanelController.collectObjectiveSources += new Action<CharacterMaster, List<ObjectivePanelController.ObjectiveSourceDescriptor>>(this.ReportObjective);
        }

        public void ReportObjective(
          CharacterMaster master,
          List<ObjectivePanelController.ObjectiveSourceDescriptor> output)
        {
            if ((int)this.crystalsKilled == (int)this.crystalCount)
                return;
            output.Add(new ObjectivePanelController.ObjectiveSourceDescriptor()
            {
                source = this,
                master = master,
                objectiveType = typeof(ObjectivePanelController.DestroyTimeCrystals)
            });
        }

        public void FixedUpdate()
        {
            if (!(bool)TeleporterInteraction.instance)
                return;
            bool flag = this.crystalsRequiredToKill > this.crystalsKilled;
            if (flag == TeleporterInteraction.instance.locked)
                return;
            if (flag)
            {
                if (!NetworkServer.active)
                    return;
                TeleporterInteraction.instance.locked = true;
            }
            else
            {
                if (NetworkServer.active)
                    TeleporterInteraction.instance.locked = false;
                ChildLocator component = TeleporterInteraction.instance.GetComponent<ModelLocator>().modelTransform.GetComponent<ChildLocator>();
                if (!(bool)component)
                    return;
                Transform child = component.FindChild("TimeCrystalBeaconBlocker");
                EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/TimeCrystalDeath"), new EffectData()
                {
                    origin = child.transform.position
                }, false);
                child.gameObject.SetActive(false);
                ObjectivePanelController.collectObjectiveSources -= new Action<CharacterMaster, List<ObjectivePanelController.ObjectiveSourceDescriptor>>(this.ReportObjective);
            }
        }
    }
}