# TaSol

TaSol is a project that aims to provide a simple structure for people around the world to register their IoT devices and use them to provide data to the community. The project is currently in its early stages and is not yet ready for production. This project started as a college project and there is an article about it being written. The article will be linked here as soon as it is published.

For now, the project has a backend with a REST API, and a frontend that is still in development. The backend is written in ASP.NET Core and the frontend is written in React. The database is a MSSQL database. There is also a broker that is used to communicate with the devices. The broker used is Mosquitto.

## Getting Started

To get started with the project, you will need to have Docker installed on your machine. You can download Docker [here](https://www.docker.com/products/docker-desktop).

After installing Docker, you will need to clone this repository. You can do that by running the following command:

```bash
git clone https://github.com/pietrobondioli/TaSol
```

After cloning the repository, you just need to run the compose file. You can do that by running the following command:

```bash
docker-compose up -d
```

After running the compose file, you will have the database, redis, and the broker running on your machine. Now just run the backend to access the API. You can do that by running the following command:

```bash
dotnet run --project TaSol.App/Web/Web.csproj
```

## Contributors

- [Pietro Bondioli](github.com/pietrobondioli)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
