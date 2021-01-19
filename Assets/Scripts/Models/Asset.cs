using UnityEngine;

public abstract class Asset
{
        protected Vector3 _position;
        protected GameObject _prefab;
        protected Scene _nextScene;

        public abstract Vector3 getPosition();
        public abstract GameObject getPrefab();
        public abstract Scene getNextScene();
}