using Dynamo.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;

namespace TimeTravel
{
    public class Snapshot
    {
        public readonly List<NodeViewModel> Nodes;
        public readonly string Author;
        public readonly DateTime Timestamp;

        public Snapshot()
        {
            this.Author = Environment.UserName;
            this.Timestamp = DateTime.UtcNow;
        }

        public Snapshot(List<NodeViewModel> nodes) : this()
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
