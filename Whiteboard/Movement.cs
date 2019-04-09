using Newtonsoft.Json;
using System.Drawing;

namespace Whiteboard
{
    public sealed class Movement
    {
        [JsonConverter(typeof(PointJsonConverter))]
        public Point From { get; set; }
        [JsonConverter(typeof(PointJsonConverter))]
        public Point To { get; set; }
    }
}
