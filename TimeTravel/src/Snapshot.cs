using Dynamo.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;

namespace TimeTravel
{
    public class Snapshot
    {
        public List<NodePreset> Nodes;
        public string Author;
        public DateTime Timestamp;

        public Snapshot()
        {
            this.Author = Environment.UserName;
            this.Timestamp = DateTime.UtcNow;
            this.Nodes = new List<NodePreset>();
        }

        public Snapshot(List<NodePreset> nodes) : this()
        {
            this.Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
        }

        public string GenerateSnapshotFilename()
        {
            var filename = $"Dynamo-Snapshot-{this.Author}.json";
            var filepath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                filename);

            return filepath;
        }

        public string ToJson()
        {
            return Json.ToJson(this);
        }

        public string ToJsonFile()
        {
            var filename = this.GenerateSnapshotFilename();
            var saved = Json.ToJsonFile(this, filename);
            return saved ? filename : null;
        }

        public static Snapshot FromJsonFile(string filepath)
        {
            return Json.FromJsonFileTo<Snapshot>(filepath);
        }
    }
}
