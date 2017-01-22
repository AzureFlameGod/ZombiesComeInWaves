using UnityEditor;
using UnityEngine;
using System.Collections;

// Automatically converts textures that have a _n or _normal suffix in the filename to normal textures
public class NormalTextureTextureProcessor : AssetPostprocessor {
	void OnPostprocessTexture(Texture2D texture) {

		string lowerCaseAssetPath = assetPath.ToLower();

		if (lowerCaseAssetPath.IndexOf("_n.") >= 0 
            || lowerCaseAssetPath.IndexOf("_normal.") >= 0
        )
		{
			Debug.Log ("Recognizing normal image: " + assetPath);

			TextureImporter importer = assetImporter as TextureImporter;
			if (importer.textureType != TextureImporterType.NormalMap)
			{
				Debug.Log ("Fixing texture type to be normal");
				importer.textureType = TextureImporterType.NormalMap;
				importer.SaveAndReimport();
			}
		}
	}
}