using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayAndNightScript : MonoBehaviour {
    public GameObject Sun;
    public Gradient Colors,
        SkyboxColors,
        LightColors;
    public Color CurrentColor,
        SkyboxColor,
        LightColor;
    public bool debug,
        lerpDebug;
    public float debug_time_hours,
        debug_time_minutes,
        debug_lerp_time;
    public Material SkyboxDay,
        SkyboxDusk,
        SkyboxNight,
        SkyboxMaterial;
    Material Skybox;
    Image FilterEffect;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        Skybox = SkyboxDay;
        FilterEffect = GetComponentInChildren<Image>();
	}
	
	// Update is called once per frame
	void Update () {
       
        Sun = GameObject.Find("Directional light");
        if (!debug)
        {
            Sun.transform.eulerAngles = new Vector3(((System.DateTime.Now.TimeOfDay.Minutes + (System.DateTime.Now.TimeOfDay.Hours * 60)) / 4) - 135, 0, 0);
            CurrentColor = Colors.Evaluate((((float)System.DateTime.Now.Minute + ((float)System.DateTime.Now.Hour * 60)) / 1439));
            SkyboxColor = SkyboxColors.Evaluate((((float)System.DateTime.Now.Minute + ((float)System.DateTime.Now.Hour * 60)) / 1439));
            LightColor = LightColors.Evaluate((((float)System.DateTime.Now.Minute + ((float)System.DateTime.Now.Hour * 60)) / 1439));
            
        }
        else
        {
            Sun.transform.eulerAngles = new Vector3(((debug_time_minutes + (debug_time_hours * 60)) / 4) - 135, 0, 0);
            CurrentColor = Colors.Evaluate(((debug_time_minutes + (debug_time_hours * 60)) / 1439));
            SkyboxColor = SkyboxColors.Evaluate(((debug_time_minutes + (debug_time_hours * 60)) / 1439));
            LightColor = LightColors.Evaluate(((debug_time_minutes + (debug_time_hours * 60)) / 1439));
        }

        //FilterEffect.color = CurrentColor;
        FilterEffect.color = new Color(0,0,0,0);
        SkyboxMaterial.SetColor("_Tint", Color.Lerp(SkyboxDay.color, SkyboxColor, 0.5f));
        Skybox = SkyboxMaterial;
        RenderSettings.skybox = Skybox;
        Sun.GetComponent<Light>().color = LightColor;
    }
}
