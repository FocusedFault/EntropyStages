using BepInEx;
using R2API;
using RoR2;
using RoR2.Navigation;
using RoR2.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace EntropyStages
{
  [BepInPlugin("com.Nuxlar.EntropyStages", "EntropyStages", "0.9.2")]
  [BepInDependency(EliteAPI.PluginGUID)]

  public class EntropyStages : BaseUnityPlugin
  {
    private SpawnCard teleporterSpawnCard = Addressables.LoadAssetAsync<SpawnCard>("RoR2/Base/Teleporters/iscTeleporter.asset").WaitForCompletion();
    private GameObject seerStation = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/bazaar/SeerStation.prefab").WaitForCompletion();
    private EliteDef voidElite = Addressables.LoadAssetAsync<EliteDef>("RoR2/DLC1/EliteVoid/edVoid.asset").WaitForCompletion();
    private SceneDef voidPlains = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC1/itgolemplains/itgolemplains.asset").WaitForCompletion();
    private SceneDef voidAphelian = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC1/itancientloft/itancientloft.asset").WaitForCompletion();
    private SceneDef voidAqueduct = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC1/itgoolake/itgoolake.asset").WaitForCompletion();
    private SceneDef voidRallypoint = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC1/itfrozenwall/itfrozenwall.asset").WaitForCompletion();
    private SceneDef voidAbyssal = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC1/itdampcave/itdampcave.asset").WaitForCompletion();
    private SceneDef voidMeadow = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC1/itskymeadow/itskymeadow.asset").WaitForCompletion();
    // private SceneDef skyMeadow = Addressables.LoadAssetAsync<SceneDef>("RoR2/Base/skymeadow/skymeadow.asset").WaitForCompletion();

    private SceneCollection sc1 = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/sgStage1.asset").WaitForCompletion();
    private SceneCollection sc2 = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/sgStage2.asset").WaitForCompletion();
    private SceneCollection sc3 = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/sgStage3.asset").WaitForCompletion();
    private SceneCollection sc4 = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/sgStage4.asset").WaitForCompletion();
    private SceneCollection sc5 = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/sgStage5.asset").WaitForCompletion();

    /*
        private static DirectorCardCategorySelection[] dccsInteractables = new DirectorCardCategorySelection[] {
          Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC1/snowyforest/dccsSnowyForestInteractablesDLC1.asset").WaitForCompletion(),
          Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/golemplains/dccsGolemplainsInteractablesDLC1.asset").WaitForCompletion(),
          Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/blackbeach/dccsBlackBeachInteractablesDLC1.asset").WaitForCompletion(),
          Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC1/ancientloft/dccsAncientLoftInteractablesDLC1.asset").WaitForCompletion(),
          Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/goolake/dccsGooLakeInteractablesDLC1.asset").WaitForCompletion(),
          Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/foggyswamp/dccsFoggySwampInteractablesDLC1.asset").WaitForCompletion(),
          Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/wispgraveyard/dccsWispGraveyardInteractablesDLC1.asset").WaitForCompletion(),
          Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC1/sulfurpools/dccsSulfurPoolsInteractablesDLC1.asset").WaitForCompletion(),
          Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/frozenwall/dccsFrozenWallInteractablesDLC1.asset").WaitForCompletion(),
          Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/shipgraveyard/dccsShipgraveyardInteractablesDLC1.asset").WaitForCompletion(),
          Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/rootjungle/dccsRootJungleInteractablesDLC1.asset").WaitForCompletion(),
          Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/dampcave/dccsDampCaveInteractablesDLC1.asset").WaitForCompletion()
        };
    */
    private static DirectorCardCategorySelection[] dccsInteractables = new DirectorCardCategorySelection[] {
      Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dccsInfiniteTowerInteractables.asset").WaitForCompletion()
    };
    private static String[] voidStageNames = new String[] { "itgolemplains", "itgoolake", "itancientloft", "itfrozenwall", "itdampcave", "itskymeadow" };

    public void Awake()
    {
      AddVoidStagesToPool();
      On.RoR2.Stage.Start += Stage_Start;
      On.RoR2.CombatDirector.Init += CombatDirector_Init;
      On.RoR2.ClassicStageInfo.Start += ClassicStageInfo_Start;
      On.RoR2.VoidSuppressorBehavior.Start += VoidSuppressorBehavior_Start;
      On.RoR2.CombatDirector.EliteTierDef.GetRandomAvailableEliteDef += EliteTierDef_GetRandomAvailableEliteDef;
      On.RoR2.UI.ObjectivePanelController.DestroyTimeCrystals.GenerateString += DestroyTimeCrystals_GenerateString;
    }

    private void VoidSuppressorBehavior_Start(On.RoR2.VoidSuppressorBehavior.orig_Start orig, VoidSuppressorBehavior self)
    {
      orig(self);
      self.numItemsToReveal = 3;
      self.itemsSuppressedPerPurchase = 3;
    }

    private void AddToSceneCollection(SceneCollection sc, List<SceneDef> scenesToAdd)
    {
      List<SceneCollection.SceneEntry> sceneList = sc._sceneEntries.ToList();
      foreach (SceneDef sd in scenesToAdd)
        sceneList.Add(new SceneCollection.SceneEntry { sceneDef = sd, weightMinusOne = 0 });

      sc._sceneEntries = sceneList.ToArray();
    }

    private void AddVoidStagesToPool()
    {

      List<SceneDef> s1Corrupted = new() { this.voidPlains };
      List<SceneDef> s2Corrupted = new() { this.voidAphelian, this.voidAqueduct };
      List<SceneDef> s3Corrupted = new() { this.voidRallypoint, this.voidMeadow };
      List<SceneDef> s4Corrupted = new() { this.voidAbyssal };

      AddToSceneCollection(sc1, s1Corrupted);
      AddToSceneCollection(sc2, s2Corrupted);
      AddToSceneCollection(sc3, s3Corrupted);
      AddToSceneCollection(sc4, s4Corrupted);

      foreach (SceneDef sd in s1Corrupted)
      {
        sd.stageOrder = 1;
        sd.destinationsGroup = sc2;
      }
      foreach (SceneDef sd in s2Corrupted)
      {
        sd.stageOrder = 2;
        sd.destinationsGroup = sc3;
      }
      foreach (SceneDef sd in s3Corrupted)
      {
        sd.stageOrder = 3;
        sd.destinationsGroup = sc4;
      }
      foreach (SceneDef sd in s4Corrupted)
      {
        sd.stageOrder = 4;
        sd.destinationsGroup = sc5;
      }
    }

    private string DestroyTimeCrystals_GenerateString(On.RoR2.UI.ObjectivePanelController.DestroyTimeCrystals.orig_GenerateString orig, ObjectivePanelController.ObjectiveTracker self)
    {
      string name = SceneManager.GetActiveScene().name;
      if (!voidStageNames.Contains(name))
        return orig(self);
      CrystalController component = GameObject.Find("InfiniteTowerSceneDirector").GetComponent<CrystalController>();
      return string.Format(Language.GetString(self.baseToken), component.crystalsKilled, component.crystalsRequiredToKill);
    }

    private void CombatDirector_Init(On.RoR2.CombatDirector.orig_Init orig)
    {
      orig();
      if (EliteAPI.VanillaEliteTiers.Length > 2)
      {
        // HONOR
        CombatDirector.EliteTierDef targetTier = EliteAPI.VanillaEliteTiers[2];
        List<EliteDef> elites = targetTier.eliteTypes.ToList();
        elites.Add(voidElite);
        targetTier.eliteTypes = elites.ToArray();
      }
      if (EliteAPI.VanillaEliteTiers.Length > 1)
      {
        CombatDirector.EliteTierDef targetTier = EliteAPI.VanillaEliteTiers[1];
        List<EliteDef> elites = targetTier.eliteTypes.ToList();
        elites.Add(voidElite);
        targetTier.eliteTypes = elites.ToArray();
      }
    }

    private EliteDef EliteTierDef_GetRandomAvailableEliteDef(On.RoR2.CombatDirector.EliteTierDef.orig_GetRandomAvailableEliteDef orig, CombatDirector.EliteTierDef self, Xoroshiro128Plus rng)
    {
      string name = SceneManager.GetActiveScene().name;
      EliteDef result = orig(self, rng);
      if (!voidStageNames.Contains(name) && result == this.voidElite)
      {
        while (true)
        {
          result = orig(self, rng);
          if (result != this.voidElite)
            return result;
        }
      }
      else
        return result;
    }

    private void ClassicStageInfo_Start(On.RoR2.ClassicStageInfo.orig_Start orig, ClassicStageInfo self)
    {
      string name = SceneManager.GetActiveScene().name;

      switch (name)
      {
        case "itgolemplains":
          self.sceneDirectorMonsterCredits = 100;
          break;
        case "itgoolake":
          self.sceneDirectorMonsterCredits = 150;
          break;
        case "itancientloft":
          self.sceneDirectorMonsterCredits = 150;
          break;
        case "itfrozenwall":
          self.sceneDirectorMonsterCredits = 200;
          break;
        case "itskymeadow":
          self.sceneDirectorMonsterCredits = 200;
          break;
        case "itdampcave":
          self.sceneDirectorMonsterCredits = 250;
          break;
      }
      orig(self);
    }

    private void Stage_Start(On.RoR2.Stage.orig_Start orig, Stage self)
    {
      string name = SceneManager.GetActiveScene().name;
      if (voidStageNames.Contains(name))
      {
        GameObject gameObject = GameObject.Find("InfiniteTowerSceneDirector");
        if ((bool)gameObject)
        {
          gameObject.AddComponent<DirectorCore>();
          SceneDirector component1 = gameObject.GetComponent<SceneDirector>();
          if ((bool)component1)
          {
            CombatDirector combatDirector = gameObject.AddComponent<CombatDirector>();
            CombatDirector combatDirector2 = gameObject.AddComponent<CombatDirector>();
            combatDirector.customName = "Director";
            combatDirector.creditMultiplier = 0.75f;
            combatDirector.minRerollSpawnInterval = 4.5f;
            combatDirector.maxRerollSpawnInterval = 9f;
            combatDirector.creditMultiplier = 1.2f;
            combatDirector.onSpawnedServer = new();
            combatDirector.moneyWaveIntervals = new RangeFloat[1]
            {
              new RangeFloat() { min = 1f, max = 1f }
            };
            combatDirector2.customName = "Monsters";
            combatDirector2.creditMultiplier = 0.75f;
            combatDirector2.minRerollSpawnInterval = 22.5f;
            combatDirector2.maxRerollSpawnInterval = 30f;
            combatDirector2.creditMultiplier = 1.2f;
            combatDirector2.onSpawnedServer = new();
            combatDirector2.moneyWaveIntervals = new RangeFloat[1]
            {
              new RangeFloat() { min = 1f, max = 1f }
            };

            combatDirector.Awake();
            combatDirector2.Awake();

            combatDirector.enabled = true;
            combatDirector2.enabled = true;

            component1.PopulateScene();
            component1.teleporterSpawnCard = this.teleporterSpawnCard;
            component1.PlaceTeleporter();

            if (!(bool)TeleporterInteraction.instance)
              return;
            ChildLocator component2 = TeleporterInteraction.instance.GetComponent<ModelLocator>().modelTransform.GetComponent<ChildLocator>();
            if (!(bool)component2)
              return;
            component2.FindChild("TimeCrystalProps").gameObject.SetActive(true);
            component2.FindChild("TimeCrystalBeaconBlocker").gameObject.SetActive(true);
            gameObject.AddComponent<CrystalController>();

            // Spawn Void Suppressors
            DirectorPlacementRule placementRule = new DirectorPlacementRule();
            placementRule.placementMode = DirectorPlacementRule.PlacementMode.Random;
            DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(Addressables.LoadAssetAsync<SpawnCard>("RoR2/DLC1/VoidSuppressor/iscVoidSuppressor.asset").WaitForCompletion(), placementRule, Run.instance.stageRng));
            for (int i = 0; i < 2; i++)
            {
              if (UnityEngine.Random.value < 0.5f)
                DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(Addressables.LoadAssetAsync<SpawnCard>("RoR2/DLC1/VoidSuppressor/iscVoidSuppressor.asset").WaitForCompletion(), placementRule, Run.instance.stageRng));
            }

            // Create spawn points
            Vector3 position1 = TeleporterInteraction.instance.transform.position;
            NodeGraph nodeGraph = SceneInfo.instance.GetNodeGraph(MapNodeGroup.GraphType.Ground);
            foreach (NodeGraph.NodeIndex withFlagCondition in nodeGraph.FindNodesInRangeWithFlagConditions(position1, 0.0f, 50, HullMask.Human, NodeFlags.None, NodeFlags.NoCharacterSpawn, false))
            {
              Vector3 position2;
              if (nodeGraph.GetNodePosition(withFlagCondition, out position2))
                SpawnPoint.AddSpawnPoint(position2, Quaternion.LookRotation(position1, Vector3.up));
            }
          }
        }
      }
      orig(self);
    }
  }
}