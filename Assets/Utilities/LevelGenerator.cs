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
        private GameObject _levelDataGroup;
        private GameObject[] _backgrounds;

        public LevelGenerator(Vector2 levelOrigin, GameObject[] tiles, string levelData, GameObject levelDataGroup, GameObject[] backgrounds) {

            _levelDataGroup = levelDataGroup;
            _tiles = tiles;
            _levelOrigin = levelOrigin;
            _levelMapAsset = Resources.Load(levelData) as TextAsset;
            _backgrounds = backgrounds;

        }

        public void GenerateLevel() {

            foreach (var bg in _backgrounds) {
                GameObject instantiatedBg =  (GameObject)UnityEngine.Object.Instantiate(bg, new Vector3(_levelOrigin.x + bg.GetComponent<Renderer>().bounds.size.x / 2 - 0.5f, _levelOrigin.y - bg.GetComponent<Renderer>().bounds.size.y / 2 + 0.5f, bg.transform.position.z), Quaternion.identity);
                instantiatedBg.transform.parent = _levelDataGroup.transform;
            }

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
                        instantiatedTile.transform.parent = _levelDataGroup.transform;
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
