<h1 align="center">
<img src=https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/logos/n8n-logo.png alt="logo" width="120" height="120">

n8n Installation Notes

</h1>


### Default Setup

1. Make sure you have [Docker](https://docs.docker.com/get-docker/) installed

2. Generate Self-signed certificate 
```bash
mkdir /opt/n8n && mv /opt/n8n
mkdir -p ./certs
openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
  -keyout ./certs/n8n.key \
  -out ./certs/n8n.crt \
  -subj "/CN=n8n.local"
```

3. Setup n8n Docker Compose and `.env` 
`.env`:
```yaml
POSTGRES_USER=<username>
POSTGRES_PASSWORD=<password>
POSTGRES_DB=n8n

POSTGRES_NON_ROOT_USER=<username>
POSTGRES_NON_ROOT_PASSWORD=<password>

ENCRYPTION_KEY=<secret>

GENERIC_TIMEZONE=<Time_Zone>
```

`Docker-Compose.yaml`:
```yaml
version: '3.8'

volumes:
  db_storage:
  n8n_storage:
  redis_storage:

x-shared: &shared
  restart: always
  image: docker.n8n.io/n8nio/n8n
  environment:
    - DB_TYPE=postgresdb
    - DB_POSTGRESDB_HOST=postgres
    - DB_POSTGRESDB_PORT=5432
    - DB_POSTGRESDB_DATABASE=${POSTGRES_DB}
    - DB_POSTGRESDB_USER=${POSTGRES_NON_ROOT_USER}
    - DB_POSTGRESDB_PASSWORD=${POSTGRES_NON_ROOT_PASSWORD}
    - EXECUTIONS_MODE=queue
    - QUEUE_BULL_REDIS_HOST=redis
    - QUEUE_HEALTH_CHECK_ACTIVE=true
    - N8N_ENCRYPTION_KEY=${ENCRYPTION_KEY}
    - GENERIC_TIMEZONE=${GENERIC_TIMEZONE}
    - N8N_SSL_CERT=/cert/n8n.crt
    - N8N_SSL_KEY=/cert/n8n.key
    - N8N_PROTOCOL=https
  links:
    - postgres
    - redis
  volumes:
    - n8n_storage:/home/node/.n8n
    - ./cert:/cert:ro
  depends_on:
    redis:
      condition: service_healthy
    postgres:
      condition: service_healthy

services:
  postgres:
    image: postgres:16
    restart: always
    environment:
      - POSTGRES_USER
      - POSTGRES_PASSWORD
      - POSTGRES_DB
      - POSTGRES_NON_ROOT_USER
      - POSTGRES_NON_ROOT_PASSWORD
    volumes:
      - db_storage:/var/lib/postgresql/data
      - ./init-data.sh:/docker-entrypoint-initdb.d/init-data.sh
    healthcheck:
      test: ['CMD-SHELL', 'pg_isready -h localhost -U ${POSTGRES_USER} -d ${POSTGRES_DB}']
      interval: 5s
      timeout: 5s
      retries: 10

  redis:
    image: redis:6-alpine
    restart: always
    volumes:
      - redis_storage:/data
    healthcheck:
      test: ['CMD', 'redis-cli', 'ping']
      interval: 5s
      timeout: 5s
      retries: 10

  n8n:
    <<: *shared
    ports:
      - 5678:5678

  n8n-worker:
    <<: *shared
    command: worker
    depends_on:
      - n8n
```

4. Run n8n docker-compose
```bash
docker compose up -d
```

### Post installation 
1. After installation, go to https://<server_ip>:5678 
2. Set up your admin account (username & password)

![login](/images/n8n/login.png)

### Useful info / Important note
- If you don't want to run n8n on HTTPS, instead of generate self-signed certificates, add below line into your `docker-compose.yaml` file.

```yaml
    - N8N_SECURE_COOKIE = false

#intead of 

    - N8N_SSL_CERT=/cert/n8n.crt
    - N8N_SSL_KEY=/cert/n8n.key
    - N8N_PROTOCOL=https
```
- I changed from Shuffle to n8n due to AI integration capabilities and ease to integrate it's components. 
