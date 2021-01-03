using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class Loading : StoryboardObjectGenerator
    {
        [Configurable] public int startTime;
        [Configurable] public int endTime;
        public string dotPath = "sb/dot.png";

        public override void Generate()
        {
            var layer = GetLayer("");
		    var boxSprites = generateBox(320, 240, 160, 20);

            var loadingBar = layer.CreateSprite(dotPath, OsbOrigin.CentreLeft, new Vector2(320 - 78, 240));
            loadingBar.ScaleVec(OsbEasing.None, startTime, endTime, 0, 16, 156, 16);
            
        }

        OsbSprite[] generateBox(float x, float y, float width, float height)
        {
            var layer = GetLayer("");
            
            float thickness = 1;
            float innerWidth = width - thickness;
            float innerHeight = height - thickness;
            // Vector2 topLeftPos = new Vector2(x - innerWidth / 2, y + innerHeight / 2);
            // Vector2 botRightPos = new Vector2(x + innerWidth / 2, y - innerHeight / 2);

            var leftBox = layer.CreateSprite(dotPath, OsbOrigin.BottomRight, new Vector2(x - innerWidth / 2, y + innerHeight / 2));
            var topBox = layer.CreateSprite(dotPath, OsbOrigin.BottomLeft, new Vector2(x - innerWidth / 2, y - innerHeight / 2));
            var rightBox = layer.CreateSprite(dotPath, OsbOrigin.TopLeft, new Vector2(x + innerWidth / 2, y - innerHeight / 2));
            var botBox = layer.CreateSprite(dotPath, OsbOrigin.TopRight, new Vector2(x + innerWidth / 2, y + innerHeight / 2));

            Vector2 leftRightScale = new Vector2(thickness, height);
            Vector2 topBotScale = new Vector2(width, thickness);

            leftBox.ScaleVec(startTime, endTime, leftRightScale.X, leftRightScale.Y, leftRightScale.X, leftRightScale.Y);
            rightBox.ScaleVec(startTime, endTime, leftRightScale.X, leftRightScale.Y, leftRightScale.X, leftRightScale.Y);
            topBox.ScaleVec(startTime, endTime, topBotScale.X, topBotScale.Y, topBotScale.X, topBotScale.Y);
            botBox.ScaleVec(startTime, endTime, topBotScale.X, topBotScale.Y, topBotScale.X, topBotScale.Y);

            var sprites = new OsbSprite[] {topBox, rightBox, botBox, leftBox};
            return sprites;
        }

        
    }
}
