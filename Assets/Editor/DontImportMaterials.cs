using UnityEditor;

// prevents Unity from automatically trying to import materials for models
public class DontImportMaterials : AssetPostprocessor
{
	public void OnPreprocessModel()
	{
		ModelImporter modelImporter = (ModelImporter) assetImporter;                    
		modelImporter.importMaterials = false;
	}  
}