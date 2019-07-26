using Dynamo.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TimeTravel;

namespace TimeTravel.Tests
{
    [TestClass]
    public class SnapshotTests
    {
        [TestMethod]
        public void CanMakeNodeViewModel()
        {
        }

        [TestMethod]
        public void GenerateSnapshotFilename_CanBuildCorrectFilename()
        {
            // Arrange
            string expectedName = Environment.UserName;
            string expectedPrefix = "Dynamo-Snapshot-";
            string expectedExtension = ".json";
            string expectedFileName = expectedPrefix + expectedName + expectedExtension;
            string expectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), expectedFileName);

            var snapshot = new Snapshot();

            // Act
            var filename = snapshot.GenerateSnapshotFilename();

            // Assert
            Assert.AreEqual(expectedPath, filename);
        }

        [TestMethod]
        public void ToJsonFile_CanSerialise()
        {
            // Arrange
            var snapshot = new Snapshot();
            var filename = snapshot.GenerateSnapshotFilename();

            // Act
            var result = snapshot.ToJsonFile();

            // Assert
            Assert.IsTrue(File.Exists(result));
            File.Delete(result);
        }

        [TestMethod]
        public void FromJsonFile_CanDeserialise()
        {
            // Arrange
            var snapshotSource = new Snapshot();
            var filename = snapshotSource.ToJsonFile();

            // Act
            var snapshot = Snapshot.FromJsonFile(filename);

            // Assert
            Assert.IsNotNull(snapshot);
            Assert.IsNotNull(snapshot.Nodes);
            Assert.IsInstanceOfType(snapshot, typeof(Snapshot));
        }
    }
}
