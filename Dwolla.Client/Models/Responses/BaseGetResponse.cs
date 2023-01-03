﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public interface IEmbeddedResponse<T>
    {
        IEmbed<T> Embedded { get; set; }
    }

    public abstract class BaseGetResponse<T> : BaseResponse, IEmbeddedResponse<T>
    {
        [JsonPropertyName("_embedded")]
        public IEmbed<T> Embedded { get; set; }

        public int Total { get; set; }
    }

    public interface IEmbed<T>
    {
        List<Error> Errors { get; set; }
        List<T> Results();
    }

    public abstract class Embed<T> : IEmbed<T>
    {
        public List<Error> Errors { get; set; }

        public abstract List<T> Results();
    }
}