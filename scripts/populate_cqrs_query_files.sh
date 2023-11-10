#!/bin/bash

# Function to populate a query file
populate_query_file() {
    local queryFile=$1
    local queryName=$(basename "${queryFile%.*}")
    local namespace=$(basename "$(dirname "$(dirname "$queryFile")")")

    cat <<- EOF > "$queryFile"
using Application.Common.Interfaces;

namespace Application.${namespace}.Queries.${queryName};

public record ${queryName}Query : IRequest<${queryName}Dto>
{
    // Properties go here
}

public class ${queryName}QueryHandler : IRequestHandler<${queryName}Query, ${queryName}Dto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ${queryName}QueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<${queryName}Dto> Handle(${queryName}Query request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
EOF
}

# Function to populate a validator file
populate_validator_file() {
    local validatorFile=$1
    local queryName=$(basename "$(dirname "$validatorFile")")
    local namespace=$(basename "$(dirname "$(dirname "$validatorFile")")")

    cat <<- EOF > "$validatorFile"
namespace Application.${namespace}.Queries.${queryName};

public class ${queryName}QueryValidator : AbstractValidator<${queryName}Query>
{
    public ${queryName}QueryValidator()
    {
        // Validation rules go here
    }
}
EOF
}

# Function to populate a DTO file
populate_dto_file() {
    local dtoFile=$1
    local dtoName=$(basename "${dtoFile%Dto.cs}")
    local namespace=$(basename "$(dirname "$(dirname "$dtoFile")")")

    cat <<- EOF > "$dtoFile"
using Domain.Entities;

namespace Application.${namespace}.Queries.${dtoName};

public class ${dtoName}Dto
{
    // DTO properties go here

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, ${dtoName}Dto>(); // Adjust the source entity as needed
        }
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

# Find and populate query files
find "$baseDir" -type f -name "*.cs" | while read -r file; do
    if [[ $file == *"Queries"* && ! -s $file ]]; then
        if [[ $file == *QueryValidator.cs ]]; then
            populate_validator_file "$file"
            echo "Populated validator: $file"
        elif [[ $file == *Dto.cs ]]; then
            populate_dto_file "$file"
            echo "Populated DTO: $file"
        else
            populate_query_file "$file"
            echo "Populated query: $file"
        fi
    fi
done

echo "All query, validator, and DTO files have been populated."
