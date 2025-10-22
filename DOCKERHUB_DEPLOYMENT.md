# DockerHub Deployment Guide

This guide explains how to build and push the EShopping microservices to DockerHub.

## Prerequisites

1. **Docker Desktop** installed and running
2. **DockerHub account** - Sign up at https://hub.docker.com
3. **Git Bash** (for Linux script on Windows) or PowerShell

## Quick Start

### Option 1: Using PowerShell (Windows)

```powershell
# 1. Login to DockerHub
docker login

# 2. Run the build and push script
.\scripts\build-and-push-dockerhub.ps1 -DockerHubUsername "your-dockerhub-username"

# With custom tag
.\scripts\build-and-push-dockerhub.ps1 -DockerHubUsername "your-dockerhub-username" -Tag "v1.0.0"
```

### Option 2: Using Bash (Linux/Mac/Git Bash)

```bash
# 1. Make script executable
chmod +x scripts/build-and-push-dockerhub.sh

# 2. Login to DockerHub
docker login

# 3. Run the build and push script
./scripts/build-and-push-dockerhub.sh your-dockerhub-username

# With custom tag
./scripts/build-and-push-dockerhub.sh your-dockerhub-username v1.0.0
```

## What Gets Built and Pushed

The script builds and pushes the following microservices:

| Service | Image Name | Port |
|---------|------------|------|
| Catalog API | `{username}/catalogapi` | 9000 |
| Basket API | `{username}/basketapi` | 9001 |
| Discount API | `{username}/discountapi` | 9002 |
| Ordering API | `{username}/orderingapi` | 9003 |
| Ocelot API Gateway | `{username}/ocelotapigw` | 9010 |

## Using docker-compose with Your Images

### Step 1: Create .env file

Copy `.env.example` to `.env`:

```bash
cp .env.example .env
```

Edit `.env` and set your DockerHub username:

```env
DOCKER_REGISTRY=your-dockerhub-username/
```

### Step 2: Update docker-compose.yml (if needed)

The `docker-compose.yml` already uses the `${DOCKER_REGISTRY}` variable for custom images.

### Step 3: Pull and Run

```bash
# Pull images from DockerHub
docker-compose pull

# Start all services
docker-compose up -d

# Check status
docker-compose ps

# View logs
docker-compose logs -f
```

## Manual Build and Push (Alternative)

If you prefer to build and push manually:

```bash
# Login to DockerHub
docker login

# Set your username
export DOCKERHUB_USERNAME=your-username

# Build and push Catalog API
docker build -t $DOCKERHUB_USERNAME/catalogapi:latest -f Services/Catalog/Catalog.API/Dockerfile .
docker push $DOCKERHUB_USERNAME/catalogapi:latest

# Build and push Basket API
docker build -t $DOCKERHUB_USERNAME/basketapi:latest -f Services/Basket/Basket.API/Dockerfile .
docker push $DOCKERHUB_USERNAME/basketapi:latest

# Build and push Discount API
docker build -t $DOCKERHUB_USERNAME/discountapi:latest -f Services/Discount/Discount.API/Dockerfile .
docker push $DOCKERHUB_USERNAME/discountapi:latest

# Build and push Ordering API
docker build -t $DOCKERHUB_USERNAME/orderingapi:latest -f Services/Ordering/Ordering.API/Dockerfile .
docker push $DOCKERHUB_USERNAME/orderingapi:latest

# Build and push Ocelot Gateway
docker build -t $DOCKERHUB_USERNAME/ocelotapigw:latest -f ApiGateways/Ocelot.ApiGateway/Dockerfile .
docker push $DOCKERHUB_USERNAME/ocelotapigw:latest
```

## Accessing Services

Once all containers are running:

| Service | URL |
|---------|-----|
| Catalog API | http://localhost:9000 |
| Basket API | http://localhost:9001 |
| Discount API | http://localhost:9002 |
| Ordering API | http://localhost:9003 |
| API Gateway | http://localhost:9010 |
| RabbitMQ Management | http://localhost:15672 (guest/guest) |
| Portainer | http://localhost:9090 |
| pgAdmin | http://localhost:5050 |
| Elasticsearch | http://localhost:9200 |
| Kibana | http://localhost:5601 |

## Troubleshooting

### Docker is not running
```bash
# Start Docker Desktop on Windows/Mac
# Or start Docker service on Linux
sudo systemctl start docker
```

### Not authenticated with DockerHub
```bash
docker login
# Enter your DockerHub username and password
```

### Build fails
```bash
# Check Docker is running
docker info

# Clean up old builds
docker system prune -a

# Try building again
```

### Image too large
```bash
# Check image sizes
docker images

# Consider using .dockerignore to exclude unnecessary files
```

## CI/CD Integration

### GitHub Actions Example

```yaml
name: Build and Push to DockerHub

on:
  push:
    branches: [ master ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v2

    - name: Login to DockerHub
      uses: docker/login-action@v1
      with:
        username: ${{ secrets.DOCKERHUB_USERNAME }}
        password: ${{ secrets.DOCKERHUB_TOKEN }}

    - name: Build and Push
      run: |
        chmod +x scripts/build-and-push-dockerhub.sh
        ./scripts/build-and-push-dockerhub.sh ${{ secrets.DOCKERHUB_USERNAME }}
```

## Security Notes

1. **Never commit credentials** - Use `.env` files and add them to `.gitignore`
2. **Use access tokens** - Create DockerHub access tokens instead of using passwords
3. **Private repositories** - Consider making repositories private on DockerHub for production
4. **Environment variables** - Don't expose sensitive data in docker-compose.yml

## Next Steps

1. Set up CI/CD pipeline for automated builds
2. Configure health checks for each service
3. Add monitoring with Prometheus and Grafana
4. Implement distributed tracing with Jaeger
5. Set up Kubernetes deployment manifests

## Support

For issues or questions:
- Check Docker logs: `docker-compose logs [service-name]`
- Verify network connectivity: `docker network inspect eshopping_default`
- Review service health: `docker-compose ps`
