# Event Ticket Booking System

A comprehensive event ticket booking system built with .NET Core backend, React frontend, PostgreSQL database, and RabbitMQ for messaging.

## Features

- **User Management**: Registration, authentication, and user profiles
- **Event Management**: Create, update, and manage events
- **Ticket Booking**: Browse events, select seats, and purchase tickets
- **Payment Processing**: Handle payments for ticket purchases
- **Order Management**: Track and manage user orders
- **Notifications**: Email notifications for payment confirmations
- **Real-time Messaging**: RabbitMQ integration for event-driven architecture

## Tech Stack

### Backend
- **Framework**: ASP.NET Core
- **Language**: C#
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **Messaging**: RabbitMQ with MassTransit
- **Authentication**: JWT Tokens

### Frontend
- **Framework**: React
- **Language**: JavaScript/TypeScript
- **Styling**: CSS/Tailwind CSS (assuming based on typical setup)

### Infrastructure
- **Database**: PostgreSQL 16
- **Message Broker**: RabbitMQ
- **Containerization**: Docker & Docker Compose

## Prerequisites

Before running this application, make sure you have the following installed:

- Docker and Docker Compose
- .NET 6.0 or later (for local development)
- Node.js 16+ and npm (for frontend development)
- Git

## Installation and Setup

1. **Clone the repository:**
   ```bash
   git clone <repository-url>
   cd infra-ETBS
   ```

2. **Environment Configuration:**
   - Copy `.env.example` to `.env`
   - Fill in the required environment variables:
     ```
     # Database Settings
     DB_USER=your_db_user
     DB_PASSWORD=your_db_password
     DB_NAME=event_ticket_db
     DB_PORT=5432

     # RabbitMQ Settings
     RABBITMQ_USER=guest
     RABBITMQ_PASS=guest

     # Application Settings
     BACKEND_PORT=8080
     FRONTEND_PORT=3000
     ```

3. **Start the Infrastructure:**
   ```bash
   # Start database and RabbitMQ
   docker-compose up -d db rabbitmq
   ```

4. **Database Setup:**
   - The database will be automatically initialized with the restore script
   - Wait for the containers to be healthy

5. **Build and Run the Backend:**
   ```bash
   cd src/backend/EventTicketBookingSystem
   dotnet restore
   dotnet build
   dotnet run
   ```

6. **Build and Run the Frontend:**
   ```bash
   cd src/frontend
   npm install
   npm start
   ```

7. **Run Full Stack with Docker:**
   ```bash
   # Build and start all services
   docker-compose up --build
   ```

## Usage

### API Endpoints

The backend provides RESTful APIs for:

- **Events**: `/api/events`
  - GET `/api/events` - Get all events
  - POST `/api/events` - Create new event
  - GET `/api/events/{id}` - Get event by ID
  - PUT `/api/events/{id}` - Update event
  - DELETE `/api/events/{id}` - Delete event

- **Users**: `/api/users`
  - POST `/api/users/register` - Register new user
  - POST `/api/users/login` - User login
  - GET `/api/users/profile` - Get user profile

- **Orders**: `/api/orders`
  - GET `/api/orders` - Get user orders
  - POST `/api/orders` - Create new order
  - GET `/api/orders/{id}` - Get order details

- **Payments**: `/api/payments`
  - POST `/api/payments` - Process payment

- **Seats**: `/api/seats`
  - GET `/api/seats/event/{eventId}` - Get available seats for event

### Accessing the Application

- **Frontend**: http://localhost:{FRONTEND_PORT}
- **Backend API**: http://localhost:{BACKEND_PORT}
- **RabbitMQ Management**: http://localhost:15672
- **PostgreSQL**: localhost:{DB_PORT}

## Development

### Project Structure

```
src/
├── backend/
│   └── EventTicketBookingSystem/
│       ├── Modules/
│       │   ├── Events/
│       │   ├── Users/
│       │   ├── Orders/
│       │   ├── Payments/
│       │   ├── Tickets/
│       │   └── Notifications/
│       ├── Infrastructure/
│       └── Properties/
└── frontend/
    └── (React application)
infra/
└── postgres/
    ├── data/
    ├── backups/
    └── scripts/
```

### Running Tests

```bash
# Backend tests
cd src/backend/EventTicketBookingSystem
dotnet test

# Frontend tests
cd src/frontend
npm test
```

### Database Migrations

```bash
cd src/backend/EventTicketBookingSystem
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

For support, email support@eventticketbookingsystem.com or create an issue in this repository.