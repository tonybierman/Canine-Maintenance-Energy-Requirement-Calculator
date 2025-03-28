# CanineMer

CanineMer is a Blazor WebAssembly application designed to calculate a dog's Maintenance Energy Requirement (MER) based on its weight and life stage. It provides a user-friendly interface to input a dog's weight in pounds and select its life stage, displaying the Resting Energy Requirement (RER), MER range (lower and upper bounds), and mean MER in kilocalories per day. The project includes robust unit tests and a continuous integration workflow via GitHub Actions.

## Features

- **MER Calculation**: Computes RER and MER (lower, upper, and mean) using veterinary-standard formulas.
- **Life Stage Support**: Supports various canine life stages (e.g., Neutered Adult, Puppy, Gestation) with specific MER factors.
- **Blazor WASM**: Runs entirely in the browser with a responsive UI.
- **Testing**: Comprehensive xUnit tests ensure calculation accuracy and exception handling.
- **CI/CD**: GitHub Actions workflow builds and tests the solution on every push or pull request.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- A modern web browser (e.g., Chrome, Firefox) for Blazor WASM
- Git (for cloning the repository)
- Optional: Visual Studio 2022 or VS Code with C# extensions for development

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/yourusername/CanineMer.git
cd CanineMer