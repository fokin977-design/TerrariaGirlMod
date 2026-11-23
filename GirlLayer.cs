using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace GirlMod
{
    // Включение скина: GirlMod.GirlPlayer.GirlSkin = true (например, из предмета/буфа)
    public class GirlPlayer : ModPlayer
    {
        public bool GirlSkin = true; // по умолчанию включён для демо
    }

    // Слой, который рисует наш спрайт-лист вместо ванильного тела
    public class GirlLayer : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.GetModPlayer<GirlPlayer>().GirlSkin;
        }

        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Leggings);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player p = drawInfo.drawPlayer;
            // 0-2 idle, 3-6 walk, 7-10 run, 11 jump, 12 fall, 13 sit
            int frame = PickFrame(p);

            Texture2D tex = ModContent.Request<Texture2D>("GirlMod/Girl_Spritesheet").Value;
            Rectangle src = new Rectangle(frame * 40, 0, 40, 56);
            Vector2 pos = (p.Center - Main.screenPosition) + new Vector2(0f, -6f);
            SpriteEffects fx = p.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            DrawData data = new DrawData(tex, pos, src, Color.White, 0f,
                new Vector2(20f, 28f), 1f, fx, 0);
            drawInfo.DrawDataCache.Add(data);
        }

        private static int PickFrame(Player p)
        {
            if (p.sitting.isSitting) return 13;
            if (p.velocity.Y < -1f) return 11;        // прыжок
            if (p.velocity.Y > 1f) return 12;         // падение

            float speed = System.Math.Abs(p.velocity.X);
            if (speed < 0.5f)
                return (int)(Main.GameUpdateCount / 20) % 3;                 // idle 0-2
            if (speed < 4.5f)
                return 3 + (int)(Main.GameUpdateCount / 7) % 4;              // walk 3-6
            return 7 + (int)(Main.GameUpdateCount / 4) % 4;                  // run 7-10
        }
    }
}
