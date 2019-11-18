using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Core.Utils
{
    public class TileSplitter : MonoBehaviour
    {
        public int sizeInPixel;
        public Texture2D spritesheet;

        public void SplitTiles()
        {
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

            var width = spritesheet.width / sizeInPixel;
            var height = spritesheet.height / sizeInPixel;
            var name = spritesheet.name;
            var totalTiles = width * height;
            var path = AssetDatabase.GetAssetPath(spritesheet) + ".meta";
            var lines = new List<string>();
            var line = string.Empty;

            using (var file = new StreamReader(path))
            {
                lines.Add(file.ReadLine());
                file.ReadLine();
                lines.Add("guid: " + System.Guid.NewGuid());

                while ((line = file.ReadLine()) != null)
                {
                    lines.Add(line);

                    if (line.Contains("sprites:")) break;
                }
            }

            var xCount = 0;
            var yCount = height;

            for (var i = 0; i < totalTiles; ++i)
            {
                var x = xCount * sizeInPixel;
                var y = yCount * sizeInPixel;

                lines.Add("    - name: " + name + "_" + i);
                lines.Add("      rect:");
                lines.Add("        serializedVersion: 2");
                lines.Add("        x: " + x);
                lines.Add("        y: " + y);
                lines.Add("        width: " + sizeInPixel);
                lines.Add("        height: " + sizeInPixel);
                lines.Add("      alignment: 0");
                lines.Add("      pivot: {x: 0, y: 0}");
                lines.Add("      border: {x: 0, y: 0, z: 0, w: 0}");

                ++xCount;

                if (xCount >= width)
                {
                    xCount = 0;
                    --yCount;
                }
            }

            lines.Add("  spritePackingTag: ");
            lines.Add("  userData: ");

            File.Delete(path);
            File.WriteAllLines(path, lines.ToArray());
        }
    }
}
