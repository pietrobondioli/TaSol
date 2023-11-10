#!/bin/bash

# Function to display usage
usage() {
    echo "Usage: $0 <Directory> -ext <File Extension> -f <File Names...>"
    exit 1
}

# Check if the minimum number of arguments is provided
if [ "$#" -lt 5 ]; then
    usage
fi

# Extract the base directory
baseDir=$1
shift

# Initialize variables
fileExt=""
declare -a fileNames=()

# Parse command-line arguments
while [[ "$#" -gt 0 ]]; do
    case $1 in
        -ext) fileExt=$2; shift 2 ;;
        -f) shift; while [[ "$#" -gt 0 ]] && [[ "$1" != -* ]]; do fileNames+=("$1"); shift; done ;;
        *) echo "Unknown parameter passed: $1"; usage ;;
    esac
done

# Check if file extension is provided
if [ -z "$fileExt" ]; then
    echo "File extension not provided."
    exit 1
fi

# Check if file names are provided
if [ ${#fileNames[@]} -eq 0 ]; then
    echo "No file names provided."
    exit 1
fi

# Create files
for name in "${fileNames[@]}"; do
    filePath="${baseDir}/${name}${fileExt}"
    touch "$filePath"
    echo "Created file: $filePath"
done

echo "All files have been created."
