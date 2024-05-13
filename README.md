# How to Restore and Run the Swagger Interface

1. Clone the repository.
2. Inside the root folder, execute the command 'dotnet restore'.
3. Then, run the project using ```dotnet run --verbosity minimal --project src/Api/Api.csproj``` or alternatively ```dotnet watch run --verbosity minimal --project src/Api/Api.csproj```. The later should open a browser window at the swagger interface.

# How to Run Tests
1. Inside the root directory execute the command ```dotnet test --verbosity normal```

# Important Notes
1. The project is built using .NET 8.

## Assumptions
1. Players don't have a fixed set of positions; they can change over time. For example, Tom Brady is currently a QB, but he could have a different role in future charts.
2. The PositionDepthChart has a relatively fixed size, and pagination is not required now or in the future.
3. The challenge does not require the ability to create new charts, but they will be pre-seeded.
4. From the sample inputs, a PositionDepth of '0' is used. Therefore the Api layer will use base index of 0.
5. None of the samples show any appended data for players, such as "BRADY, TOM".
6. There is no need to support casing variations, e.g., "Nhl" is equivalent to "NHL".
7. The samples do not indicate the usage of a comma in the player names, so it will not be considered.
8. If there are no players in a position or at a particular depth, that position will not be returned, and only players in that position will be returned, respectively.

## Conclusions
1. It is assumed that for this exercise, the positions will be unique. For example, in the NFL, there would only be one "TE" position.

## Responses to the "Important Notes"
***How do we scale the solution?***

**Adding more Sports? MLB, NHL, NBA?**: 

The solution already supports multiple Leagues

**Adding all the NFL teams?**: 
    
The solution already supports all the NFL teams.

***How do you go about testing your solution***

**Are you handling all the various edge cases correctly?**: 
    
Validators, and unit tests.

**Can you add any automated unit tests to your solution?**: 

Yes, the solution comes with tests ready to go.

***Think about your code organization, maintainability, and other factors that improve readability and understanding of your code***

I implemented Clean Architecture project structure, particularly on the Infrastructure layer so it can be migrated from using InMemory to a traditional DB