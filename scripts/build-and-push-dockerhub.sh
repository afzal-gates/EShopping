#!/bin/bash
# EShopping - Build and Push to DockerHub
# This script builds all microservices images and pushes them to DockerHub

set -e

# Colors
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color

# Check arguments
if [ -z "$1" ]; then
    echo -e "${RED}Error: DockerHub username required${NC}"
    echo "Usage: ./build-and-push-dockerhub.sh <dockerhub-username> [tag]"
    exit 1
fi

DOCKERHUB_USERNAME=$1
TAG=${2:-latest}

echo -e "${CYAN}===========================================${NC}"
echo -e "${CYAN}EShopping - DockerHub Build & Push Script${NC}"
echo -e "${CYAN}===========================================${NC}"
echo ""

# Check if Docker is running
echo -e "${YELLOW}Checking Docker status...${NC}"
if ! docker info > /dev/null 2>&1; then
    echo -e "${RED}✗ Docker is not running. Please start Docker.${NC}"
    exit 1
fi
echo -e "${GREEN}✓ Docker is running${NC}"

# Check if logged into DockerHub
echo -e "${YELLOW}Checking DockerHub authentication...${NC}"
if ! docker info 2>&1 | grep -q "Username:"; then
    echo -e "${YELLOW}⚠ Not logged into DockerHub. Please login first:${NC}"
    echo "  docker login"
    exit 1
fi
echo -e "${GREEN}✓ Authenticated with DockerHub${NC}"
echo ""

# Navigate to project root
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
PROJECT_ROOT="$(dirname "$SCRIPT_DIR")"
cd "$PROJECT_ROOT"

echo -e "${CYAN}Project Root: $PROJECT_ROOT${NC}"
echo ""

# Define services to build
declare -a services=(
    "catalog.api:catalogapi:Services/Catalog/Catalog.API/Dockerfile"
    "basket.api:basketapi:Services/Basket/Basket.API/Dockerfile"
    "discount.api:discountapi:Services/Discount/Discount.API/Dockerfile"
    "ordering.api:orderingapi:Services/Ordering/Ordering.API/Dockerfile"
    "ocelotapigw:ocelotapigw:ApiGateways/Ocelot.ApiGateway/Dockerfile"
)

SUCCESS_COUNT=0
FAIL_COUNT=0
declare -a RESULTS=()

for service_info in "${services[@]}"; do
    IFS=':' read -r SERVICE_NAME IMAGE_NAME DOCKERFILE <<< "$service_info"

    IMAGE_FULL="$DOCKERHUB_USERNAME/$IMAGE_NAME"
    FULL_IMAGE_NAME="$IMAGE_FULL:$TAG"

    echo -e "${CYAN}===========================================${NC}"
    echo -e "${YELLOW}Building: $SERVICE_NAME${NC}"
    echo -e "Image: $FULL_IMAGE_NAME"
    echo -e "${CYAN}===========================================${NC}"

    # Build the image
    echo -e "${YELLOW}Step 1/2: Building Docker image...${NC}"
    if docker build -t "$FULL_IMAGE_NAME" -f "$DOCKERFILE" .; then
        echo -e "${GREEN}✓ Build successful${NC}"

        # Tag as latest if not already
        if [ "$TAG" != "latest" ]; then
            docker tag "$FULL_IMAGE_NAME" "$IMAGE_FULL:latest"
        fi

        # Push to DockerHub
        echo -e "${YELLOW}Step 2/2: Pushing to DockerHub...${NC}"
        if docker push "$FULL_IMAGE_NAME"; then
            echo -e "${GREEN}✓ Push successful: $FULL_IMAGE_NAME${NC}"
            SUCCESS_COUNT=$((SUCCESS_COUNT + 1))
            RESULTS+=("SUCCESS|$SERVICE_NAME|$FULL_IMAGE_NAME")

            # Push latest tag if different
            if [ "$TAG" != "latest" ]; then
                docker push "$IMAGE_FULL:latest"
            fi
        else
            echo -e "${RED}✗ Push failed: $FULL_IMAGE_NAME${NC}"
            FAIL_COUNT=$((FAIL_COUNT + 1))
            RESULTS+=("FAILED (Push)|$SERVICE_NAME|$FULL_IMAGE_NAME")
        fi
    else
        echo -e "${RED}✗ Build failed: $SERVICE_NAME${NC}"
        FAIL_COUNT=$((FAIL_COUNT + 1))
        RESULTS+=("FAILED (Build)|$SERVICE_NAME|$FULL_IMAGE_NAME")
    fi

    echo ""
done

# Summary
echo -e "${CYAN}===========================================${NC}"
echo -e "${CYAN}BUILD & PUSH SUMMARY${NC}"
echo -e "${CYAN}===========================================${NC}"
echo ""

for result in "${RESULTS[@]}"; do
    IFS='|' read -r STATUS SERVICE IMAGE <<< "$result"
    if [[ "$STATUS" == "SUCCESS" ]]; then
        echo -e "${GREEN}$STATUS\t$SERVICE → $IMAGE${NC}"
    else
        echo -e "${RED}$STATUS\t$SERVICE → $IMAGE${NC}"
    fi
done

echo ""
echo "Total: ${#services[@]} | Success: $SUCCESS_COUNT | Failed: $FAIL_COUNT"

if [ $FAIL_COUNT -eq 0 ]; then
    echo ""
    echo -e "${GREEN}✓ All images successfully pushed to DockerHub!${NC}"
    echo ""
    echo -e "${CYAN}To pull and run your images:${NC}"
    echo "  docker-compose pull"
    echo "  docker-compose up -d"
    exit 0
else
    echo ""
    echo -e "${YELLOW}⚠ Some images failed to build/push. Check errors above.${NC}"
    exit 1
fi
