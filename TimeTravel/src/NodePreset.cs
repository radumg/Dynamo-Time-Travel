using Dynamo.Graph.Nodes;
using Dynamo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTravel
{
    public class NodePreset
    {
        public string Id { get; set; }
        public string NodeType { get; set; }
        public NodeInputData InputData { get; set; }
        public NodeOutputData OutputData { get; set; }

        public NodePreset()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public NodePreset(NodeModel nodeModel):this()
        {
            if (nodeModel == null)
                throw new ArgumentNullException(nameof(nodeModel));

            this.Id = nodeModel.GUID.ToString();
            this.NodeType = nodeModel.NodeType;
            this.InputData = nodeModel.InputData;
            this.OutputData = nodeModel.OutputData;
        }
    }
}
