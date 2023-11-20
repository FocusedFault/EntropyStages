using BepInEx;
using BepInEx.Configuration;
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
  [BepInPlugin("com.Nuxlar.EntropyStages", "EntropyStages", "0.9.9")]
  [BepInDependency(EliteAPI.PluginGUID)]
  [BepInDependency(PrefabAPI.PluginGUID)]

  public class EntropyStages : BaseUnityPlugin
  {
    private SpawnCard teleporterSpawnCard = Addressables.LoadAssetAsync<SpawnCard>("RoR2/Base/Teleporters/iscTeleporter.asset").WaitForCompletion();
    private GameObject tearPortal = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/PortalArena/PortalArena.prefab").WaitForCompletion(), "NuxVoidTear");
    private InteractableSpawnCard deepVoidSpawnCard = Addressables.LoadAssetAsync<InteractableSpawnCard>("RoR2/DLC1/DeepVoidPortal/iscDeepVoidPortal.asset").WaitForCompletion();
    private GameObject tear = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/gauntlets/GauntletEntranceOrb.prefab").WaitForCompletion().transform.GetChild(0).gameObject, "NuxTearVFX");
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
    private InteractableSpawnCard iscVoidTear = ScriptableObject.CreateInstance<InteractableSpawnCard>();

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
    private GameObject voidSeed = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidCamp/VoidCamp.prefab").WaitForCompletion(), "RescueMissionNux");
    private static Material shieldMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/Engi/matDefenseMatrix.mat").WaitForCompletion();
    private static Material shieldMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Engi/matDefenseMatrixCenter.mat").WaitForCompletion();
    private GameObject survivorPod = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/SurvivorPod/SurvivorPod.prefab").WaitForCompletion(), "EmergencyPodNux");
    private InteractableSpawnCard interactableSpawnCard = ScriptableObject.CreateInstance<InteractableSpawnCard>();
    private static String[] voidStageNames = new String[] { "itgolemplains", "itgoolake", "itancientloft", "itfrozenwall", "itdampcave", "itskymeadow" };
    public static ConfigEntry<float> riftChance;
    public static ConfigEntry<bool> balanceToggle;
    private static ConfigFile ESConfig { get; set; }
    public void Awake()
    {
      ESConfig = new ConfigFile(Paths.ConfigPath + "\\com.Nuxlar.EntropyStages.cfg", true);
      riftChance = ESConfig.Bind<float>("General", "Rift Spawn Chance", 0.25f, "How likely is it for a void rift to appear?");
      balanceToggle = ESConfig.Bind<bool>("General", "Toggle Stage Balancer", true, "Enable/Disable vanilla stage balancing tweaks.");
      if (balanceToggle.Value)
        new StageBalancer();
      /*
      Transform monsterInteractables = voidSeed.transform.GetChild(0);
      Transform propsElites = voidSeed.transform.GetChild(1); // should be fine as is
      Transform emitterProp = voidSeed.transform.GetChild(2); // replace with drop pod
      Transform pointLight = voidSeed.transform.GetChild(3); // inactive

      Destroy(monsterInteractables.GetComponent<FogDamageController>());
      Destroy(monsterInteractables.GetComponent<SphereZone>());
      monsterInteractables.GetComponent<TeamFilter>().teamIndex = TeamIndex.Monster;
      //  monsterInteractables.GetComponent<CombatDirector>().teamIndex = TeamIndex.Monster;
      monsterInteractables.GetComponent<CampDirector>().interactableDirectorCards = new();
      monsterInteractables.GetComponent<CampDirector>().campMaximumRadius = 30;

      // propsElites.GetComponent<CombatDirector>().teamIndex = TeamIndex.Monster;
      propsElites.GetComponent<CampDirector>().campMaximumRadius = 30;

      emitterProp.GetChild(3).GetChild(0).localScale /= 2;
      emitterProp.GetChild(3).GetChild(0).GetComponent<MeshRenderer>().sharedMaterials = new Material[] { shieldMat, shieldMat2 };
      MeshCollider mc = emitterProp.GetChild(3).GetChild(0).gameObject.AddComponent<MeshCollider>();
      emitterProp.GetChild(3).GetChild(0).gameObject.AddComponent<ReverseNormals>();
      emitterProp.GetChild(3).GetChild(0).gameObject.AddComponent<DisableCollisionsIfInTrigger>().colliderToIgnore = emitterProp.GetChild(3).GetChild(0).gameObject.GetComponent<SphereCollider>();
      emitterProp.GetChild(0).gameObject.SetActive(false);
      emitterProp.GetChild(1).gameObject.SetActive(false);
      emitterProp.GetChild(2).gameObject.SetActive(false);
      Instantiate(survivorPod.transform.GetChild(0).GetChild(0), emitterProp).localPosition = new Vector3(0, 1.5f, 0);

      pointLight.gameObject.SetActive(true);

      voidSeed.GetComponent<GenericDisplayNameProvider>().displayToken = "Emergency Pod";
      PrefabAPI.RegisterNetworkPrefab(voidSeed);

      interactableSpawnCard.name = "iscEmergencyPodNux";
      interactableSpawnCard.prefab = voidSeed;
      interactableSpawnCard.sendOverNetwork = true;
      interactableSpawnCard.hullSize = HullClassification.Golem;
      interactableSpawnCard.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Ground;
      interactableSpawnCard.requiredFlags = RoR2.Navigation.NodeFlags.None;
      interactableSpawnCard.forbiddenFlags = RoR2.Navigation.NodeFlags.NoShrineSpawn | RoR2.Navigation.NodeFlags.NoChestSpawn;
    */
      SetupVoidTear();

      AddVoidStagesToPool();
      On.RoR2.Stage.Start += Stage_Start;
      On.RoR2.CombatDirector.Init += CombatDirector_Init;
      On.RoR2.ClassicStageInfo.Start += ClassicStageInfo_Start;
      On.RoR2.VoidSuppressorBehavior.Start += VoidSuppressorBehavior_Start;
      On.RoR2.CombatDirector.EliteTierDef.GetRandomAvailableEliteDef += EliteTierDef_GetRandomAvailableEliteDef;
      On.RoR2.UI.ObjectivePanelController.DestroyTimeCrystals.GenerateString += DestroyTimeCrystals_GenerateString;
      On.RoR2.TeleporterInteraction.AttemptToSpawnAllEligiblePortals += TeleporterInteraction_AttemptToSpawnAllEligiblePortals;
    }

    private void SetupVoidTear()
    {
      tearPortal.GetComponent<GenericInteraction>().contextToken = "Enter ???";
      tearPortal.GetComponent<SceneExitController>().destinationScene = voidPlains;
      tearPortal.GetComponent<SceneExitController>().useRunNextStageScene = false;
      // Destroy(tearPortal.transform.GetChild(0).gameObject);
      foreach (Transform child in tearPortal.transform.GetChild(0))
      {
        if (child.name != "Collider")
          Destroy(child.gameObject);
      }

      Transform tearVFX = Instantiate(tear, tearPortal.transform).transform;
      Destroy(tearVFX.GetComponent<ObjectScaleCurve>());
      tearVFX.localScale = new Vector3(0.75f, 0.75f, 0.75f);
      tearVFX.localPosition = new Vector3(0f, 5f, 0f);
      tearVFX.localRotation = Quaternion.identity;
      tearVFX.Rotate(new Vector3(0, 0, 45));

      iscVoidTear.name = "iscVoidTearNux";
      iscVoidTear.prefab = tearPortal;
      iscVoidTear.sendOverNetwork = deepVoidSpawnCard.sendOverNetwork;
      iscVoidTear.hullSize = deepVoidSpawnCard.hullSize;
      iscVoidTear.nodeGraphType = deepVoidSpawnCard.nodeGraphType;
      iscVoidTear.requiredFlags = deepVoidSpawnCard.requiredFlags;
      iscVoidTear.forbiddenFlags = deepVoidSpawnCard.forbiddenFlags;

      PrefabAPI.RegisterNetworkPrefab(tearPortal);
    }

    private void VoidSuppressorBehavior_Start(On.RoR2.VoidSuppressorBehavior.orig_Start orig, VoidSuppressorBehavior self)
    {
      orig(self);
      self.numItemsToReveal = 3;
      self.itemsSuppressedPerPurchase = 3;
    }

    private void AddVoidStagesToPool()
    {

      List<SceneDef> s1Corrupted = new() { this.voidPlains };
      List<SceneDef> s2Corrupted = new() { this.voidAphelian, this.voidAqueduct };
      List<SceneDef> s3Corrupted = new() { this.voidRallypoint, this.voidMeadow };
      List<SceneDef> s4Corrupted = new() { this.voidAbyssal };

      foreach (SceneDef sd in s1Corrupted)
        sd.destinationsGroup = sc2;
      foreach (SceneDef sd in s2Corrupted)
        sd.destinationsGroup = sc3;
      foreach (SceneDef sd in s3Corrupted)
        sd.destinationsGroup = sc4;
      foreach (SceneDef sd in s4Corrupted)
        sd.destinationsGroup = sc5;
    }

    private string DestroyTimeCrystals_GenerateString(On.RoR2.UI.ObjectivePanelController.DestroyTimeCrystals.orig_GenerateString orig, ObjectivePanelController.ObjectiveTracker self)
    {
      string name = SceneManager.GetActiveScene().name;
      if (!voidStageNames.Contains(name))
        return orig(self);
      CrystalController component = (CrystalController)InstanceTracker.FindInstancesEnumerable(typeof(CrystalController)).First();
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

    private GameObject SpawnTear(TeleporterInteraction tpInteraction)
    {
      GameObject voidTearInstance = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(iscVoidTear, new DirectorPlacementRule()
      {
        minDistance = 10f,
        maxDistance = 40f,
        placementMode = DirectorPlacementRule.PlacementMode.Approximate,
        position = tpInteraction.transform.position,
        spawnOnTarget = tpInteraction.transform
      }, tpInteraction.rng));
      return voidTearInstance;
    }

    private void TeleporterInteraction_AttemptToSpawnAllEligiblePortals(On.RoR2.TeleporterInteraction.orig_AttemptToSpawnAllEligiblePortals orig, TeleporterInteraction self)
    {
      string name = SceneManager.GetActiveScene().name;
      if (UnityEngine.Random.value < riftChance.Value)
      {
        GameObject voidTearInstance;
        switch (name)
        {
          case "golemplains":
            voidTearInstance = SpawnTear(self);
            if ((bool)voidTearInstance)
              voidTearInstance.GetComponent<SceneExitController>().destinationScene = voidPlains;
            break;
          case "goolake":
            voidTearInstance = SpawnTear(self);
            if ((bool)voidTearInstance)
              voidTearInstance.GetComponent<SceneExitController>().destinationScene = voidAqueduct;
            break;
          case "ancientloft":
            voidTearInstance = SpawnTear(self);
            if ((bool)voidTearInstance)
              voidTearInstance.GetComponent<SceneExitController>().destinationScene = voidAphelian;
            break;
          case "frozenwall":
            voidTearInstance = SpawnTear(self);
            if ((bool)voidTearInstance)
              voidTearInstance.GetComponent<SceneExitController>().destinationScene = voidRallypoint;
            break;
          case "dampcave":
            voidTearInstance = SpawnTear(self);
            if ((bool)voidTearInstance)
              voidTearInstance.GetComponent<SceneExitController>().destinationScene = voidAbyssal;
            break;
          case "skymeadow":
            voidTearInstance = SpawnTear(self);
            if ((bool)voidTearInstance)
              voidTearInstance.GetComponent<SceneExitController>().destinationScene = voidMeadow;
            break;
        }
      }
      // NetworkServer.Spawn(voidTearInstance);
      orig(self);
    }

    private void ClassicStageInfo_Start(On.RoR2.ClassicStageInfo.orig_Start orig, ClassicStageInfo self)
    {
      string name = SceneManager.GetActiveScene().name;
      switch (name)
      {
        case "itgolemplains":
          self.sceneDirectorInteractibleCredits /= 4;
          self.sceneDirectorMonsterCredits = 100;
          break;
        case "itgoolake":
          self.sceneDirectorInteractibleCredits /= 4;
          self.sceneDirectorMonsterCredits = 150;
          break;
        case "itancientloft":
          self.sceneDirectorInteractibleCredits /= 4;
          self.sceneDirectorMonsterCredits = 150;
          break;
        case "itfrozenwall":
          self.sceneDirectorInteractibleCredits /= 4;
          self.sceneDirectorMonsterCredits = 175;
          break;
        case "itdampcave":
          self.sceneDirectorInteractibleCredits /= 4;
          self.sceneDirectorMonsterCredits = 200;
          break;
        case "itskymeadow":
          self.sceneDirectorInteractibleCredits /= 4;
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
          DirectorCore dc = gameObject.GetComponent<DirectorCore>();
          if (!(bool)dc)
            gameObject.AddComponent<DirectorCore>();
          if (self.isServer)
          {
            gameObject.AddComponent<CrystalController>();
            SceneDirector component1 = gameObject.GetComponent<SceneDirector>();
            if ((bool)component1)
            {
              CombatDirector[] cd = gameObject.GetComponents<CombatDirector>();
              if (cd.Length == 0)
              {
                CombatDirector combatDirector = gameObject.AddComponent<CombatDirector>();
                CombatDirector combatDirector2 = gameObject.AddComponent<CombatDirector>();
                combatDirector.customName = "Director";
                combatDirector.minRerollSpawnInterval = 4.5f;
                combatDirector.maxRerollSpawnInterval = 9f;
                combatDirector.creditMultiplier = 1.075f;
                combatDirector.onSpawnedServer = new();
                combatDirector.moneyWaveIntervals = new RangeFloat[1]
                {
              new RangeFloat() { min = 1f, max = 1f }
                };
                combatDirector2.customName = "Director";
                combatDirector2.minRerollSpawnInterval = 22.5f;
                combatDirector2.maxRerollSpawnInterval = 30f;
                combatDirector2.creditMultiplier = 1.075f;
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
              }
              if (!(bool)TeleporterInteraction.instance)
                return;
              Vector3 position1 = TeleporterInteraction.instance.transform.position;
              NodeGraph nodeGraph = SceneInfo.instance.GetNodeGraph(MapNodeGroup.GraphType.Ground);
              foreach (NodeGraph.NodeIndex withFlagCondition in nodeGraph.FindNodesInRangeWithFlagConditions(position1, 0.0f, 50, HullMask.Human, NodeFlags.None, NodeFlags.NoCharacterSpawn, false))
              {
                Vector3 position2;
                if (nodeGraph.GetNodePosition(withFlagCondition, out position2))
                  SpawnPoint.AddSpawnPoint(position2, Quaternion.LookRotation(position1, Vector3.up));
              }
              // Spawn Void Suppressors
              DirectorPlacementRule placementRule = new DirectorPlacementRule();
              placementRule.placementMode = DirectorPlacementRule.PlacementMode.Random;
              DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(Addressables.LoadAssetAsync<SpawnCard>("RoR2/DLC1/VoidSuppressor/iscVoidSuppressor.asset").WaitForCompletion(), placementRule, Run.instance.stageRng));
            }
          }
          if (!(bool)TeleporterInteraction.instance)
            return;
          ChildLocator component2 = TeleporterInteraction.instance.GetComponent<ModelLocator>().modelTransform.GetComponent<ChildLocator>();
          if (!(bool)component2)
            return;
          component2.FindChild("TimeCrystalProps").gameObject.SetActive(true);
          component2.FindChild("TimeCrystalBeaconBlocker").gameObject.SetActive(true);
        }
      }
      orig(self);
    }
  }
}