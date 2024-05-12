# How to Restore and Run the Swagger Interface

1. Clone the repository			
2. Inside the root folder, execute the command 'dotnet restore'
3. Then, dotnet run with 'dotnet run --verbosity minimal --project src/Api/Api.csproj' or alternatively 'dotnet watch run --verbosity minimal --project src/Api/Api.csproj'
4. If you are running the project via VS Code, the previous command wont automatically open the Swagger UI. You can open the browser on the respective port from the link+/swagger in the output.
    Typically the it will open on http://localhost:5126/swagger/index.html

# How to Run tests

# Important Notes
1. The project is .NET 8


# How to run prebuild HTTP requests



# Assumptions, Conclusions and Considerations
## Assumptions
1. Players don't have a fixed set of positions, they can change over time. Eg, Tom Brady is a QB, but he could be a any other role in future Charts.
2. The DepthChart Is a relatively fixed size and that Pagination isn't something that is required, now or in the future.
3. The Challenge doesn't require being able to create new Charts, however they will be seeded.
4. From the Sample Inputs, there is a depth of '0' used. The conclusion from this is that function will run on a base index of 0.
5. Not of the Samples show any of the plater appended data, for example "BRADY, TOM "
6. There is no need support casing variations, eg Nhl == NHL.
7. Samples don't indicate the usage of a name comma in their inputs or output, they won't be considered.

## Conclusions
1. It was assumed that for this exercise that the positions would be unique, for example. In NFL there would only be "TE" position, even though that may not be the case.

## Responses to the "Important Notes"
A.
    1:
    2:

B.
    1:
    2:

C:

D:

TODO:
Add Api Request Models.
Add Api level DTO to return the data in the specified format.
