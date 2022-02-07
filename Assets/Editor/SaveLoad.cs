using Strategy.Common;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace Strategy
{
    public static class SaveLoad
    {
        [MenuItem("Levels/New")]
        public static void New()
        {
            Close();
            
            LevelObject levelObject = new GameObject("Level").AddComponent<LevelObject>();
        }

        [MenuItem("Levels/Close")]
        public static void Close()
        {
            LevelObject levelObject = Object.FindObjectOfType<LevelObject>();

            if(levelObject != null)
                Object.DestroyImmediate(levelObject.gameObject);
        }

        [MenuItem("Levels/Save")]
        public static void Save()
        {
            LevelObject levelObject = Object.FindObjectOfType<LevelObject>();
            Level currentLevel = new Level();
            currentLevel.Name = levelObject.Name;

            EnvironmentLevelItem[] environmentLevelItems = Object.FindObjectsOfType<EnvironmentLevelItem>();
            foreach(EnvironmentLevelItem currentItem in environmentLevelItems)
            {
                Vector3 currentPosition = currentItem.gameObject.transform.position;
                Position position = new Position(currentPosition.x, currentPosition.y, currentPosition.z);

                Quaternion currentRotation = currentItem.gameObject.transform.rotation;
                Rotation rotation= new Rotation(currentRotation.x, currentRotation.y, currentRotation.z, currentRotation.w);

                EnvironmentItem newEnvironmentItem = new EnvironmentItem(currentItem.Prefab, position, rotation);

                if(currentItem.IsGround)
                    currentLevel.Ground.Add(newEnvironmentItem);
                else
                    currentLevel.Decorations.Add(newEnvironmentItem);
            }

            BuildingLevelItem[] buildingLevelItems = Object.FindObjectsOfType<BuildingLevelItem>();
            foreach(BuildingLevelItem currentItem in buildingLevelItems)
            {
                Vector3 currentPosition = currentItem.gameObject.transform.position;
                Position position = new Position(currentPosition.x, currentPosition.y, currentPosition.z);

                Quaternion currentRotation = currentItem.gameObject.transform.rotation;
                Rotation rotation = new Rotation(currentRotation.x, currentRotation.y, currentRotation.z, currentRotation.w);

                SavedLevelItem newSavedLevelItem = new SavedLevelItem(currentItem.Type, currentItem.Prefab, currentItem.Team, position, rotation);
                currentLevel.Buildings.Add(newSavedLevelItem);
            }

            UnitLevelItem[] unitLevelItems = Object.FindObjectsOfType<UnitLevelItem>();
            foreach(UnitLevelItem currentItem in unitLevelItems)
            {
                Vector3 currentPosition = currentItem.gameObject.transform.position;
                Position position = new Position(currentPosition.x, currentPosition.y, currentPosition.z);

                Quaternion currentRotation = currentItem.gameObject.transform.rotation;
                Rotation rotation = new Rotation(currentRotation.x, currentRotation.y, currentRotation.z, currentRotation.w);

                SavedLevelItem newSavedLevelItem = new SavedLevelItem(currentItem.Type, currentItem.Prefab, currentItem.Team, position, rotation);
                currentLevel.Units.Add(newSavedLevelItem);
            }

            string fileName = Path.Combine(Application.dataPath, "Levels/Level0.json");
            string levelData = JsonUtility.ToJson(currentLevel);
            File.WriteAllText(fileName, levelData);
        }

        [MenuItem("Levels/Load")]
        public static void Load()
        {
            Close();
            
            LevelObject levelObject = new GameObject("Level").AddComponent<LevelObject>();

            string fileName = Path.Combine(Application.dataPath, "Levels/Level0.json");
            string levelData = File.ReadAllText(fileName);

            Level currentLevel = JsonUtility.FromJson<Level>(levelData);
            levelObject.Name = currentLevel.Name;

            GameObject groundObject = new GameObject("Ground");
            groundObject.transform.SetParent(levelObject.gameObject.transform);
            foreach(EnvironmentItem currentItem in currentLevel.Ground)
            {
                GameObject currentGround = GetNewObject(currentItem.Prefab, currentItem.Position, currentItem.Rotation, groundObject.transform);
                EnvironmentLevelItem environmentLevelItem = currentGround.GetComponent<EnvironmentLevelItem>();
                environmentLevelItem.IsGround = true;
            }

            GameObject decorationsObject = new GameObject("Decorations");
            decorationsObject.transform.SetParent(levelObject.gameObject.transform);
            foreach(EnvironmentItem currentItem in currentLevel.Decorations)
            {
                GameObject currentDecoration = GetNewObject(currentItem.Prefab, currentItem.Position, currentItem.Rotation, decorationsObject.transform);
            }

            GameObject buildingsObject = new GameObject("Buildings");
            buildingsObject.transform.SetParent(levelObject.gameObject.transform);
            foreach(SavedLevelItem currentItem in currentLevel.Buildings)
            {
                GameObject currentBuilding = GetNewObject(currentItem.Prefab, currentItem.Position, currentItem.Rotation, buildingsObject.transform);
                BuildingLevelItem buildingLevelItem = currentBuilding.GetComponent<BuildingLevelItem>();
                buildingLevelItem.Prefab = currentItem.Prefab;
                buildingLevelItem.Team = currentItem.Team;
            }

            GameObject unitsObject = new GameObject("Units");
            unitsObject.transform.SetParent(levelObject.gameObject.transform);
            foreach(SavedLevelItem currentItem in currentLevel.Units)
            {
                GameObject currentUnit = GetNewObject(currentItem.Prefab, currentItem.Position, currentItem.Rotation, unitsObject.transform);
                UnitLevelItem unitLevelItem = currentUnit.GetComponent<UnitLevelItem>();
                unitLevelItem.Prefab = currentItem.Prefab;
                unitLevelItem.Team = currentItem.Team;
            }
        }

        private static GameObject GetNewObject(string prefab, Position position, Rotation rotation, Transform parent)
        {
            GameObject currentPrefab = Resources.Load<GameObject>(prefab);
            Vector3 currentPosition = new Vector3(position.x, position.y, position.z);
            Quaternion currentRotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
            GameObject newObject = Object.Instantiate(currentPrefab, currentPosition, currentRotation, parent);

            return newObject;
        }
    }
}
