using System;
using System.Threading.Tasks;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octopus.Cli.Util
{
    public static class FeatureDetectionExtensions
    {
        public static async Task<bool> SupportsChannels(this IOctopusAsyncRepository repository)
        {
            var hasChannelLink = await repository.HasLink("Channels").ConfigureAwait(false);
            if (!hasChannelLink)
                // When default space is off and SpaceId is not provided, we check if it is in post space world, as channels are always available in spaces
                return await repository.HasLink("SpaceHome").ConfigureAwait(false);

            return true;
        }

        public static async Task<bool> SupportsTenants(this IOctopusAsyncRepository repository)
        {
            var hasTenantLink = await repository.HasLink("Tenants").ConfigureAwait(false);
            if (!hasTenantLink)
                // When default space is off and SpaceId is not provided, we check if it is in post space world, as tenants are always available in spaces
                return await repository.HasLink("SpaceHome").ConfigureAwait(false);

            return true;
        }

        public static bool UsePostForChannelVersionRuleTest(this RootResource source)
        {
            // Assume octo 3.4 should use the OctopusServer 3.4 POST, otherwise if we're certain this is an older Octopus Server use the GET method
            return source == null ||
                !SemanticVersion.TryParse(source.Version, out var octopusServerVersion) ||
                octopusServerVersion >= new SemanticVersion("3.4");
        }

        public static bool HasProjectDeploymentSettingsSeparation(this RootResource source)
        {
            // The separation of Projects from DeploymentSettings was exposed from 2021.2 onwards
            return source == null ||
                !SemanticVersion.TryParse(source.Version, out var octopusServerVersion) ||
                octopusServerVersion >= new SemanticVersion("2021.2");
        }
    }
}
