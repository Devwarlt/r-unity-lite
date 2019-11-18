using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Core.Utils
{
    public class SpriteMetadataGenerator : MonoBehaviour
    {
        public int sizeInPixel;
        public Texture2D spritesheet;

        private Dictionary<int, KeyValuePair<int, string>> spritesData = new Dictionary<int, KeyValuePair<int, string>>();

        public void GenerateSpriteMetadata()
        {
            if (spritesheet == null)
            {
                Debug.LogError("Spritesheet is null!");
                return;
            }

            if (sizeInPixel == 0)
            {
                Debug.LogError("Size in pixel shouldn't be zero!");
                return;
            }

            if (sizeInPixel > spritesheet.width)
            {
                Debug.LogError("Size in pixel shouldn't be greater than spritesheet width!");
                return;
            }

            if (sizeInPixel > spritesheet.height)
            {
                Debug.LogError("Size in pixel shouldn't be greater than spritesheet height!");
                return;
            }

            spritesData.Clear();

            var width = spritesheet.width / sizeInPixel;
            var height = spritesheet.height / sizeInPixel;
            var name = spritesheet.name;
            var totalSprites = width * height;
            var path = AssetDatabase.GetAssetPath(spritesheet) + ".meta";
            var lines = new List<string>();
            var line = string.Empty;

            Debug.LogWarningFormat("Creating {0} sprite metadata...", totalSprites);

            using (var file = new StreamReader(path))
            {
                lines.Add(file.ReadLine()); // fileFormatVersion
                lines.Add(file.ReadLine()); // guid
                lines.Add(file.ReadLine()); // TextureImporter
                lines.Add("  internalIDToNameTable:");

                for (var i = 0; i < totalSprites; i++)
                {
                    var spriteName = string.Format("{0}_{1}", name, i);
                    var internalId = GU.ComputeFileID(spritesheet.GetType(), spriteName);

                    lines.Add("  - first:");
                    lines.Add("      213: " + internalId.ToString());
                    lines.Add("    second: " + spriteName);

                    try
                    {
                        spritesData.Add(i, new KeyValuePair<int, string>(internalId, spriteName));
                    }
                    catch (ArgumentException) { Debug.LogErrorFormat("Duplicated entry for sprite ID {0}!", i); }
                }

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("sprites:")) break;

                    lines.Add(line);
                }
            }

            lines.Add("    sprites:");

            var xCount = 0;
            var yCount = height - 1;

            Debug.LogWarning("Generating metadata for multiple sprites...");

            for (var j = 0; j < totalSprites; j++)
            {
                var x = xCount * sizeInPixel;
                var y = yCount * sizeInPixel;
                var spriteData = spritesData[j];

                lines.Add("    - serializedVersion: 2");
                lines.Add("      name: " + spriteData.Value);
                lines.Add("      rect:");
                lines.Add("        serializedVersion: 2");
                lines.Add("        x: " + x);
                lines.Add("        y: " + y);
                lines.Add("        width: " + sizeInPixel);
                lines.Add("        height: " + sizeInPixel);
                lines.Add("      alignment: 0");
                lines.Add("      pivot: {x: 0.5, y: 0.5}");
                lines.Add("      border: {x: 0, y: 0, z: 0, w: 0}");
                lines.Add("      outline: []");
                lines.Add("      physicsShape: []");
                lines.Add("      tessellationDetail: 0");
                lines.Add("      bones: []");
                lines.Add("      spriteID: " + System.Guid.NewGuid());
                lines.Add("      internalID: " + spriteData.Key);
                lines.Add("      vertices: []");
                lines.Add("      indices:");
                lines.Add("      edges: []");
                lines.Add("      weights: []");

                xCount++;

                if (xCount >= width)
                {
                    xCount = 0;
                    --yCount;
                }
            }

            Debug.LogWarningFormat("Appending required metadata declaration for spritesheet '{0}'...", name);

            lines.Add("    outline: []");
            lines.Add("    physicsShape: []");
            lines.Add("    bones: []");
            lines.Add("    spriteID: " + System.Guid.NewGuid());
            lines.Add("    internalID: 0");
            lines.Add("    vertices: []");
            lines.Add("    indices:");
            lines.Add("    edges: []");
            lines.Add("    weights: []");
            lines.Add("    secondaryTextures: []");
            lines.Add("  spritePackingTag: ");
            lines.Add("  pSDRemoveMatte: 0");
            lines.Add("  pSDShowRemoveMatteOption: 0");
            lines.Add("  userData:");
            lines.Add("  assetBundleName:");
            lines.Add("  assetBundleVariant:");

            Debug.LogWarning("Well done! Deleting old metadata file from resources...");

            File.Delete(path);

            Debug.LogWarning("Old metadata has been deleted, writting changes...");

            File.WriteAllLines(path, lines.ToArray());

            Debug.LogWarningFormat("Success! You have successfully splitted and generated {0} metadata to spritesheet '{1}'! Metadata has been saved at:\nPath: {2}", totalSprites, name, path);
        }
    }
}
