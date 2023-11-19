using RoR2;
using RoR2.Skills;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace EntropyStages
{
    public class StageBalancer : MonoBehaviour
    {
        // Misc
        SkillDef AWUUlt = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/RoboBallBoss/SuperFireDelayKnockup.asset").WaitForCompletion();
        // Spawn Cards
        SpawnCard categoryChest2Healing = Addressables.LoadAssetAsync<SpawnCard>("RoR2/DLC1/CategoryChest2/iscCategoryChest2Healing.asset").WaitForCompletion();
        SpawnCard categoryChest2Damage = Addressables.LoadAssetAsync<SpawnCard>("RoR2/DLC1/CategoryChest2/iscCategoryChest2Damage.asset").WaitForCompletion();
        SpawnCard larva = Addressables.LoadAssetAsync<SpawnCard>("RoR2/DLC1/AcidLarva/cscAcidLarva.asset").WaitForCompletion();
        SpawnCard vulture = Addressables.LoadAssetAsync<SpawnCard>("RoR2/Base/Vulture/cscVulture.asset").WaitForCompletion();
        SpawnCard probe = Addressables.LoadAssetAsync<SpawnCard>("RoR2/Base/RoboBallBoss/cscRoboBallMini.asset").WaitForCompletion();
        SpawnCard alphaConstruct = Addressables.LoadAssetAsync<SpawnCard>("RoR2/DLC1/MajorAndMinorConstruct/cscMinorConstruct.asset").WaitForCompletion();
        SpawnCard apothecary = Addressables.LoadAssetAsync<SpawnCard>("RoR2/DLC1/ClayGrenadier/cscClayGrenadier.asset").WaitForCompletion();
        SpawnCard templar = Addressables.LoadAssetAsync<SpawnCard>("RoR2/Base/ClayBruiser/cscClayBruiser.asset").WaitForCompletion();
        SpawnCard lemurian = Addressables.LoadAssetAsync<SpawnCard>("RoR2/Base/Lemurian/cscLemurian.asset").WaitForCompletion();
        SpawnCard vermin = Addressables.LoadAssetAsync<SpawnCard>("RoR2/DLC1/Vermin/cscVermin.asset").WaitForCompletion();
        SpawnCard hermitCrab = Addressables.LoadAssetAsync<SpawnCard>("RoR2/Base/HermitCrab/cscHermitCrab.asset").WaitForCompletion();
        SpawnCard grovetender = Addressables.LoadAssetAsync<SpawnCard>("RoR2/Base/Gravekeeper/cscGravekeeper.asset").WaitForCompletion();
        SpawnCard xiConstruct = Addressables.LoadAssetAsync<SpawnCard>("RoR2/DLC1/MajorAndMinorConstruct/cscMegaConstruct.asset").WaitForCompletion();
        SpawnCard magmaWorm = Addressables.LoadAssetAsync<SpawnCard>("RoR2/Base/MagmaWorm/cscMagmaWorm.asset").WaitForCompletion();
        // Monsters 
        DirectorCardCategorySelection aqueductMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/goolake/dccsGooLakeMonstersDLC1.asset").WaitForCompletion();
        DirectorCardCategorySelection rpdMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/frozenwall/dccsFrozenWallMonstersDLC1.asset").WaitForCompletion();
        DirectorCardCategorySelection poolsMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC1/sulfurpools/dccsSulfurPoolsMonstersDLC1.asset").WaitForCompletion();
        DirectorCardCategorySelection acresMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/wispgraveyard/dccsWispGraveyardMonstersDLC1.asset").WaitForCompletion();
        DirectorCardCategorySelection sirensMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/shipgraveyard/dccsShipgraveyardMonstersDLC1.asset").WaitForCompletion();
        DirectorCardCategorySelection abyssalMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/dampcave/dccsDampCaveMonstersDLC1.asset").WaitForCompletion();
        DirectorCardCategorySelection groveMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/rootjungle/dccsRootJungleMonstersDLC1.asset").WaitForCompletion();
        DirectorCardCategorySelection meadowMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/skymeadow/dccsSkyMeadowMonstersDLC1.asset").WaitForCompletion();

        // Interactables
        private DirectorCardCategorySelection forestInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC1/snowyforest/dccsSnowyForestInteractablesDLC1.asset").WaitForCompletion();
        private DirectorCardCategorySelection plainsInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/golemplains/dccsGolemplainsInteractablesDLC1.asset").WaitForCompletion();
        private DirectorCardCategorySelection bbInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/blackbeach/dccsBlackBeachInteractablesDLC1.asset").WaitForCompletion();
        private DirectorCardCategorySelection aphelianInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC1/ancientloft/dccsAncientLoftInteractablesDLC1.asset").WaitForCompletion();
        private DirectorCardCategorySelection aqueductInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/goolake/dccsGooLakeInteractablesDLC1.asset").WaitForCompletion();
        private DirectorCardCategorySelection wetlandInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/foggyswamp/dccsFoggySwampInteractablesDLC1.asset").WaitForCompletion();
        private DirectorCardCategorySelection acresInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/wispgraveyard/dccsWispGraveyardInteractablesDLC1.asset").WaitForCompletion();
        private DirectorCardCategorySelection poolsInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/DLC1/sulfurpools/dccsSulfurPoolsInteractablesDLC1.asset").WaitForCompletion();
        private DirectorCardCategorySelection rpdInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/frozenwall/dccsFrozenWallInteractablesDLC1.asset").WaitForCompletion();
        private DirectorCardCategorySelection sirensInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/shipgraveyard/dccsShipgraveyardInteractablesDLC1.asset").WaitForCompletion();
        private DirectorCardCategorySelection groveInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/rootjungle/dccsRootJungleInteractablesDLC1.asset").WaitForCompletion();
        private DirectorCardCategorySelection abyssalInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/dampcave/dccsDampCaveInteractablesDLC1.asset").WaitForCompletion();

        public StageBalancer()
        {
            AWUUlt.beginSkillCooldownOnSkillEnd = true;
            On.RoR2.Stage.Start += Stage_Start;
            On.RoR2.ClassicStageInfo.Start += ClassicStageInfo_Start;
            On.RoR2.TimedChestController.OnEnable += TimedChestController_OnEnable;
            On.RoR2.GoldshoresMissionController.Start += GoldshoresMissionController_Start;
            On.RoR2.MoonBatteryMissionController.Awake += MoonBatteryMissionController_Awake;
            On.RoR2.VoidStageMissionController.Start += VoidStageMissionController_Start;
            // Stage 1
            ChangeCatChest(forestInteractables, categoryChest2Healing, "iscCategoryChest2Damage");
            ChangeCatChest(plainsInteractables, categoryChest2Damage, "iscCategoryChest2Healing");
            // Stage 2
            AddMiniboss(aqueductMonsters, templar);
            ChangeCatChest(aqueductInteractables, categoryChest2Healing, "iscCategoryChest2Damage");
            ChangeCatChest(wetlandInteractables, categoryChest2Damage, "iscCategoryChest2Healing");
            RemoveAphelianChest();
            // Stage 3
            ChangeEnemy(rpdMonsters, alphaConstruct, "cscVerminSnowy");
            ChangeEnemy(poolsMonsters, hermitCrab, "cscMinorConstruct");
            ChangeEnemy(acresMonsters, lemurian, "cscBeetle");
            ChangeMiniboss(acresMonsters, apothecary, "cscBeetleGuard");
            ChangeBoss(poolsMonsters, magmaWorm, "cscMegaConstruct");
            ChangeBoss(rpdMonsters, xiConstruct, "cscClayBoss");
            ChangeCatChest(acresInteractables, categoryChest2Healing, "iscCategoryChest2Damage");
            ChangeCatChest(poolsInteractables, categoryChest2Damage, "iscCategoryChest2Healing");
            AcresBarrelIncrease();
            PoolsBarrelDecrease();
            // Stage 4
            ChangeEnemy(sirensMonsters, lemurian, "cscAcidLarva");
            ChangeEnemy(groveMonsters, alphaConstruct, "cscAcidLarva");
            ChangeEnemy(abyssalMonsters, larva, "cscHermitCrab");
            ChangeBoss(groveMonsters, grovetender, "cscClayBoss");
            ChangeBoss(groveMonsters, xiConstruct, "cscVagrant");
            ChangeCatChest(abyssalInteractables, categoryChest2Healing, "iscCategoryChest2Damage");
            ChangeCatChest(sirensInteractables, categoryChest2Damage, "iscCategoryChest2Healing");
            BalanceSirenGrove();
            SirensDroneShrineDecrease();
            // Stage 5
            // InspectMonsters(meadowMonsters);
            ChangeEnemy(meadowMonsters, vulture, "cscMinorConstruct");
            ChangeBoss(meadowMonsters, magmaWorm, "cscMegaConstruct");
        }
        private void InspectMonsters(DirectorCardCategorySelection dccs)
        {
            for (int i = 0; i < dccs.categories.Length; i++)
            {
                DirectorCardCategorySelection.Category category = dccs.categories[i];
                Debug.LogWarning(category.name);
                Debug.LogWarning("--------------------------------------");

                foreach (DirectorCard card in category.cards)
                    Debug.LogWarning(card.spawnCard.name);
            }
        }

        private void GoldshoresMissionController_Start(On.RoR2.GoldshoresMissionController.orig_Start orig, GoldshoresMissionController self)
        {
            self.beaconsToSpawnOnMap = 4;
            orig(self);
        }

        private void VoidStageMissionController_Start(On.RoR2.VoidStageMissionController.orig_Start orig, VoidStageMissionController self)
        {
            self.batteryCount = 2;
            orig(self);
        }

        private void MoonBatteryMissionController_Awake(On.RoR2.MoonBatteryMissionController.orig_Awake orig, MoonBatteryMissionController self)
        {
            orig(self);
            self._numRequiredBatteries = 2;
        }

        private void Stage_Start(On.RoR2.Stage.orig_Start orig, Stage self)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "blackbeach")
            {
                GameObject statueHolder = GameObject.Find("HOLDER: Preplaced Objects");
                if (statueHolder)
                {
                    if (!statueHolder.transform.GetChild(0).gameObject.activeSelf && !statueHolder.transform.GetChild(1).gameObject.activeSelf && !statueHolder.transform.GetChild(2).gameObject.activeSelf)
                    {
                        Debug.Log("StageBalancer: Manually activating random newt altar");
                        statueHolder.transform.GetChild(UnityEngine.Random.Range(0, 2)).gameObject.SetActive(true);
                    }
                }
            }
            orig(self);
        }

        private void ClassicStageInfo_Start(On.RoR2.ClassicStageInfo.orig_Start orig, ClassicStageInfo self)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "golemplains" || sceneName == "golemplains2" || sceneName == "blackbeach" || sceneName == "blackbeach2")
                self.sceneDirectorInteractibleCredits = 230;
            if (sceneName == "snowyforest")
                self.sceneDirectorInteractibleCredits = 290;
            if (sceneName == "foggyswamp")
                self.sceneDirectorInteractibleCredits = 320;
            if (sceneName == "shipgraveyard" || sceneName == "rootjungle")
                self.sceneDirectorInteractibleCredits = 430;
            if (sceneName == "dampcavesimple" && self.bonusInteractibleCreditObjects != null)
            {
                Debug.Log("StageBalancer: Adjusting extra abyssal credits");
                for (int index = 0; index < self.bonusInteractibleCreditObjects.Length; ++index)
                    self.bonusInteractibleCreditObjects[index].points /= 2;
            }
            orig(self);
        }

        private void TimedChestController_OnEnable(On.RoR2.TimedChestController.orig_OnEnable orig, TimedChestController self)
        {
            self.lockTime = 720;
            orig(self);
        }

        private void ChangeCatChest(DirectorCardCategorySelection dccs, SpawnCard newChest, string chestName)
        {
            for (int i = 0; i < dccs.categories.Length; i++)
            {
                DirectorCardCategorySelection.Category category = dccs.categories[i];
                if (category.name == "Chests")
                {
                    List<DirectorCard> dcList = new();
                    dcList.Add(new() { spawnCard = newChest, selectionWeight = 3 });
                    foreach (DirectorCard card in category.cards)
                    {
                        if (card.spawnCard.name != chestName)
                            dcList.Add(card);
                    }
                    dccs.categories[i].cards = dcList.ToArray();
                }
            }
        }

        private void ChangeEnemy(DirectorCardCategorySelection dccs, SpawnCard newEnemy, string enemyName)
        {
            for (int i = 0; i < dccs.categories.Length; i++)
            {
                DirectorCardCategorySelection.Category category = dccs.categories[i];
                if (category.name == "Basic Monsters")
                {
                    List<DirectorCard> dcList = new();
                    foreach (DirectorCard card in category.cards)
                    {
                        if (card.spawnCard.name != enemyName)
                            dcList.Add(card);
                        else
                            dcList.Add(new() { spawnCard = newEnemy, selectionWeight = card.selectionWeight });

                    }
                    dccs.categories[i].cards = dcList.ToArray();
                }
            }
        }
        private void AddMiniboss(DirectorCardCategorySelection dccs, SpawnCard newEnemy)
        {
            for (int i = 0; i < dccs.categories.Length; i++)
            {
                DirectorCardCategorySelection.Category category = dccs.categories[i];
                if (category.name == "Minibosses")
                {
                    List<DirectorCard> dcList = new();
                    dcList.Add(new() { spawnCard = newEnemy, selectionWeight = 1 });
                    foreach (DirectorCard card in category.cards)
                        dcList.Add(card);
                    dccs.categories[i].cards = dcList.ToArray();
                }
            }
        }
        private void ChangeMiniboss(DirectorCardCategorySelection dccs, SpawnCard newEnemy, string enemyName)
        {
            for (int i = 0; i < dccs.categories.Length; i++)
            {
                DirectorCardCategorySelection.Category category = dccs.categories[i];
                if (category.name == "Minibosses")
                {
                    List<DirectorCard> dcList = new();
                    foreach (DirectorCard card in category.cards)
                    {
                        if (card.spawnCard.name != enemyName)
                            dcList.Add(card);
                        else
                            dcList.Add(new() { spawnCard = newEnemy, selectionWeight = card.selectionWeight });

                    }
                    dccs.categories[i].cards = dcList.ToArray();
                }
            }
        }

        private void ChangeBoss(DirectorCardCategorySelection dccs, SpawnCard newBoss, string bossName)
        {
            for (int i = 0; i < dccs.categories.Length; i++)
            {
                DirectorCardCategorySelection.Category category = dccs.categories[i];
                if (category.name == "Champions")
                {
                    List<DirectorCard> dcList = new();
                    foreach (DirectorCard card in category.cards)
                    {
                        if (card.spawnCard.name != bossName)
                            dcList.Add(card);
                        else
                            dcList.Add(new() { spawnCard = newBoss, selectionWeight = card.selectionWeight });
                    }
                    dccs.categories[i].cards = dcList.ToArray();
                }
            }
        }
        private void RemoveAphelianChest()
        {
            for (int i = 0; i < aphelianInteractables.categories.Length; i++)
            {
                DirectorCardCategorySelection.Category category = aphelianInteractables.categories[i];
                if (category.name == "Chests")
                {
                    List<DirectorCard> dcList = new();
                    foreach (DirectorCard card in category.cards)
                    {
                        if (card.spawnCard.name != "iscLunarChest")
                            dcList.Add(card);
                    }
                    aphelianInteractables.categories[i].cards = dcList.ToArray();
                }
            }
        }

        private void AcresBarrelIncrease()
        {
            for (int i = 0; i < acresInteractables.categories.Length; i++)
            {
                DirectorCardCategorySelection.Category category = acresInteractables.categories[i];
                if (category.name == "Barrels")
                    category.selectionWeight *= 1.2f;
            }
        }

        private void PoolsBarrelDecrease()
        {
            for (int i = 0; i < poolsInteractables.categories.Length; i++)
            {
                DirectorCardCategorySelection.Category category = poolsInteractables.categories[i];
                if (category.name == "Barrels")
                    category.selectionWeight *= 0.6f;
            }
        }

        private void RPDDroneDecrease()
        {
            for (int i = 0; i < rpdInteractables.categories.Length; i++)
            {
                DirectorCardCategorySelection.Category category = rpdInteractables.categories[i];
                if (category.name == "Drones")
                    category.selectionWeight /= 2f;
            }
        }

        private void SirensDroneShrineDecrease()
        {
            for (int i = 0; i < sirensInteractables.categories.Length; i++)
            {
                DirectorCardCategorySelection.Category category = sirensInteractables.categories[i];
                if (category.name == "Drones" || category.name == "Shrines")
                    category.selectionWeight /= 2f;
            }
        }

        private void BalanceSirenGrove()
        {
            for (int i = 0; i < sirensInteractables.categories.Length; i++)
            {
                DirectorCardCategorySelection.Category category = sirensInteractables.categories[i];
                if (category.name == "Chests")
                {
                    foreach (DirectorCard card in category.cards)
                    {
                        if (card.spawnCard.name == "iscChest1" || card.spawnCard.name == "iscChest2")
                            card.selectionWeight -= (int)Math.Round(card.selectionWeight * 0.1);
                        if (card.spawnCard.name == "iscTripleShopLarge")
                            card.selectionWeight += (int)Math.Round(card.selectionWeight * 0.2);
                    }
                }
            }
            for (int i = 0; i < groveInteractables.categories.Length; i++)
            {
                DirectorCardCategorySelection.Category category = groveInteractables.categories[i];
                if (category.name == "Chests")
                {
                    foreach (DirectorCard card in category.cards)
                    {
                        if (card.spawnCard.name == "iscChest1" || card.spawnCard.name == "iscChest2")
                            card.selectionWeight -= (int)Math.Round(card.selectionWeight * 0.1);
                        if (card.spawnCard.name == "iscCasinoChest")
                            card.selectionWeight += (int)Math.Round(card.selectionWeight * 0.3);
                    }
                }
            }
        }
    }
}