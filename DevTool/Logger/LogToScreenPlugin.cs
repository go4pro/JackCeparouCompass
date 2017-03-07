﻿namespace Turbo.Plugins.Jack.DevTool.Logger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Turbo.Plugins.Default;

    public class LogToScreenPlugin : BasePlugin, IInGameTopPainter
    {
        public TopLabelDecorator MessageFrame { get; set; }
        public Dictionary<LogLevel, IFont> Fonts { get; set; }

        public LogToScreenPlugin()
        {
            Enabled = true;
            Fonts = new Dictionary<LogLevel, IFont>();
        }

        public override void Load(IController hud)
        {
            base.Load(hud);
            var fontFamily = "consolas";
            MessageFrame = new TopLabelDecorator(hud)
            {
                BackgroundBrush = Hud.Render.CreateBrush(178, 0, 0, 0, 0),
                BorderBrush = Hud.Render.CreateBrush(255, 0, 0, 0, 2),
                TextFont = Hud.Render.CreateFont(fontFamily, 7, 224, 240, 240, 64, true, false, false),
                //AlertTextFunc = () => string.Join("\n", Log.Messages.Select(m => m.Message)),
            };

            Fonts.Add(LogLevel.Error, Hud.Render.CreateFont(fontFamily, 7, 224, 255, 0, 0, true, false, false));
            Fonts.Add(LogLevel.All, Hud.Render.CreateFont(fontFamily, 7, 224, 240, 240, 64, true, false, false));
            Fonts.Add(LogLevel.Info, Hud.Render.CreateFont(fontFamily, 7, 224, 240, 240, 64, true, false, false));
            Fonts.Add(LogLevel.Debug, Hud.Render.CreateFont(fontFamily, 7, 224, 240, 240, 64, true, false, false));
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.BeforeClip) return;

            // dumb test
            //Log.Error(Guid.NewGuid().ToString());

            //try
            //{
            //    var item = Hud.Inventory.GetSnoItem(1612258795);
            //    Simon.Log.Debug(item.NameEnglish);
            //}
            //catch (Exception ex)
            //{
            //    Simon.Log.Debug(ex.Message);
            //}
            ////337492 Nephalem Rift
            //foreach (var s in Hud.Game.Quests.Where(q => q.SnoQuest.Type != QuestType.Bounty))
            //{
            //    Log.Debug("{0} {1}", s.SnoQuest.Sno, s.SnoQuest.NameEnglish);
            //}
            //foreach (var s in Hud.Game.Me.Powers.PassiveSlots)
            //{
            //    Log.Debug("{0} {1}", s.Sno, s.NameEnglish);
            //}
            //foreach (var s in Hud.Game.Me.Powers.UsedPassives)
            //{
            //    Log.Error("{0} {1}", s.Sno, s.NameEnglish);
            //}

            if (Hud.Input.IsKeyDown(Keys.X))
            {
                Jack.Log.Messages.Clear();
            }
            if (Jack.Log.Messages.Count == 0) return;

            var screenSize = Hud.Window.Size;
            var x = screenSize.Width * 0.0f;
            var y = screenSize.Height * 0.1042f;

            var estimatedWidth = Jack.Log.Messages.Max(m => m.Message.Length) * 8f; // TODO : fix for long exceptions
            var estimatedHeight = (Jack.Log.Messages.Count + 1) * 14f + 20 + 2;

            MessageFrame.Paint(x, y, estimatedWidth, estimatedHeight, HorizontalAlign.Left);

            x += 10;
            y += 4;

            Fonts[LogLevel.All].DrawText("Jack says :", x, y);
            y += 14;

            foreach (var message in Jack.Log.Messages)
            {
                Fonts[message.Level].DrawText(message.Message, x, y);
                y += 14;
            }
        }
    }
}