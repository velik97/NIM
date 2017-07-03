using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapConfigSet", menuName = "Configs/Map Config Set", order = 1)]
public class MapConfigSet : ScriptableObject {

	public MapConfig[] mapConfigs;

}
