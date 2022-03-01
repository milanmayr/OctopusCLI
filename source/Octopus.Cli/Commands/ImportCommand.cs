using System;
using System.Threading.Tasks;
using Octopus.Cli.Util;
using Octopus.CommandLine;
using Octopus.CommandLine.Commands;

namespace Octopus.Cli.Commands
{
    [Command("import", Description = "Imports an Octopus object from an export file. Deprecated. Please see https://g.octopushq.com/DataMigration for alternative options.")]
    public class ImportCommand : CommandBase
    {
        public ImportCommand(ICommandOutputProvider commandOutputProvider) : base(commandOutputProvider)
        {
        }

        public override Task Execute(string[] commandLineArgs)
        {
            throw new CommandException($"The {AssemblyExtensions.GetExecutableName()} import/export commands have been deprecated. See https://g.octopushq.com/DataMigration for alternative options.");
        }
    }
}
