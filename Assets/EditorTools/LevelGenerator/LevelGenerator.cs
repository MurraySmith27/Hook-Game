using UnityEngine;
using System.Collections;
using UnityEditor;

public class LevelGenerator : ScriptableWizard
{

    public Texture2D tileMap;

    public int tileSize = 1;
    public ColorToGameObject[] colorMappings;
    //This creates a menu option that can be used from the unity editor.
    [MenuItem("Custom Tools/Generate Level From Image")]
    static void CreateWizard(){
        ScriptableWizard.DisplayWizard<LevelGenerator> ("Level Generator", "Generate Level From Image", "Destroy Old Level");
    }

    void OnWizardCreate(){
        for (int row = 0; row < tileMap.width; row++){
            for (int col = 0; col < tileMap.height; col++){
                GenerateTile(row, col);
            }
        }
    }

    void GenerateTile(int row, int col){

        Color pixelColor = tileMap.GetPixel(row, col);

        if (pixelColor.a == 0){
            return; // pixel is transparent
        }
        
        foreach (ColorToGameObject colorMapping in colorMappings){
            if (colorMapping.color.r == pixelColor.r && colorMapping.color.g == pixelColor.g && colorMapping.color.b == pixelColor.b){
                Vector3 position = new Vector3(0, col * tileSize, row * tileSize);
                Instantiate(colorMapping.prefab, position, Quaternion.identity);
            }
        }
    }
}


