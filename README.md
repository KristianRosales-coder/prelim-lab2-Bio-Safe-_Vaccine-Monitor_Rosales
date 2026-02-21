README.md Content:
# Bio-Safe Vaccine Monitor
## Prelim Lab Activity 02 - Scenario B
### Submitted by: Kristian Carlo Q. Rosales

This program implements a vaccine storage monitoring system using:
- Inheritance (MedicalStorage base class)
- Encapsulation (private fields with public properties)
- Polymorphism (virtual/override methods)
- Abstraction (IAlertable interface)
- Exception Handling (try-catch-finally blocks)

## How to Run
1. dotnet build
2. dotnet run

## Features
- Monitor ultra-cold freezers (-80°C to -60°C)
- Monitor standard fridges (2°C to 8°C)
- Temperature validation
- Door open timeout alerts (30-second rule)
- Continuous monitoring with menu system
