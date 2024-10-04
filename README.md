# .NET Interview Template

## Context
This sample application is supposed to check if a given application name is installed by looking into the Windows registry.
It then should report the installation status to a mock web API.

## Directions
Steps through the 5 TODOs to complete the functionality and get the tests to pass.

## Assumptions
 * I assumed that the process will be checking for software installed in the system context, so I did not check each user branch for user installed software.
 * I assumed that the dependencies should be updated due to the deprecated and vulnerable dependencies that were in place.
 * I assumed that I should be using the latest C# lang version and nullable inspections.

## My approach
I started at the entry point. After working through `Main` I turned to my `Task List` to work through the TODO items. By then all tests were passing. However I noticed that the tests did not actually test if there was any functionality to the implementation. In lieu of integration testing I decided to simply run the program against my own machine. I made sure that it could actually detect some installed software. More extensive testing would be required to really prove this out. I would not use this method as-is in production
