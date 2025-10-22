# EShopping - Build and Push to DockerHub
# This script builds all microservices images and pushes them to DockerHub

param(
    [Parameter(Mandatory=$true)]
    [string]$DockerHubUsername,

    [Parameter(Mandatory=$false)]
    [string]$Tag = "latest"
)

Write-Host "===========================================" -ForegroundColor Cyan
Write-Host "EShopping - DockerHub Build & Push Script" -ForegroundColor Cyan
Write-Host "===========================================" -ForegroundColor Cyan
Write-Host ""

# Check if Docker is running
Write-Host "Checking Docker status..." -ForegroundColor Yellow
try {
    docker info | Out-Null
    Write-Host "✓ Docker is running" -ForegroundColor Green
} catch {
    Write-Host "✗ Docker is not running. Please start Docker Desktop." -ForegroundColor Red
    exit 1
}

# Check if logged into DockerHub
Write-Host "Checking DockerHub authentication..." -ForegroundColor Yellow
$loginCheck = docker info 2>&1 | Select-String "Username:"
if (-not $loginCheck) {
    Write-Host "⚠ Not logged into DockerHub. Please login first:" -ForegroundColor Yellow
    Write-Host "  docker login" -ForegroundColor White
    exit 1
}
Write-Host "✓ Authenticated with DockerHub" -ForegroundColor Green
Write-Host ""

# Navigate to project root
$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectRoot = Split-Path -Parent $scriptPath
Set-Location $projectRoot

Write-Host "Project Root: $projectRoot" -ForegroundColor Cyan
Write-Host ""

# Define services to build
$services = @(
    @{Name="catalog.api"; Image="catalogapi"; Dockerfile="Services/Catalog/Catalog.API/Dockerfile"},
    @{Name="basket.api"; Image="basketapi"; Dockerfile="Services/Basket/Basket.API/Dockerfile"},
    @{Name="discount.api"; Image="discountapi"; Dockerfile="Services/Discount/Discount.API/Dockerfile"},
    @{Name="ordering.api"; Image="orderingapi"; Dockerfile="Services/Ordering/Ordering.API/Dockerfile"},
    @{Name="ocelotapigw"; Image="ocelotapigw"; Dockerfile="ApiGateways/Ocelot.ApiGateway/Dockerfile"}
)

$successCount = 0
$failCount = 0
$results = @()

foreach ($service in $services) {
    $serviceName = $service.Name
    $imageName = "$DockerHubUsername/$($service.Image)"
    $fullImageName = "${imageName}:${Tag}"

    Write-Host "===========================================" -ForegroundColor Cyan
    Write-Host "Building: $serviceName" -ForegroundColor Yellow
    Write-Host "Image: $fullImageName" -ForegroundColor White
    Write-Host "===========================================" -ForegroundColor Cyan

    # Build the image
    Write-Host "Step 1/2: Building Docker image..." -ForegroundColor Yellow
    docker build -t $fullImageName -f $service.Dockerfile .

    if ($LASTEXITCODE -eq 0) {
        Write-Host "✓ Build successful" -ForegroundColor Green

        # Tag as latest if not already
        if ($Tag -ne "latest") {
            docker tag $fullImageName "${imageName}:latest"
        }

        # Push to DockerHub
        Write-Host "Step 2/2: Pushing to DockerHub..." -ForegroundColor Yellow
        docker push $fullImageName

        if ($LASTEXITCODE -eq 0) {
            Write-Host "✓ Push successful: $fullImageName" -ForegroundColor Green
            $successCount++
            $results += @{Service=$serviceName; Status="SUCCESS"; Image=$fullImageName}

            # Push latest tag if different
            if ($Tag -ne "latest") {
                docker push "${imageName}:latest"
            }
        } else {
            Write-Host "✗ Push failed: $fullImageName" -ForegroundColor Red
            $failCount++
            $results += @{Service=$serviceName; Status="FAILED (Push)"; Image=$fullImageName}
        }
    } else {
        Write-Host "✗ Build failed: $serviceName" -ForegroundColor Red
        $failCount++
        $results += @{Service=$serviceName; Status="FAILED (Build)"; Image=$fullImageName}
    }

    Write-Host ""
}

# Summary
Write-Host "===========================================" -ForegroundColor Cyan
Write-Host "BUILD & PUSH SUMMARY" -ForegroundColor Cyan
Write-Host "===========================================" -ForegroundColor Cyan
Write-Host ""

foreach ($result in $results) {
    $color = if ($result.Status -eq "SUCCESS") { "Green" } else { "Red" }
    $status = $result.Status.PadRight(15)
    Write-Host "$status $($result.Service) → $($result.Image)" -ForegroundColor $color
}

Write-Host ""
Write-Host "Total: $($services.Count) | Success: $successCount | Failed: $failCount" -ForegroundColor White

if ($failCount -eq 0) {
    Write-Host ""
    Write-Host "✓ All images successfully pushed to DockerHub!" -ForegroundColor Green
    Write-Host ""
    Write-Host "To pull and run your images:" -ForegroundColor Cyan
    Write-Host "  docker-compose pull" -ForegroundColor White
    Write-Host "  docker-compose up -d" -ForegroundColor White
    exit 0
} else {
    Write-Host ""
    Write-Host "⚠ Some images failed to build/push. Check errors above." -ForegroundColor Yellow
    exit 1
}
