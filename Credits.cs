using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;


namespace StorybrewScripts
{
    public class Credits : StoryboardObjectGenerator
    {
        string fontName = "超極細ゴシック";
        private FontGenerator font;
        public override void Generate()
        {
            font = FontGenerator("sb/Credits");
            double beatDuration = Beatmap.GetTimingPointAt((int)175716).BeatDuration;

            string Mappers = "Mapper: toybot";

            CreateText(175716, 175716 + beatDuration, "Title: 丸の内サディスティック (neetskills remix)");
            CreateText(175716 + beatDuration, 175716 + beatDuration * 2, "Artist: 椎名林檎");
            CreateText(175716 + beatDuration * 2, 175716 + beatDuration * 3, "Mapset: -Mikan");
            CreateText(175716 + beatDuration * 3, 175716 + beatDuration * 4, Mappers);
            CreateText(175716 + beatDuration * 4, 175716 + beatDuration * 5, "Storyboard: MRL");

            CreateText(175716 + beatDuration * 5, 175716 + beatDuration * 6, "Title: 丸の内サディスティック (neetskills remix)");
            CreateText(175716 + beatDuration * 6, 175716 + beatDuration * 7, "Artist: 椎名林檎");
            CreateText(175716 + beatDuration * 7, 175716 + beatDuration * 8, "Mapset: -Mikan");
            CreateText(175716 + beatDuration * 8, 175716 + beatDuration * 8.5f, Mappers);
            CreateText(175716 + beatDuration * 8.5f, 175716 + beatDuration * 9f, "Storyboard: MRL");

            CreateText(175716 + beatDuration * 9f, 175716 + beatDuration * 9.5f, "Title: 丸の内サディスティック (neetskills remix)");
            CreateText(175716 + beatDuration * 9.5f, 175716 + beatDuration * 10f, "Artist: 椎名林檎");
            CreateText(175716 + beatDuration * 10f, 175716 + beatDuration * 10.5f, "Mapset: -Mikan");
            CreateText(175716 + beatDuration * 10.5f, 175716 + beatDuration * 11f, Mappers);
            CreateText(175716 + beatDuration * 11f, 175716 + beatDuration * 11.5f, "Storyboard: MRL");

            CreateThanks();
        }

        FontGenerator FontGenerator(string output)
        {
            var font = LoadFont(output, new FontDescription()
            {
                FontPath = fontName,
                FontSize = 40,
                Color = Color4.White,
                Padding = Vector2.Zero,
                FontStyle = FontStyle.Regular,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            },
            new FontGlow()
            {
                Radius = 0,
                Color = Color4.White,
            },
            new FontOutline()
            {
                Thickness = 1,
                Color = Color4.White,
            });

            return font;
        }

        public void CreateText(double startTime, double endTime, string text)
        {
            var texture = font.GetTexture(text);
            var sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre);

            sprite.Scale(startTime, 0.3f * 1);
            sprite.Fade(startTime, endTime, 1, 1);
        }

        public void CreateThanks()
        {
            double startTime = 181876;
            double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

            string thanksRight = "Thanks for Playing!";
            List<string> rndStrings = new List<string>();

            System.Random rnd = new System.Random();
            for (int i = 0; i < 10; i++)
            {
                char[][] chars = new char[][]
                {
                    "Thanks".OrderBy(x => rnd.Next()).ToArray(),
                    "for".OrderBy(x => rnd.Next()).ToArray(),
                    "Playing!".OrderBy(x => rnd.Next()).ToArray()
                };

                rndStrings.Add(new string(chars[0]) + " " + new string(chars[1]) + " " + new string(chars[2]));
            }


            CreateText(startTime, startTime + beatDuration * 0.5f, rndStrings[0]);

            // 182144 to 183216
            for (int i = 1; i < 10; i++)
            {
                CreateText(182144 + beatDuration * ((i -1) / 4f), 182144 + beatDuration * (i / 4f) , rndStrings[i]);
            }

            CreateText(183350, 183350 + beatDuration * 0.5f, "khanTs rof Pgayinl!");
            CreateText(183350 + beatDuration * 0.5f, 183350 + beatDuration * 1f, "khanTs for Pgayinl!");
            CreateText(183350 + beatDuration * 1f, 183350 + beatDuration * 1.25f, "Thanks for Pgayinl!");
            CreateText(183350 + beatDuration * 1.25f, 183350 + beatDuration * 1.5f, "Thanks for gPayinl!");
            CreateText(183350 + beatDuration * 1.5f, 183350 + beatDuration * 1.75f, "Thanks for lPaying!");

            CreateText(184287, 186430, thanksRight);
        }
    }
}
