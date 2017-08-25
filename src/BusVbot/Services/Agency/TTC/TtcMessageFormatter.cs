﻿using System;
using System.Linq;
using BusVbot.Bot;
using BusVbot.Configurations;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace BusVbot.Services.Agency.TTC
{
    public class TtcMessageFormatter : AgencyMessageFormatterBase
    {
        public TtcMessageFormatter(IOptions<AgencyTimeZonesAccessor> timezoneOptions)
            : base(Constants.Tag, timezoneOptions)
        {

        }

        public override InlineKeyboardMarkup CreateInlineKeyboardForDirections(string routeTag, string[] directions)
        {
            if (directions.Length != 2)
            {
                return base.CreateInlineKeyboardForDirections(routeTag, directions);
            }

            int keysPerRow = -1;
            int keyboardRows = -1;

            if (directions.Contains("NORTH", StringComparer.OrdinalIgnoreCase))
            {
                keyboardRows = 2;
                keysPerRow = 1;
                directions = directions.OrderBy(d => d).ToArray();
            }
            else if (directions.Contains("WEST", StringComparer.OrdinalIgnoreCase))
            {
                keyboardRows = 1;
                keysPerRow = 2;
                directions = directions.OrderByDescending(d => d).ToArray();
            }

            var keyboard = new InlineKeyboardButton[keyboardRows][];

            for (int i = 0; i < keyboard.Length; i++)
            {
                var buttons = directions
                    .Skip(i * keysPerRow)
                    .Take(keysPerRow)
                    .Select(d => new InlineKeyboardCallbackButton(
                        d, CommonConstants.CallbackQueries.BusCommand.BusDirectionPrefix + d
                    ) as InlineKeyboardButton)
                    .ToArray();
                keyboard[i] = buttons;
            }

            return new InlineKeyboardMarkup
            {
                InlineKeyboard = keyboard,
            };
        }

        private static class Constants
        {
            public const string Tag = "ttc";
        }
    }
}
