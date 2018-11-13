using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager S_INSTANCE = null;

    public static GameManager INSTANCE {
        get {
            if (S_INSTANCE == null) {
                S_INSTANCE = FindObjectOfType(typeof(GameManager)) as GameManager;
            }

            if (S_INSTANCE == null) {
                GameObject obj = new GameObject("GameManager");
                S_INSTANCE = obj.AddComponent(typeof(GameManager)) as GameManager;
                Debug.Log("Could not locate an GameManager object. \n GameManager was Generated Automatically.");
            }

            return S_INSTANCE;
        }
    }

    public GameObject[] artifactPrefabs;
    public GameObject flagPrefab;

    public Game[] games;
    int currentGame = 0;

    TextureGrabber grabber;

    private void Awake () {
        grabber = GetComponentInChildren<TextureGrabber>();

        games[currentGame].SpawnArtifacts();

        // Game 1
        //noduleInfo = new Nodule[0];
        //artifactInfo = GenerateRandomArtifacts();

    }

    public void ChangeCurrentGame ( int index) {
        if (index == 1) {
            Debug.Log("LEVEL UP");
            transform.parent.GetComponent<SceneManager>().GoToScene(10);
            currentGame = index;
            grabber.ResetBool();
            games[currentGame].SpawnArtifacts();
            return;
        }
        if (index >= games.Length) {
            Debug.Log("END IT ALL");
            transform.parent.GetComponent<SceneManager>().GoToScene(11);
            return;
        }
        currentGame = index;
        transform.parent.GetComponent<SceneManager>().GoToScene(8);
        grabber.ResetBool();
        games[currentGame].SpawnArtifacts();
    }

    /*
    public Artifact[] GenerateRandomArtifacts () {
        int amount = Random.Range(1, 4);

        Artifact[] artifactInfo = new Artifact[amount];
        for (int i = 0; i < amount; i++) {
            artifactInfo[i] = new Artifact();
        }

        return artifactInfo;
    }*/

    public Texture[] GetCurrentGameTextures () {
        return games[currentGame].images;
    }

    public int GetNoduleMaxAmount () {
        return games[currentGame].nodules.Length;
    }

    public int GetNoduleAmount () {
        return games[currentGame].GetNodulesFound();
    }

    public int GetArtifactMaxAmount () {
        return games[currentGame].artifacts.Length;
    }

    public int GetArtifactAmount () {
        return games[currentGame].GetArtifactsFound();
    }

    public void AddFlagToCurrentGame (Flag flag) {
        games[currentGame].AddFlag(flag);
        flag.Draw();
    }

    public void CheckForObject (Flag flag, string currentTool) {
        Game game = games[currentGame];
        float size = 100.0f;

        Vector3 position = flag.realPos * 10;
        position.z = 0.0f;

        if (currentTool == "nodules") {
            /*for (int i = 0; i < game.nodules.Length; i++) {

                if (Vector3.Distance(position, game.nodules[i].position) <= 25.0f) game.nodules[i].Find();
                game.nodules[i].Find();

            }*/

            game.FoundNodule();
        }

        if (currentTool == "artifacts") {
            for (int i = 0; i < game.artifacts.Length; i++) {

                if (Vector3.Distance(position, game.artifacts[i].realPos) <= 50.0f) game.artifacts[i].Find();

            }
        }
    }


    public void SendGameDataToSubmission () {
        SubmissionScreen submission = transform.parent.GetComponentInChildren<SubmissionScreen>();
        submission.textArtString = GetArtifactAmount().ToString() + "/" + GetArtifactMaxAmount().ToString();
        submission.textNodString = GetNoduleAmount().ToString();
    }

    public void Submit () {
        games[currentGame].DestroyArtifacts();
        games[currentGame].DestroyFlags();
        ChangeCurrentGame(++currentGame);
        //GoToScene();
    }







    [System.Serializable]
    public class Game {

        public string name;
        public int index;
        public Texture[] images;
        public Nodule[] nodules;
        public Artifact[] artifacts;

        int nodulesFound;
        int artifactsFound;

        GameObject[] artifactsGameObjects;
        List<Flag> flags;

        public Game (int index, Texture[] images, Nodule[] noduleInfo, Artifact[] artifactInfo) {
            this.name = index.ToString();
            this.index = index;
            this.images = images;
            this.nodules = noduleInfo;
            this.artifacts = artifactInfo;

            Init();
            SpawnArtifacts();
        }

        private void Init () {

        }

        public void DestroyArtifacts () {
            if (artifactsGameObjects == null) return;
            foreach (GameObject gameObject in artifactsGameObjects) {
                Destroy(gameObject);
            }
        }

        public void DestroyFlags () {
            if (flags == null) return;
            foreach (Flag flag in flags) {
                Destroy(flag.gameObject);
            }
        }

        public void SpawnArtifacts () {
            artifactsGameObjects = new GameObject[artifacts.Length];

            for (int i = 0; i < artifacts.Length; i++) {
                int index;
                switch (artifacts[i].type) {
                    case Artifact.ArtifactType.Elephant:
                        index = 0;
                        break;
                    case Artifact.ArtifactType.Monkey:
                        index = 1;
                        break;
                    case Artifact.ArtifactType.Football:
                        index = 2;
                        break;
                    case Artifact.ArtifactType.Basketball:
                        index = 3;
                        break;
                    case Artifact.ArtifactType.Tennisball:
                        index = 4;
                        break;
                    default:
                        index = 0;
                        break;
                }
                Debug.Log("Spawning a new Artifact with type " + artifacts[i].type.ToString() + " and index " + i.ToString() + ".");
                artifactsGameObjects[i] = Instantiate(INSTANCE.artifactPrefabs[index], INSTANCE.grabber.transform.position + new Vector3(artifacts[i].position.x, artifacts[i].position.y), Quaternion.identity, INSTANCE.grabber.transform.parent);
                artifactsGameObjects[i].GetComponent<FadeByLayer>().setLayer(artifacts[i].layer);
                artifacts[i].realPos = artifactsGameObjects[i].transform.localPosition;
            }
        }

        public void AddFlag (Flag flag) {
            if (flags == null) flags = new List<Flag>();
            flags.Add(flag);
        }

        public void SpawnFlags () {
            foreach (Flag flag in flags) {
                if(!flag.drawn) flag.Draw();
            }
        }

        public int GetNodulesFound () {
            return nodulesFound;
        }

        public int GetArtifactsFound () {
            return artifactsFound;
        }

        public void FoundNodule () { nodulesFound++; }

        public void FoundArtifact () { artifactsFound++; }

    }

    [System.Serializable]
    public struct Nodule {

        public Vector2 position;
        float size;
        bool found;

        public Nodule (Vector2 position, float size, bool found) {
            this.position = position;
            this.size = size;
            this.found = found;
        }

        public void Find () {
            found = true;
            INSTANCE.games[INSTANCE.currentGame].FoundNodule();
            Debug.Log("Nodule got found at pos: " + position);
        }

    }

    [System.Serializable]
    public class Artifact {

        public enum ArtifactType {
            Elephant, Monkey, Football, Basketball, Tennisball
        }

        public Vector2 position;
        public Vector3 realPos;
        public float layer;
        public float size;
        public ArtifactType type;
        bool found;

        public Artifact ( Vector2 position, float layer, float size, ArtifactType type, bool found ) {
            this.position = position;
            this.layer = layer;
            this.size = size;
            this.type = type;
            this.found = found;

            //Init();
        }

        private void Init () {
            position.x = Random.Range(0, 512);
            position.y = Random.Range(0, 512);

            size = Random.Range(0.5f, 1.0f);
        }

        public void Find () {
            found = true;
            INSTANCE.games[INSTANCE.currentGame].FoundArtifact();
            Debug.Log("Artifact got found at pos: " + position);
        }

    }

    public class Flag {

        public Vector3 position;
        public Vector3 realPos;
        public Color colour;
        public float layer;

        public bool drawn;
        public GameObject gameObject;

        public Flag (Vector3 position, float layer, Color colour) {
            this.position = position;
            this.layer = layer;
            this.colour = colour;
        }

        public void Draw () {
            gameObject = Instantiate(GameManager.INSTANCE.flagPrefab, position, Quaternion.identity, GameManager.INSTANCE.transform);
            realPos = gameObject.transform.localPosition;
            gameObject.GetComponentInChildren<Image>().color = colour;
            gameObject.GetComponent<FadeByLayer>().setLayer(layer);
            drawn = true;
        }

    }

}

