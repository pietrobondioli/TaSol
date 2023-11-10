#!/bin/bash

# Function to populate a command file
populate_command_file() {
    local commandFile=$1
    local commandName=$(basename "${commandFile%.*}")
    local namespace=$(basename "$(dirname "$(dirname "$commandFile")")")

    cat <<- EOF > "$commandFile"
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.${namespace}.Commands.${commandName};

public record ${commandName}Command : IRequest<long>
{
    // Properties go here
}

public class ${commandName}CommandHandler : IRequestHandler<${commandName}Command, long>
{
    private readonly IApplicationDbContext _context;

    public ${commandName}CommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(${commandName}Command request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        return default; // Replace with actual return
    }
}
EOF
}

# Function to populate a validator file
populate_validator_file() {
    local validatorFile=$1
    local commandName=$(basename "${validatorFile%CommandValidator.*}")
    local namespace=$(basename "$(dirname "$(dirname "$validatorFile")")")

    cat <<- EOF > "$validatorFile"
namespace Application.${namespace}.Commands.${commandName};

public class ${commandName}CommandValidator : AbstractValidator<${commandName}Command>
{
    public ${commandName}CommandValidator()
    {
        // Validation rules go here
    }
}
EOF
}

# Main script starts here

# Check if the correct number of arguments is provided
if [ "$#" -ne 1 ]; then
    echo "Usage: $0 <Application Directory>"
    exit 1
fi

# Extract the base directory
baseDir=$1

# Find and populate command files
find "$baseDir" -type f -name "*.cs" | while read -r file; do
    if [[ $file == *"Commands"* && ! -s $file ]]; then
        if [[ $file == *CommandValidator.cs ]]; then
            populate_validator_file "$file"
            echo "Populated validator: $file"
        else
            populate_command_file "$file"
            echo "Populated command: $file"
        fi
    fi
done

echo "All command and validator files have been populated."
