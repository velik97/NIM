using UnityEngine;
using UnityEngine.SceneManagement;

public static class CrossDataHandler {

	private static int currentThemeIndex;
	public static int CurrentThemeIndex {
		get {
			return currentThemeIndex;
		}
		set {
			currentThemeIndex = Mathf.Clamp (value, 0, themeSet.themes.Length - 1);
			Serialize ();
		}
	}

	public static Theme CurrentTheme {
		get {
			return themeSet.themes [currentThemeIndex];
		}
	}

	private static int maxAvailableThemeIndex;
	public static int MaxAvailableThemeIndex {
		get {
			return maxAvailableThemeIndex;
		}
		set {
			maxAvailableThemeIndex = Mathf.Clamp (value, 0, themeSet.themes.Length - 1);
			Serialize ();
		}
	}

	public static int PassedTutorial {get; set;}

	public static int TutorialLevel { get; set;}

	private static int currentLevel;
	public static int CurrentLevel {
		get {
			return currentLevel;
		}
		set {
			currentLevel = Mathf.Clamp (value, 0, mapConfigSet.mapConfigs.Length - 1);
			Serialize ();
		}
	}

	public static MapConfig CurrentMapConfig {
		get {
			return mapConfigSet.mapConfigs [currentLevel];
		}
	}

	private static int winTimes;
	public static int WinTimes {
		get {
			return winTimes;
		}
		set
		{
			winTimes = value;
			Serialize ();
		}
	}

	private static int maxAvailableLevel;
	public static int MaxAvailableLevel {
		get {
			return maxAvailableLevel;
		}
		set {
			maxAvailableLevel = Mathf.Clamp (value, 0, mapConfigSet.mapConfigs.Length);
			Serialize ();
		}
	}

	public static int PlayersCount {get; set;}

	private static int hintsCount;
	public static int HintsCount { 
		get {
			return hintsCount;
		}
		set {
			hintsCount = Mathf.Max (value, 0);
			Serialize ();
		}
	}

	public static ThemeSet themeSet;
	public static MapConfigSet mapConfigSet;
	public static PositionConfig positionConfig;

	public static void Serialize () {
		string data = 
			CurrentThemeIndex.ToString () + "|" +
			MaxAvailableThemeIndex.ToString () + "|" +
			PassedTutorial.ToString () + "|" +
			TutorialLevel.ToString () + "|" +
			CurrentLevel.ToString () + "|" +
			WinTimes.ToString () + "|" +
			MaxAvailableLevel.ToString () + "|" +
			PlayersCount.ToString () + "|" +
			HintsCount.ToString ();

		PlayerPrefs.SetString ("Data", data);
	}

	public static void Deserialize () {
		FindRefs ();

		if (PlayerPrefs.HasKey ("Data")) {
			string data = PlayerPrefs.GetString ("Data");
			string[] tokens = data.Split ('|');
			CurrentThemeIndex = int.Parse (tokens [0]);
			MaxAvailableThemeIndex = int.Parse (tokens [1]);
			PassedTutorial = int.Parse (tokens [2]);
			TutorialLevel = int.Parse (tokens [3]);
			CurrentLevel = int.Parse (tokens [4]);
			WinTimes = int.Parse (tokens [5]);
			MaxAvailableLevel = int.Parse (tokens [6]);
			PlayersCount = int.Parse (tokens [7]);
			HintsCount = int.Parse (tokens [8]);
		} else {
			CurrentThemeIndex = 0;
			MaxAvailableThemeIndex = 0;
			PassedTutorial = 0;
			TutorialLevel = 0;
			CurrentLevel = 0;
			WinTimes = 0;
			MaxAvailableLevel = 0;
			PlayersCount = 0;
			HintsCount = 3;
		}

	}

	static void FindRefs () {
		themeSet = ConfigsHolder.Instance.themeSet;
		mapConfigSet = ConfigsHolder.Instance.mapConfigSet;
		positionConfig = ConfigsHolder.Instance.positionConfig;
	}

	public static void Reboot () {
		FindRefs ();
		CurrentThemeIndex = 0;
		MaxAvailableThemeIndex = 0;
		PassedTutorial = 0;
		TutorialLevel = 0;
		CurrentLevel = 0;
		WinTimes = 0;
		MaxAvailableLevel = 0;
		PlayersCount = 0;
		HintsCount = 3;
		Serialize ();
	}

	public static void LoadScene (int index) {
		Serialize ();
		SceneManager.LoadScene (index);
	}


}
