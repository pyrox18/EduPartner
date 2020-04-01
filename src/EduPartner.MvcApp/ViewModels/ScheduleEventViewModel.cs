using System;
using System.Text.Json.Serialization;

namespace EduPartner.MvcApp.ViewModels
{
    public class ScheduleEventViewModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("start")]
        public string Start { get; set; }

        [JsonPropertyName("end")]
        public string End { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
