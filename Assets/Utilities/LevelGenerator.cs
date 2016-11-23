using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Utilities {
    class LevelGenerator {

        TextAsset _levelMapAsset;
        Vector2 _levelOrigin;
        GameObject[] _tiles;
        private Transform _levelDataGroup;

        public LevelGenerator(Vector2 levelOrigin, GameObject[] tiles, string levelData, Transform levelDataGroup) {

            _levelDataGroup = levelDataGroup;
            _tiles = tiles;
            _levelOrigin = levelOrigin;
            _levelMapAsset = Resources.Load(levelData) as TextAsset;

        }

        public void GenerateLevel() {

            int i = 0;
            int j = 0;

            foreach (var character in _levelMapAsset.text) {

                if(character == ' ' || character == '\r') {
                    j++;
                    continue;
                } else if (character == '\n') {
                    i++;
                    j = 0;
                    continue;
                } else {

                    GameObject tileToInstantiate = FindTile(character.ToString());

                    if(tileToInstantiate != null) {

                        GameObject instantiatedTile;
                        instantiatedTile = (GameObject)UnityEngine.Object.Instantiate(tileToInstantiate, new Vector3(_levelOrigin.x + j, _levelOrigin.y - i), tileToInstantiate.transform.rotation);
                        instantiatedTile.transform.parent = _levelDataGroup;
                    }

                    j++;
                }
            }
        }

        private GameObject FindTile(string TileCode) {

            foreach (var tileObject in _tiles) {

                if(tileObject.name == TileCode) {

                    return tileObject;
                }
            }

            return null;

        }
    }
}
