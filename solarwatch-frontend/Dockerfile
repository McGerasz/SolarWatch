# Using node:16-alpine as the base image
FROM node:16-alpine

# Create and set /app as the default work directory
WORKDIR /app

# Copy package.json and package-lock.json first for caching
COPY package*.json ./

COPY . .
# Install npm dependencies
RUN npm install
# Copy the entire application code to the container

# Build React project
RUN npm run build

# Install serve node package
RUN npm install -g serve

# Expose to port
EXPOSE 3000

CMD ["serve","-p","3000","build/"]