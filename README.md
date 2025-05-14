# Student Internship Tracker

A web application built with ASP.NET Core for managing student internships and applications.

## Features

- Track available internship opportunities
- Manage student profiles and applications
- Monitor application status and progress
- Upload and display company logos
- Responsive design for mobile and desktop

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- SQLite

### Installation

1. Clone the repository
```bash
git clone https://github.com/yourusername/Student-Internship-Tracker.git
```

2. Navigate to the project directory
```bash
cd Student-Internship-Tracker
```

3. Run the application
```bash
dotnet run
```

The application will be available at `http://localhost:5242`

## Database

The application uses SQLite for data storage. The database will be automatically created and seeded with initial data on first run.

## Project Structure

- `Data/` - Database context and seed data
- `Models/` - Entity models (Student, Internship, Application)
- `Pages/` - Razor Pages for the web interface
- `wwwroot/` - Static files (CSS, JavaScript, Images)

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Open a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details
