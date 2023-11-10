#!/bin/bash

# Function to populate an event handler file
populate_event_handler_file() {
    local handlerFile=$1
    local handlerName=$(basename "${handlerFile%.*}")
    local eventName=${handlerName%EventHandler}
    local namespace=$(basename "$(dirname "$(dirname "$handlerFile")")")

    cat <<- EOF > "$handlerFile"
using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.${namespace}.EventHandlers;

public class ${handlerName} : INotificationHandler<${eventName}Event>
{
    private readonly ILogger<${handlerName}> _logger;

    public ${handlerName}(ILogger<${handlerName}> logger)
    {
        _logger = logger;
    }

    public Task Handle(${eventName}Event notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
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

# Find and populate event handler files
find "$baseDir" -type f -name "*EventHandler.cs" | while read -r file; do
    populate_event_handler_file "$file"
    echo "Populated event handler: $file"
done

echo "All event handler files have been populated."
