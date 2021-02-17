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
    public class DistortLines : StoryboardObjectGenerator
    {
        public string dotPath = "sb/dot.png";
        // public string fadePath = "sb/fade.png";

        public override void Generate()
        {
            var layer = GetLayer("");
            GenerateDistortLines(layer, 112501, 130716);
        }

        public void GenerateDistortLines(StoryboardLayer layer, int startTime, int endTime)
        {
            OsuHitObject previousHitobject = Beatmap.HitObjects.ElementAt(0);

            

            var sprites = new OsbSprite[]
            {
                layer.CreateSprite(dotPath, OsbOrigin.Centre, new Vector2(320, 240)),
                layer.CreateSprite(dotPath, OsbOrigin.Centre, new Vector2(320, 240)),
                layer.CreateSprite(dotPath, OsbOrigin.Centre, new Vector2(320, 240))
            };

            


            foreach (var hitobject in Beatmap.HitObjects)
            {
                if ((startTime != 0 || endTime != 0) &&
                    (hitobject.StartTime < startTime - 5 || endTime - 5 <= hitobject.StartTime))
                    continue;


                var bitmap = GetMapsetBitmap(dotPath);
                float scaleY = 480f / bitmap.Height;
                float scaleX = 25f / bitmap.Width;

                if (previousHitobject.StartTime < 90403)
                {
                    previousHitobject = hitobject;
                    sprites[0].ScaleVec(hitobject.StartTime - 150, scaleX, scaleY);
                    sprites[1].ScaleVec(hitobject.StartTime - 150, scaleX, scaleY);
                    sprites[2].ScaleVec(hitobject.StartTime - 150, scaleX, scaleY);
                    sprites[0].Additive(hitobject.StartTime - 150);
                    sprites[1].Additive(hitobject.StartTime - 150);
                    sprites[2].Additive(hitobject.StartTime - 150);

                    sprites[0].Color(hitobject.StartTime - 150, colorRGB(200, 0, 0));
                    sprites[1].Color(hitobject.StartTime - 150, colorRGB(0, 200, 0));
                    sprites[2].Color(hitobject.StartTime - 150, colorRGB(0, 0, 200));

                    sprites[0].Fade(hitobject.StartTime - 150, 0.6f);
                    sprites[1].Fade(hitobject.StartTime - 150, 0.6f);
                    sprites[2].Fade(hitobject.StartTime - 150, 0.6f);


                }

                if (previousHitobject is OsuSlider)
                {
                    MoveX(sprites, previousHitobject.EndTime, hitobject.StartTime, previousHitobject.PositionAtTime(previousHitobject.EndTime).X, hitobject.Position.X);

                    double bruh = previousHitobject.StartTime + ((hitobject.StartTime - previousHitobject.StartTime) / 2);
                    Fade(sprites, OsbEasing.OutExpo, previousHitobject.StartTime, bruh, 0.6f, 0.9f);
                    Fade(sprites, OsbEasing.InExpo, bruh, hitobject.StartTime, 0.9f, 0.6f);

                    ScaleVec(sprites, OsbEasing.OutExpo, previousHitobject.StartTime, bruh, scaleX, scaleY, scaleX * 1.3f, scaleY * 1.3f);
                    ScaleVec(sprites, OsbEasing.InExpo, bruh, hitobject.StartTime, scaleX * 1.3f, scaleY * 1.3f, scaleX, scaleY);
                }
                else
                {
                    MoveX(sprites, previousHitobject.StartTime, hitobject.StartTime, previousHitobject.Position.X, hitobject.Position.X);

                    double bruh = previousHitobject.StartTime + ((hitobject.StartTime - previousHitobject.StartTime) / 2);
                    Fade(sprites, OsbEasing.OutExpo, previousHitobject.StartTime, bruh, 0.6f, 0.9f);
                    Fade(sprites, OsbEasing.InExpo, bruh, hitobject.StartTime, 0.9f, 0.6f);

                    ScaleVec(sprites, OsbEasing.OutExpo, previousHitobject.StartTime, bruh, scaleX, scaleY, scaleX * 1.5f, scaleY * 1.5f);
                    ScaleVec(sprites, OsbEasing.InExpo, bruh, hitobject.StartTime, scaleX * 1.5f, scaleY * 1.5f, scaleX, scaleY);
                }


                previousHitobject = hitobject;

                if (hitobject is OsuSlider)
                {
                    var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 8;
                    var start = hitobject.StartTime;
                    while (true)
                    {
                        var end = start + timestep;

                        var complete = hitobject.EndTime - end < 5;
                        if (complete) end = hitobject.EndTime;

                        var startPosition = sprites[0].PositionAt(start);
                        MoveX(sprites, start, end, startPosition.X, hitobject.PositionAtTime(end).X);

                        if (complete) break;
                        start += timestep;
                    }
                }
            }
        }

        void MoveX(OsbSprite[] sprites, double startTime, double endTime, float startX, float endX)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].MoveX(startTime, endTime, startX + i * 5, endX + i * 5);
            }
        }

        void Fade(OsbSprite[] sprites, OsbEasing easing, double startTime, double endTime, float startF, float endF)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].Fade(easing, startTime, endTime, startF, endF);
            }
        }

        void ScaleVec(OsbSprite[] sprites, OsbEasing easing, double startTime, double endTime, float startX, float startY, float endX, float endY)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].ScaleVec(easing, startTime, endTime, startX, startY, endX, endY);
            }
        }

        public Color4 colorRGB(float r, float g, float b)
        {
            return new Color4(r / 255, g / 255, b / 255, 1);
        }
    }
}
