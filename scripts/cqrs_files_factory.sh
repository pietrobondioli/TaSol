#!/bin/bash

# Function to display usage
usage() {
    echo "Usage: $0 <Base Directory> -c <Command Names>... -q <Query Names>... -eh <EventHandler Names>..."
    exit 1
}

# Check if the base directory is provided
if [ "$#" -lt 2 ]; then
    usage
fi

# Extract the base directory
baseDir=$1
shift

# Initialize arrays for different types
commands=()
queries=()
eventHandlers=()

# Parse command-line arguments
while [[ "$#" -gt 0 ]]; do
    case $1 in
        -c) shift; while [[ "$#" -gt 0 ]] && [[ "$1" != -* ]]; do commands+=("$1"); shift; done ;;
        -q) shift; while [[ "$#" -gt 0 ]] && [[ "$1" != -* ]]; do queries+=("$1"); shift; done ;;
        -eh) shift; while [[ "$#" -gt 0 ]] && [[ "$1" != -* ]]; do eventHandlers+=("$1"); shift; done ;;
        *) echo "Unknown parameter passed: $1"; usage ;;
    esac
done

# Function to create command files
create_command() {
    local name=$1
    local folderName="${baseDir}/Commands/$name"
    local commandFile="${folderName}/${name}.cs"
    local validatorFile="${folderName}/${name}CommandValidator.cs"

    mkdir -p "$folderName"
    touch "$commandFile"
    touch "$validatorFile"
}

# Function to create query files
create_query() {
    local name=$1
    local folderName="${baseDir}/Queries/$name"
    local queryFile="${folderName}/${name}.cs"
    local validatorFile="${folderName}/${name}QueryValidator.cs"
    local dtoFile="${folderName}/${name}Dto.cs"

    mkdir -p "$folderName"
    touch "$queryFile"
    touch "$validatorFile"
    touch "$dtoFile"
}

# Function to create event handler files
create_event_handler() {
    local name=$1
    local handlerFile="${baseDir}/EventHandlers/${name}EventHandler.cs"

    mkdir -p "${baseDir}/EventHandlers"
    touch "$handlerFile"
}

# Create command files
for name in "${commands[@]}"; do
    create_command "$name"
    echo "Created command $name"
done

# Create query files
for name in "${queries[@]}"; do
    create_query "$name"
    echo "Created query $name"
done

# Create event handler files
for name in "${eventHandlers[@]}"; do
    create_event_handler "$name"
    echo "Created event handler $name"
done

echo "All items have been created."
