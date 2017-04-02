﻿namespace Turbo.Plugins.Jack.Decorators
{
    using System;
    using System.Collections.Generic;
    using Turbo.Plugins.Default;
    using Turbo.Plugins.Jack.TextToSpeech;

    public class SoundAlertDecorator<T> : IWorldDecorator where T : IActor
    {
        public bool Enabled { get; set; }
        public IController Hud { get; private set; }
        public WorldLayer Layer { get; private set; }

        public SoundAlert<T> SoundAlert { get; private set; }

        public SoundAlertDecorator()
        {
            Enabled = true;
        }

        public SoundAlertDecorator(IController hud)
        {
            Hud = hud;
            Enabled = true;
            Layer = WorldLayer.Ground;
        }

        public SoundAlertDecorator(IController hud, SoundAlert<T> soundAlert = null) : this(hud)
        {
            SoundAlert = soundAlert ?? new SoundAlert<T>() { TextFunc = (actor) => actor.SnoActor.NameLocalized };

            /*if (soundAlert != null)
            {
                SoundAlert = soundAlert;
            }
            else
            {
                if (typeof(IItem) == typeof(T))
                {
                    SoundAlert = new SoundAlert<T>() { TextFunc = (item) => item.SnoItem.NameLocalized };
                }
                else if (typeof(IMonster) == typeof(T))
                {
                    SoundAlert = new SoundAlert<T>() { TextFunc = (monster) => monster.SnoMonster.NameLocalized };
                }
                else
                {
                    SoundAlert = new SoundAlert<T>() { TextFunc = (actor) => actor.SnoActor.NameLocalized };
                }
            }/**/
        }

        public void Paint(IActor actor, IWorldCoordinate coord, string text)
        {
            if (!Enabled) return;
            if (actor == null) return;

            SoundAlertManagerPlugin.Register<T>(actor, SoundAlert);
        }

        public IEnumerable<ITransparent> GetTransparents()
        {
            yield break;
        }
    }
}