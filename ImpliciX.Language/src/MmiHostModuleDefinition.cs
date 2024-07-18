using System.Collections.Generic;
using System.Linq;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;

namespace ImpliciX.Language
{
    public class MmiHostModuleDefinition
    {
        public MmiHostModuleDefinition()
        {
            ApplicationsSoftwareDevices = new Dictionary<SoftwareDeviceNode, string>();
        }
        public Dictionary<SoftwareDeviceNode, string> ApplicationsSoftwareDevices { get; set; }
        public SoftwareDeviceNode BspSoftwareDeviceNode { get; set; }
        public CommandUrn<NoArg> CommitUpdate { get; set; }
        public CommandUrn<NoArg> Restart { get; set; }
        
        public CommandUrn<NoArg> Reboot { get; set;}

        public PropertyUrn<Percentage> Brightness { get; set; }
        public bool IsApplicationUpdate(Urn urn) => 
            ApplicationsSoftwareDevices.Keys.Any(sd => sd._update.Urn.Equals(urn));
        
        public bool IsBspUpdate(Urn urn) => 
            BspSoftwareDeviceNode!=null && BspSoftwareDeviceNode._update.Urn.Equals(urn);
        

        public bool IsCommitCommand(Urn urn) => 
            CommitUpdate.Equals(urn);
        
        public bool IsRestartBoilerAppCommand(Urn urn) => Restart.Equals(urn);

        public bool IsRebootCommand(Urn urn) => Reboot.Equals(urn); 

        public Result<string> IdentifyTargetedSoftware(SoftwareDeviceNode softwareDeviceNode)
        {
           if (ApplicationsSoftwareDevices.TryGetValue(softwareDeviceNode, out var softwareName))
           {
               return softwareName;
           }

           return new Error("Targeted_Software_Not_Found","Software device not found");
        }


    }
}