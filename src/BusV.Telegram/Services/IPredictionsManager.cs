﻿using System.Threading.Tasks;
using BusV.Ops;
using BusV.Telegram.Models;
using NextBus.NET.Models;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace BusV.Telegram.Services
{
    public interface IPredictionsManager
    {
        bool ValidateRouteFormat(string routeTag);

        Task<string> GetSampleRouteTextAsync(UserChat userchat);

        Task TryReplyWithPredictionsAsync(IBot bot, UserChat userchat, int replyToMessageId);

        Task UpdateMessagePredictionsAsync(IBot bot, ChatId chatId, int messageId, Location location,
            string agency, string route, string direction);

        Task<(Location BusStopLocation, RoutePrediction[] Predictions)> GetPredictionsReplyAsync
            (Location userLocation, string agencyTag, string routeTag, string direction);

        (bool Success, string Route, string Direction) TryParseToRouteDirection(string input);

        Task<(string RouteTag, string Direction)> GetCachedRouteDirectionAsync(UserChat userchat);

        Task CacheRouteDirectionAsync(UserChat userchat, string routeTag, string direction);
    }
}
