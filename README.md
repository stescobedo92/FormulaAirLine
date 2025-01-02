# FormulaAirLine

A project demonstrating the integration between **.NET** and **RabbitMQ**, showcasing how to effectively manage communication between services using message brokers.

## 🚀 Overview

FormulaAirLine is a sample application built with **.NET** that leverages **RabbitMQ** for asynchronous messaging and communication. The project serves as a practical example for developers seeking to understand how to implement robust message-based architectures in .NET applications.

## 📚 Features

- 🛠 **.NET Integration**: Built with .NET technologies for backend services.
- 📡 **RabbitMQ Communication**: Implements RabbitMQ as a message broker for service communication.
- 📊 **Scalability**: Design patterns supporting scalable architectures.

## 🛠️ Technologies Used

- **.NET Core / .NET Framework**
- **RabbitMQ**
- **Docker** *(optional for RabbitMQ setup)*

## 📦 Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/stescobedo92/FormulaAirLine.git
   ```
2. Navigate to the project directory:
   ```bash
   cd FormulaAirLine
   ```
3. Install dependencies:
   ```bash
   dotnet restore
   ```
4. Start RabbitMQ server *(using Docker)*:
   ```bash
   docker run -it --rm -p 5672:5672 -p 15672:15672 rabbitmq:3-management
   ```

## 🚦 Usage

1. Start the application:
   ```bash
   dotnet run
   ```
2. Verify RabbitMQ is running by accessing:
   [http://localhost:15672](http://localhost:15672)
3. Follow the logs to see message interactions.

## 🧩 How It Works

- **Producer**: Publishes messages to RabbitMQ queues.
- **Consumer**: Listens for messages from RabbitMQ and processes them.
- **Message Broker (RabbitMQ)**: Manages and routes messages between services.

## 📖 Documentation

For detailed documentation, refer to the official RabbitMQ documentation:
[https://www.rabbitmq.com/documentation.html](https://www.rabbitmq.com/documentation.html)

## 🤝 Contributing
Contributions are welcome! Please open an issue or submit a pull request.
