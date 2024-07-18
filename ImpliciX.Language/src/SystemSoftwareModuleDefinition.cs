using System;
using System.Collections.Generic;
using ImpliciX.Language.Model;

namespace ImpliciX.Language
{
    public class SystemSoftwareModuleDefinition
    {
        public Func<string, bool> IsPackageAllowedForUpdate { get; set; } 
        public CommandNode<PackageLocation> GeneralUpdateCommand { get; set; }
        public PropertyUrn<SoftwareVersion> ReleaseVersion { get; set; }
        public PropertyUrn<UpdateState> UpdateState { get; set; }
        public CommandNode<NoArg> CleanVersionSettings { get; set; }
        public CommandNode<NoArg> CommitUpdateCommand { get; set; }
        public CommandUrn<NoArg> RebootCommand { get; set; }
        public IDictionary<string, SoftwareDeviceNode> SoftwareMap { get; set; }
 }
}